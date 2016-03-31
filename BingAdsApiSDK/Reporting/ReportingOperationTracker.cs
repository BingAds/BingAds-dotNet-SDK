//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.BingAds.Reporting
{
    internal class ReportingOperationTracker : IDisposable
    {
        private const int InitialStatusCheckIntervalInMs = 1000;

        private const int NumberOfInitialStatusChecks = 5;

        private Timer _updateProgressTimer;

        private readonly ReportingStatusProvider _statusProvider;

        private readonly ServiceClient<IReportingService> _reportingServiceClient;

        private readonly TaskCompletionSource<ReportingOperationStatus> _taskCompletionSource;

        private readonly CancellationToken _cancellationToken;

        private readonly int _statusCheckIntervalInMs;

        private bool _stopTracking;

        private ReportingOperationStatus _currentStatus;

        public ReportingOperationTracker(ReportingStatusProvider statusProvider,
                                         ServiceClient<IReportingService> bulkServiceClient,
                                         CancellationToken cancellationToken,
                                         int statusCheckIntervalInMs)
        {
            _statusProvider = statusProvider;

            _reportingServiceClient = bulkServiceClient;

            _cancellationToken = cancellationToken;

            _statusCheckIntervalInMs = statusCheckIntervalInMs;

            _taskCompletionSource = new TaskCompletionSource<ReportingOperationStatus>();

            _updateProgressTimer = new Timer(PollOperationStatus);
        }

        public Task<ReportingOperationStatus> TrackResultFileAsync()
        {
            StartTracking();

            return _taskCompletionSource.Task;
        }

        private void StartTracking()
        {
            _updateProgressTimer.Change(InitialStatusCheckIntervalInMs, InitialStatusCheckIntervalInMs);
        }

        private int _statusUpdateCount;

        private int _updateInProgress;

        // Make sure all the exceptions get handled inside this method. "async void" won't allow to handle them otherwise
        private async void PollOperationStatus(object obj)
        {
            try
            {
                if (Interlocked.CompareExchange(ref _updateInProgress, 1, 0) == 1)
                {
                    return;
                }

                try
                {
                    if (TrackingWasStopped())
                    {
                        return;
                    }

                    if (CancelPollingIfRequestedByUser())
                    {
                        return;
                    }

                    _statusUpdateCount++;

                    ChangeTimerIntervalIfNeeded();

                    try
                    {
                        await RefreshStatus().ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {    
                        StopTracking();

                        PropagateExceptionToCallingThread(new CouldNotGetReportingDownloadStatusException("Get download status failed", e));

                        return;
                    }
                    
                    CompleteTaskIfOperationIsComplete();
                }
                finally
                {
                    _updateInProgress = 0;
                }
            }
            catch (Exception ex)
            {
                StopTracking();

                PropagateExceptionToCallingThread(ex);
            }
        }

        private void ChangeTimerIntervalIfNeeded()
        {
            if (_statusUpdateCount == NumberOfInitialStatusChecks)
            {
                _updateProgressTimer.Change(_statusCheckIntervalInMs, _statusCheckIntervalInMs);
            }
        }

        private bool TrackingWasStopped()
        {
            return _stopTracking;
        }

        private bool CancelPollingIfRequestedByUser()
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                StopTracking();

                CancelTask();

                return true;
            }

            return false;
        }

        private const int MaxGetStatusRetries = 3;

        private async Task RefreshStatus()
        {
            _currentStatus = await GetStatusWithRetries().ConfigureAwait(false);
        }

        private async Task<ReportingOperationStatus> GetStatusWithRetries()
        {
            var retriesLeft = MaxGetStatusRetries;

            do
            {
                try
                {
                    return await _statusProvider.GetCurrentStatus(_reportingServiceClient).ConfigureAwait(false);
                }
                catch (CommunicationException)
                {
                    if (retriesLeft-- <= 0)
                    {
                        throw;
                    }
                }
                catch (TimeoutException)
                {
                    if (retriesLeft-- <= 0)
                    {
                        throw;
                    }
                }
            } while (true);
        }

        private void CompleteTaskIfOperationIsComplete()
        {
            if (OperationIsComplete())
            {
                StopTracking();

                CompleteTaskWithResult();
            }
        }

        private bool OperationIsComplete()
        {
            return _statusProvider.IsFinalStatus(_currentStatus);
        }

        private void StopTracking()
        {
            _stopTracking = true;

            _updateProgressTimer.Dispose();
        }

        private void CompleteTaskWithResult()
        {
            _taskCompletionSource.SetResult(_currentStatus);
        }

        private void CancelTask()
        {
            _taskCompletionSource.SetCanceled();
        }

        private void PropagateExceptionToCallingThread(Exception ex)
        {
            _taskCompletionSource.SetException(ex);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_updateProgressTimer != null)
                {
                    _updateProgressTimer.Dispose();
                    _updateProgressTimer = null;
                }
            }
        }
    }
}
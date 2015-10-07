using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk.Operations
{
    internal class BulkOperationTracker<TStatus> : IBulkOperationTracker<TStatus>, IDisposable
    {
        private const int InitialStatusCheckIntervalInMs = 1000;

        private const int NumberOfInitialStatusChecks = 5;

        private Timer _updateProgressTimer;

        private readonly IBulkOperationStatusProvider<TStatus> _statusProvider;

        private readonly ServiceClient<IBulkService> _bulkServiceClient;

        private readonly TaskCompletionSource<BulkOperationStatus<TStatus>> _taskCompletionSource;

        private readonly IProgress<BulkOperationProgressInfo> _progress;

        private readonly CancellationToken _cancellationToken;

        private readonly int _statusCheckIntervalInMs;

        private bool _stopTracking;

        int _lastProgressReported;

        private BulkOperationStatus<TStatus> _currentStatus;

        public BulkOperationTracker(IBulkOperationStatusProvider<TStatus> statusProvider, ServiceClient<IBulkService> bulkServiceClient, IProgress<BulkOperationProgressInfo> progress, CancellationToken cancellationToken, int statusCheckIntervalInMs)
        {
            _statusProvider = statusProvider;

            _bulkServiceClient = bulkServiceClient;

            _progress = progress;

            _cancellationToken = cancellationToken;

            _statusCheckIntervalInMs = statusCheckIntervalInMs;

            _taskCompletionSource = new TaskCompletionSource<BulkOperationStatus<TStatus>>();

            _updateProgressTimer = new Timer(PollOperationStatus);
        }

        public Task<BulkOperationStatus<TStatus>> TrackResultFileAsync()
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

                    await RefreshStatus().ConfigureAwait(false);

                    ReportProgressIfNeeded();
                    
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

        private async Task<BulkOperationStatus<TStatus>> GetStatusWithRetries()
        {
            var retriesLeft = MaxGetStatusRetries;

            do
            {
                try
                {
                    return await _statusProvider.GetCurrentStatus(_bulkServiceClient).ConfigureAwait(false);
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

        private void ReportProgressIfNeeded()
        {
            if (!UserRequestedProgressReports())
            {
                return;
            }

            if (ProgressChangedSinceLastReport())
            {
                ReportProgress();
            }
        }

        private bool UserRequestedProgressReports()
        {
            return _progress != null;
        }

        private void ReportProgress()
        {
            _progress.Report(new BulkOperationProgressInfo(_currentStatus.PercentComplete));

            UpdateLastProgressReported();
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

        private bool ProgressChangedSinceLastReport()
        {
            return _currentStatus.PercentComplete != _lastProgressReported;
        }

        private void UpdateLastProgressReported()
        {
            _lastProgressReported = _currentStatus.PercentComplete;
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

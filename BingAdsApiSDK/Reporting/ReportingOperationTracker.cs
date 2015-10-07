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

                    await RefreshStatus().ConfigureAwait(false);

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
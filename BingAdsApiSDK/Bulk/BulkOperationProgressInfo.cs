namespace Microsoft.BingAds.Bulk
{
    /// <summary>
    /// Contains percent complete progress information for the bulk operation.
    /// </summary>
    public class BulkOperationProgressInfo
    {
        private readonly int _percentComplete;

        /// <summary>
        /// Percent complete progress information for the bulk operation.
        /// </summary>
        public int PercentComplete
        {
            get { return _percentComplete; }            
        }

        internal BulkOperationProgressInfo(int percentComplete)
        {
            _percentComplete = percentComplete;
        }
    }
}

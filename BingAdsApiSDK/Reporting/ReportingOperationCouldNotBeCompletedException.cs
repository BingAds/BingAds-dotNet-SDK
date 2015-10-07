using System;

namespace Microsoft.BingAds.Reporting
{
    /// <summary>
    /// This exception is thrown if you are attempting to poll for a completed results file and the reporting service returns a failed status. 
    /// </summary>
    [Serializable]
    public class ReportingOperationCouldNotBeCompletedException : Exception
    {
    }
}
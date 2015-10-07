using System;

namespace Microsoft.BingAds.Reporting
{
    /// <summary>
    /// This exception is thrown if you are attempting to download a results file that is not yet available for download. 
    /// </summary>
    [Serializable]
    public class ReportingOperationInProgressException : Exception
    {
    }
}
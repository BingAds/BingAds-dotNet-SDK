namespace Microsoft.BingAds
{
    /// <summary>
    /// Represents details of an error returned from the Microsft Account authorization server.
    /// </summary>
    public class OAuthErrorDetails
    {        
        /// <summary>
        /// The error code of the OAuth error.
        /// </summary>
        public string Error { get; internal set; }
     
        /// <summary>
        /// The description of the OAuth error.
        /// </summary>
        public string Description { get; internal set; }
    }
}
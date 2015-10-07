using System;
using System.Runtime.Serialization;

namespace Microsoft.BingAds
{
    /// <summary>
    /// This exception is thrown if an error was returned from the Microsft Account authorization server.
    /// </summary>
    [Serializable]
    public class OAuthTokenRequestException : Exception
    {
        /// <summary>
        /// Represents details of an error returned from the Microsft Account authorization server.
        /// </summary>
        public OAuthErrorDetails Details { get; private set; }

        /// <summary>
        /// Initializes a new instance of the OAuthTokenRequestException with the specified error message and OAuth error details.
        /// </summary>
        /// <param name="message">The error message returned by the client library.</param>
        /// <param name="details">The details of an error returned from the Microsft Account authorization server.</param>
        public OAuthTokenRequestException(string message, OAuthErrorDetails details)
            : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// Initializes a new instance of the OAuthTokenRequestException with the specified error message, OAuth error details, and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="details">The details of an authentication error returned from the Microsft Account authorization server.</param>
        /// <param name="inner">The inner exception.</param>
        public OAuthTokenRequestException(string message, OAuthErrorDetails details, Exception inner)
            : base(message, inner)
        {
            Details = details;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="info">Reserved for internal use.</param>
        /// <param name="context">Reserved for internal use.</param>
        protected OAuthTokenRequestException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            Details = new OAuthErrorDetails
            {
                Description = info.GetString("Description"),
                Error = info.GetString("Error")
            };
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Description", Details.Description);

            info.AddValue("Error", Details.Error);
        }

        /// <summary>
        /// Returns the message.
        /// </summary>
        public override string Message
        {
            get { return base.Message + " Details: " + Details.Error + " - " + Details.Description; }
        }
    }
}

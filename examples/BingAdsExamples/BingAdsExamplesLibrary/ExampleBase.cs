using System;
using System.Threading.Tasks;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary
{
    /// <summary>
    /// Provides a base class for extending and experimenting with Bing Ads API examples. 
    /// </summary>
    public abstract class ExampleBase
    {
        public delegate void SendMessageDelegate(String msg);
        public SendMessageDelegate OutputStatusMessage;

        /// <summary>
        /// The description of the example.
        /// </summary>
        public abstract String Description { get; }

        /// <summary>
        /// The name of the example
        /// </summary>
        public String ExampleName
        {
            get
            {
                var s = GetType();
                return s.Name;
            }
        }

        /// <summary>
        /// Initializes a new instance of the ExampleBase class, and sets the default output status message.
        /// </summary>
        protected ExampleBase()
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }

        /// <summary>
        /// Write to the console by default, if the example does not implement its own OutputStatusMessage method.
        /// </summary>
        /// <param name="msg">The message sent to console output.</param>
        private void OutputStatusMessageDefault(String msg)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Each example must implement either Run or RunAsync as the main entry point. 
        /// </summary>
        /// <param name="authorizationData">Represents a user who intends to access the corresponding customer and account. </param>
        public virtual void Run(AuthorizationData authorizationData)
        {
            throw new NotImplementedException("You must implement either Run or RunAsync");
        }

        /// <summary>
        /// Each example must implement either Run or RunAsync as the main entry point. 
        /// </summary>
        /// <param name="authorizationData">Represents a user who intends to access the corresponding customer and account.</param>
        /// <returns></returns>
        public virtual async Task RunAsync(AuthorizationData authorizationData)
        {
            await Task.Factory.StartNew(() => Run(authorizationData));
        }
    }
}

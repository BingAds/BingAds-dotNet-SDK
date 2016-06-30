using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingAdsExamplesLibrary.V9
{
    /// <summary>
    /// Provides a base class for extending and experimenting with Bing Ads examples. 
    /// </summary>
    public abstract class ExampleBase : BingAdsExamplesLibrary.ExampleBase
    {
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
        
    }
}

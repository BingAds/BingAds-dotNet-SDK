// Copyright 2014 Microsoft Corporation 

// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 

//    http://www.apache.org/licenses/LICENSE-2.0 

// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License. 

using System;
using System.Threading.Tasks;
using Microsoft.BingAds;

namespace BingAdsExamples
{
    /// <summary>
    /// Provides a base class for extending and experimenting with Bing Ads examples. 
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

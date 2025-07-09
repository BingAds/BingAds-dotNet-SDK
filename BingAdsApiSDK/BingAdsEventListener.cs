//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 13.0
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

using System.Diagnostics.Tracing;
using System.Text;

namespace Microsoft.BingAds
{
    /// <summary>
    /// Provides a way to log and handle events raised by BingAds SDK.
    /// </summary>
    public class BingAdsEventListener : EventListener
    {
        private static readonly List<BingAdsEventListener> ActiveInstances = new();

        private readonly EventLevel _eventLevel;

        private readonly Action<Event> _onEvent;

        private volatile EventSource _eventSource;

        private volatile bool _isInitialized;

        /// <summary>
        /// Creates a new <see cref="BingAdsEventListener"/> that logs events using <see cref="Console.WriteLine(string)"/>
        /// </summary>
        /// <param name="eventLevel"></param>
        /// <returns>A new <see cref="BingAdsEventListener"/> object</returns>
        public static BingAdsEventListener CreateConsoleLogger(EventLevel eventLevel)
        {
            return new BingAdsEventListener(
                eventLevel,
                // EventWrittenEventArgs.TimeStamp is not available on netstandard2.0
                bingAdsEvent => Console.WriteLine($"{DateTime.UtcNow:O} [{bingAdsEvent.EventData.Level.ToString().ToUpperInvariant()}] {bingAdsEvent.GetDescription()}")
            );
        }

        /// <summary>
        /// Creates a new <see cref="BingAdsEventListener"/> allowing your application to handle raised events.
        /// </summary>
        /// <param name="eventLevel">Minimum level of events that should be handled.</param>
        /// <param name="onEvent">Action to execute when an event is raised.</param>
        public BingAdsEventListener(EventLevel eventLevel, Action<Event> onEvent)
        {
            _eventLevel = eventLevel;

            _onEvent = onEvent;

            _isInitialized = true;

            EnableEventsIfInitialized();
        }

        /// <summary>
        /// Ensures that the created event listener object will not be garbage collected and will continue handling events while the application is running.
        /// </summary>
        public void KeepActive()
        {
            lock (ActiveInstances)
            {
                ActiveInstances.Add(this);
            }
        }

        private void EnableEventsIfInitialized()
        {
            if (_isInitialized && _eventSource != null)
            {
                EnableEvents(_eventSource, _eventLevel);
            }
        }

        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            base.OnEventSourceCreated(eventSource);

            if (eventSource.Name == "Microsoft.BingAds")
            {
                _eventSource = eventSource;

                EnableEventsIfInitialized();
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData.EventSource != _eventSource)
            {
                return;
            }

            _onEvent(new Event(eventData));
        }

        /// <summary>
        /// Provides methods to access event data.
        /// </summary>
        public class Event
        {
            internal Event(EventWrittenEventArgs eventData)
            {
                EventData = eventData;
            }

            /// <summary>
            /// Raw data that was used to generate the event.
            /// </summary>
            public EventWrittenEventArgs EventData { get; }

            /// <summary>
            /// Gets detailed event description.
            /// </summary>
            /// <returns>A string containing event details.</returns>
            public string GetDescription()
            {
                var sb = BuildDescription(EventData);

                return sb.ToString();
            }

            private static StringBuilder BuildDescription(EventWrittenEventArgs eventData)
            {
                var sb = new StringBuilder();

                if (eventData.PayloadNames == null)
                {
                    sb.Append("PayloadNames is null for EventId ").AppendLine(eventData.EventId.ToString());

                    return sb;
                }

                if (eventData.Payload == null)
                {
                    sb.Append("Payload is null for EventId ").AppendLine(eventData.EventId.ToString());

                    return sb;
                }

                var payloadArray = eventData.Payload.ToArray();

                sb.AppendLine(string.Format(eventData.Message, payloadArray));

                var isFirstLine = true;

                for (var index = 0; index < eventData.PayloadNames.Count; index++)
                {
                    var fieldName = eventData.PayloadNames[index];

                    if (fieldName == "CorrelationId")
                    {
                        continue;
                    }

                    var isMultilineField = fieldName is "Headers" or "Body";

                    if (!isFirstLine)
                    {
                        if (isMultilineField)
                        {
                            sb.AppendLine();
                            sb.Append('=', fieldName.Length);
                            sb.AppendLine();
                        }
                        else
                        {
                            sb.Append(';').Append(' ');
                        }
                    }

                    sb.Append(fieldName);

                    if (isMultilineField)
                    {
                        sb.AppendLine();
                        sb.Append('=', fieldName.Length);
                        sb.AppendLine();
                    }
                    else
                    {
                        sb.Append(':').Append(' ');
                    }

                    sb.Append(payloadArray[index]);

                    isFirstLine = false;
                }

                sb.AppendLine();

                return sb;
            }
        }
    }
}

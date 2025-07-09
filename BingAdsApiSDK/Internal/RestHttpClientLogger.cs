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

using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Text;

namespace Microsoft.BingAds.Internal
{
    class RestHttpClientLogger
    {
        private readonly Events _log = Events.Log;

        private const int MaxBodyLength = 10000;

        public async ValueTask<object> LogRequestStartAsync(HttpRequestMessage request, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_log.IsEnabled(EventLevel.Verbose, EventKeywords.None))
            {
                var body = await request.Content.ReadAsStringAsync().ConfigureAwait(false);

                body = TruncateBodyIfNeeded(body);

                var headers = FormatHeaders(request.Headers.Concat(request.Content.Headers));

                _log.RequestStart(Activity.Current?.Id, request.Method.Method, request.RequestUri.ToString(), headers, body);
            }

            return null;
        }

        public async ValueTask LogRequestStopAsync(object context, HttpRequestMessage request, HttpResponseMessage response, Stopwatch stopwatch, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_log.IsEnabled(EventLevel.Verbose, EventKeywords.None))
            {
                var headersDuration = stopwatch.Elapsed.TotalMilliseconds;

                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var responseDuration = stopwatch.Elapsed.TotalMilliseconds;

                body = TruncateBodyIfNeeded(body);

                var headers = FormatHeaders(response.Headers.Concat(response.Content.Headers));

                _log.RequestStop(Activity.Current?.Id, (int)response.StatusCode, headersDuration, responseDuration, headers, body);
            }
        }

        public ValueTask LogRequestFailedAsync(object context, HttpRequestMessage request, HttpResponseMessage response, Exception exception, TimeSpan elapsed, CancellationToken cancellationToken = new CancellationToken())
        {
            // TODO: add exception logging

            return new ValueTask();
        }

        private static string TruncateBodyIfNeeded(string content)
        {
            if (content.Length <= MaxBodyLength)
            {
                return content;
            }

            return content.Substring(0, MaxBodyLength) + $" ... skipped {content.Length - MaxBodyLength} characters ...";
        }

        private static string FormatHeaders(IEnumerable<KeyValuePair<string, IEnumerable<string>>> httpHeaders)
        {
            var sb = new StringBuilder();

            var firstLine = true;

            foreach (var header in httpHeaders)
            {
                if (!firstLine)
                {
                    sb.AppendLine();
                }

                var isSensitiveHeader = header.Key.Equals("Authorization", StringComparison.InvariantCultureIgnoreCase) ||
                                        header.Key.Equals("Password", StringComparison.InvariantCultureIgnoreCase);

                var valueToLog = isSensitiveHeader ? "XXX" : string.Join(", ", header.Value);
                
                sb.Append(header.Key).Append(": ").Append(valueToLog);

                firstLine = false;
            }

            return sb.ToString();
        }
    }
}

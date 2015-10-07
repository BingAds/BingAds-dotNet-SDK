using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft.BingAds.Internal
{
    internal class HttpService : IHttpService
    {
        public Task<HttpResponseMessage> PostAsync(Uri requestUri, List<KeyValuePair<string, string>> formValues)
        {
            var client = new HttpClient();

            return client.PostAsync(requestUri, new FormUrlEncodedContent(formValues));
        }

        public async Task DownloadFileAsync(Uri fileUri, string localFilePath, bool overwrite)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(fileUri).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsFileAsync(localFilePath, overwrite).ConfigureAwait(false);
        }

        public async Task UploadFileAsync(Uri uri, string uploadFilePath, Action<HttpRequestHeaders> addHeadersAction)
        {
            using (var stream = File.OpenRead(uploadFilePath))
            {
                var client = new HttpClient();

                addHeadersAction(client.DefaultRequestHeaders);

                var multiPart = new MultipartFormDataContent
                {
                    { new StreamContent(stream), "file", string.Format("\"{0}{1}\"", Guid.NewGuid(), Path.GetExtension(uploadFilePath)) }
                };

                var response = await client.PostAsync(uri, multiPart).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Microsoft.BingAds.Internal
{
    internal interface IHttpService
    {
        Task<HttpResponseMessage> PostAsync(Uri requestUri, List<KeyValuePair<string, string>> formValues);

        Task DownloadFileAsync(Uri fileUri, string localFilePath, bool overwrite);

        Task UploadFileAsync(Uri fileUri, string uploadFilePath, Action<HttpRequestHeaders> addHeadersAction);
    }
}

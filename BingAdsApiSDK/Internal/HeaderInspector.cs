using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Microsoft.BingAds.Internal
{
    internal class HeaderInspector : IClientMessageInspector
    {
        private readonly string _headerName;
        private readonly string _headerValue;

        public HeaderInspector(string headerName, string headerValue)
        {
            _headerName = headerName;
            _headerValue = headerValue;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            object existingMessageProperty;

            HttpRequestMessageProperty requestMessageProperty;

            if (!request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out existingMessageProperty))
            {
                requestMessageProperty = new HttpRequestMessageProperty();

                request.Properties.Add(HttpRequestMessageProperty.Name, requestMessageProperty);
            }
            else
            {
                requestMessageProperty = existingMessageProperty as HttpRequestMessageProperty;
            }

            if (requestMessageProperty != null)
            {
                requestMessageProperty.Headers[_headerName] = _headerValue;
            }

            return null;
        }
    }
}

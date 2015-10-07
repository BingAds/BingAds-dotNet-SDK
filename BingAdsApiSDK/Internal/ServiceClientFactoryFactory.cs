using System;

namespace Microsoft.BingAds.Internal
{
    internal static class ServiceClientFactoryFactory
    {
        internal static Func<IServiceClientFactory> CreateCustomClientFactory { get; set; }

        public static IServiceClientFactory CreateServiceClientFactory()
        {
            return CreateCustomClientFactory == null ? new ServiceClientFactory() : CreateCustomClientFactory();
        }
    }
}

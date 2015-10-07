using System;
using System.ServiceModel.Channels;

namespace Microsoft.BingAds.Internal
{
    internal interface IServiceClientFactory
    {
        IChannelFactory<T> CreateChannelFactory<T>(ApiEnvironment env)
            where T: class;

        T CreateServiceFromFactory<T>(IChannelFactory<T> channelFactory)
            where T: class;

        Type[] SupportedServiceTypes { get; }
    }
}
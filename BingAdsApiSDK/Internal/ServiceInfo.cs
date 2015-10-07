using System;

namespace Microsoft.BingAds.Internal
{
    internal class ServiceInfo
    {
        public string ProductionUrl { get; set; }
        public string SandboxUrl { get; set; }

        public string GetUrl(ApiEnvironment environment)
        {
            switch (environment)
            {
                case ApiEnvironment.Sandbox:
                    if (SandboxUrl == null)
                    {
                        throw new InvalidOperationException("The service is not available in Sandbox");
                    }
                    return SandboxUrl;
                case ApiEnvironment.Production:
                    return ProductionUrl;
                default:
                    throw new ArgumentException("invalid environment name", "environment");
            }
        }
    }
}
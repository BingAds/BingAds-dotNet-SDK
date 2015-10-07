using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Web.Configuration;
using System.Web.Hosting;
using Microsoft.BingAds.AdIntelligence;
using Microsoft.BingAds.Bulk;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.CustomerBilling;
using Microsoft.BingAds.CustomerManagement;
using Microsoft.BingAds.Optimizer;
using Microsoft.BingAds.Reporting;
using IAdInsightServiceV10 = Microsoft.BingAds.V10.AdInsight.IAdInsightService;
using IBulkServiceV10 = Microsoft.BingAds.V10.Bulk.IBulkService;
using ICampaignManagementServiceV10 = Microsoft.BingAds.V10.CampaignManagement.ICampaignManagementService;

namespace Microsoft.BingAds.Internal
{
    internal class ServiceClientFactory : IServiceClientFactory
    {
        private static readonly Dictionary<Type, ServiceInfo> Endpoints = new Dictionary<Type, ServiceInfo>
        {
            { 
                typeof (IAdIntelligenceService), new ServiceInfo
                {
                    ProductionUrl = "https://api.bingads.microsoft.com/Api/Advertiser/AdIntelligence/v9/AdIntelligenceService.svc",
                    SandboxUrl = "https://api.sandbox.bingads.microsoft.com/Api/Advertiser/AdIntelligence/v9/AdIntelligenceService.svc"
                }
            },
            { 
                typeof (IBulkService), new ServiceInfo
                {
                    ProductionUrl = "https://api.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v9/BulkService.svc",
                    SandboxUrl = "https://api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v9/BulkService.svc"
                }
            },
            { 
                typeof (ICampaignManagementService), new ServiceInfo
                {
                    ProductionUrl = "https://api.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v9/CampaignManagementService.svc",
                    SandboxUrl = "https://api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v9/CampaignManagementService.svc"
                }
            },
            { 
                typeof (ICustomerBillingService), new ServiceInfo
                {
                    ProductionUrl = "https://clientcenter.api.bingads.microsoft.com/Api/Billing/v9/CustomerBillingService.svc",
                    SandboxUrl = null
                }
            },
            { 
                typeof (ICustomerManagementService), new ServiceInfo
                {
                    ProductionUrl = "https://clientcenter.api.bingads.microsoft.com/Api/CustomerManagement/v9/CustomerManagementService.svc",
                    SandboxUrl = "https://clientcenter.api.sandbox.bingads.microsoft.com/Api/CustomerManagement/v9/CustomerManagementService.svc"
                }
            },
            { 
                typeof (IOptimizerService), new ServiceInfo
                {
                    ProductionUrl = "https://api.bingads.microsoft.com/Api/Advertiser/Optimizer/v9/OptimizerService.svc",
                    SandboxUrl = "https://api.sandbox.bingads.microsoft.com/Api/Advertiser/Optimizer/v9/OptimizerService.svc"
                }
            },
            { 
                typeof (IReportingService), new ServiceInfo
                {
                    ProductionUrl = "https://api.bingads.microsoft.com/Api/Advertiser/Reporting/v9/ReportingService.svc",
                    SandboxUrl = "https://api.sandbox.bingads.microsoft.com/Api/Advertiser/Reporting/v9/ReportingService.svc"
                }
            },
            {
                typeof (IAdInsightServiceV10), new ServiceInfo
                {
                    ProductionUrl = "https://adinsight.api.bingads.microsoft.com/Api/Advertiser/AdInsight/V10/AdInsightService.svc",
                    SandboxUrl = "https://adinsight.api.sandbox.bingads.microsoft.com/Api/Advertiser/AdInsight/V10/AdInsightService.svc"
                }
            },
            {
                typeof (ICampaignManagementServiceV10), new ServiceInfo
                {
                    ProductionUrl = "https://campaign.api.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v10/CampaignManagementService.svc",
                    SandboxUrl = "https://campaign.api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v10/CampaignManagementService.svc"
                }
            },
            {
                typeof (IBulkServiceV10), new ServiceInfo
                {
                    ProductionUrl = "https://bulk.api.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v10/BulkService.svc",
                    SandboxUrl = "https://bulk.api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v10/BulkService.svc"
                }
            },
        };

        private static readonly Type[] ServiceTypes;

        public Type[] SupportedServiceTypes
        {
            get { return ServiceTypes; }
        }

        private static readonly Dictionary<Type, string> ConfigurationNamesByInterfaceTypes;

        static ServiceClientFactory()
        {
            ServiceTypes = new[]
            {
                typeof (IAdIntelligenceService), typeof (IBulkService), typeof (ICampaignManagementService),
                typeof (ICustomerBillingService), typeof (ICustomerManagementService),
                typeof (IOptimizerService), typeof (IReportingService), typeof (IAdInsightServiceV10), typeof (IBulkServiceV10),
                typeof (ICampaignManagementServiceV10)
            };

            ConfigurationNamesByInterfaceTypes = new Dictionary<Type, string>();

            foreach (var serviceType in ServiceTypes)
            {
                var serviceContractAttribute = (ServiceContractAttribute)Attribute.GetCustomAttribute(serviceType, typeof(ServiceContractAttribute));

                ConfigurationNamesByInterfaceTypes[serviceType] = serviceContractAttribute.ConfigurationName;
            }
        }

        public IChannelFactory<TClient> CreateChannelFactory<TClient>(ApiEnvironment env)
            where TClient : class
        {
            var endpoint = GetEndpointFromConfiguration(typeof(TClient));

            var factory = endpoint != null 
                ? CreateChannelFactoryForCustomEndpoint<TClient>(endpoint) 
                : CreateChannelFactoryForStandardEndpoint<TClient>(env);

            factory.Endpoint.Behaviors.Add(new UserAgentBehavior());

            return factory;
        }

        public T CreateServiceFromFactory<T>(IChannelFactory<T> channelFactory)
            where T : class
        {
            var concreteChannelFactory = channelFactory as ChannelFactory<T>;

            if (concreteChannelFactory != null)
            {
                return concreteChannelFactory.CreateChannel();
            }

            throw new InvalidOperationException("Invalid IChannelFactory type: " + channelFactory.GetType());
        }

        private static ChannelFactory<TClient> CreateChannelFactoryForCustomEndpoint<TClient>(ChannelEndpointElement endpoint)
            where TClient : class
        {
            return new ChannelFactory<TClient>(string.IsNullOrEmpty(endpoint.EndpointConfiguration) ? "*" : endpoint.EndpointConfiguration);
        }

        private static ChannelFactory<TClient> CreateChannelFactoryForStandardEndpoint<TClient>(ApiEnvironment env)
            where TClient : class
        {
            var serviceInfo = Endpoints[typeof(TClient)];

            var endpointAddress = new EndpointAddress(serviceInfo.GetUrl(env));

            return new ChannelFactory<TClient>(new BasicHttpBinding(BasicHttpSecurityMode.Transport) { MaxReceivedMessageSize = int.MaxValue }, endpointAddress);
        }

        private ChannelEndpointElement GetEndpointFromConfiguration(Type serviceInterfaceType)
        {
            try
            {
                var clientSection = HostingEnvironment.IsHosted ?
                    (ClientSection)WebConfigurationManager.GetSection("system.serviceModel/client") :
                    (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");

                var contractName = ConfigurationNamesByInterfaceTypes[serviceInterfaceType];

                if (clientSection != null && clientSection.Endpoints != null)
                {
                    var endpoint = clientSection.Endpoints.Cast<ChannelEndpointElement>().FirstOrDefault(x => x.Contract == contractName);

                    return endpoint;
                }

                return null;
            }
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }
    }
}

//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.5
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Web.Configuration;
using System.Web.Hosting;
using Microsoft.BingAds.CustomerBilling;
using Microsoft.BingAds.CustomerManagement;
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
            {
                typeof (V11.CustomerBilling.ICustomerBillingService), new ServiceInfo
                {
                    ProductionUrl = "https://clientcenter.api.bingads.microsoft.com/Api/Billing/v11/CustomerBillingService.svc",
                    SandboxUrl = null
                }
            },
            {
                typeof (V11.CustomerManagement.ICustomerManagementService), new ServiceInfo
                {
                    ProductionUrl = "https://clientcenter.api.bingads.microsoft.com/Api/CustomerManagement/v11/CustomerManagementService.svc",
                    SandboxUrl = "https://clientcenter.api.sandbox.bingads.microsoft.com/Api/CustomerManagement/v11/CustomerManagementService.svc"
                }
            },
            {
                typeof (V11.Reporting.IReportingService), new ServiceInfo
                {
                    ProductionUrl = "https://reporting.api.bingads.microsoft.com/Api/Advertiser/Reporting/v11/ReportingService.svc",
                    SandboxUrl = "https://reporting.api.sandbox.bingads.microsoft.com/Api/Advertiser/Reporting/v11/ReportingService.svc"
                }
            },
            {
                typeof (V11.AdInsight.IAdInsightService), new ServiceInfo
                {
                    ProductionUrl = "https://adinsight.api.bingads.microsoft.com/Api/Advertiser/AdInsight/V11/AdInsightService.svc",
                    SandboxUrl = "https://adinsight.api.sandbox.bingads.microsoft.com/Api/Advertiser/AdInsight/V11/AdInsightService.svc"
                }
            },
            {
                typeof (V11.CampaignManagement.ICampaignManagementService), new ServiceInfo
                {
                    ProductionUrl = "https://campaign.api.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v11/CampaignManagementService.svc",
                    SandboxUrl = "https://campaign.api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v11/CampaignManagementService.svc"
                }
            },
            {
                typeof (V11.Bulk.IBulkService), new ServiceInfo
                {
                    ProductionUrl = "https://bulk.api.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v11/BulkService.svc",
                    SandboxUrl = "https://bulk.api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v11/BulkService.svc"
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
                typeof (ICustomerBillingService), typeof (ICustomerManagementService),
                typeof (IReportingService), typeof (IAdInsightServiceV10), typeof (IBulkServiceV10),
                typeof (ICampaignManagementServiceV10), typeof(V11.CustomerBilling.ICustomerBillingService), 
                typeof (V11.CustomerManagement.ICustomerManagementService), typeof(V11.Reporting.IReportingService),
                typeof (V11.Bulk.IBulkService), typeof(V11.CampaignManagement.ICampaignManagementService),
                typeof (V11.AdInsight.IAdInsightService)
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

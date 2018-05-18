//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.12
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
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.BingAds.Internal
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    public class ServiceClientFactory : IServiceClientFactory
    {
        private static readonly Dictionary<Type, ServiceInfo> Endpoints = new Dictionary<Type, ServiceInfo>
        {
            {
                typeof (V11.CustomerBilling.ICustomerBillingService), new ServiceInfo
                {
                    ProductionUrl = "https://clientcenter.api.bingads.microsoft.com/Api/Billing/v11/CustomerBillingService.svc",
                    SandboxUrl = "https://clientcenter.api.sandbox.bingads.microsoft.com/Api/Billing/v11/CustomerBillingService.svc"
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
            {
                typeof (V12.CustomerBilling.ICustomerBillingService), new ServiceInfo
                {
                    ProductionUrl = "https://clientcenter.api.bingads.microsoft.com/Api/Billing/v12/CustomerBillingService.svc",
                    SandboxUrl = "https://clientcenter.api.sandbox.bingads.microsoft.com/Api/Billing/v12/CustomerBillingService.svc"
                }
            },
            {
                typeof (V12.CustomerManagement.ICustomerManagementService), new ServiceInfo
                {
                    ProductionUrl = "https://clientcenter.api.bingads.microsoft.com/Api/CustomerManagement/v12/CustomerManagementService.svc",
                    SandboxUrl = "https://clientcenter.api.sandbox.bingads.microsoft.com/Api/CustomerManagement/v12/CustomerManagementService.svc"
                }
            },
            {
                typeof (V12.Reporting.IReportingService), new ServiceInfo
                {
                    ProductionUrl = "https://reporting.api.bingads.microsoft.com/Api/Advertiser/Reporting/v12/ReportingService.svc",
                    SandboxUrl = "https://reporting.api.sandbox.bingads.microsoft.com/Api/Advertiser/Reporting/v12/ReportingService.svc"
                }
            },
            {
                typeof (V12.AdInsight.IAdInsightService), new ServiceInfo
                {
                    ProductionUrl = "https://adinsight.api.bingads.microsoft.com/Api/Advertiser/AdInsight/V12/AdInsightService.svc",
                    SandboxUrl = "https://adinsight.api.sandbox.bingads.microsoft.com/Api/Advertiser/AdInsight/V12/AdInsightService.svc"
                }
            },
            {
                typeof (V12.CampaignManagement.ICampaignManagementService), new ServiceInfo
                {
                    ProductionUrl = "https://campaign.api.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v12/CampaignManagementService.svc",
                    SandboxUrl = "https://campaign.api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v12/CampaignManagementService.svc"
                }
            },
            {
                typeof (V12.Bulk.IBulkService), new ServiceInfo
                {
                    ProductionUrl = "https://bulk.api.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v12/BulkService.svc",
                    SandboxUrl = "https://bulk.api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v12/BulkService.svc"
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
                typeof (V11.CustomerBilling.ICustomerBillingService),
                typeof (V11.Reporting.IReportingService),
                typeof (V11.CustomerManagement.ICustomerManagementService),
                typeof (V11.Bulk.IBulkService),
                typeof (V11.CampaignManagement.ICampaignManagementService),
                typeof (V11.AdInsight.IAdInsightService),
                typeof (V12.CustomerBilling.ICustomerBillingService),
                typeof (V12.Reporting.IReportingService),
                typeof (V12.CustomerManagement.ICustomerManagementService),
                typeof (V12.Bulk.IBulkService),
                typeof (V12.CampaignManagement.ICampaignManagementService),
                typeof (V12.AdInsight.IAdInsightService)
            };

            ConfigurationNamesByInterfaceTypes = new Dictionary<Type, string>();

            foreach (var serviceType in ServiceTypes)
            {
                var serviceContractAttribute = (ServiceContractAttribute)Attribute.GetCustomAttribute(serviceType, typeof(ServiceContractAttribute));

                ConfigurationNamesByInterfaceTypes[serviceType] = serviceContractAttribute.ConfigurationName;
            }
        }

        public virtual IChannelFactory<TClient> CreateChannelFactory<TClient>(ApiEnvironment env)
            where TClient : class
        {
            var factory = CreateChannelFactoryForStandardEndpoint<TClient>(env);

            factory.Endpoint.EndpointBehaviors.Add(new UserAgentBehavior());
            factory.Endpoint.EndpointBehaviors.Add(new SimpleTraceBehavior());

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


        private static ChannelFactory<TClient> CreateChannelFactoryForStandardEndpoint<TClient>(ApiEnvironment env)
            where TClient : class
        {
            var serviceInfo = Endpoints[typeof(TClient)];

            var endpointAddress = new EndpointAddress(serviceInfo.GetUrl(env));

            return new ChannelFactory<TClient>(new BasicHttpBinding(BasicHttpSecurityMode.Transport)
            {
                MaxReceivedMessageSize = 52428800,
                MaxBufferSize = 52428800,
                ReceiveTimeout = TimeSpan.FromMinutes(10),
                SendTimeout = TimeSpan.FromMinutes(10),
                OpenTimeout = TimeSpan.FromMinutes(1),
                CloseTimeout = TimeSpan.FromMinutes(1)
            }, endpointAddress);
        }

    }
}

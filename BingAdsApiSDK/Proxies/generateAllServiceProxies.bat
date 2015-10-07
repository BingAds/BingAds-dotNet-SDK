call generateProxy AdIntelligence AdIntelligence https://api.sandbox.bingads.microsoft.com/Api/Advertiser/AdIntelligence/v9/AdIntelligenceService.svc
call generateProxy Bulk Bulk https://api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v9/BulkService.svc
call generateProxy CampaignManagement CampaignManagement https://api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v9/CampaignManagementService.svc
call generateProxy CustomerBilling CustomerBilling https://clientcenter.api.bingads.microsoft.com/Api/Billing/v9/CustomerBillingService.svc
call generateProxy CustomerManagement CustomerManagement https://clientcenter.api.sandbox.bingads.microsoft.com/Api/CustomerManagement/v9/CustomerManagementService.svc
call generateProxy Optimizer Optimizer https://api.sandbox.bingads.microsoft.com/Api/Advertiser/Optimizer/v9/OptimizerService.svc
call generateProxy Reporting Reporting https://api.sandbox.bingads.microsoft.com/Api/Advertiser/Reporting/v9/ReportingService.svc

MKDIR V10
CD V10
call ..\generateProxy AdInsight V10.AdInsight https://adinsight.api.sandbox.bingads.microsoft.com/Api/Advertiser/AdInsight/V10/AdInsightService.svc
call ..\generateProxy Bulk V10.Bulk https://bulk.api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v10/BulkService.svc
call ..\generateProxy CampaignManagement V10.CampaignManagement https://campaign.api.sandbox.bingads.microsoft.com/Api/Advertiser/CampaignManagement/v10/CampaignManagementService.svc
CD ..
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Reporting;

namespace BingAdsExamplesLibrary.V13
{
    public class ReportingExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | Reporting V13"; }
        }
        public ServiceClient<IReportingService> ReportingService;
        public ReportingExampleHelper(SendMessageDelegate OutputStatusMessageDefault)
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }
        public async Task<PollGenerateReportResponse> PollGenerateReportAsync(
            String reportRequestId)
        {
            var request = new PollGenerateReportRequest
            {
                ReportRequestId = reportRequestId
            };

            return (await ReportingService.CallAsync((s, r) => s.PollGenerateReportAsync(r), request));
        }
        public async Task<SubmitGenerateReportResponse> SubmitGenerateReportAsync(
            ReportRequest reportRequest)
        {
            var request = new SubmitGenerateReportRequest
            {
                ReportRequest = reportRequest
            };

            return (await ReportingService.CallAsync((s, r) => s.SubmitGenerateReportAsync(r), request));
        }
        public void OutputAccountPerformanceReportFilter(AccountPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage("* * * End OutputAccountPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfAccountPerformanceReportFilter(IList<AccountPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAccountPerformanceReportRequest(AccountPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAccountPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAccountPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAccountPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfAccountPerformanceReportRequest(IList<AccountPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputAccountReportScope(AccountReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountReportScope * * *");
                OutputStatusMessage("AccountIds:");
                OutputArrayOfLong(dataObject.AccountIds);
                OutputStatusMessage("* * * End OutputAccountReportScope * * *");
            }
        }
        public void OutputArrayOfAccountReportScope(IList<AccountReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountReportScope(dataObject);
                    }
                }
            }
        }
        public void OutputAccountThroughAdGroupReportScope(AccountThroughAdGroupReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountThroughAdGroupReportScope * * *");
                OutputStatusMessage("AccountIds:");
                OutputArrayOfLong(dataObject.AccountIds);
                OutputStatusMessage("AdGroups:");
                OutputArrayOfAdGroupReportScope(dataObject.AdGroups);
                OutputStatusMessage("Campaigns:");
                OutputArrayOfCampaignReportScope(dataObject.Campaigns);
                OutputStatusMessage("* * * End OutputAccountThroughAdGroupReportScope * * *");
            }
        }
        public void OutputArrayOfAccountThroughAdGroupReportScope(IList<AccountThroughAdGroupReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountThroughAdGroupReportScope(dataObject);
                    }
                }
            }
        }
        public void OutputAccountThroughCampaignReportScope(AccountThroughCampaignReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAccountThroughCampaignReportScope * * *");
                OutputStatusMessage("AccountIds:");
                OutputArrayOfLong(dataObject.AccountIds);
                OutputStatusMessage("Campaigns:");
                OutputArrayOfCampaignReportScope(dataObject.Campaigns);
                OutputStatusMessage("* * * End OutputAccountThroughCampaignReportScope * * *");
            }
        }
        public void OutputArrayOfAccountThroughCampaignReportScope(IList<AccountThroughCampaignReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAccountThroughCampaignReportScope(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiError(AdApiError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputAdApiError * * *");
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiError(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiFaultDetail * * *");
                OutputStatusMessage("Errors:");
                OutputArrayOfAdApiError(dataObject.Errors);
                OutputStatusMessage("* * * End OutputAdApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputAdDynamicTextPerformanceReportFilter(AdDynamicTextPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdDynamicTextPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputAdDynamicTextPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfAdDynamicTextPerformanceReportFilter(IList<AdDynamicTextPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdDynamicTextPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAdDynamicTextPerformanceReportRequest(AdDynamicTextPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdDynamicTextPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAdDynamicTextPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAdDynamicTextPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAdDynamicTextPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfAdDynamicTextPerformanceReportRequest(IList<AdDynamicTextPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdDynamicTextPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionByAdReportFilter(AdExtensionByAdReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionByAdReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage("* * * End OutputAdExtensionByAdReportFilter * * *");
            }
        }
        public void OutputArrayOfAdExtensionByAdReportFilter(IList<AdExtensionByAdReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionByAdReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionByAdReportRequest(AdExtensionByAdReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionByAdReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAdExtensionByAdReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAdExtensionByAdReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAdExtensionByAdReportRequest * * *");
            }
        }
        public void OutputArrayOfAdExtensionByAdReportRequest(IList<AdExtensionByAdReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionByAdReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionByKeywordReportFilter(AdExtensionByKeywordReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionByKeywordReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputStatusMessage("* * * End OutputAdExtensionByKeywordReportFilter * * *");
            }
        }
        public void OutputArrayOfAdExtensionByKeywordReportFilter(IList<AdExtensionByKeywordReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionByKeywordReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionByKeywordReportRequest(AdExtensionByKeywordReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionByKeywordReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAdExtensionByKeywordReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAdExtensionByKeywordReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAdExtensionByKeywordReportRequest * * *");
            }
        }
        public void OutputArrayOfAdExtensionByKeywordReportRequest(IList<AdExtensionByKeywordReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionByKeywordReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionDetailReportFilter(AdExtensionDetailReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionDetailReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage("* * * End OutputAdExtensionDetailReportFilter * * *");
            }
        }
        public void OutputArrayOfAdExtensionDetailReportFilter(IList<AdExtensionDetailReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionDetailReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAdExtensionDetailReportRequest(AdExtensionDetailReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdExtensionDetailReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAdExtensionDetailReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAdExtensionDetailReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAdExtensionDetailReportRequest * * *");
            }
        }
        public void OutputArrayOfAdExtensionDetailReportRequest(IList<AdExtensionDetailReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdExtensionDetailReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupPerformanceReportFilter(AdGroupPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage("* * * End OutputAdGroupPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfAdGroupPerformanceReportFilter(IList<AdGroupPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupPerformanceReportRequest(AdGroupPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAdGroupPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAdGroupPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAdGroupPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfAdGroupPerformanceReportRequest(IList<AdGroupPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputAdGroupReportScope(AdGroupReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdGroupReportScope * * *");
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
                OutputStatusMessage("* * * End OutputAdGroupReportScope * * *");
            }
        }
        public void OutputArrayOfAdGroupReportScope(IList<AdGroupReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdGroupReportScope(dataObject);
                    }
                }
            }
        }
        public void OutputAdPerformanceReportFilter(AdPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputAdPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfAdPerformanceReportFilter(IList<AdPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAdPerformanceReportRequest(AdPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAdPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAdPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAdPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfAdPerformanceReportRequest(IList<AdPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputAgeGenderAudienceReportFilter(AgeGenderAudienceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAgeGenderAudienceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputAgeGenderAudienceReportFilter * * *");
            }
        }
        public void OutputArrayOfAgeGenderAudienceReportFilter(IList<AgeGenderAudienceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAgeGenderAudienceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAgeGenderAudienceReportRequest(AgeGenderAudienceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAgeGenderAudienceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAgeGenderAudienceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAgeGenderAudienceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAgeGenderAudienceReportRequest * * *");
            }
        }
        public void OutputArrayOfAgeGenderAudienceReportRequest(IList<AgeGenderAudienceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAgeGenderAudienceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputApiFaultDetail(ApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApiFaultDetail * * *");
                OutputStatusMessage("BatchErrors:");
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputStatusMessage("OperationErrors:");
                OutputArrayOfOperationError(dataObject.OperationErrors);
                OutputStatusMessage("* * * End OutputApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfApiFaultDetail(IList<ApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApplicationFault * * *");
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(null != adapifaultdetail)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifaultdetail = dataObject as ApiFaultDetail;
                if(null != apifaultdetail)
                {
                    OutputApiFaultDetail((ApiFaultDetail)dataObject);
                }
                OutputStatusMessage("* * * End OutputApplicationFault * * *");
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApplicationFault(dataObject);
                    }
                }
            }
        }
        public void OutputAudiencePerformanceReportFilter(AudiencePerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAudiencePerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage("* * * End OutputAudiencePerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfAudiencePerformanceReportFilter(IList<AudiencePerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAudiencePerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputAudiencePerformanceReportRequest(AudiencePerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAudiencePerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfAudiencePerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputAudiencePerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputAudiencePerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfAudiencePerformanceReportRequest(IList<AudiencePerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAudiencePerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBatchError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputBatchError * * *");
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBatchError(dataObject);
                    }
                }
            }
        }
        public void OutputBudgetSummaryReportRequest(BudgetSummaryReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBudgetSummaryReportRequest * * *");
                OutputStatusMessage("Columns:");
                OutputArrayOfBudgetSummaryReportColumn(dataObject.Columns);
                OutputStatusMessage("Scope:");
                OutputAccountThroughCampaignReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputBudgetSummaryReportRequest * * *");
            }
        }
        public void OutputArrayOfBudgetSummaryReportRequest(IList<BudgetSummaryReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBudgetSummaryReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputCallDetailReportFilter(CallDetailReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCallDetailReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage("* * * End OutputCallDetailReportFilter * * *");
            }
        }
        public void OutputArrayOfCallDetailReportFilter(IList<CallDetailReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCallDetailReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputCallDetailReportRequest(CallDetailReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCallDetailReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfCallDetailReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputCallDetailReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputCallDetailReportRequest * * *");
            }
        }
        public void OutputArrayOfCallDetailReportRequest(IList<CallDetailReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCallDetailReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignPerformanceReportFilter(CampaignPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage("* * * End OutputCampaignPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfCampaignPerformanceReportFilter(IList<CampaignPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignPerformanceReportRequest(CampaignPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfCampaignPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputCampaignPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughCampaignReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputCampaignPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfCampaignPerformanceReportRequest(IList<CampaignPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputCampaignReportScope(CampaignReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCampaignReportScope * * *");
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage("* * * End OutputCampaignReportScope * * *");
            }
        }
        public void OutputArrayOfCampaignReportScope(IList<CampaignReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCampaignReportScope(dataObject);
                    }
                }
            }
        }
        public void OutputConversionPerformanceReportFilter(ConversionPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputConversionPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputStatusMessage("Keywords:");
                OutputArrayOfString(dataObject.Keywords);
                OutputStatusMessage("* * * End OutputConversionPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfConversionPerformanceReportFilter(IList<ConversionPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputConversionPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputConversionPerformanceReportRequest(ConversionPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputConversionPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfConversionPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputConversionPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputConversionPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfConversionPerformanceReportRequest(IList<ConversionPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputConversionPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputDate(Date dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDate * * *");
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("Month: {0}", dataObject.Month));
                OutputStatusMessage(string.Format("Year: {0}", dataObject.Year));
                OutputStatusMessage("* * * End OutputDate * * *");
            }
        }
        public void OutputArrayOfDate(IList<Date> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDate(dataObject);
                    }
                }
            }
        }
        public void OutputDestinationUrlPerformanceReportFilter(DestinationUrlPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDestinationUrlPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputDestinationUrlPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfDestinationUrlPerformanceReportFilter(IList<DestinationUrlPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDestinationUrlPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputDestinationUrlPerformanceReportRequest(DestinationUrlPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDestinationUrlPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfDestinationUrlPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputDestinationUrlPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputDestinationUrlPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfDestinationUrlPerformanceReportRequest(IList<DestinationUrlPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDestinationUrlPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputDSAAutoTargetPerformanceReportFilter(DSAAutoTargetPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDSAAutoTargetPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("BidStrategyType: {0}", dataObject.BidStrategyType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DynamicAdTargetStatus: {0}", dataObject.DynamicAdTargetStatus));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputDSAAutoTargetPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfDSAAutoTargetPerformanceReportFilter(IList<DSAAutoTargetPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDSAAutoTargetPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputDSAAutoTargetPerformanceReportRequest(DSAAutoTargetPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDSAAutoTargetPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfDSAAutoTargetPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputDSAAutoTargetPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputDSAAutoTargetPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfDSAAutoTargetPerformanceReportRequest(IList<DSAAutoTargetPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDSAAutoTargetPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputDSACategoryPerformanceReportFilter(DSACategoryPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDSACategoryPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputDSACategoryPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfDSACategoryPerformanceReportFilter(IList<DSACategoryPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDSACategoryPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputDSACategoryPerformanceReportRequest(DSACategoryPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDSACategoryPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfDSACategoryPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputDSACategoryPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputDSACategoryPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfDSACategoryPerformanceReportRequest(IList<DSACategoryPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDSACategoryPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputDSASearchQueryPerformanceReportFilter(DSASearchQueryPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDSASearchQueryPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("ExcludeZeroClicks: {0}", dataObject.ExcludeZeroClicks));
                OutputStatusMessage(string.Format("FeedUrl: {0}", dataObject.FeedUrl));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("SearchQueries:");
                OutputArrayOfString(dataObject.SearchQueries);
                OutputStatusMessage("* * * End OutputDSASearchQueryPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfDSASearchQueryPerformanceReportFilter(IList<DSASearchQueryPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDSASearchQueryPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputDSASearchQueryPerformanceReportRequest(DSASearchQueryPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputDSASearchQueryPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfDSASearchQueryPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputDSASearchQueryPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputDSASearchQueryPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfDSASearchQueryPerformanceReportRequest(IList<DSASearchQueryPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputDSASearchQueryPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputGeographicPerformanceReportFilter(GeographicPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputGeographicPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage("CountryCode:");
                OutputArrayOfString(dataObject.CountryCode);
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputGeographicPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfGeographicPerformanceReportFilter(IList<GeographicPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputGeographicPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputGeographicPerformanceReportRequest(GeographicPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputGeographicPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfGeographicPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputGeographicPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputGeographicPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfGeographicPerformanceReportRequest(IList<GeographicPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputGeographicPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputGoalsAndFunnelsReportFilter(GoalsAndFunnelsReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputGoalsAndFunnelsReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage("GoalIds:");
                OutputArrayOfLong(dataObject.GoalIds);
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputStatusMessage("* * * End OutputGoalsAndFunnelsReportFilter * * *");
            }
        }
        public void OutputArrayOfGoalsAndFunnelsReportFilter(IList<GoalsAndFunnelsReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputGoalsAndFunnelsReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputGoalsAndFunnelsReportRequest(GoalsAndFunnelsReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputGoalsAndFunnelsReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfGoalsAndFunnelsReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputGoalsAndFunnelsReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputGoalsAndFunnelsReportRequest * * *");
            }
        }
        public void OutputArrayOfGoalsAndFunnelsReportRequest(IList<GoalsAndFunnelsReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputGoalsAndFunnelsReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordPerformanceReportFilter(KeywordPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage("AdRelevance:");
                OutputArrayOfInt(dataObject.AdRelevance);
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("BidMatchType: {0}", dataObject.BidMatchType));
                OutputStatusMessage(string.Format("BidStrategyType: {0}", dataObject.BidStrategyType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeliveredMatchType: {0}", dataObject.DeliveredMatchType));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage("ExpectedCtr:");
                OutputArrayOfInt(dataObject.ExpectedCtr);
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputStatusMessage("Keywords:");
                OutputArrayOfString(dataObject.Keywords);
                OutputStatusMessage("LandingPageExperience:");
                OutputArrayOfInt(dataObject.LandingPageExperience);
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("QualityScore:");
                OutputArrayOfInt(dataObject.QualityScore);
                OutputStatusMessage("* * * End OutputKeywordPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfKeywordPerformanceReportFilter(IList<KeywordPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordPerformanceReportRequest(KeywordPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfKeywordPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputKeywordPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage(string.Format("MaxRows: {0}", dataObject.MaxRows));
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Sort:");
                OutputArrayOfKeywordPerformanceReportSort(dataObject.Sort);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputKeywordPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfKeywordPerformanceReportRequest(IList<KeywordPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputKeywordPerformanceReportSort(KeywordPerformanceReportSort dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputKeywordPerformanceReportSort * * *");
                OutputStatusMessage(string.Format("SortColumn: {0}", dataObject.SortColumn));
                OutputStatusMessage(string.Format("SortOrder: {0}", dataObject.SortOrder));
                OutputStatusMessage("* * * End OutputKeywordPerformanceReportSort * * *");
            }
        }
        public void OutputArrayOfKeywordPerformanceReportSort(IList<KeywordPerformanceReportSort> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputKeywordPerformanceReportSort(dataObject);
                    }
                }
            }
        }
        public void OutputNegativeKeywordConflictReportFilter(NegativeKeywordConflictReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNegativeKeywordConflictReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputStatusMessage("* * * End OutputNegativeKeywordConflictReportFilter * * *");
            }
        }
        public void OutputArrayOfNegativeKeywordConflictReportFilter(IList<NegativeKeywordConflictReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNegativeKeywordConflictReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputNegativeKeywordConflictReportRequest(NegativeKeywordConflictReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputNegativeKeywordConflictReportRequest * * *");
                OutputStatusMessage("Columns:");
                OutputArrayOfNegativeKeywordConflictReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputNegativeKeywordConflictReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("* * * End OutputNegativeKeywordConflictReportRequest * * *");
            }
        }
        public void OutputArrayOfNegativeKeywordConflictReportRequest(IList<NegativeKeywordConflictReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputNegativeKeywordConflictReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOperationError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputOperationError * * *");
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOperationError(dataObject);
                    }
                }
            }
        }
        public void OutputProductDimensionPerformanceReportFilter(ProductDimensionPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductDimensionPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputProductDimensionPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfProductDimensionPerformanceReportFilter(IList<ProductDimensionPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductDimensionPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputProductDimensionPerformanceReportRequest(ProductDimensionPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductDimensionPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfProductDimensionPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputProductDimensionPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputProductDimensionPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfProductDimensionPerformanceReportRequest(IList<ProductDimensionPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductDimensionPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputProductMatchCountReportRequest(ProductMatchCountReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductMatchCountReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfProductMatchCountReportColumn(dataObject.Columns);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputProductMatchCountReportRequest * * *");
            }
        }
        public void OutputArrayOfProductMatchCountReportRequest(IList<ProductMatchCountReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductMatchCountReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputProductNegativeKeywordConflictReportFilter(ProductNegativeKeywordConflictReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductNegativeKeywordConflictReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage("* * * End OutputProductNegativeKeywordConflictReportFilter * * *");
            }
        }
        public void OutputArrayOfProductNegativeKeywordConflictReportFilter(IList<ProductNegativeKeywordConflictReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductNegativeKeywordConflictReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputProductNegativeKeywordConflictReportRequest(ProductNegativeKeywordConflictReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductNegativeKeywordConflictReportRequest * * *");
                OutputStatusMessage("Columns:");
                OutputArrayOfProductNegativeKeywordConflictReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputProductNegativeKeywordConflictReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("* * * End OutputProductNegativeKeywordConflictReportRequest * * *");
            }
        }
        public void OutputArrayOfProductNegativeKeywordConflictReportRequest(IList<ProductNegativeKeywordConflictReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductNegativeKeywordConflictReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputProductPartitionPerformanceReportFilter(ProductPartitionPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductPartitionPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputProductPartitionPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfProductPartitionPerformanceReportFilter(IList<ProductPartitionPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductPartitionPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputProductPartitionPerformanceReportRequest(ProductPartitionPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductPartitionPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfProductPartitionPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputProductPartitionPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputProductPartitionPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfProductPartitionPerformanceReportRequest(IList<ProductPartitionPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductPartitionPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputProductPartitionUnitPerformanceReportFilter(ProductPartitionUnitPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductPartitionUnitPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputProductPartitionUnitPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfProductPartitionUnitPerformanceReportFilter(IList<ProductPartitionUnitPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductPartitionUnitPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputProductPartitionUnitPerformanceReportRequest(ProductPartitionUnitPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductPartitionUnitPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfProductPartitionUnitPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputProductPartitionUnitPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputProductPartitionUnitPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfProductPartitionUnitPerformanceReportRequest(IList<ProductPartitionUnitPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductPartitionUnitPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputProductSearchQueryPerformanceReportFilter(ProductSearchQueryPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductSearchQueryPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("ExcludeZeroClicks: {0}", dataObject.ExcludeZeroClicks));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("SearchQueries:");
                OutputArrayOfString(dataObject.SearchQueries);
                OutputStatusMessage("* * * End OutputProductSearchQueryPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfProductSearchQueryPerformanceReportFilter(IList<ProductSearchQueryPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductSearchQueryPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputProductSearchQueryPerformanceReportRequest(ProductSearchQueryPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProductSearchQueryPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfProductSearchQueryPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputProductSearchQueryPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputProductSearchQueryPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfProductSearchQueryPerformanceReportRequest(IList<ProductSearchQueryPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProductSearchQueryPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputProfessionalDemographicsAudienceReportFilter(ProfessionalDemographicsAudienceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProfessionalDemographicsAudienceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputProfessionalDemographicsAudienceReportFilter * * *");
            }
        }
        public void OutputArrayOfProfessionalDemographicsAudienceReportFilter(IList<ProfessionalDemographicsAudienceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProfessionalDemographicsAudienceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputProfessionalDemographicsAudienceReportRequest(ProfessionalDemographicsAudienceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputProfessionalDemographicsAudienceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfProfessionalDemographicsAudienceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputProfessionalDemographicsAudienceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputProfessionalDemographicsAudienceReportRequest * * *");
            }
        }
        public void OutputArrayOfProfessionalDemographicsAudienceReportRequest(IList<ProfessionalDemographicsAudienceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputProfessionalDemographicsAudienceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputPublisherUsagePerformanceReportFilter(PublisherUsagePerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPublisherUsagePerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputPublisherUsagePerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfPublisherUsagePerformanceReportFilter(IList<PublisherUsagePerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPublisherUsagePerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputPublisherUsagePerformanceReportRequest(PublisherUsagePerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPublisherUsagePerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfPublisherUsagePerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputPublisherUsagePerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputPublisherUsagePerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfPublisherUsagePerformanceReportRequest(IList<PublisherUsagePerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPublisherUsagePerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputReportRequest(ReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputReportRequest * * *");
                OutputStatusMessage(string.Format("ExcludeColumnHeaders: {0}", dataObject.ExcludeColumnHeaders));
                OutputStatusMessage(string.Format("ExcludeReportFooter: {0}", dataObject.ExcludeReportFooter));
                OutputStatusMessage(string.Format("ExcludeReportHeader: {0}", dataObject.ExcludeReportHeader));
                OutputStatusMessage(string.Format("Format: {0}", dataObject.Format));
                OutputStatusMessage(string.Format("FormatVersion: {0}", dataObject.FormatVersion));
                OutputStatusMessage(string.Format("ReportName: {0}", dataObject.ReportName));
                OutputStatusMessage(string.Format("ReturnOnlyCompleteData: {0}", dataObject.ReturnOnlyCompleteData));
                var accountperformancereportrequest = dataObject as AccountPerformanceReportRequest;
                if(null != accountperformancereportrequest)
                {
                    OutputAccountPerformanceReportRequest((AccountPerformanceReportRequest)dataObject);
                }
                var addynamictextperformancereportrequest = dataObject as AdDynamicTextPerformanceReportRequest;
                if(null != addynamictextperformancereportrequest)
                {
                    OutputAdDynamicTextPerformanceReportRequest((AdDynamicTextPerformanceReportRequest)dataObject);
                }
                var adextensionbyadreportrequest = dataObject as AdExtensionByAdReportRequest;
                if(null != adextensionbyadreportrequest)
                {
                    OutputAdExtensionByAdReportRequest((AdExtensionByAdReportRequest)dataObject);
                }
                var adextensionbykeywordreportrequest = dataObject as AdExtensionByKeywordReportRequest;
                if(null != adextensionbykeywordreportrequest)
                {
                    OutputAdExtensionByKeywordReportRequest((AdExtensionByKeywordReportRequest)dataObject);
                }
                var adextensiondetailreportrequest = dataObject as AdExtensionDetailReportRequest;
                if(null != adextensiondetailreportrequest)
                {
                    OutputAdExtensionDetailReportRequest((AdExtensionDetailReportRequest)dataObject);
                }
                var adgroupperformancereportrequest = dataObject as AdGroupPerformanceReportRequest;
                if(null != adgroupperformancereportrequest)
                {
                    OutputAdGroupPerformanceReportRequest((AdGroupPerformanceReportRequest)dataObject);
                }
                var adperformancereportrequest = dataObject as AdPerformanceReportRequest;
                if(null != adperformancereportrequest)
                {
                    OutputAdPerformanceReportRequest((AdPerformanceReportRequest)dataObject);
                }
                var agegenderaudiencereportrequest = dataObject as AgeGenderAudienceReportRequest;
                if(null != agegenderaudiencereportrequest)
                {
                    OutputAgeGenderAudienceReportRequest((AgeGenderAudienceReportRequest)dataObject);
                }
                var audienceperformancereportrequest = dataObject as AudiencePerformanceReportRequest;
                if(null != audienceperformancereportrequest)
                {
                    OutputAudiencePerformanceReportRequest((AudiencePerformanceReportRequest)dataObject);
                }
                var budgetsummaryreportrequest = dataObject as BudgetSummaryReportRequest;
                if(null != budgetsummaryreportrequest)
                {
                    OutputBudgetSummaryReportRequest((BudgetSummaryReportRequest)dataObject);
                }
                var calldetailreportrequest = dataObject as CallDetailReportRequest;
                if(null != calldetailreportrequest)
                {
                    OutputCallDetailReportRequest((CallDetailReportRequest)dataObject);
                }
                var campaignperformancereportrequest = dataObject as CampaignPerformanceReportRequest;
                if(null != campaignperformancereportrequest)
                {
                    OutputCampaignPerformanceReportRequest((CampaignPerformanceReportRequest)dataObject);
                }
                var conversionperformancereportrequest = dataObject as ConversionPerformanceReportRequest;
                if(null != conversionperformancereportrequest)
                {
                    OutputConversionPerformanceReportRequest((ConversionPerformanceReportRequest)dataObject);
                }
                var destinationurlperformancereportrequest = dataObject as DestinationUrlPerformanceReportRequest;
                if(null != destinationurlperformancereportrequest)
                {
                    OutputDestinationUrlPerformanceReportRequest((DestinationUrlPerformanceReportRequest)dataObject);
                }
                var dsaautotargetperformancereportrequest = dataObject as DSAAutoTargetPerformanceReportRequest;
                if(null != dsaautotargetperformancereportrequest)
                {
                    OutputDSAAutoTargetPerformanceReportRequest((DSAAutoTargetPerformanceReportRequest)dataObject);
                }
                var dsacategoryperformancereportrequest = dataObject as DSACategoryPerformanceReportRequest;
                if(null != dsacategoryperformancereportrequest)
                {
                    OutputDSACategoryPerformanceReportRequest((DSACategoryPerformanceReportRequest)dataObject);
                }
                var dsasearchqueryperformancereportrequest = dataObject as DSASearchQueryPerformanceReportRequest;
                if(null != dsasearchqueryperformancereportrequest)
                {
                    OutputDSASearchQueryPerformanceReportRequest((DSASearchQueryPerformanceReportRequest)dataObject);
                }
                var geographicperformancereportrequest = dataObject as GeographicPerformanceReportRequest;
                if(null != geographicperformancereportrequest)
                {
                    OutputGeographicPerformanceReportRequest((GeographicPerformanceReportRequest)dataObject);
                }
                var goalsandfunnelsreportrequest = dataObject as GoalsAndFunnelsReportRequest;
                if(null != goalsandfunnelsreportrequest)
                {
                    OutputGoalsAndFunnelsReportRequest((GoalsAndFunnelsReportRequest)dataObject);
                }
                var keywordperformancereportrequest = dataObject as KeywordPerformanceReportRequest;
                if(null != keywordperformancereportrequest)
                {
                    OutputKeywordPerformanceReportRequest((KeywordPerformanceReportRequest)dataObject);
                }
                var negativekeywordconflictreportrequest = dataObject as NegativeKeywordConflictReportRequest;
                if(null != negativekeywordconflictreportrequest)
                {
                    OutputNegativeKeywordConflictReportRequest((NegativeKeywordConflictReportRequest)dataObject);
                }
                var productdimensionperformancereportrequest = dataObject as ProductDimensionPerformanceReportRequest;
                if(null != productdimensionperformancereportrequest)
                {
                    OutputProductDimensionPerformanceReportRequest((ProductDimensionPerformanceReportRequest)dataObject);
                }
                var productmatchcountreportrequest = dataObject as ProductMatchCountReportRequest;
                if(null != productmatchcountreportrequest)
                {
                    OutputProductMatchCountReportRequest((ProductMatchCountReportRequest)dataObject);
                }
                var productnegativekeywordconflictreportrequest = dataObject as ProductNegativeKeywordConflictReportRequest;
                if(null != productnegativekeywordconflictreportrequest)
                {
                    OutputProductNegativeKeywordConflictReportRequest((ProductNegativeKeywordConflictReportRequest)dataObject);
                }
                var productpartitionperformancereportrequest = dataObject as ProductPartitionPerformanceReportRequest;
                if(null != productpartitionperformancereportrequest)
                {
                    OutputProductPartitionPerformanceReportRequest((ProductPartitionPerformanceReportRequest)dataObject);
                }
                var productpartitionunitperformancereportrequest = dataObject as ProductPartitionUnitPerformanceReportRequest;
                if(null != productpartitionunitperformancereportrequest)
                {
                    OutputProductPartitionUnitPerformanceReportRequest((ProductPartitionUnitPerformanceReportRequest)dataObject);
                }
                var productsearchqueryperformancereportrequest = dataObject as ProductSearchQueryPerformanceReportRequest;
                if(null != productsearchqueryperformancereportrequest)
                {
                    OutputProductSearchQueryPerformanceReportRequest((ProductSearchQueryPerformanceReportRequest)dataObject);
                }
                var professionaldemographicsaudiencereportrequest = dataObject as ProfessionalDemographicsAudienceReportRequest;
                if(null != professionaldemographicsaudiencereportrequest)
                {
                    OutputProfessionalDemographicsAudienceReportRequest((ProfessionalDemographicsAudienceReportRequest)dataObject);
                }
                var publisherusageperformancereportrequest = dataObject as PublisherUsagePerformanceReportRequest;
                if(null != publisherusageperformancereportrequest)
                {
                    OutputPublisherUsagePerformanceReportRequest((PublisherUsagePerformanceReportRequest)dataObject);
                }
                var searchcampaignchangehistoryreportrequest = dataObject as SearchCampaignChangeHistoryReportRequest;
                if(null != searchcampaignchangehistoryreportrequest)
                {
                    OutputSearchCampaignChangeHistoryReportRequest((SearchCampaignChangeHistoryReportRequest)dataObject);
                }
                var searchqueryperformancereportrequest = dataObject as SearchQueryPerformanceReportRequest;
                if(null != searchqueryperformancereportrequest)
                {
                    OutputSearchQueryPerformanceReportRequest((SearchQueryPerformanceReportRequest)dataObject);
                }
                var shareofvoicereportrequest = dataObject as ShareOfVoiceReportRequest;
                if(null != shareofvoicereportrequest)
                {
                    OutputShareOfVoiceReportRequest((ShareOfVoiceReportRequest)dataObject);
                }
                var userlocationperformancereportrequest = dataObject as UserLocationPerformanceReportRequest;
                if(null != userlocationperformancereportrequest)
                {
                    OutputUserLocationPerformanceReportRequest((UserLocationPerformanceReportRequest)dataObject);
                }
                OutputStatusMessage("* * * End OutputReportRequest * * *");
            }
        }
        public void OutputArrayOfReportRequest(IList<ReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputReportRequestStatus(ReportRequestStatus dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputReportRequestStatus * * *");
                OutputStatusMessage(string.Format("ReportDownloadUrl: {0}", dataObject.ReportDownloadUrl));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage("* * * End OutputReportRequestStatus * * *");
            }
        }
        public void OutputArrayOfReportRequestStatus(IList<ReportRequestStatus> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputReportRequestStatus(dataObject);
                    }
                }
            }
        }
        public void OutputReportTime(ReportTime dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputReportTime * * *");
                OutputStatusMessage("CustomDateRangeEnd:");
                OutputDate(dataObject.CustomDateRangeEnd);
                OutputStatusMessage("CustomDateRangeStart:");
                OutputDate(dataObject.CustomDateRangeStart);
                OutputStatusMessage(string.Format("PredefinedTime: {0}", dataObject.PredefinedTime));
                OutputStatusMessage(string.Format("ReportTimeZone: {0}", dataObject.ReportTimeZone));
                OutputStatusMessage("* * * End OutputReportTime * * *");
            }
        }
        public void OutputArrayOfReportTime(IList<ReportTime> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputReportTime(dataObject);
                    }
                }
            }
        }
        public void OutputSearchCampaignChangeHistoryReportFilter(SearchCampaignChangeHistoryReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSearchCampaignChangeHistoryReportFilter * * *");
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("HowChanged: {0}", dataObject.HowChanged));
                OutputStatusMessage(string.Format("ItemChanged: {0}", dataObject.ItemChanged));
                OutputStatusMessage("* * * End OutputSearchCampaignChangeHistoryReportFilter * * *");
            }
        }
        public void OutputArrayOfSearchCampaignChangeHistoryReportFilter(IList<SearchCampaignChangeHistoryReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSearchCampaignChangeHistoryReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputSearchCampaignChangeHistoryReportRequest(SearchCampaignChangeHistoryReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSearchCampaignChangeHistoryReportRequest * * *");
                OutputStatusMessage("Columns:");
                OutputArrayOfSearchCampaignChangeHistoryReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputSearchCampaignChangeHistoryReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputSearchCampaignChangeHistoryReportRequest * * *");
            }
        }
        public void OutputArrayOfSearchCampaignChangeHistoryReportRequest(IList<SearchCampaignChangeHistoryReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSearchCampaignChangeHistoryReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputSearchQueryPerformanceReportFilter(SearchQueryPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSearchQueryPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeliveredMatchType: {0}", dataObject.DeliveredMatchType));
                OutputStatusMessage(string.Format("ExcludeZeroClicks: {0}", dataObject.ExcludeZeroClicks));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("SearchQueries:");
                OutputArrayOfString(dataObject.SearchQueries);
                OutputStatusMessage("* * * End OutputSearchQueryPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfSearchQueryPerformanceReportFilter(IList<SearchQueryPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSearchQueryPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputSearchQueryPerformanceReportRequest(SearchQueryPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputSearchQueryPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfSearchQueryPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputSearchQueryPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputSearchQueryPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfSearchQueryPerformanceReportRequest(IList<SearchQueryPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputSearchQueryPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputShareOfVoiceReportFilter(ShareOfVoiceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputShareOfVoiceReportFilter * * *");
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("BidMatchType: {0}", dataObject.BidMatchType));
                OutputStatusMessage(string.Format("BidStrategyType: {0}", dataObject.BidStrategyType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeliveredMatchType: {0}", dataObject.DeliveredMatchType));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputStatusMessage("Keywords:");
                OutputArrayOfString(dataObject.Keywords);
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputShareOfVoiceReportFilter * * *");
            }
        }
        public void OutputArrayOfShareOfVoiceReportFilter(IList<ShareOfVoiceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputShareOfVoiceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputShareOfVoiceReportRequest(ShareOfVoiceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputShareOfVoiceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfShareOfVoiceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputShareOfVoiceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputShareOfVoiceReportRequest * * *");
            }
        }
        public void OutputArrayOfShareOfVoiceReportRequest(IList<ShareOfVoiceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputShareOfVoiceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputUserLocationPerformanceReportFilter(UserLocationPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputUserLocationPerformanceReportFilter * * *");
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage("CountryCode:");
                OutputArrayOfString(dataObject.CountryCode);
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage("* * * End OutputUserLocationPerformanceReportFilter * * *");
            }
        }
        public void OutputArrayOfUserLocationPerformanceReportFilter(IList<UserLocationPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputUserLocationPerformanceReportFilter(dataObject);
                    }
                }
            }
        }
        public void OutputUserLocationPerformanceReportRequest(UserLocationPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputUserLocationPerformanceReportRequest * * *");
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputStatusMessage("Columns:");
                OutputArrayOfUserLocationPerformanceReportColumn(dataObject.Columns);
                OutputStatusMessage("Filter:");
                OutputUserLocationPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage("Scope:");
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputStatusMessage("Time:");
                OutputReportTime(dataObject.Time);
                OutputStatusMessage("* * * End OutputUserLocationPerformanceReportRequest * * *");
            }
        }
        public void OutputArrayOfUserLocationPerformanceReportRequest(IList<UserLocationPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputUserLocationPerformanceReportRequest(dataObject);
                    }
                }
            }
        }
        public void OutputReportFormat(ReportFormat valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ReportFormat)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfReportFormat(IList<ReportFormat> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputReportFormat(valueSet);
                }
            }
        }
        public void OutputReportAggregation(ReportAggregation valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ReportAggregation)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfReportAggregation(IList<ReportAggregation> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputReportAggregation(valueSet);
                }
            }
        }
        public void OutputAccountPerformanceReportColumn(AccountPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AccountPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAccountPerformanceReportColumn(IList<AccountPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAccountPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputAccountStatusReportFilter(AccountStatusReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AccountStatusReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAccountStatusReportFilter(IList<AccountStatusReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAccountStatusReportFilter(valueSet);
                }
            }
        }
        public void OutputAdDistributionReportFilter(AdDistributionReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdDistributionReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdDistributionReportFilter(IList<AdDistributionReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdDistributionReportFilter(valueSet);
                }
            }
        }
        public void OutputDeviceOSReportFilter(DeviceOSReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DeviceOSReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDeviceOSReportFilter(IList<DeviceOSReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDeviceOSReportFilter(valueSet);
                }
            }
        }
        public void OutputDeviceTypeReportFilter(DeviceTypeReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DeviceTypeReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDeviceTypeReportFilter(IList<DeviceTypeReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDeviceTypeReportFilter(valueSet);
                }
            }
        }
        public void OutputReportTimePeriod(ReportTimePeriod valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ReportTimePeriod)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfReportTimePeriod(IList<ReportTimePeriod> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputReportTimePeriod(valueSet);
                }
            }
        }
        public void OutputReportTimeZone(ReportTimeZone valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ReportTimeZone)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfReportTimeZone(IList<ReportTimeZone> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputReportTimeZone(valueSet);
                }
            }
        }
        public void OutputCampaignPerformanceReportColumn(CampaignPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CampaignPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCampaignPerformanceReportColumn(IList<CampaignPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCampaignPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputCampaignStatusReportFilter(CampaignStatusReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CampaignStatusReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCampaignStatusReportFilter(IList<CampaignStatusReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCampaignStatusReportFilter(valueSet);
                }
            }
        }
        public void OutputAdDynamicTextPerformanceReportColumn(AdDynamicTextPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdDynamicTextPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdDynamicTextPerformanceReportColumn(IList<AdDynamicTextPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdDynamicTextPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputAdGroupStatusReportFilter(AdGroupStatusReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupStatusReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupStatusReportFilter(IList<AdGroupStatusReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupStatusReportFilter(valueSet);
                }
            }
        }
        public void OutputAdStatusReportFilter(AdStatusReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdStatusReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdStatusReportFilter(IList<AdStatusReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdStatusReportFilter(valueSet);
                }
            }
        }
        public void OutputAdTypeReportFilter(AdTypeReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdTypeReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdTypeReportFilter(IList<AdTypeReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdTypeReportFilter(valueSet);
                }
            }
        }
        public void OutputKeywordStatusReportFilter(KeywordStatusReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(KeywordStatusReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfKeywordStatusReportFilter(IList<KeywordStatusReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputKeywordStatusReportFilter(valueSet);
                }
            }
        }
        public void OutputLanguageReportFilter(LanguageReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(LanguageReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfLanguageReportFilter(IList<LanguageReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputLanguageReportFilter(valueSet);
                }
            }
        }
        public void OutputAdGroupPerformanceReportColumn(AdGroupPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdGroupPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdGroupPerformanceReportColumn(IList<AdGroupPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdGroupPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputAdPerformanceReportColumn(AdPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdPerformanceReportColumn(IList<AdPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputKeywordPerformanceReportColumn(KeywordPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(KeywordPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfKeywordPerformanceReportColumn(IList<KeywordPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputKeywordPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputBidMatchTypeReportFilter(BidMatchTypeReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BidMatchTypeReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBidMatchTypeReportFilter(IList<BidMatchTypeReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBidMatchTypeReportFilter(valueSet);
                }
            }
        }
        public void OutputBidStrategyTypeReportFilter(BidStrategyTypeReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BidStrategyTypeReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBidStrategyTypeReportFilter(IList<BidStrategyTypeReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBidStrategyTypeReportFilter(valueSet);
                }
            }
        }
        public void OutputDeliveredMatchTypeReportFilter(DeliveredMatchTypeReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DeliveredMatchTypeReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDeliveredMatchTypeReportFilter(IList<DeliveredMatchTypeReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDeliveredMatchTypeReportFilter(valueSet);
                }
            }
        }
        public void OutputSortOrder(SortOrder valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(SortOrder)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfSortOrder(IList<SortOrder> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputSortOrder(valueSet);
                }
            }
        }
        public void OutputDestinationUrlPerformanceReportColumn(DestinationUrlPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DestinationUrlPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDestinationUrlPerformanceReportColumn(IList<DestinationUrlPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDestinationUrlPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputBudgetSummaryReportColumn(BudgetSummaryReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(BudgetSummaryReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfBudgetSummaryReportColumn(IList<BudgetSummaryReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputBudgetSummaryReportColumn(valueSet);
                }
            }
        }
        public void OutputAgeGenderAudienceReportColumn(AgeGenderAudienceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AgeGenderAudienceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAgeGenderAudienceReportColumn(IList<AgeGenderAudienceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAgeGenderAudienceReportColumn(valueSet);
                }
            }
        }
        public void OutputProfessionalDemographicsAudienceReportColumn(ProfessionalDemographicsAudienceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProfessionalDemographicsAudienceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProfessionalDemographicsAudienceReportColumn(IList<ProfessionalDemographicsAudienceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProfessionalDemographicsAudienceReportColumn(valueSet);
                }
            }
        }
        public void OutputUserLocationPerformanceReportColumn(UserLocationPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(UserLocationPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfUserLocationPerformanceReportColumn(IList<UserLocationPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputUserLocationPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputPublisherUsagePerformanceReportColumn(PublisherUsagePerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PublisherUsagePerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPublisherUsagePerformanceReportColumn(IList<PublisherUsagePerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPublisherUsagePerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputSearchQueryPerformanceReportColumn(SearchQueryPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(SearchQueryPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfSearchQueryPerformanceReportColumn(IList<SearchQueryPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputSearchQueryPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputConversionPerformanceReportColumn(ConversionPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ConversionPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfConversionPerformanceReportColumn(IList<ConversionPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputConversionPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputGoalsAndFunnelsReportColumn(GoalsAndFunnelsReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(GoalsAndFunnelsReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfGoalsAndFunnelsReportColumn(IList<GoalsAndFunnelsReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputGoalsAndFunnelsReportColumn(valueSet);
                }
            }
        }
        public void OutputNegativeKeywordConflictReportColumn(NegativeKeywordConflictReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(NegativeKeywordConflictReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfNegativeKeywordConflictReportColumn(IList<NegativeKeywordConflictReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputNegativeKeywordConflictReportColumn(valueSet);
                }
            }
        }
        public void OutputSearchCampaignChangeHistoryReportColumn(SearchCampaignChangeHistoryReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(SearchCampaignChangeHistoryReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfSearchCampaignChangeHistoryReportColumn(IList<SearchCampaignChangeHistoryReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputSearchCampaignChangeHistoryReportColumn(valueSet);
                }
            }
        }
        public void OutputChangeTypeReportFilter(ChangeTypeReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ChangeTypeReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfChangeTypeReportFilter(IList<ChangeTypeReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputChangeTypeReportFilter(valueSet);
                }
            }
        }
        public void OutputChangeEntityReportFilter(ChangeEntityReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ChangeEntityReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfChangeEntityReportFilter(IList<ChangeEntityReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputChangeEntityReportFilter(valueSet);
                }
            }
        }
        public void OutputAdExtensionByAdReportColumn(AdExtensionByAdReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdExtensionByAdReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdExtensionByAdReportColumn(IList<AdExtensionByAdReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdExtensionByAdReportColumn(valueSet);
                }
            }
        }
        public void OutputAdExtensionByKeywordReportColumn(AdExtensionByKeywordReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdExtensionByKeywordReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdExtensionByKeywordReportColumn(IList<AdExtensionByKeywordReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdExtensionByKeywordReportColumn(valueSet);
                }
            }
        }
        public void OutputAudiencePerformanceReportColumn(AudiencePerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AudiencePerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAudiencePerformanceReportColumn(IList<AudiencePerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAudiencePerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputAdExtensionDetailReportColumn(AdExtensionDetailReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AdExtensionDetailReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAdExtensionDetailReportColumn(IList<AdExtensionDetailReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAdExtensionDetailReportColumn(valueSet);
                }
            }
        }
        public void OutputShareOfVoiceReportColumn(ShareOfVoiceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ShareOfVoiceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfShareOfVoiceReportColumn(IList<ShareOfVoiceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputShareOfVoiceReportColumn(valueSet);
                }
            }
        }
        public void OutputProductDimensionPerformanceReportColumn(ProductDimensionPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProductDimensionPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProductDimensionPerformanceReportColumn(IList<ProductDimensionPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProductDimensionPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputProductPartitionPerformanceReportColumn(ProductPartitionPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProductPartitionPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProductPartitionPerformanceReportColumn(IList<ProductPartitionPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProductPartitionPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputProductPartitionUnitPerformanceReportColumn(ProductPartitionUnitPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProductPartitionUnitPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProductPartitionUnitPerformanceReportColumn(IList<ProductPartitionUnitPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProductPartitionUnitPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputProductSearchQueryPerformanceReportColumn(ProductSearchQueryPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProductSearchQueryPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProductSearchQueryPerformanceReportColumn(IList<ProductSearchQueryPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProductSearchQueryPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputProductMatchCountReportColumn(ProductMatchCountReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProductMatchCountReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProductMatchCountReportColumn(IList<ProductMatchCountReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProductMatchCountReportColumn(valueSet);
                }
            }
        }
        public void OutputProductNegativeKeywordConflictReportColumn(ProductNegativeKeywordConflictReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ProductNegativeKeywordConflictReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfProductNegativeKeywordConflictReportColumn(IList<ProductNegativeKeywordConflictReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputProductNegativeKeywordConflictReportColumn(valueSet);
                }
            }
        }
        public void OutputCallDetailReportColumn(CallDetailReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(CallDetailReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfCallDetailReportColumn(IList<CallDetailReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputCallDetailReportColumn(valueSet);
                }
            }
        }
        public void OutputGeographicPerformanceReportColumn(GeographicPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(GeographicPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfGeographicPerformanceReportColumn(IList<GeographicPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputGeographicPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputDSASearchQueryPerformanceReportColumn(DSASearchQueryPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DSASearchQueryPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDSASearchQueryPerformanceReportColumn(IList<DSASearchQueryPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDSASearchQueryPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputDSAAutoTargetPerformanceReportColumn(DSAAutoTargetPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DSAAutoTargetPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDSAAutoTargetPerformanceReportColumn(IList<DSAAutoTargetPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDSAAutoTargetPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputDynamicAdTargetStatusReportFilter(DynamicAdTargetStatusReportFilter valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DynamicAdTargetStatusReportFilter)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDynamicAdTargetStatusReportFilter(IList<DynamicAdTargetStatusReportFilter> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDynamicAdTargetStatusReportFilter(valueSet);
                }
            }
        }
        public void OutputDSACategoryPerformanceReportColumn(DSACategoryPerformanceReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DSACategoryPerformanceReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDSACategoryPerformanceReportColumn(IList<DSACategoryPerformanceReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDSACategoryPerformanceReportColumn(valueSet);
                }
            }
        }
        public void OutputReportRequestStatusType(ReportRequestStatusType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ReportRequestStatusType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfReportRequestStatusType(IList<ReportRequestStatusType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputReportRequestStatusType(valueSet);
                }
            }
        }
        public void OutputArrayOfString(IList<string> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputKeyValuePairOfstringstring(KeyValuePair<string,string> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringstring(IList<KeyValuePair<string,string>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringstring(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOflonglong(KeyValuePair<long,long> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOflonglong(IList<KeyValuePair<long,long>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOflonglong(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOfstringbase64Binary(KeyValuePair<string,byte[]> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringbase64Binary(IList<KeyValuePair<string,byte[]>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringbase64Binary(dataObject);
                }
            }
        }
    }
}
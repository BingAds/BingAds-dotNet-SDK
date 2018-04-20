using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V12.Reporting;

namespace BingAdsExamplesLibrary.V12
{
    public class ReportingExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | Reporting V12"; }
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
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
            }
        }
        public void OutputArrayOfAccountPerformanceReportFilter(IList<AccountPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAccountPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAccountPerformanceReportRequest(AccountPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAccountPerformanceReportColumn(dataObject.Columns);
                OutputAccountPerformanceReportFilter(dataObject.Filter);
                OutputAccountReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAccountPerformanceReportRequest(IList<AccountPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAccountPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAccountReportScope(AccountReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfLong(dataObject.AccountIds);
            }
        }
        public void OutputArrayOfAccountReportScope(IList<AccountReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAccountReportScope(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAccountThroughAdGroupReportScope(AccountThroughAdGroupReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfLong(dataObject.AccountIds);
                OutputArrayOfAdGroupReportScope(dataObject.AdGroups);
                OutputArrayOfCampaignReportScope(dataObject.Campaigns);
            }
        }
        public void OutputArrayOfAccountThroughAdGroupReportScope(IList<AccountThroughAdGroupReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAccountThroughAdGroupReportScope(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAccountThroughCampaignReportScope(AccountThroughCampaignReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfLong(dataObject.AccountIds);
                OutputArrayOfCampaignReportScope(dataObject.Campaigns);
            }
        }
        public void OutputArrayOfAccountThroughCampaignReportScope(IList<AccountThroughCampaignReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAccountThroughCampaignReportScope(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdApiError(AdApiError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfAdApiError(dataObject.Errors);
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdDynamicTextPerformanceReportFilter(AdDynamicTextPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfAdDynamicTextPerformanceReportFilter(IList<AdDynamicTextPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdDynamicTextPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdDynamicTextPerformanceReportRequest(AdDynamicTextPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAdDynamicTextPerformanceReportColumn(dataObject.Columns);
                OutputAdDynamicTextPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAdDynamicTextPerformanceReportRequest(IList<AdDynamicTextPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdDynamicTextPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionByAdReportFilter(AdExtensionByAdReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
            }
        }
        public void OutputArrayOfAdExtensionByAdReportFilter(IList<AdExtensionByAdReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionByAdReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionByAdReportRequest(AdExtensionByAdReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAdExtensionByAdReportColumn(dataObject.Columns);
                OutputAdExtensionByAdReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAdExtensionByAdReportRequest(IList<AdExtensionByAdReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionByAdReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionByKeywordReportFilter(AdExtensionByKeywordReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
            }
        }
        public void OutputArrayOfAdExtensionByKeywordReportFilter(IList<AdExtensionByKeywordReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionByKeywordReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionByKeywordReportRequest(AdExtensionByKeywordReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAdExtensionByKeywordReportColumn(dataObject.Columns);
                OutputAdExtensionByKeywordReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAdExtensionByKeywordReportRequest(IList<AdExtensionByKeywordReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionByKeywordReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionDetailReportFilter(AdExtensionDetailReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
            }
        }
        public void OutputArrayOfAdExtensionDetailReportFilter(IList<AdExtensionDetailReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionDetailReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdExtensionDetailReportRequest(AdExtensionDetailReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAdExtensionDetailReportColumn(dataObject.Columns);
                OutputAdExtensionDetailReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAdExtensionDetailReportRequest(IList<AdExtensionDetailReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdExtensionDetailReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupPerformanceReportFilter(AdGroupPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputArrayOfString(dataObject.LanguageCode);
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
            }
        }
        public void OutputArrayOfAdGroupPerformanceReportFilter(IList<AdGroupPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupPerformanceReportRequest(AdGroupPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAdGroupPerformanceReportColumn(dataObject.Columns);
                OutputAdGroupPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAdGroupPerformanceReportRequest(IList<AdGroupPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdGroupReportScope(AdGroupReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
                OutputStatusMessage(string.Format("AdGroupId: {0}", dataObject.AdGroupId));
            }
        }
        public void OutputArrayOfAdGroupReportScope(IList<AdGroupReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdGroupReportScope(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdPerformanceReportFilter(AdPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfAdPerformanceReportFilter(IList<AdPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdPerformanceReportRequest(AdPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAdPerformanceReportColumn(dataObject.Columns);
                OutputAdPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAdPerformanceReportRequest(IList<AdPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAgeGenderAudienceReportFilter(AgeGenderAudienceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfAgeGenderAudienceReportFilter(IList<AgeGenderAudienceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAgeGenderAudienceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAgeGenderAudienceReportRequest(AgeGenderAudienceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAgeGenderAudienceReportColumn(dataObject.Columns);
                OutputAgeGenderAudienceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAgeGenderAudienceReportRequest(IList<AgeGenderAudienceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAgeGenderAudienceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAgeGenderDemographicReportFilter(AgeGenderDemographicReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfAgeGenderDemographicReportFilter(IList<AgeGenderDemographicReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAgeGenderDemographicReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAgeGenderDemographicReportRequest(AgeGenderDemographicReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAgeGenderDemographicReportColumn(dataObject.Columns);
                OutputAgeGenderDemographicReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAgeGenderDemographicReportRequest(IList<AgeGenderDemographicReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAgeGenderDemographicReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApiFaultDetail(ApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputArrayOfOperationError(dataObject.OperationErrors);
            }
        }
        public void OutputArrayOfApiFaultDetail(IList<ApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(adapifaultdetail != null)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifaultdetail = dataObject as ApiFaultDetail;
                if(apifaultdetail != null)
                {
                    OutputApiFaultDetail((ApiFaultDetail)dataObject);
                }
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApplicationFault(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAudiencePerformanceReportFilter(AudiencePerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
            }
        }
        public void OutputArrayOfAudiencePerformanceReportFilter(IList<AudiencePerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAudiencePerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAudiencePerformanceReportRequest(AudiencePerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfAudiencePerformanceReportColumn(dataObject.Columns);
                OutputAudiencePerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfAudiencePerformanceReportRequest(IList<AudiencePerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAudiencePerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBatchError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBudgetSummaryReportRequest(BudgetSummaryReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBudgetSummaryReportColumn(dataObject.Columns);
                OutputAccountThroughCampaignReportScope(dataObject.Scope);
                OutputBudgetSummaryReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfBudgetSummaryReportRequest(IList<BudgetSummaryReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBudgetSummaryReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBudgetSummaryReportTime(BudgetSummaryReportTime dataObject)
        {
            if (null != dataObject)
            {
                OutputDate(dataObject.CustomDateRangeEnd);
                OutputDate(dataObject.CustomDateRangeStart);
                OutputStatusMessage(string.Format("PredefinedTime: {0}", dataObject.PredefinedTime));
                OutputStatusMessage(string.Format("ReportTimeZone: {0}", dataObject.ReportTimeZone));
            }
        }
        public void OutputArrayOfBudgetSummaryReportTime(IList<BudgetSummaryReportTime> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBudgetSummaryReportTime(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCallDetailReportFilter(CallDetailReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
            }
        }
        public void OutputArrayOfCallDetailReportFilter(IList<CallDetailReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCallDetailReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCallDetailReportRequest(CallDetailReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfCallDetailReportColumn(dataObject.Columns);
                OutputCallDetailReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfCallDetailReportRequest(IList<CallDetailReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCallDetailReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaignPerformanceReportFilter(CampaignPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
            }
        }
        public void OutputArrayOfCampaignPerformanceReportFilter(IList<CampaignPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaignPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaignPerformanceReportRequest(CampaignPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfCampaignPerformanceReportColumn(dataObject.Columns);
                OutputCampaignPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughCampaignReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfCampaignPerformanceReportRequest(IList<CampaignPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaignPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputCampaignReportScope(CampaignReportScope dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("CampaignId: {0}", dataObject.CampaignId));
            }
        }
        public void OutputArrayOfCampaignReportScope(IList<CampaignReportScope> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputCampaignReportScope(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputConversionPerformanceReportFilter(ConversionPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputArrayOfString(dataObject.Keywords);
            }
        }
        public void OutputArrayOfConversionPerformanceReportFilter(IList<ConversionPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputConversionPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputConversionPerformanceReportRequest(ConversionPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfConversionPerformanceReportColumn(dataObject.Columns);
                OutputConversionPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfConversionPerformanceReportRequest(IList<ConversionPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputConversionPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDate(Date dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Day: {0}", dataObject.Day));
                OutputStatusMessage(string.Format("Month: {0}", dataObject.Month));
                OutputStatusMessage(string.Format("Year: {0}", dataObject.Year));
            }
        }
        public void OutputArrayOfDate(IList<Date> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDate(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDestinationUrlPerformanceReportFilter(DestinationUrlPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfDestinationUrlPerformanceReportFilter(IList<DestinationUrlPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDestinationUrlPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDestinationUrlPerformanceReportRequest(DestinationUrlPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfDestinationUrlPerformanceReportColumn(dataObject.Columns);
                OutputDestinationUrlPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfDestinationUrlPerformanceReportRequest(IList<DestinationUrlPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDestinationUrlPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDSAAutoTargetPerformanceReportFilter(DSAAutoTargetPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("BidStrategyType: {0}", dataObject.BidStrategyType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DynamicAdTargetStatus: {0}", dataObject.DynamicAdTargetStatus));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfDSAAutoTargetPerformanceReportFilter(IList<DSAAutoTargetPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDSAAutoTargetPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDSAAutoTargetPerformanceReportRequest(DSAAutoTargetPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfDSAAutoTargetPerformanceReportColumn(dataObject.Columns);
                OutputDSAAutoTargetPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfDSAAutoTargetPerformanceReportRequest(IList<DSAAutoTargetPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDSAAutoTargetPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDSACategoryPerformanceReportFilter(DSACategoryPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfDSACategoryPerformanceReportFilter(IList<DSACategoryPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDSACategoryPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDSACategoryPerformanceReportRequest(DSACategoryPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfDSACategoryPerformanceReportColumn(dataObject.Columns);
                OutputDSACategoryPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfDSACategoryPerformanceReportRequest(IList<DSACategoryPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDSACategoryPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDSASearchQueryPerformanceReportFilter(DSASearchQueryPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("ExcludeZeroClicks: {0}", dataObject.ExcludeZeroClicks));
                OutputArrayOfString(dataObject.LanguageCode);
                OutputArrayOfString(dataObject.SearchQueries);
            }
        }
        public void OutputArrayOfDSASearchQueryPerformanceReportFilter(IList<DSASearchQueryPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDSASearchQueryPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDSASearchQueryPerformanceReportRequest(DSASearchQueryPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfDSASearchQueryPerformanceReportColumn(dataObject.Columns);
                OutputDSASearchQueryPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfDSASearchQueryPerformanceReportRequest(IList<DSASearchQueryPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputDSASearchQueryPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputGeographicPerformanceReportFilter(GeographicPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputArrayOfString(dataObject.CountryCode);
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfGeographicPerformanceReportFilter(IList<GeographicPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputGeographicPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputGeographicPerformanceReportRequest(GeographicPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfGeographicPerformanceReportColumn(dataObject.Columns);
                OutputGeographicPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfGeographicPerformanceReportRequest(IList<GeographicPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputGeographicPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputGoalsAndFunnelsReportFilter(GoalsAndFunnelsReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceOS: {0}", dataObject.DeviceOS));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputArrayOfLong(dataObject.GoalIds);
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
            }
        }
        public void OutputArrayOfGoalsAndFunnelsReportFilter(IList<GoalsAndFunnelsReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputGoalsAndFunnelsReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputGoalsAndFunnelsReportRequest(GoalsAndFunnelsReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfGoalsAndFunnelsReportColumn(dataObject.Columns);
                OutputGoalsAndFunnelsReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfGoalsAndFunnelsReportRequest(IList<GoalsAndFunnelsReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputGoalsAndFunnelsReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordPerformanceReportFilter(KeywordPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputArrayOfInt(dataObject.AdRelevance);
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("BidMatchType: {0}", dataObject.BidMatchType));
                OutputStatusMessage(string.Format("BidStrategyType: {0}", dataObject.BidStrategyType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeliveredMatchType: {0}", dataObject.DeliveredMatchType));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputArrayOfInt(dataObject.ExpectedCtr);
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputArrayOfString(dataObject.Keywords);
                OutputArrayOfInt(dataObject.LandingPageExperience);
                OutputArrayOfString(dataObject.LanguageCode);
                OutputArrayOfInt(dataObject.QualityScore);
            }
        }
        public void OutputArrayOfKeywordPerformanceReportFilter(IList<KeywordPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordPerformanceReportRequest(KeywordPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfKeywordPerformanceReportColumn(dataObject.Columns);
                OutputKeywordPerformanceReportFilter(dataObject.Filter);
                OutputStatusMessage(string.Format("MaxRows: {0}", dataObject.MaxRows));
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputArrayOfKeywordPerformanceReportSort(dataObject.Sort);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfKeywordPerformanceReportRequest(IList<KeywordPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputKeywordPerformanceReportSort(KeywordPerformanceReportSort dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("SortColumn: {0}", dataObject.SortColumn));
                OutputStatusMessage(string.Format("SortOrder: {0}", dataObject.SortOrder));
            }
        }
        public void OutputArrayOfKeywordPerformanceReportSort(IList<KeywordPerformanceReportSort> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeywordPerformanceReportSort(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNegativeKeywordConflictReportFilter(NegativeKeywordConflictReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
            }
        }
        public void OutputArrayOfNegativeKeywordConflictReportFilter(IList<NegativeKeywordConflictReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNegativeKeywordConflictReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputNegativeKeywordConflictReportRequest(NegativeKeywordConflictReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfNegativeKeywordConflictReportColumn(dataObject.Columns);
                OutputNegativeKeywordConflictReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
            }
        }
        public void OutputArrayOfNegativeKeywordConflictReportRequest(IList<NegativeKeywordConflictReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputNegativeKeywordConflictReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOperationError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductDimensionPerformanceReportFilter(ProductDimensionPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfProductDimensionPerformanceReportFilter(IList<ProductDimensionPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductDimensionPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductDimensionPerformanceReportRequest(ProductDimensionPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfProductDimensionPerformanceReportColumn(dataObject.Columns);
                OutputProductDimensionPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfProductDimensionPerformanceReportRequest(IList<ProductDimensionPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductDimensionPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductMatchCountReportRequest(ProductMatchCountReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfProductMatchCountReportColumn(dataObject.Columns);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfProductMatchCountReportRequest(IList<ProductMatchCountReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductMatchCountReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductPartitionPerformanceReportFilter(ProductPartitionPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfProductPartitionPerformanceReportFilter(IList<ProductPartitionPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductPartitionPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductPartitionPerformanceReportRequest(ProductPartitionPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfProductPartitionPerformanceReportColumn(dataObject.Columns);
                OutputProductPartitionPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfProductPartitionPerformanceReportRequest(IList<ProductPartitionPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductPartitionPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductPartitionUnitPerformanceReportFilter(ProductPartitionUnitPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfProductPartitionUnitPerformanceReportFilter(IList<ProductPartitionUnitPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductPartitionUnitPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductPartitionUnitPerformanceReportRequest(ProductPartitionUnitPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfProductPartitionUnitPerformanceReportColumn(dataObject.Columns);
                OutputProductPartitionUnitPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfProductPartitionUnitPerformanceReportRequest(IList<ProductPartitionUnitPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductPartitionUnitPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductSearchQueryPerformanceReportFilter(ProductSearchQueryPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("ExcludeZeroClicks: {0}", dataObject.ExcludeZeroClicks));
                OutputArrayOfString(dataObject.LanguageCode);
                OutputArrayOfString(dataObject.SearchQueries);
            }
        }
        public void OutputArrayOfProductSearchQueryPerformanceReportFilter(IList<ProductSearchQueryPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductSearchQueryPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProductSearchQueryPerformanceReportRequest(ProductSearchQueryPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfProductSearchQueryPerformanceReportColumn(dataObject.Columns);
                OutputProductSearchQueryPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfProductSearchQueryPerformanceReportRequest(IList<ProductSearchQueryPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProductSearchQueryPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProfessionalDemographicsAudienceReportFilter(ProfessionalDemographicsAudienceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfProfessionalDemographicsAudienceReportFilter(IList<ProfessionalDemographicsAudienceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProfessionalDemographicsAudienceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputProfessionalDemographicsAudienceReportRequest(ProfessionalDemographicsAudienceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfProfessionalDemographicsAudienceReportColumn(dataObject.Columns);
                OutputProfessionalDemographicsAudienceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfProfessionalDemographicsAudienceReportRequest(IList<ProfessionalDemographicsAudienceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputProfessionalDemographicsAudienceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPublisherUsagePerformanceReportFilter(PublisherUsagePerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfPublisherUsagePerformanceReportFilter(IList<PublisherUsagePerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPublisherUsagePerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPublisherUsagePerformanceReportRequest(PublisherUsagePerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfPublisherUsagePerformanceReportColumn(dataObject.Columns);
                OutputPublisherUsagePerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfPublisherUsagePerformanceReportRequest(IList<PublisherUsagePerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPublisherUsagePerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputReportRequest(ReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ExcludeColumnHeaders: {0}", dataObject.ExcludeColumnHeaders));
                OutputStatusMessage(string.Format("ExcludeReportFooter: {0}", dataObject.ExcludeReportFooter));
                OutputStatusMessage(string.Format("ExcludeReportHeader: {0}", dataObject.ExcludeReportHeader));
                OutputStatusMessage(string.Format("Format: {0}", dataObject.Format));
                OutputStatusMessage(string.Format("Language: {0}", dataObject.Language));
                OutputStatusMessage(string.Format("ReportName: {0}", dataObject.ReportName));
                OutputStatusMessage(string.Format("ReturnOnlyCompleteData: {0}", dataObject.ReturnOnlyCompleteData));
                var accountperformancereportrequest = dataObject as AccountPerformanceReportRequest;
                if(accountperformancereportrequest != null)
                {
                    OutputAccountPerformanceReportRequest((AccountPerformanceReportRequest)dataObject);
                }
                var addynamictextperformancereportrequest = dataObject as AdDynamicTextPerformanceReportRequest;
                if(addynamictextperformancereportrequest != null)
                {
                    OutputAdDynamicTextPerformanceReportRequest((AdDynamicTextPerformanceReportRequest)dataObject);
                }
                var adextensionbyadreportrequest = dataObject as AdExtensionByAdReportRequest;
                if(adextensionbyadreportrequest != null)
                {
                    OutputAdExtensionByAdReportRequest((AdExtensionByAdReportRequest)dataObject);
                }
                var adextensionbykeywordreportrequest = dataObject as AdExtensionByKeywordReportRequest;
                if(adextensionbykeywordreportrequest != null)
                {
                    OutputAdExtensionByKeywordReportRequest((AdExtensionByKeywordReportRequest)dataObject);
                }
                var adextensiondetailreportrequest = dataObject as AdExtensionDetailReportRequest;
                if(adextensiondetailreportrequest != null)
                {
                    OutputAdExtensionDetailReportRequest((AdExtensionDetailReportRequest)dataObject);
                }
                var adgroupperformancereportrequest = dataObject as AdGroupPerformanceReportRequest;
                if(adgroupperformancereportrequest != null)
                {
                    OutputAdGroupPerformanceReportRequest((AdGroupPerformanceReportRequest)dataObject);
                }
                var adperformancereportrequest = dataObject as AdPerformanceReportRequest;
                if(adperformancereportrequest != null)
                {
                    OutputAdPerformanceReportRequest((AdPerformanceReportRequest)dataObject);
                }
                var agegenderaudiencereportrequest = dataObject as AgeGenderAudienceReportRequest;
                if(agegenderaudiencereportrequest != null)
                {
                    OutputAgeGenderAudienceReportRequest((AgeGenderAudienceReportRequest)dataObject);
                }
                var agegenderdemographicreportrequest = dataObject as AgeGenderDemographicReportRequest;
                if(agegenderdemographicreportrequest != null)
                {
                    OutputAgeGenderDemographicReportRequest((AgeGenderDemographicReportRequest)dataObject);
                }
                var audienceperformancereportrequest = dataObject as AudiencePerformanceReportRequest;
                if(audienceperformancereportrequest != null)
                {
                    OutputAudiencePerformanceReportRequest((AudiencePerformanceReportRequest)dataObject);
                }
                var budgetsummaryreportrequest = dataObject as BudgetSummaryReportRequest;
                if(budgetsummaryreportrequest != null)
                {
                    OutputBudgetSummaryReportRequest((BudgetSummaryReportRequest)dataObject);
                }
                var calldetailreportrequest = dataObject as CallDetailReportRequest;
                if(calldetailreportrequest != null)
                {
                    OutputCallDetailReportRequest((CallDetailReportRequest)dataObject);
                }
                var campaignperformancereportrequest = dataObject as CampaignPerformanceReportRequest;
                if(campaignperformancereportrequest != null)
                {
                    OutputCampaignPerformanceReportRequest((CampaignPerformanceReportRequest)dataObject);
                }
                var conversionperformancereportrequest = dataObject as ConversionPerformanceReportRequest;
                if(conversionperformancereportrequest != null)
                {
                    OutputConversionPerformanceReportRequest((ConversionPerformanceReportRequest)dataObject);
                }
                var destinationurlperformancereportrequest = dataObject as DestinationUrlPerformanceReportRequest;
                if(destinationurlperformancereportrequest != null)
                {
                    OutputDestinationUrlPerformanceReportRequest((DestinationUrlPerformanceReportRequest)dataObject);
                }
                var dsaautotargetperformancereportrequest = dataObject as DSAAutoTargetPerformanceReportRequest;
                if(dsaautotargetperformancereportrequest != null)
                {
                    OutputDSAAutoTargetPerformanceReportRequest((DSAAutoTargetPerformanceReportRequest)dataObject);
                }
                var dsacategoryperformancereportrequest = dataObject as DSACategoryPerformanceReportRequest;
                if(dsacategoryperformancereportrequest != null)
                {
                    OutputDSACategoryPerformanceReportRequest((DSACategoryPerformanceReportRequest)dataObject);
                }
                var dsasearchqueryperformancereportrequest = dataObject as DSASearchQueryPerformanceReportRequest;
                if(dsasearchqueryperformancereportrequest != null)
                {
                    OutputDSASearchQueryPerformanceReportRequest((DSASearchQueryPerformanceReportRequest)dataObject);
                }
                var geographicperformancereportrequest = dataObject as GeographicPerformanceReportRequest;
                if(geographicperformancereportrequest != null)
                {
                    OutputGeographicPerformanceReportRequest((GeographicPerformanceReportRequest)dataObject);
                }
                var goalsandfunnelsreportrequest = dataObject as GoalsAndFunnelsReportRequest;
                if(goalsandfunnelsreportrequest != null)
                {
                    OutputGoalsAndFunnelsReportRequest((GoalsAndFunnelsReportRequest)dataObject);
                }
                var keywordperformancereportrequest = dataObject as KeywordPerformanceReportRequest;
                if(keywordperformancereportrequest != null)
                {
                    OutputKeywordPerformanceReportRequest((KeywordPerformanceReportRequest)dataObject);
                }
                var negativekeywordconflictreportrequest = dataObject as NegativeKeywordConflictReportRequest;
                if(negativekeywordconflictreportrequest != null)
                {
                    OutputNegativeKeywordConflictReportRequest((NegativeKeywordConflictReportRequest)dataObject);
                }
                var productdimensionperformancereportrequest = dataObject as ProductDimensionPerformanceReportRequest;
                if(productdimensionperformancereportrequest != null)
                {
                    OutputProductDimensionPerformanceReportRequest((ProductDimensionPerformanceReportRequest)dataObject);
                }
                var productmatchcountreportrequest = dataObject as ProductMatchCountReportRequest;
                if(productmatchcountreportrequest != null)
                {
                    OutputProductMatchCountReportRequest((ProductMatchCountReportRequest)dataObject);
                }
                var productpartitionperformancereportrequest = dataObject as ProductPartitionPerformanceReportRequest;
                if(productpartitionperformancereportrequest != null)
                {
                    OutputProductPartitionPerformanceReportRequest((ProductPartitionPerformanceReportRequest)dataObject);
                }
                var productpartitionunitperformancereportrequest = dataObject as ProductPartitionUnitPerformanceReportRequest;
                if(productpartitionunitperformancereportrequest != null)
                {
                    OutputProductPartitionUnitPerformanceReportRequest((ProductPartitionUnitPerformanceReportRequest)dataObject);
                }
                var productsearchqueryperformancereportrequest = dataObject as ProductSearchQueryPerformanceReportRequest;
                if(productsearchqueryperformancereportrequest != null)
                {
                    OutputProductSearchQueryPerformanceReportRequest((ProductSearchQueryPerformanceReportRequest)dataObject);
                }
                var professionaldemographicsaudiencereportrequest = dataObject as ProfessionalDemographicsAudienceReportRequest;
                if(professionaldemographicsaudiencereportrequest != null)
                {
                    OutputProfessionalDemographicsAudienceReportRequest((ProfessionalDemographicsAudienceReportRequest)dataObject);
                }
                var publisherusageperformancereportrequest = dataObject as PublisherUsagePerformanceReportRequest;
                if(publisherusageperformancereportrequest != null)
                {
                    OutputPublisherUsagePerformanceReportRequest((PublisherUsagePerformanceReportRequest)dataObject);
                }
                var searchcampaignchangehistoryreportrequest = dataObject as SearchCampaignChangeHistoryReportRequest;
                if(searchcampaignchangehistoryreportrequest != null)
                {
                    OutputSearchCampaignChangeHistoryReportRequest((SearchCampaignChangeHistoryReportRequest)dataObject);
                }
                var searchqueryperformancereportrequest = dataObject as SearchQueryPerformanceReportRequest;
                if(searchqueryperformancereportrequest != null)
                {
                    OutputSearchQueryPerformanceReportRequest((SearchQueryPerformanceReportRequest)dataObject);
                }
                var shareofvoicereportrequest = dataObject as ShareOfVoiceReportRequest;
                if(shareofvoicereportrequest != null)
                {
                    OutputShareOfVoiceReportRequest((ShareOfVoiceReportRequest)dataObject);
                }
                var userlocationperformancereportrequest = dataObject as UserLocationPerformanceReportRequest;
                if(userlocationperformancereportrequest != null)
                {
                    OutputUserLocationPerformanceReportRequest((UserLocationPerformanceReportRequest)dataObject);
                }
            }
        }
        public void OutputArrayOfReportRequest(IList<ReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputReportRequestStatus(ReportRequestStatus dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("ReportDownloadUrl: {0}", dataObject.ReportDownloadUrl));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
            }
        }
        public void OutputArrayOfReportRequestStatus(IList<ReportRequestStatus> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputReportRequestStatus(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputReportTime(ReportTime dataObject)
        {
            if (null != dataObject)
            {
                OutputDate(dataObject.CustomDateRangeEnd);
                OutputDate(dataObject.CustomDateRangeStart);
                OutputStatusMessage(string.Format("PredefinedTime: {0}", dataObject.PredefinedTime));
                OutputStatusMessage(string.Format("ReportTimeZone: {0}", dataObject.ReportTimeZone));
            }
        }
        public void OutputArrayOfReportTime(IList<ReportTime> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputReportTime(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSearchCampaignChangeHistoryReportFilter(SearchCampaignChangeHistoryReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("HowChanged: {0}", dataObject.HowChanged));
                OutputStatusMessage(string.Format("ItemChanged: {0}", dataObject.ItemChanged));
            }
        }
        public void OutputArrayOfSearchCampaignChangeHistoryReportFilter(IList<SearchCampaignChangeHistoryReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSearchCampaignChangeHistoryReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSearchCampaignChangeHistoryReportRequest(SearchCampaignChangeHistoryReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfSearchCampaignChangeHistoryReportColumn(dataObject.Columns);
                OutputSearchCampaignChangeHistoryReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfSearchCampaignChangeHistoryReportRequest(IList<SearchCampaignChangeHistoryReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSearchCampaignChangeHistoryReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSearchQueryPerformanceReportFilter(SearchQueryPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("AdStatus: {0}", dataObject.AdStatus));
                OutputStatusMessage(string.Format("AdType: {0}", dataObject.AdType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeliveredMatchType: {0}", dataObject.DeliveredMatchType));
                OutputStatusMessage(string.Format("ExcludeZeroClicks: {0}", dataObject.ExcludeZeroClicks));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputArrayOfString(dataObject.LanguageCode);
                OutputArrayOfString(dataObject.SearchQueries);
            }
        }
        public void OutputArrayOfSearchQueryPerformanceReportFilter(IList<SearchQueryPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSearchQueryPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputSearchQueryPerformanceReportRequest(SearchQueryPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfSearchQueryPerformanceReportColumn(dataObject.Columns);
                OutputSearchQueryPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfSearchQueryPerformanceReportRequest(IList<SearchQueryPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputSearchQueryPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputShareOfVoiceReportFilter(ShareOfVoiceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountStatus: {0}", dataObject.AccountStatus));
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputStatusMessage(string.Format("AdGroupStatus: {0}", dataObject.AdGroupStatus));
                OutputStatusMessage(string.Format("BidMatchType: {0}", dataObject.BidMatchType));
                OutputStatusMessage(string.Format("BidStrategyType: {0}", dataObject.BidStrategyType));
                OutputStatusMessage(string.Format("CampaignStatus: {0}", dataObject.CampaignStatus));
                OutputStatusMessage(string.Format("DeliveredMatchType: {0}", dataObject.DeliveredMatchType));
                OutputStatusMessage(string.Format("DeviceType: {0}", dataObject.DeviceType));
                OutputStatusMessage(string.Format("KeywordStatus: {0}", dataObject.KeywordStatus));
                OutputArrayOfString(dataObject.Keywords);
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfShareOfVoiceReportFilter(IList<ShareOfVoiceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputShareOfVoiceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputShareOfVoiceReportRequest(ShareOfVoiceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfShareOfVoiceReportColumn(dataObject.Columns);
                OutputShareOfVoiceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfShareOfVoiceReportRequest(IList<ShareOfVoiceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputShareOfVoiceReportRequest(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputUserLocationPerformanceReportFilter(UserLocationPerformanceReportFilter dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AdDistribution: {0}", dataObject.AdDistribution));
                OutputArrayOfString(dataObject.CountryCode);
                OutputArrayOfString(dataObject.LanguageCode);
            }
        }
        public void OutputArrayOfUserLocationPerformanceReportFilter(IList<UserLocationPerformanceReportFilter> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputUserLocationPerformanceReportFilter(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputUserLocationPerformanceReportRequest(UserLocationPerformanceReportRequest dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Aggregation: {0}", dataObject.Aggregation));
                OutputArrayOfUserLocationPerformanceReportColumn(dataObject.Columns);
                OutputUserLocationPerformanceReportFilter(dataObject.Filter);
                OutputAccountThroughAdGroupReportScope(dataObject.Scope);
                OutputReportTime(dataObject.Time);
            }
        }
        public void OutputArrayOfUserLocationPerformanceReportRequest(IList<UserLocationPerformanceReportRequest> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputUserLocationPerformanceReportRequest(dataObject);
                    OutputStatusMessage("\n");
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
        public void OutputReportLanguage(ReportLanguage valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(ReportLanguage)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfReportLanguage(IList<ReportLanguage> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputReportLanguage(valueSet);
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
        public void OutputAgeGenderDemographicReportColumn(AgeGenderDemographicReportColumn valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(AgeGenderDemographicReportColumn)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfAgeGenderDemographicReportColumn(IList<AgeGenderDemographicReportColumn> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputAgeGenderDemographicReportColumn(valueSet);
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
                    OutputStatusMessage(string.Format("Value of the string: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the long: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable long: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the int: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable int: {0}", item));
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
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.BingAds.V11.Bulk;
using Microsoft.BingAds.V11.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;
using System.Threading.Tasks;
using Microsoft.BingAds.V11.Internal.Bulk.Entities;
using Microsoft.BingAds.V11.Internal.Bulk.Entities.AdExtensions;

namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// Provides default file paths and custom example implementations for a subset of BulkServiceManager and BulkOperation methods. 
    /// You can use the methods in this class or call the BulkServiceManager and BulkOperation methods directly in your application.
    /// </summary>
    public abstract class BulkExampleBase : ExampleBase
    {
        /// <summary>
        /// Provides methods for downloading and uploading bulk files. 
        /// </summary>
        protected static BulkServiceManager BulkServiceManager;

        /// <summary>
        /// Provides methods to write a bulk entity to file. 
        /// </summary>
        protected static BulkFileWriter Writer;

        /// <summary>
        /// Provides methods to read bulk entities from a file. 
        /// </summary>
        protected static BulkFileReader Reader;

        /// <summary>
        /// The directory for the bulk files.
        /// </summary>
        protected const string FileDirectory = @"c:\bulk\";

        /// <summary>
        /// The name of the bulk download file.
        /// </summary>
        protected const string DownloadFileName = @"download.csv";

        /// <summary>
        /// The name of the bulk upload file.
        /// </summary>
        protected const string UploadFileName = @"upload.csv";

        /// <summary>
        /// The name of the bulk upload result file.
        /// </summary>
        protected const string ResultFileName = @"result.csv";

        /// <summary>
        /// The bulk file extension type.
        /// </summary>
        protected const DownloadFileType FileType = DownloadFileType.Csv;

        /// <summary>
        /// The maximum amount of time (in milliseconds) that you want to wait for the bulk download or upload.
        /// </summary>
        protected const int TimeoutInMilliseconds = 36000000;

        protected long accountIdKey = -1;
        protected long tagIdKey = -2;
        protected long appAdExtensionIdKey = -11;
        protected long callAdExtensionIdKey = -12;
        protected long calloutAdExtensionIdKey = -13;
        protected long imageAdExtensionIdKey = -14;
        protected long locationAdExtensionIdKey = -15;
        protected long priceAdExtensionIdKey = -24;
        protected long reviewAdExtensionIdKey = -16;
        protected long siteLinksAdExtensionIdKey = -17;
        protected long sitelink2AdExtensionIdKey = -17;
        protected long structuredSnippetAdExtensionIdKey = -18;
        protected long negativeKeywordListIdKey = -19;
        protected long budgetIdKey = -20;
        protected long remarketingListIdKey = -21;
        protected long labelIdKey = -22;
        protected long campaignIdKey = -111;
        protected long searchAndContentCampaignIdKey = -112;
        protected long shoppingCampaignIdKey = -113;
        protected long dynamicSearchAdsCampaignIdKey = -114;
        protected long adGroupIdKey = -1111;
        protected long negativeKeywordIdKey = -11111;
        protected long keywordIdKey = -11112;
        protected long adIdKey = -11113;
        protected long appInstallAdIdKey = -11114;
        protected long dynamicSearchAdIdKey = -11115;
        protected long expandedTextAdIdKey = -11116;
        protected long productAdIdKey = -11117;
        protected long textAdIdKey = -11118;

        protected CampaignManagementExampleHelper CampaignManagementExampleHelper;
        
        /// <summary>
        /// Writes the specified entities to a local file and uploads the file. We could have uploaded directly
        /// without writing to file. This example writes to file as an exercise so that you can view the structure 
        /// of the bulk records being uploaded as needed. 
        /// </summary>
        /// <param name="uploadEntities"></param>
        /// <returns></returns>
        protected async Task<BulkFileReader> WriteEntitiesAndUploadFileAsync(IEnumerable<BulkEntity> uploadEntities)
        {
            Writer = new BulkFileWriter(FileDirectory + UploadFileName);

            foreach (var entity in uploadEntities)
            {
                Writer.WriteEntity(entity);
            }

            Writer.Dispose();

            var fileUploadParameters = new FileUploadParameters
            {
                ResultFileDirectory = FileDirectory,
                CompressUploadFile = true,
                ResultFileName = ResultFileName,
                OverwriteResultFile = true,
                UploadFilePath = FileDirectory + UploadFileName,
                ResponseMode = ResponseMode.ErrorsAndResults
            };

            var bulkFilePath = await BulkServiceManager.UploadFileAsync(fileUploadParameters);

            return new BulkFileReader(bulkFilePath, ResultFileType.Upload, FileType);
        }

        /// <summary>
        /// Outputs the list of BulkAccount
        /// </summary>
        protected void OutputBulkAccounts(IEnumerable<BulkAccount> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAccount: \n");
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("CustomerId: {0}", entity.CustomerId));
                OutputStatusMessage(string.Format("Id: {0}", entity.Id));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("SyncTime: {0}", entity.SyncTime));
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroup.
        /// </summary>
        protected void OutputBulkAdGroups(IEnumerable<BulkAdGroup> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroup: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("IsExpired: {0}", entity.IsExpired));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);
                OutputBulkQualityScoreData(entity.QualityScoreData);

                // Output the Campaign Management AdGroup Object
                CampaignManagementExampleHelper.OutputAdGroup(entity.AdGroup);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupAgeCriterion.
        /// </summary>
        protected void OutputBulkAdGroupAgeCriterions(IEnumerable<BulkAdGroupAgeCriterion> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                if (entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupAgeCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management BiddableAdGroupCriterion Object
                    CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

                    if (entity.HasErrors)
                    {
                        OutputBulkErrors(entity.Errors);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupDayTimeCriterion.
        /// </summary>
        protected void OutputBulkAdGroupDayTimeCriterions(IEnumerable<BulkAdGroupDayTimeCriterion> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                if (entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupDayTimeCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management BiddableAdGroupCriterion Object
                    CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

                    if (entity.HasErrors)
                    {
                        OutputBulkErrors(entity.Errors);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupDeviceCriterion.
        /// </summary>
        protected void OutputBulkAdGroupDeviceCriterions(IEnumerable<BulkAdGroupDeviceCriterion> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                if (entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupDeviceCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management BiddableAdGroupCriterion Object
                    CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

                    if (entity.HasErrors)
                    {
                        OutputBulkErrors(entity.Errors);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupGenderCriterion.
        /// </summary>
        protected void OutputBulkAdGroupGenderCriterions(IEnumerable<BulkAdGroupGenderCriterion> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                if (entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupGenderCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management BiddableAdGroupCriterion Object
                    CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

                    if (entity.HasErrors)
                    {
                        OutputBulkErrors(entity.Errors);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupLocationCriterion.
        /// </summary>
        protected void OutputBulkAdGroupLocationCriterions(IEnumerable<BulkAdGroupLocationCriterion> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                if (entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupLocationCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management BiddableAdGroupCriterion Object
                    CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

                    if (entity.HasErrors)
                    {
                        OutputBulkErrors(entity.Errors);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupLocationIntentCriterion.
        /// </summary>
        protected void OutputBulkAdGroupLocationIntentCriterions(IEnumerable<BulkAdGroupLocationIntentCriterion> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                if (entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupLocationIntentCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management BiddableAdGroupCriterion Object
                    CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

                    if (entity.HasErrors)
                    {
                        OutputBulkErrors(entity.Errors);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeLocationCriterion.
        /// </summary>
        protected void OutputBulkAdGroupNegativeLocationCriterions(IEnumerable<BulkAdGroupNegativeLocationCriterion> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                if (entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupNegativeLocationCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management NegativeAdGroupCriterion Object
                    CampaignManagementExampleHelper.OutputNegativeAdGroupCriterion(entity.NegativeAdGroupCriterion);

                    if (entity.HasErrors)
                    {
                        OutputBulkErrors(entity.Errors);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupRadiusCriterion.
        /// </summary>
        protected void OutputBulkAdGroupLocationIntentCriterions(IEnumerable<BulkAdGroupRadiusCriterion> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                if (entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupRadiusCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management BiddableAdGroupCriterion Object
                    CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

                    if (entity.HasErrors)
                    {
                        OutputBulkErrors(entity.Errors);
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupAppAdExtension.
        /// </summary>
        protected void OutputBulkAdGroupAppAdExtensions(IEnumerable<BulkAdGroupAppAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupAppAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupImageAdExtension.
        /// </summary>
        protected void OutputBulkAdGroupImageAdExtensions(IEnumerable<BulkAdGroupImageAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupImageAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeKeyword.
        /// </summary>
        protected void OutputBulkAdGroupNegativeKeywords(IEnumerable<BulkAdGroupNegativeKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupNegativeKeyword: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.NegativeKeyword != null)
                {
                    // Output the Campaign Management NegativeKeyword Object
                    CampaignManagementExampleHelper.OutputNegativeKeyword(entity.NegativeKeyword);
                }
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeSites.
        /// </summary>
        protected void OutputBulkAdGroupNegativeSites(IEnumerable<BulkAdGroupNegativeSites> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupNegativeSites object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupNegativeSites.NegativeSites objects. For example if you want to print the 
                // BulkAdGroupNegativeSites prior to upload.
                if (entity.NegativeSites.Count == 0)
                {
                    OutputStatusMessage("\nBulkAdGroupNegativeSites: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    CampaignManagementExampleHelper.OutputAdGroupNegativeSites(entity.AdGroupNegativeSites);
                }
                else
                {
                    OutputBulkAdGroupNegativeSite(entity.NegativeSites);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeSite.
        /// </summary>
        protected void OutputBulkAdGroupNegativeSite(IEnumerable<BulkAdGroupNegativeSite> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupNegativeSite: \n");
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("Website: {0}", entity.Website));

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupSiteLinkAdExtension.
        /// </summary>
        protected void OutputBulkAdGroupSiteLinkAdExtensions(IEnumerable<BulkAdGroupSiteLinkAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupSiteLinkAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAppAdExtension.
        /// </summary>
        protected void OutputBulkAppAdExtensions(IEnumerable<BulkAppAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAppAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management AppAdExtension Object
                CampaignManagementExampleHelper.OutputAppAdExtension(entity.AppAdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCallAdExtension.
        /// </summary>
        protected void OutputBulkCallAdExtensions(IEnumerable<BulkCallAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCallAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management CallAdExtension Object
                CampaignManagementExampleHelper.OutputCallAdExtension(entity.CallAdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCalloutAdExtension.
        /// </summary>
        protected void OutputBulkCalloutAdExtensions(IEnumerable<BulkCalloutAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCalloutAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management CalloutAdExtension Object
                CampaignManagementExampleHelper.OutputCalloutAdExtension(entity.CalloutAdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkReviewAdExtension.
        /// </summary>
        protected void OutputBulkReviewAdExtensions(IEnumerable<BulkReviewAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkReviewAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management ReviewAdExtension Object
                CampaignManagementExampleHelper.OutputReviewAdExtension(entity.ReviewAdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }



        /// <summary>
        /// Outputs the list of BulkBudget.
        /// </summary>
        protected void OutputBulkBudgets(IEnumerable<BulkBudget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkBudget: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management Budget Object
                CampaignManagementExampleHelper.OutputBudget(entity.Budget);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaign.
        /// </summary>
        protected void OutputBulkCampaigns(IEnumerable<BulkCampaign> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaign: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);
                OutputBulkQualityScoreData(entity.QualityScoreData);

                // Output the Campaign Management Campaign Object
                CampaignManagementExampleHelper.OutputCampaign(entity.Campaign);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignAppAdExtension.
        /// </summary>
        protected void OutputBulkCampaignAppAdExtensions(IEnumerable<BulkCampaignAppAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignAppAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignCallAdExtension.
        /// </summary>
        protected void OutputBulkCampaignCallAdExtensions(IEnumerable<BulkCampaignCallAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignCallAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignCalloutAdExtension.
        /// </summary>
        protected void OutputBulkCampaignCalloutAdExtensions(IEnumerable<BulkCampaignCalloutAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignCalloutAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignReviewAdExtension.
        /// </summary>
        protected void OutputBulkCampaignReviewAdExtensions(IEnumerable<BulkCampaignReviewAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignReviewAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignImageAdExtension.
        /// </summary>
        protected void OutputBulkCampaignImageAdExtensions(IEnumerable<BulkCampaignImageAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignImageAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignLocationAdExtension.
        /// </summary>
        protected void OutputBulkCampaignLocationAdExtensions(IEnumerable<BulkCampaignLocationAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignLocationAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeKeyword.
        /// </summary>
        protected void OutputBulkCampaignNegativeKeywords(IEnumerable<BulkCampaignNegativeKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignNegativeKeyword: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.NegativeKeyword != null)
                {
                    // Output the Campaign Management NegativeKeyword Object
                    CampaignManagementExampleHelper.OutputNegativeKeyword(entity.NegativeKeyword);
                }
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeKeywordList.
        /// </summary>
        protected void OutputBulkCampaignNegativeKeywordLists(IEnumerable<BulkCampaignNegativeKeywordList> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignNegativeKeywordList: \n");
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.SharedEntityAssociation != null)
                {
                    OutputStatusMessage(string.Format("EntityId: {0}",
                        entity.SharedEntityAssociation.EntityId));
                    OutputStatusMessage(string.Format("EntityType: {0}",
                        entity.SharedEntityAssociation.EntityType));
                    OutputStatusMessage(string.Format("SharedEntityId: {0}",
                        entity.SharedEntityAssociation.SharedEntityId));
                    OutputStatusMessage(string.Format("SharedEntityType: {0}",
                        entity.SharedEntityAssociation.SharedEntityType));
                }

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeSites.
        /// </summary>
        protected void OutputBulkCampaignNegativeSites(IEnumerable<BulkCampaignNegativeSites> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignNegativeSites object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignNegativeSites.NegativeSites objects. For example if you want to print the 
                // BulkCampaignNegativeSites prior to upload.
                if (entity.NegativeSites.Count == 0)
                {
                    OutputStatusMessage("\nBulkCampaignNegativeSites: \n");
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    CampaignManagementExampleHelper.OutputCampaignNegativeSites(entity.CampaignNegativeSites);
                }
                else
                {
                    OutputBulkCampaignNegativeSite(entity.NegativeSites);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeSite.
        /// </summary>
        protected void OutputBulkCampaignNegativeSite(IEnumerable<BulkCampaignNegativeSite> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignNegativeSite: \n");
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("Website: {0}", entity.Website));

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignSiteLinkAdExtension.
        /// </summary>
        protected void OutputBulkCampaignSiteLinkAdExtensions(IEnumerable<BulkCampaignSiteLinkAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignSiteLinkAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Campaign Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupAdExtensionAssociation
        /// </summary>
        protected void OutputBulkAdGroupAdExtensionAssociation(IEnumerable<BulkAdGroupAdExtensionAssociation> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                var bulkAdGroupAppAdExtension = entity as BulkAdGroupAppAdExtension;
                if (bulkAdGroupAppAdExtension != null)
                    OutputStatusMessage("\nBulkAdGroupAppAdExtension:\n");
                var bulkAdGroupCalloutAdExtension = entity as BulkAdGroupCalloutAdExtension;
                if (bulkAdGroupCalloutAdExtension != null)
                    OutputStatusMessage("\nBulkAdGroupCalloutAdExtension:\n");
                var bulkAdGroupImageAdExtension = entity as BulkAdGroupImageAdExtension;
                if (bulkAdGroupImageAdExtension != null)
                    OutputStatusMessage("\nBulkAdGroupImageAdExtension:\n");
                var bulkAdGroupReviewAdExtension = entity as BulkAdGroupReviewAdExtension;
                if (bulkAdGroupReviewAdExtension != null)
                    OutputStatusMessage("\nBulkAdGroupReviewAdExtension:\n");
                var bulkAdGroupSiteLinkAdExtension = entity as BulkAdGroupSiteLinkAdExtension;
                if (bulkAdGroupSiteLinkAdExtension != null)
                    OutputStatusMessage("\nBulkAdGroupSiteLinkAdExtension:\n");
                var bulkAdGroupSitelink2AdExtension = entity as BulkAdGroupSitelink2AdExtension;
                if (bulkAdGroupSitelink2AdExtension != null)
                    OutputStatusMessage("\nBulkAdGroupSitelink2AdExtension:\n");
                var bulkAdGroupStructuredSnippetAdExtension = entity as BulkAdGroupStructuredSnippetAdExtension;
                if (bulkAdGroupStructuredSnippetAdExtension != null)
                    OutputStatusMessage("\nBulkAdGroupStructuredSnippetAdExtension:\n");

                // Output the properties specific to BulkAdGroupAdExtensionAssociation
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));

                // Output the properties shared by all BulkAdExtensionAssociation
                OutputBulkAdExtensionAssociation(entity);
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignAdExtensionAssociation
        /// </summary>
        protected void OutputBulkCampaignAdExtensionAssociations(IEnumerable<BulkCampaignAdExtensionAssociation> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                var bulkCampaignAppAdExtension = entity as BulkCampaignAppAdExtension;
                if (bulkCampaignAppAdExtension != null)
                    OutputStatusMessage("\nBulkCampaignAppAdExtension:\n");
                var bulkCampaignCallAdExtension = entity as BulkCampaignCallAdExtension;
                if (bulkCampaignCallAdExtension != null)
                    OutputStatusMessage("\nBulkCampaignCallAdExtension:\n");
                var bulkCampaignCalloutAdExtension = entity as BulkCampaignCalloutAdExtension;
                if (bulkCampaignCalloutAdExtension != null)
                    OutputStatusMessage("\nBulkCampaignCalloutAdExtension:\n");
                var bulkCampaignImageAdExtension = entity as BulkCampaignImageAdExtension;
                if (bulkCampaignImageAdExtension != null)
                    OutputStatusMessage("\nBulkCampaignImageAdExtension:\n");
                var bulkCampaignLocationAdExtension = entity as BulkCampaignLocationAdExtension;
                if (bulkCampaignLocationAdExtension != null)
                    OutputStatusMessage("\nBulkCampaignLocationAdExtension:\n");
                var bulkCampaignReviewAdExtension = entity as BulkCampaignReviewAdExtension;
                if (bulkCampaignReviewAdExtension != null)
                    OutputStatusMessage("\nBulkCampaignReviewAdExtension:\n");
                var bulkCampaignSiteLinkAdExtension = entity as BulkCampaignSiteLinkAdExtension;
                if (bulkCampaignSiteLinkAdExtension != null)
                    OutputStatusMessage("\nBulkCampaignSiteLinkAdExtension:\n");
                var bulkCampaignSitelink2AdExtension = entity as BulkCampaignSitelink2AdExtension;
                if (bulkCampaignSitelink2AdExtension != null)
                    OutputStatusMessage("\nBulkCampaignSitelink2AdExtension:\n");
                var bulkCampaignStructuredSnippetAdExtension = entity as BulkCampaignStructuredSnippetAdExtension;
                if (bulkCampaignStructuredSnippetAdExtension != null)
                    OutputStatusMessage("\nBulkCampaignStructuredSnippetAdExtension:\n");

                // Output the properties specific to BulkCampaignAdExtensionAssociation
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));

                // Output the properties shared by all BulkAdExtensionAssociation
                OutputBulkAdExtensionAssociation(entity);
            }
        }

        /// <summary>
        /// Outputs the properties shared by all BulkAdExtensionAssociation
        /// </summary>
        protected void OutputBulkAdExtensionAssociation(BulkAdExtensionAssociation entity)
        {
            if (entity.AdExtensionIdToEntityIdAssociation != null)
            {
                OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                OutputStatusMessage(string.Format("EntityId (Campaign Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
            }
            OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
            OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
            OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
            OutputStatusMessage(string.Format("Status: {0}", entity.Status));

            OutputBulkPerformanceData(entity.PerformanceData);

            if (entity.HasErrors)
            {
                OutputBulkErrors(entity.Errors);
            }
        }

        /// <summary>
        /// Outputs the list of BulkImageAdExtension.
        /// </summary>
        protected void OutputBulkImageAdExtensions(IEnumerable<BulkImageAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkImageAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management ImageAdExtension Object
                CampaignManagementExampleHelper.OutputImageAdExtension(entity.ImageAdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkKeyword.
        /// </summary>
        protected void OutputBulkKeywords(IEnumerable<BulkKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkKeyword: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);
                OutputBulkQualityScoreData(entity.QualityScoreData);
                OutputBulkBidSuggestions(entity.BidSuggestions);

                // Output the Campaign Management Keyword Object
                CampaignManagementExampleHelper.OutputKeyword(entity.Keyword);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }
        /// <summary>
        /// Outputs the list of BulkLocationAdExtension.
        /// </summary>
        protected void OutputBulkLocationAdExtensions(IEnumerable<BulkLocationAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkLocationAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management LocationAdExtension Object
                CampaignManagementExampleHelper.OutputLocationAdExtension(entity.LocationAdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkNegativeKeywordList.
        /// </summary>
        protected void OutputBulkNegativeKeywordLists(IEnumerable<BulkNegativeKeywordList> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkNegativeKeywordList: \n");
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                CampaignManagementExampleHelper.OutputNegativeKeywordList(entity.NegativeKeywordList);
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkPriceAdExtension.
        /// </summary>
        protected void OutputBulkPriceAdExtensions(IEnumerable<BulkPriceAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkPriceAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management PriceAdExtension Object
                CampaignManagementExampleHelper.OutputPriceAdExtension(entity.PriceAdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkProductAd.
        /// </summary>
        protected void OutputBulkProductAds(IEnumerable<BulkProductAd> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkProductAd: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);

                // Output the Campaign Management ProductAd Object
                CampaignManagementExampleHelper.OutputProductAd(entity.ProductAd);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignProductScope.
        /// </summary>
        protected void OutputBulkCampaignProductScopes(IEnumerable<BulkCampaignProductScope> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignProductScope: \n");
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management BiddableCampaignCriterion
                CampaignManagementExampleHelper.OutputBiddableCampaignCriterion(entity.BiddableCampaignCriterion);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }

                OutputStatusMessage("\n");
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupProductPartition.
        /// </summary>
        protected void OutputBulkAdGroupProductPartitions(IEnumerable<BulkAdGroupProductPartition> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupProductPartition: \n");
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // BulkAdGroupProductPartition can have either BiddableAdGroupCriterion or NegativeAdGroupCriterion

                var biddableAdGroupCriterion = entity.AdGroupCriterion as BiddableAdGroupCriterion;
                if (biddableAdGroupCriterion != null)
                {
                    // Output the Campaign Management BiddableAdGroupCriterion
                    CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(biddableAdGroupCriterion);

                }
                else
                {
                    var negativeAdGroupCriterion = entity.AdGroupCriterion as NegativeAdGroupCriterion;
                    if (negativeAdGroupCriterion != null)
                    {
                        // Output the Campaign Management NegativeAdGroupCriterion
                        CampaignManagementExampleHelper.OutputNegativeAdGroupCriterion(negativeAdGroupCriterion);
                    }
                }

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }

                OutputStatusMessage("\n");
            }
        }

        /// <summary>
        /// Outputs the list of BulkSharedNegativeKeyword.
        /// </summary>
        protected void OutputBulkSharedNegativeKeywords(IEnumerable<BulkSharedNegativeKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkSharedNegativeKeyword: \n");
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                CampaignManagementExampleHelper.OutputNegativeKeyword(entity.NegativeKeyword);
                OutputStatusMessage(string.Format("NegativeKeywordListId: {0}", entity.NegativeKeywordListId));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkSiteLinkAdExtension.
        /// </summary>
        protected void OutputBulkSiteLinkAdExtensions(IEnumerable<BulkSiteLinkAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkSiteLinkAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management SiteLinksAdExtension Object
                CampaignManagementExampleHelper.OutputSiteLinksAdExtension(entity.SiteLinksAdExtension);

                if (entity.SiteLinks != null && entity.SiteLinks.Count > 0)
                {
                    OutputBulkSiteLinks(entity.SiteLinks);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkSiteLink.
        /// </summary>
        protected void OutputBulkSiteLinks(IEnumerable<BulkSiteLink> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkSiteLink: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Order: {0}", entity.Order));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("Version: {0}", entity.Version));

                // Output the Campaign Management SiteLink Object
                CampaignManagementExampleHelper.OutputSiteLink(entity.SiteLink);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkSitelink2AdExtension.
        /// </summary>
        protected void OutputBulkSitelink2AdExtensions(IEnumerable<BulkSitelink2AdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkSitelink2AdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management Sitelink2AdExtension Object
                CampaignManagementExampleHelper.OutputSitelink2AdExtension(entity.Sitelink2AdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkStructuredSnippetAdExtension.
        /// </summary>
        protected void OutputBulkStructuredSnippetAdExtensions(IEnumerable<BulkStructuredSnippetAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkStructuredSnippetAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management StructuredSnippetAdExtension Object
                CampaignManagementExampleHelper.OutputStructuredSnippetAdExtension(entity.StructuredSnippetAdExtension);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkExpandedTextAd.
        /// </summary>
        protected void OutputBulkExpandedTextAds(IEnumerable<BulkExpandedTextAd> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkExpandedTextAd: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);

                // Output the Campaign Management ExpandedTextAd Object
                CampaignManagementExampleHelper.OutputExpandedTextAd(entity.ExpandedTextAd);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkTextAd.
        /// </summary>
        protected void OutputBulkTextAds(IEnumerable<BulkTextAd> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkTextAd: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);

                // Output the Campaign Management TextAd Object
                CampaignManagementExampleHelper.OutputTextAd(entity.TextAd);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkRemarketingList.
        /// </summary>
        protected void OutputBulkRemarketingLists(IEnumerable<BulkRemarketingList> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkRemarketingList: \n");
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management RemarketingList Object
                CampaignManagementExampleHelper.OutputRemarketingList(entity.RemarketingList);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupRemarketingListAssociation.
        /// </summary>
        protected void OutputBulkAdGroupRemarketingListAssociations(IEnumerable<BulkAdGroupRemarketingListAssociation> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupRemarketingList: \n");
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management BiddableAdGroupCriterion Object
                CampaignManagementExampleHelper.OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

                if (entity.HasErrors)
                {
                    OutputBulkErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the PerformanceData
        /// </summary>
        private void OutputBulkPerformanceData(PerformanceData performanceData)
        {
            if (performanceData != null)
            {
                OutputStatusMessage("PerformanceData: ");
                OutputStatusMessage(string.Format("AverageCostPerClick: {0}", performanceData.AverageCostPerClick));
                OutputStatusMessage(string.Format("AverageCostPerThousandImpressions: {0}", performanceData.AverageCostPerThousandImpressions));
                OutputStatusMessage(string.Format("AveragePosition: {0}", performanceData.AveragePosition));
                OutputStatusMessage(string.Format("Clicks: {0}", performanceData.Clicks));
                OutputStatusMessage(string.Format("ClickThroughRate: {0}", performanceData.ClickThroughRate));
                OutputStatusMessage(string.Format("Conversions: {0}", performanceData.Conversions));
                OutputStatusMessage(string.Format("CostPerConversion: {0}", performanceData.CostPerConversion));
                OutputStatusMessage(string.Format("Impressions: {0}", performanceData.Impressions));
                OutputStatusMessage(string.Format("Spend: {0}", performanceData.Spend));
            }
        }

        /// <summary>
        /// Outputs the PerformanceData
        /// </summary>
        private void OutputBulkQualityScoreData(QualityScoreData qualityScoreData)
        {
            if (qualityScoreData != null)
            {
                OutputStatusMessage("QualityScoreData: ");
                OutputStatusMessage(string.Format("KeywordRelevance: {0}", qualityScoreData.KeywordRelevance));
                OutputStatusMessage(string.Format("LandingPageRelevance: {0}", qualityScoreData.LandingPageRelevance));
                OutputStatusMessage(string.Format("LandingPageUserExperience: {0}", qualityScoreData.LandingPageUserExperience));
                OutputStatusMessage(string.Format("QualityScore: {0}", qualityScoreData.QualityScore));
            }
        }

        /// <summary>
        /// Outputs the BidSuggestionData
        /// </summary>
        private void OutputBulkBidSuggestions(BidSuggestionData bidSuggestions)
        {
            if (bidSuggestions != null)
            {
                OutputStatusMessage("BidSuggestions: ");
                OutputStatusMessage(string.Format("BestPosition: {0}", bidSuggestions.BestPosition));
                OutputStatusMessage(string.Format("MainLine: {0}", bidSuggestions.MainLine));
                OutputStatusMessage(string.Format("FirstPage: {0}", bidSuggestions.FirstPage));
            }
        }

        /// <summary>
        /// Outputs the list of BulkError.
        /// </summary>
        /// <param name="errors"></param>
        private void OutputBulkErrors(IEnumerable<BulkError> errors)
        {
            foreach (var error in errors)
            {
                OutputStatusMessage(string.Format("Error: {0}", error.Error));
                OutputStatusMessage(string.Format("Number: {0}", error.Number));
            }
        }

        #region BulkEntityExamples


        protected BulkEntity GetExampleBulkCampaign()
        {
            // Map properties in the Bulk file to the BulkAdGroup
            var bulkCampaign = new BulkCampaign
            {
                // 'Parent Id' column header in the Bulk file
                // This is not required for upload because the parent account identifier is set 
                // with the CustomerAccountId header element in the Bulk upload service request.
                // AccountId = OptionalAccountIdHere,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // Campaign object of the Campaign Management service.
                Campaign = new Campaign
                {
                    // 'Bid Strategy Type' column header in the Bulk file
                    BiddingScheme = new EnhancedCpcBiddingScheme(),
                    // 'Budget Id' column header in the Bulk file
                    BudgetId = null,
                    // 'Budget Type' column header in the Bulk file
                    BudgetType = BudgetLimitType.DailyBudgetStandard,
                    // 'Campaign Type' column header in the Bulk file
                    CampaignType = CampaignType.SearchAndContent,
                    // 'Budget' column header in the Bulk file
                    DailyBudget = 50,
                    // 'Description' column header in the Bulk file
                    Description = "Red shoes line.",
                    // 'Id' column header in the Bulk file
                    Id = campaignIdKey,
                    // 'Language' column header in the Bulk file
                    Languages = null,
                    // 'Name' column header in the Bulk file
                    Name = "Women's Shoes " + DateTime.UtcNow,
                    // 'Bid Adjustment' column header in the Bulk file
                    NativeBidAdjustment = null,
                    // 'Status' column header in the Bulk file
                    Status = CampaignStatus.Paused,
                    // 'Time Zone' column header in the Bulk file
                    TimeZone = "PacificTimeUSCanadaTijuana",
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate =
                        "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}",
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                }
            };

            return bulkCampaign;
        }

        protected BulkEntity GetExampleBulkAdGroup()
        {
            // Map properties in the Bulk file to the BulkAdGroup
            var bulkAdGroup = new BulkAdGroup
            {
                // 'Campaign' column header in the Bulk file
                CampaignName = "ParentCampaignNameGoesHere",
                // 'Parent Id' column header in the Bulk file
                CampaignId = campaignIdKey,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // AdGroup object of the Campaign Management service.
                AdGroup = new AdGroup
                {
                    // 'Search Network' or 'Content Network' column header in the Bulk file
                    AdDistribution = AdDistribution.Search,
                    // 'Ad Rotation' column header in the Bulk file
                    AdRotation = new AdRotation
                    {
                        Type = AdRotationType.RotateAdsEvenly
                    },
                    // 'Bid Adjustment' column header in the Bulk file
                    NativeBidAdjustment = 10,
                    // 'Bid Strategy Type' column header in the Bulk file
                    BiddingScheme = new ManualCpcBiddingScheme { },
                    // 'Content Bid' column header in the Bulk file
                    ContentMatchBid = null,
                    // 'End Date' column header in the Bulk file
                    EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                    {
                        Month = 12,
                        Day = 31,
                        Year = DateTime.UtcNow.Year + 1
                    },
                    // 'Id' column header in the Bulk file
                    Id = adGroupIdKey,
                    // 'Language' column header in the Bulk file
                    Language = "English",
                    // 'Ad Group' column header in the Bulk file
                    Name = "Women's Red Shoe Sale",
                    // 'Network Distribution' column header in the Bulk file
                    Network = Network.OwnedAndOperatedAndSyndicatedSearch,
                    // 'Remarketing Targeting Setting' column header in the Bulk file
                    RemarketingTargetingSetting = RemarketingTargetingSetting.TargetAndBid,
                    // 'Search Bid' column header in the Bulk file
                    SearchBid = new Bid
                    {
                        Amount = 0.10
                    },
                    // 'Start Date' column header in the Bulk file
                    StartDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                    {
                        Month = DateTime.UtcNow.Month,
                        Day = DateTime.UtcNow.Day,
                        Year = DateTime.UtcNow.Year
                    },
                    // 'Status' column header in the Bulk file
                    Status = AdGroupStatus.Active,
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate =
                        "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}",
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
            };

            return bulkAdGroup;
        }

        protected BulkEntity GetExampleBulkCampaignAgeCriterion()
        {
            // Map properties in the Bulk file to the BulkCampaignAgeCriterion
            var bulkCampaignAgeCriterion = new BulkCampaignAgeCriterion
            {
                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableCampaignCriterion object of the Campaign Management service.

                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,

                    Criterion = new AgeCriterion
                    {
                        // 'Target' column header in the Bulk file
                        AgeRange = AgeRange.EighteenToTwentyFour,
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = CampaignCriterionStatus.Active,
                }
            };

            return bulkCampaignAgeCriterion;
        }

        protected BulkEntity GetExampleBulkCampaignDayTimeCriterion()
        {
            // Map properties in the Bulk file to the BulkCampaignDayTimeCriterion
            var bulkCampaignDayTimeCriterion = new BulkCampaignDayTimeCriterion
            {
                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableCampaignCriterion object of the Campaign Management service.

                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,

                    Criterion = new DayTimeCriterion
                    {
                        // 'Target' column header in the Bulk file
                        Day = Day.Monday,

                        // 'From Hour' column header in the Bulk file
                        FromHour = 0,

                        // 'To Hour' column header in the Bulk file
                        ToHour = 4,

                        // 'From Minute' column header in the Bulk file
                        FromMinute = Minute.Zero,

                        // 'To Minute' column header in the Bulk file
                        ToMinute = Minute.Zero
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = CampaignCriterionStatus.Active,
                }
            };

            return bulkCampaignDayTimeCriterion;
        }

        protected List<BulkCampaignDeviceCriterion> GetExampleBulkCampaignDeviceCriterions()
        {
            var bulkCampaignDeviceCriterions = new[] {
                // Map properties in the Bulk file to the BulkCampaignDeviceCriterion
                new BulkCampaignDeviceCriterion
                {
                    // 'Campaign' column header in the Bulk file is read-only
                    CampaignName = null,

                    // 'Client Id' column header in the Bulk file
                    ClientId = "ClientIdGoesHere",
                    
                    // Map properties in the Bulk file to the 
                    // BiddableCampaignCriterion object of the Campaign Management service.

                    BiddableCampaignCriterion = new BiddableCampaignCriterion
                    {
                        // 'Parent Id' column header in the Bulk file
                        CampaignId = campaignIdKey,

                        Criterion = new DeviceCriterion
                        {
                            // 'Target' column header in the Bulk file
                            DeviceName = "Computers",
                        },

                        CriterionBid = new BidMultiplier
                        {
                            // 'Bid Adjustment' column header in the Bulk file
                            Multiplier = 20,
                        },

                        // 'Id' column header in the Bulk file
                        Id = null,

                        // 'Status' column header in the Bulk file
                        Status = CampaignCriterionStatus.Active,
                    }
                },
                new BulkCampaignDeviceCriterion
                {
                    ClientId = "ClientIdGoesHere",
                    BiddableCampaignCriterion = new BiddableCampaignCriterion
                    {
                        CampaignId = campaignIdKey,
                        Criterion = new DeviceCriterion
                        {
                            DeviceName = "Smartphones",
                        },
                        CriterionBid = new BidMultiplier
                        {
                            Multiplier = 0,
                        },
                    }
                },
                new BulkCampaignDeviceCriterion
                {
                    ClientId = "ClientIdGoesHere",
                    BiddableCampaignCriterion = new BiddableCampaignCriterion
                    {
                        CampaignId = campaignIdKey,
                        Criterion = new DeviceCriterion
                        {
                            DeviceName = "Tablets",
                        },
                        CriterionBid = new BidMultiplier
                        {
                            Multiplier = 0,
                        },
                    }
                },
            };

            return bulkCampaignDeviceCriterions.ToList();
        }

        protected BulkEntity GetExampleBulkCampaignGenderCriterion()
        {
            // Map properties in the Bulk file to the BulkCampaignGenderCriterion
            var bulkCampaignGenderCriterion = new BulkCampaignGenderCriterion
            {
                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableCampaignCriterion object of the Campaign Management service.

                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,

                    Criterion = new GenderCriterion
                    {
                        // 'Target' column header in the Bulk file
                        GenderType = GenderType.Female
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = CampaignCriterionStatus.Active,
                }
            };

            return bulkCampaignGenderCriterion;
        }

        protected BulkEntity GetExampleBulkCampaignLocationCriterion()
        {
            // Map properties in the Bulk file to the BulkCampaignLocationCriterion
            var bulkCampaignLocationCriterion = new BulkCampaignLocationCriterion
            {
                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableCampaignCriterion object of the Campaign Management service.

                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,

                    Criterion = new LocationCriterion
                    {
                        // 'Target' column header in the Bulk file
                        LocationId = 190,

                        // 'Sub Type' column header in the Bulk file is read-only
                        LocationType = "Country"
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = CampaignCriterionStatus.Active,
                }
            };

            return bulkCampaignLocationCriterion;
        }

        protected BulkEntity GetExampleBulkCampaignLocationIntentCriterion()
        {
            // Map properties in the Bulk file to the BulkCampaignLocationIntentCriterion
            var bulkCampaignLocationIntentCriterion = new BulkCampaignLocationIntentCriterion
            {
                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableCampaignCriterion object of the Campaign Management service.

                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,

                    Criterion = new LocationIntentCriterion
                    {
                        // 'Target' column header in the Bulk file
                        IntentOption = IntentOption.PeopleIn
                    },

                    CriterionBid = null,

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = CampaignCriterionStatus.Active,
                }
            };

            return bulkCampaignLocationIntentCriterion;
        }

        protected BulkEntity GetExampleBulkCampaignNegativeLocationCriterion()
        {
            // Map properties in the Bulk file to the BulkCampaignNegativeLocationCriterion
            var bulkCampaignNegativeLocationCriterion = new BulkCampaignNegativeLocationCriterion
            {
                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // NegativeCampaignCriterion object of the Campaign Management service.

                NegativeCampaignCriterion = new NegativeCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,

                    Criterion = new LocationCriterion
                    {
                        // 'Target' column header in the Bulk file
                        LocationId = 79381,

                        // 'Sub Type' column header in the Bulk file is read-only
                        LocationType = "PostalCode"
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = CampaignCriterionStatus.Active,
                }
            };

            return bulkCampaignNegativeLocationCriterion;
        }

        protected BulkEntity GetExampleBulkCampaignRadiusCriterion()
        {
            // Map properties in the Bulk file to the BulkCampaignRadiusCriterion
            var bulkCampaignRadiusCriterion = new BulkCampaignRadiusCriterion
            {
                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableCampaignCriterion object of the Campaign Management service.

                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,

                    Criterion = new RadiusCriterion
                    {
                        // 'Latitude' column header in the Bulk file
                        LatitudeDegrees = 10.5,

                        // 'Longitude' column header in the Bulk file
                        LongitudeDegrees = 40.5,

                        // 'Name' column header in the Bulk file
                        Name = "RadiusName",

                        // 'Radius' column header in the Bulk file
                        Radius = 10,

                        // 'Unit' column header in the Bulk file
                        RadiusUnit = DistanceUnit.Kilometers,
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = CampaignCriterionStatus.Active,
                }
            };

            return bulkCampaignRadiusCriterion;
        }

        protected BulkEntity GetExampleBulkAdGroupAgeCriterion()
        {
            // Map properties in the Bulk file to the BulkAdGroupAgeCriterion
            var bulkAdGroupAgeCriterion = new BulkAdGroupAgeCriterion
            {
                // 'Ad Group' column header in the Bulk file is read-only
                AdGroupName = null,

                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableAdGroupCriterion object of the Campaign Management service.

                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,

                    Criterion = new AgeCriterion
                    {
                        // 'Target' column header in the Bulk file
                        AgeRange = AgeRange.EighteenToTwentyFour,
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Active,
                }
            };

            return bulkAdGroupAgeCriterion;
        }

        protected BulkEntity GetExampleBulkAdGroupDayTimeCriterion()
        {
            // Map properties in the Bulk file to the BulkAdGroupDayTimeCriterion
            var bulkAdGroupDayTimeCriterion = new BulkAdGroupDayTimeCriterion
            {
                // 'Ad Group' column header in the Bulk file is read-only
                AdGroupName = null,

                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableAdGroupCriterion object of the Campaign Management service.

                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,

                    Criterion = new DayTimeCriterion
                    {
                        // 'Target' column header in the Bulk file
                        Day = Day.Monday,

                        // 'From Hour' column header in the Bulk file
                        FromHour = 0,

                        // 'To Hour' column header in the Bulk file
                        ToHour = 4,

                        // 'From Minute' column header in the Bulk file
                        FromMinute = Minute.Zero,

                        // 'To Minute' column header in the Bulk file
                        ToMinute = Minute.Zero
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Active,
                }
            };

            return bulkAdGroupDayTimeCriterion;
        }

        protected List<BulkAdGroupDeviceCriterion> GetExampleBulkAdGroupDeviceCriterions()
        {
            var bulkAdGroupDeviceCriterions = new[] {
                // Map properties in the Bulk file to the BulkAdGroupDeviceCriterion
                new BulkAdGroupDeviceCriterion
                {
                    // 'Ad Group' column header in the Bulk file is read-only
                    AdGroupName = null,
                    
                    // 'Campaign' column header in the Bulk file is read-only
                    CampaignName = null,

                    // 'Client Id' column header in the Bulk file
                    ClientId = "ClientIdGoesHere",

                    // Map properties in the Bulk file to the 
                    // BiddableAdGroupCriterion object of the Campaign Management service.

                    BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                    {
                        // 'Parent Id' column header in the Bulk file
                        AdGroupId = adGroupIdKey,

                        Criterion = new DeviceCriterion
                        {
                            // 'Target' column header in the Bulk file
                            DeviceName = "Computers",
                        },

                        CriterionBid = new BidMultiplier
                        {
                            // 'Bid Adjustment' column header in the Bulk file
                            Multiplier = 20,
                        },

                        // 'Id' column header in the Bulk file
                        Id = null,

                        // 'Status' column header in the Bulk file
                        Status = AdGroupCriterionStatus.Active,
                    }
                },
                new BulkAdGroupDeviceCriterion
                {
                    ClientId = "ClientIdGoesHere",
                    BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                    {
                        AdGroupId = adGroupIdKey,
                        Criterion = new DeviceCriterion
                        {
                            DeviceName = "Smartphones",
                        },
                        CriterionBid = new BidMultiplier
                        {
                            Multiplier = 0,
                        },
                    }
                },
                new BulkAdGroupDeviceCriterion
                {
                    ClientId = "ClientIdGoesHere",
                    BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                    {
                        AdGroupId = adGroupIdKey,
                        Criterion = new DeviceCriterion
                        {
                            DeviceName = "Tablets",
                        },
                        CriterionBid = new BidMultiplier
                        {
                            Multiplier = 0,
                        },
                    }
                },
            };

            return bulkAdGroupDeviceCriterions.ToList();
        }

        protected BulkEntity GetExampleBulkAdGroupGenderCriterion()
        {
            // Map properties in the Bulk file to the BulkAdGroupGenderCriterion
            var bulkAdGroupGenderCriterion = new BulkAdGroupGenderCriterion
            {
                // 'Ad Group' column header in the Bulk file is read-only
                AdGroupName = null,

                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableAdGroupCriterion object of the Campaign Management service.

                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,

                    Criterion = new GenderCriterion
                    {
                        // 'Target' column header in the Bulk file
                        GenderType = GenderType.Female
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Active,
                }
            };

            return bulkAdGroupGenderCriterion;
        }

        protected BulkEntity GetExampleBulkAdGroupLocationCriterion()
        {
            // Map properties in the Bulk file to the BulkAdGroupLocationCriterion
            var bulkAdGroupLocationCriterion = new BulkAdGroupLocationCriterion
            {
                // 'Ad Group' column header in the Bulk file is read-only
                AdGroupName = null,

                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableAdGroupCriterion object of the Campaign Management service.

                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,

                    Criterion = new LocationCriterion
                    {
                        // 'Target' column header in the Bulk file
                        LocationId = 190,

                        // 'Sub Type' column header in the Bulk file is read-only
                        LocationType = "Country"
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Active,
                }
            };

            return bulkAdGroupLocationCriterion;
        }

        protected BulkEntity GetExampleBulkAdGroupLocationIntentCriterion()
        {
            // Map properties in the Bulk file to the BulkAdGroupLocationIntentCriterion
            var bulkAdGroupLocationIntentCriterion = new BulkAdGroupLocationIntentCriterion
            {
                // 'Ad Group' column header in the Bulk file is read-only
                AdGroupName = null,

                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableAdGroupCriterion object of the Campaign Management service.

                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,

                    Criterion = new LocationIntentCriterion
                    {
                        // 'Target' column header in the Bulk file
                        IntentOption = IntentOption.PeopleIn
                    },

                    CriterionBid = null,

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Active,
                }
            };

            return bulkAdGroupLocationIntentCriterion;
        }

        protected BulkEntity GetExampleBulkAdGroupNegativeLocationCriterion()
        {
            // Map properties in the Bulk file to the BulkAdGroupNegativeLocationCriterion
            var bulkAdGroupNegativeLocationCriterion = new BulkAdGroupNegativeLocationCriterion
            {
                // 'Ad Group' column header in the Bulk file is read-only
                AdGroupName = null,

                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // NegativeAdGroupCriterion object of the Campaign Management service.

                NegativeAdGroupCriterion = new NegativeAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,

                    Criterion = new LocationCriterion
                    {
                        // 'Target' column header in the Bulk file
                        LocationId = 79381,

                        // 'Sub Type' column header in the Bulk file is read-only
                        LocationType = "PostalCode"
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Active,
                }
            };

            return bulkAdGroupNegativeLocationCriterion;
        }

        protected BulkEntity GetExampleBulkAdGroupRadiusCriterion()
        {
            // Map properties in the Bulk file to the BulkAdGroupRadiusCriterion
            var bulkAdGroupRadiusCriterion = new BulkAdGroupRadiusCriterion
            {
                // 'Ad Group' column header in the Bulk file is read-only
                AdGroupName = null,

                // 'Campaign' column header in the Bulk file is read-only
                CampaignName = null,

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableAdGroupCriterion object of the Campaign Management service.

                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,

                    Criterion = new RadiusCriterion
                    {
                        // 'Latitude' column header in the Bulk file
                        LatitudeDegrees = 10.5,

                        // 'Longitude' column header in the Bulk file
                        LongitudeDegrees = 40.5,

                        // 'Name' column header in the Bulk file
                        Name = "RadiusName",

                        // 'Radius' column header in the Bulk file
                        Radius = 10,

                        // 'Unit' column header in the Bulk file
                        RadiusUnit = DistanceUnit.Kilometers,
                    },

                    CriterionBid = new BidMultiplier
                    {
                        // 'Bid Adjustment' column header in the Bulk file
                        Multiplier = 20,
                    },

                    // 'Id' column header in the Bulk file
                    Id = null,

                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Active,
                }
            };

            return bulkAdGroupRadiusCriterion;
        }

        protected BulkEntity GetBulkAppAdExtension()
        {
            // Map properties in the Bulk file to the BulkAppAdExtension
            var bulkAppAdExtension = new BulkAppAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // AppAdExtension object of the Campaign Management service.
                AppAdExtension = new AppAdExtension
                {
                    // 'App Platform' column header in the Bulk file
                    AppPlatform = "Windows",
                    // 'App Id' column header in the Bulk file
                    AppStoreId = "AppStoreIdGoesHere",
                    // 'Destination Url' column header in the Bulk file
                    DestinationUrl = "DestinationUrlGoesHere",
                    // 'Text' column header in the Bulk file
                    DisplayText = "Contoso",
                    // 'Id' column header in the Bulk file
                    Id = appAdExtensionIdKey,
                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                },
            };

            return bulkAppAdExtension;
        }

        protected BulkEntity GetBulkCallAdExtension()
        {
            // Map properties in the Bulk file to the BulkCallAdExtension
            var bulkCallAdExtension = new BulkCallAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // CallAdExtension object of the Campaign Management service.
                CallAdExtension = new CallAdExtension
                {
                    // 'Country Code' column header in the Bulk file
                    CountryCode = "US",
                    // 'Id' column header in the Bulk file
                    Id = callAdExtensionIdKey,
                    // 'Call Only' column header in the Bulk file
                    IsCallOnly = true,
                    // 'Call Tracking Enabled' column header in the Bulk file
                    IsCallTrackingEnabled = true,
                    // 'Phone Number' column header in the Bulk file
                    PhoneNumber = "2065550100",
                    // 'Toll Free' column header in the Bulk file
                    RequireTollFreeTrackingNumber = false,

                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                },
            };

            return bulkCallAdExtension;
        }

        protected BulkEntity GetBulkCalloutAdExtension()
        {
            // Map properties in the Bulk file to the BulkCallAdExtension
            var bulkCalloutAdExtension = new BulkCalloutAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // CalloutAdExtension object of the Campaign Management service.
                CalloutAdExtension = new CalloutAdExtension
                {
                    // 'Id' column header in the Bulk file
                    Id = calloutAdExtensionIdKey,
                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                    // 'Callout Text' column header in the Bulk file
                    Text = "Callout Text",
                },
            };

            return bulkCalloutAdExtension;
        }

        protected BulkEntity GetBulkImageAdExtension()
        {
            // Map properties in the Bulk file to the BulkImageAdExtension
            var bulkImageAdExtension = new BulkImageAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // ImageAdExtension object of the Campaign Management service.
                ImageAdExtension = new ImageAdExtension
                {
                    // 'Alternative Text' column header in the Bulk file
                    AlternativeText = "ImageAdExtension Alternative Text",
                    // 'Destination Url' column header in the Bulk file
                    DestinationUrl = null,
                    // 'Id' column header in the Bulk file
                    Id = imageAdExtensionIdKey,
                    // 'Media Ids' column header in the Bulk file
                    ImageMediaIds = new long[] { /*ImageMediaIdGoesHere*/ },
                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                },
            };

            return bulkImageAdExtension;
        }

        protected BulkEntity GetBulkLocationAdExtension()
        {
            // Map properties in the Bulk file to the BulkLocationAdExtension
            var bulkLocationAdExtension = new BulkLocationAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // LocationAdExtension object of the Campaign Management service.
                LocationAdExtension = new LocationAdExtension
                {
                    Address = new Address
                    {
                        // 'City' column header in the Bulk file
                        CityName = "Woodinville",
                        // 'Country Code' column header in the Bulk file
                        CountryCode = "US",
                        // 'Postal Code' column header in the Bulk file
                        PostalCode = "98608",
                        // 'State Or Province Code' column header in the Bulk file
                        ProvinceCode = null,
                        // 'Province Name' column header in the Bulk file
                        ProvinceName = "WA",
                        // 'Address Line 1' column header in the Bulk file
                        StreetAddress = "1234 Washington Place",
                        // 'Address Line 2' column header in the Bulk file
                        StreetAddress2 = "Suite 1210",
                    },
                    // 'Business Name' column header in the Bulk file
                    CompanyName = "Contoso Shoes",
                    // 'Geo Code Status' column header in the Bulk file
                    GeoCodeStatus = null,
                    GeoPoint = new GeoPoint
                    {
                        // 'Latitude' column header in the Bulk file
                        LatitudeInMicroDegrees = 0,
                        // 'Longitude' column header in the Bulk file
                        LongitudeInMicroDegrees = 0,
                    },
                    // 'Map Icon' column header in the Bulk file
                    IconMediaId = null,
                    // 'Id' column header in the Bulk file
                    Id = locationAdExtensionIdKey,
                    // 'Media Ids' column header in the Bulk file
                    ImageMediaId = /*ImageMediaIdGoesHere*/null,
                    // 'Phone Number' column header in the Bulk file
                    PhoneNumber = "206-555-0100",

                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                },
            };

            return bulkLocationAdExtension;
        }


        protected BulkEntity GetBulkPriceAdExtension()
        {
            // Map properties in the Bulk file to the BulkPriceAdExtension
            var bulkPriceAdExtension = new BulkPriceAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // PriceAdExtension object of the Campaign Management service.
                PriceAdExtension = new PriceAdExtension
                {
                    // 'Language' column header in the Bulk file
                    Language = "English",
                    // 'Id' column header in the Bulk file
                    Id = priceAdExtensionIdKey,
                    // 'Price Extension Type' column header in the Bulk file
                    PriceExtensionType = PriceExtensionType.Events,

                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,

                    // TableRows must include between 3 and 8 PriceTableRow
                    TableRows = new PriceTableRow[]
                    {
                        // Each PriceTableRow is mapped to columns with suffix 1..8. 
                        // This example shows 3 price table rows i.e., with column suffix from 1..3
                        new PriceTableRow
                        {
                            // 'Currency Code 1' column header in the Bulk file
                            CurrencyCode = "USD",
                            // 'Price Description 1' column header in the Bulk file
                            Description = "Come to the new event",
                            // 'Final Url 1' column header in the Bulk file
                            FinalUrls = new[] {
                                // Each Url is delimited by a semicolon (;) in the Bulk file
                                "http://www.contoso.com/womenshoesale"
                            },
                            // 'Final Mobile Url 1' column header in the Bulk file
                            FinalMobileUrls = new[] {
                                // Each Url is delimited by a semicolon (;) in the Bulk file
                                "http://mobile.contoso.com/womenshoesale"
                            },
                            // 'Header 1' column header in the Bulk file
                            Header = "New Event",
                            // 'Price 1' column header in the Bulk file
                            Price = 9.99,
                            // 'Price Qualifier 1' column header in the Bulk file
                            PriceQualifier = PriceQualifier.From,
                            // 'Price Unit 1' column header in the Bulk file
                            PriceUnit = PriceUnit.PerDay,
                        },
                        new PriceTableRow
                        {
                            // 'Currency Code 2' column header in the Bulk file
                            CurrencyCode = "USD",
                            // 'Price Description 2' column header in the Bulk file
                            Description = "Come to the next event",
                            // 'Final Url 2' column header in the Bulk file
                            FinalUrls = new[] {
                                // Each Url is delimited by a semicolon (;) in the Bulk file
                                "http://www.contoso.com/womenshoesale"
                            },
                            // 'Final Mobile Url 2' column header in the Bulk file
                            FinalMobileUrls = new[] {
                                // Each Url is delimited by a semicolon (;) in the Bulk file
                                "http://mobile.contoso.com/womenshoesale"
                            },
                            // 'Header 2' column header in the Bulk file
                            Header = "Next Event",
                            // 'Price 2' column header in the Bulk file
                            Price = 9.99,
                            // 'Price Qualifier 2' column header in the Bulk file
                            PriceQualifier = PriceQualifier.From,
                            // 'Price Unit 2' column header in the Bulk file
                            PriceUnit = PriceUnit.PerDay,
                        },
                        new PriceTableRow
                        {
                            // 'Currency Code 3' column header in the Bulk file
                            CurrencyCode = "USD",
                            // 'Price Description 3' column header in the Bulk file
                            Description = "Come to the final event",
                            // 'Final Url 3' column header in the Bulk file
                            FinalUrls = new[] {
                                // Each Url is delimited by a semicolon (;) in the Bulk file
                                "http://www.contoso.com/womenshoesale"
                            },
                            // 'Final Mobile Url 3' column header in the Bulk file
                            FinalMobileUrls = new[] {
                                // Each Url is delimited by a semicolon (;) in the Bulk file
                                "http://mobile.contoso.com/womenshoesale"
                            },
                            // 'Header 3' column header in the Bulk file
                            Header = "Final Event",
                            // 'Price 3' column header in the Bulk file
                            Price = 9.99,
                            // 'Price Qualifier 3' column header in the Bulk file
                            PriceQualifier = PriceQualifier.From,
                            // 'Price Unit 3' column header in the Bulk file
                            PriceUnit = PriceUnit.PerDay,
                        },
                    },
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
            };

            return bulkPriceAdExtension;
        }

        protected BulkEntity GetBulkReviewAdExtension()
        {
            // Map properties in the Bulk file to the BulkReviewAdExtension
            var bulkReviewAdExtension = new BulkReviewAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // ReviewAdExtension object of the Campaign Management service.
                ReviewAdExtension = new ReviewAdExtension
                {
                    // 'Id' column header in the Bulk file
                    Id = reviewAdExtensionIdKey,
                    // 'Is Exact' column header in the Bulk file
                    IsExact = true,
                    // 'Source' column header in the Bulk file
                    Source = "Review Source Name",
                    // 'Text' column header in the Bulk file
                    Text = "Review Text",
                    // 'Url' column header in the Bulk file
                    Url = "http://review.contoso.com",

                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                },
            };

            return bulkReviewAdExtension;
        }

        protected BulkEntity GetBulkSiteLinkAdExtension()
        {
            // Map properties in the Bulk file to the BulkSiteLinkAdExtension
            var bulkSiteLinkAdExtension = new BulkSiteLinkAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,

                // Map properties in the Bulk file to the 
                // SiteLinksAdExtension object of the Campaign Management service.
                SiteLinksAdExtension = new SiteLinksAdExtension
                {
                    // 'Id' column header in the Bulk file
                    Id = siteLinksAdExtensionIdKey,
                    SiteLinks = new[]
                    {
                        // Map properties in the Bulk file to the 
                        // SiteLink object of the Campaign Management service.
                        new SiteLink
                        {
                            // 'Sitelink Extension Description1' column header in the Bulk file
                            Description1 = "Simple & Transparent.",
                            // 'Sitelink Extension Description2' column header in the Bulk file
                            Description2 = "No Upfront Cost.",
                            // 'Text' column header in the Bulk file
                            DisplayText = "Women's Shoe Sale",
                            // 'Destination Url' column header in the Bulk file
                            DestinationUrl = "",
                            // 'Final Url' column header in the Bulk file
                            FinalUrls = new[] {
                                // Each Url is delimited by a semicolon (;) in the Bulk file
                                "http://www.contoso.com/womenshoesale"
                            },
                            // 'Mobile Final Url' column header in the Bulk file
                            FinalMobileUrls = new[] {
                                // Each Url is delimited by a semicolon (;) in the Bulk file
                                "http://mobile.contoso.com/womenshoesale"
                            },
                            // 'Ad Schedule' column header in the Bulk file
                            Scheduling = new Schedule
                            {
                                // Each day and time range is delimited by a semicolon (;) in the Bulk file
                                DayTimeRanges = new[]
                                {
                                    // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                                    new DayTime
                                    {
                                        Day = Day.Monday,
                                        StartHour = 9,
                                        StartMinute = Minute.Zero,
                                        EndHour = 21,
                                        EndMinute = Minute.Zero,
                                    },
                                },
                                // 'End Date' column header in the Bulk file
                                EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                                {
                                    Month = 12,
                                    Day = 31,
                                    Year = DateTime.UtcNow.Year + 1
                                },
                                // 'Start Date' column header in the Bulk file
                                StartDate = null,
                                // 'Use Searcher Time Zone' column header in the Bulk file
                                UseSearcherTimeZone = false,
                            },
                            // 'Tracking Template' column header in the Bulk file
                            TrackingUrlTemplate = null,
                            // 'Custom Parameter' column header in the Bulk file
                            UrlCustomParameters = new CustomParameters
                            {
                                // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                                Parameters = new[] {
                                    new CustomParameter(){
                                        Key = "promoCode",
                                        Value = "PROMO1"
                                    },
                                    new CustomParameter(){
                                        Key = "season",
                                        Value = "summer"
                                    },
                                }
                            },
                        }
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                },
            };

            return bulkSiteLinkAdExtension;
        }

        protected BulkEntity GetBulkSiteLink()
        {
            // Map properties in the Bulk file to the BulkSiteLink
            var bulkSiteLink = new BulkSiteLink
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Id' column header in the Bulk file
                AdExtensionId = siteLinksAdExtensionIdKey,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // SiteLink object of the Campaign Management service.
                SiteLink = new SiteLink
                {
                    // 'Sitelink Extension Description1' column header in the Bulk file
                    Description1 = "Simple & Transparent.",
                    // 'Sitelink Extension Description2' column header in the Bulk file
                    Description2 = "No Upfront Cost.",
                    // 'Text' column header in the Bulk file
                    DisplayText = "Women's Shoe Sale",
                    // 'Destination Url' column header in the Bulk file
                    DestinationUrl = "",
                    // 'Final Url' column header in the Bulk file
                    FinalUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://www.contoso.com/womenshoesale"
                    },
                    // 'Mobile Final Url' column header in the Bulk file
                    FinalMobileUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://mobile.contoso.com/womenshoesale"
                    },
                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },

                // 'Status' column header in the Bulk file
                Status = AdExtensionStatus.Active,
            };

            return bulkSiteLink;
        }

        protected BulkEntity GetBulkSitelink2AdExtension()
        {
            // Map properties in the Bulk file to the BulkSitelink2AdExtension
            var bulkSitelink2AdExtension = new BulkSitelink2AdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // Sitelink2AdExtension object of the Campaign Management service.
                Sitelink2AdExtension = new Sitelink2AdExtension
                {
                    // 'Id' column header in the Bulk file
                    Id = sitelink2AdExtensionIdKey,
                    // 'Sitelink Extension Description1' column header in the Bulk file
                    Description1 = "Simple & Transparent.",
                    // 'Sitelink Extension Description2' column header in the Bulk file
                    Description2 = "No Upfront Cost.",
                    // 'Text' column header in the Bulk file
                    DisplayText = "Women's Shoe Sale",
                    // 'Destination Url' column header in the Bulk file
                    DestinationUrl = "",
                    // 'Final Url' column header in the Bulk file
                    FinalUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://www.contoso.com/womenshoesale"
                    },
                    // 'Mobile Final Url' column header in the Bulk file
                    FinalMobileUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://mobile.contoso.com/womenshoesale"
                    },
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },

                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                },
            };

            return bulkSitelink2AdExtension;
        }

        protected BulkEntity GetBulkStructuredSnippetAdExtension()
        {
            // Map properties in the Bulk file to the BulkStructuredSnippetAdExtension
            var bulkStructuredSnippetAdExtension = new BulkStructuredSnippetAdExtension
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // StructuredSnippetAdExtension object of the Campaign Management service.
                StructuredSnippetAdExtension = new StructuredSnippetAdExtension
                {
                    // 'Header' column header in the Bulk file
                    Header = "Brands",
                    // 'Id' column header in the Bulk file
                    Id = structuredSnippetAdExtensionIdKey,

                    // 'Ad Schedule' column header in the Bulk file
                    Scheduling = new Schedule
                    {
                        // Each day and time range is delimited by a semicolon (;) in the Bulk file
                        DayTimeRanges = new[]
                        {
                            // Within each day and time range the format is Day[StartHour:StartMinue-EndHour:EndMinute].
                            new DayTime
                            {
                                Day = Day.Monday,
                                StartHour = 9,
                                StartMinute = Minute.Zero,
                                EndHour = 21,
                                EndMinute = Minute.Zero,
                            },
                        },
                        // 'End Date' column header in the Bulk file
                        EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        // 'Start Date' column header in the Bulk file
                        StartDate = null,
                        // 'Use Searcher Time Zone' column header in the Bulk file
                        UseSearcherTimeZone = false,
                    },

                    // 'Status' column header in the Bulk file
                    Status = AdExtensionStatus.Active,
                    // 'Structured Snippet Values' column header in the Bulk file
                    // Each value is delimited by a semicolon (;) in the Bulk file
                    Values = new[] { "Windows", "Xbox", "Skype" },

                },
            };

            return bulkStructuredSnippetAdExtension;
        }

        protected BulkEntity GetBulkBudget()
        {
            // Map properties in the Bulk file to the BulkBudget
            var bulkBudget = new BulkBudget
            {
                // 'Parent Id' column header in the Bulk file
                AccountId = 0,

                // Map properties in the Bulk file to the 
                // Budget object of the Campaign Management service.
                Budget = new Budget
                {
                    // 'Budget' column header in the Bulk file
                    Amount = 50,
                    // 'Budget Type' column header in the Bulk file
                    BudgetType = BudgetLimitType.DailyBudgetAccelerated,
                    // 'Budget Name' column header in the Bulk file
                    Name = "My Shared Budget " + DateTime.UtcNow,
                    // 'Id' column header in the Bulk file
                    Id = budgetIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkBudget;
        }

        protected BulkEntity GetBulkRemarketingList()
        {
            // Map properties in the Bulk file to the BulkRemarketingList
            var bulkRemarketingList = new BulkRemarketingList
            {
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // RemarketingList object of the Campaign Management service.
                RemarketingList = new RemarketingList
                {
                    // 'Description' column header in the Bulk file
                    Description = "New list with CustomEventsRule",
                    // 'Id' column header in the Bulk file
                    Id = remarketingListIdKey,
                    // 'Membership Duration' column header in the Bulk file
                    MembershipDuration = 30,
                    // 'Audience' column header in the Bulk file
                    Name = "Remarketing List with CustomEventsRule " + DateTime.UtcNow,
                    // 'Parent Id' column header in the Bulk file
                    ParentId = accountIdKey,
                    // 'Remarketing Rule' column header in the Bulk file
                    Rule = new CustomEventsRule
                    {
                        // The rule definition is translated to the following logical expression: 
                        // CustomEvents(Category Equals video) and (Action Equals play) and (Label Equals trailer) 
                        // and (Value Equals 5)
                        Action = "play",
                        ActionOperator = StringOperator.Equals,
                        Category = "video",
                        CategoryOperator = StringOperator.Equals,
                        Label = "trailer",
                        LabelOperator = StringOperator.Equals,
                        Value = 5.00m,
                        ValueOperator = NumberOperator.Equals,
                    },
                    // 'Scope' column header in the Bulk file
                    Scope = EntityScope.Account,
                    // 'UET Tag Id' column header in the Bulk file
                    TagId = tagIdKey
                },

                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkRemarketingList;
        }

        protected BulkEntity GetBulkAdGroup()
        {
            // Map properties in the Bulk file to the BulkAdGroup
            var bulkAdGroup = new BulkAdGroup
            {
                // 'Campaign' column header in the Bulk file
                CampaignName = "ParentCampaignNameGoesHere",
                // 'Parent Id' column header in the Bulk file
                CampaignId = campaignIdKey,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // AdGroup object of the Campaign Management service.
                AdGroup = new AdGroup
                {
                    // 'Search Network' or 'Content Network' column header in the Bulk file
                    AdDistribution = AdDistribution.Search,
                    // 'Ad Rotation' column header in the Bulk file
                    AdRotation = new AdRotation
                    {
                        Type = AdRotationType.RotateAdsEvenly
                    },
                    // 'Bid Strategy Type' column header in the Bulk file
                    BiddingScheme = new ManualCpcBiddingScheme { },
                    // 'Content Bid' column header in the Bulk file
                    ContentMatchBid = null,
                    // 'End Date' column header in the Bulk file
                    EndDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                    {
                        Month = 12,
                        Day = 31,
                        Year = DateTime.UtcNow.Year + 1
                    },
                    // 'Id' column header in the Bulk file
                    Id = adGroupIdKey,
                    // 'Language' column header in the Bulk file
                    Language = "English",
                    // 'Ad Group' column header in the Bulk file
                    Name = "Women's Red Shoe Sale",
                    // 'Bid Adjustment' column header in the Bulk file
                    NativeBidAdjustment = 10,
                    // 'Network Distribution' column header in the Bulk file
                    Network = Network.OwnedAndOperatedAndSyndicatedSearch,
                    // 'Remarketing Targeting Setting' column header in the Bulk file
                    RemarketingTargetingSetting = RemarketingTargetingSetting.TargetAndBid,
                    // 'Search Bid' column header in the Bulk file
                    SearchBid = new Bid
                    {
                        Amount = 0.10
                    },
                    // 'Start Date' column header in the Bulk file
                    StartDate = new Microsoft.BingAds.V11.CampaignManagement.Date
                    {
                        Month = DateTime.UtcNow.Month,
                        Day = DateTime.UtcNow.Day,
                        Year = DateTime.UtcNow.Year
                    },
                    // 'Status' column header in the Bulk file
                    Status = AdGroupStatus.Paused,
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
            };

            return bulkAdGroup;
        }

        protected BulkEntity GetBulkAdGroupAppAdExtension()
        {
            // Map properties in the Bulk file to the BulkAdGroupAppAdExtension
            var bulkAdGroupAppAdExtension = new BulkAdGroupAppAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = appAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = adGroupIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkAdGroupAppAdExtension;
        }

        protected BulkEntity GetBulkAdGroupCalloutAdExtension()
        {
            // Map properties in the Bulk file to the BulkAdGroupCalloutAdExtension
            var bulkAdGroupCalloutAdExtension = new BulkAdGroupCalloutAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = calloutAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = adGroupIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkAdGroupCalloutAdExtension;
        }

        protected BulkEntity GetBulkAdGroupImageAdExtension()
        {
            // Map properties in the Bulk file to the BulkAdGroupCalloutAdExtension
            var bulkAdGroupImageAdExtension = new BulkAdGroupImageAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = imageAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = adGroupIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkAdGroupImageAdExtension;
        }

        protected BulkEntity GetBulkAdGroupReviewAdExtension()
        {
            // Map properties in the Bulk file to the BulkAdGroupReviewAdExtension
            var bulkAdGroupReviewAdExtension = new BulkAdGroupReviewAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = reviewAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = adGroupIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkAdGroupReviewAdExtension;
        }

        protected BulkEntity GetBulkAdGroupSitelink2AdExtension()
        {
            // Map properties in the Bulk file to the BulkAdGroupSitelink2AdExtension
            var bulkAdGroupSitelink2AdExtension = new BulkAdGroupSitelink2AdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = sitelink2AdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = adGroupIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkAdGroupSitelink2AdExtension;
        }

        protected BulkEntity GetBulkAdGroupSiteLinkAdExtension()
        {
            // Map properties in the Bulk file to the BulkAdGroupSiteLinkAdExtension
            var bulkAdGroupSiteLinkAdExtension = new BulkAdGroupSiteLinkAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = siteLinksAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = adGroupIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkAdGroupSiteLinkAdExtension;
        }

        protected BulkEntity GetBulkAdGroupStructuredSnippetAdExtension()
        {
            // Map properties in the Bulk file to the BulkAdGroupStructuredSnippetAdExtension
            var bulkAdGroupStructuredSnippetAdExtension = new BulkAdGroupStructuredSnippetAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = structuredSnippetAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = adGroupIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkAdGroupStructuredSnippetAdExtension;
        }

        protected BulkEntity GetBulkAdGroupDynamicSearchAdTarget()
        {
            // Map properties in the Bulk file to the BulkAdGroupDynamicSearchAdTarget
            var bulkAdGroupDynamicSearchAdTarget = new BulkAdGroupDynamicSearchAdTarget
            {
                // Map properties in the Bulk file to the 
                // BiddableAdGroupCriterion object of the Campaign Management service.
                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,
                    Criterion = new Webpage
                    {
                        Parameter = new WebpageParameter
                        {
                            // Set Conditions null if you want to target all webpages
                            Conditions = new[]
                            {
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 1' column header in the Bulk file
                                    Argument = "contoso.com",
                                    // 'Dynamic Ad Target Condition 1' column header in the Bulk file
                                    Operand = WebpageConditionOperand.Url
                                },
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 2' column header in the Bulk file
                                    Argument = "US/CA/SFO",
                                    // 'Dynamic Ad Target Condition 2' column header in the Bulk file
                                    Operand = WebpageConditionOperand.Category
                                },
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 3' column header in the Bulk file
                                    Argument = "flowers",
                                    // 'Dynamic Ad Target Condition 3' column header in the Bulk file
                                    Operand = WebpageConditionOperand.PageContent
                                },
                            },
                            // 'Name' column header in the Bulk file
                            CriterionName = "Bulk Ad Group Dynamic Search Ad Target"
                        }
                    },
                    CriterionBid = new FixedBid
                    {
                        // 'Bid' column header in the Bulk file
                        Amount = 0.50
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Paused,
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
                // 'Ad Group' column header in the Bulk file
                AdGroupName = null,
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
            };

            return bulkAdGroupDynamicSearchAdTarget;
        }

        protected BulkEntity GetBulkAdGroupNegativeDynamicSearchAdTarget()
        {
            // Map properties in the Bulk file to the BulkAdGroupNegativeDynamicSearchAdTarget
            var bulkAdGroupNegativeDynamicSearchAdTarget = new BulkAdGroupNegativeDynamicSearchAdTarget
            {
                // Map properties in the Bulk file to the 
                // NegativeAdGroupCriterion object of the Campaign Management service.
                NegativeAdGroupCriterion = new NegativeAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,
                    Criterion = new Webpage
                    {
                        Parameter = new WebpageParameter
                        {
                            Conditions = new[]
                            {
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 1' column header in the Bulk file
                                    Argument = "contoso.com",
                                    // 'Dynamic Ad Target Condition 1' column header in the Bulk file
                                    Operand = WebpageConditionOperand.Url
                                },
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 2' column header in the Bulk file
                                    Argument = "US/CA/",
                                    // 'Dynamic Ad Target Condition 2' column header in the Bulk file
                                    Operand = WebpageConditionOperand.Category
                                },
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 3' column header in the Bulk file
                                    Argument = "flowers",
                                    // 'Dynamic Ad Target Condition 3' column header in the Bulk file
                                    Operand = WebpageConditionOperand.PageContent
                                },
                            },
                            // 'Name' column header in the Bulk file
                            CriterionName = "Bulk Ad Group Negative Dynamic Search Ad Target"
                        }
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Paused,
                },
                // 'Ad Group' column header in the Bulk file
                AdGroupName = null,
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
            };

            return bulkAdGroupNegativeDynamicSearchAdTarget;
        }

        protected BulkEntity GetBulkAdGroupNegativeKeyword()
        {
            // Map properties in the Bulk file to the BulkAdGroupNegativeKeyword
            var bulkAdGroupNegativeKeyword = new BulkAdGroupNegativeKeyword
            {
                // 'Parent Id' column header in the Bulk file
                AdGroupId = adGroupIdKey,
                // 'Ad Group' column header in the Bulk file
                AdGroupName = null,
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // NegativeKeyword object of the Campaign Management service.
                NegativeKeyword = new NegativeKeyword
                {
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Match Type' column header in the Bulk file
                    MatchType = MatchType.Exact,
                    // 'Text' column header in the Bulk file
                    Text = "shoes"
                },

                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkAdGroupNegativeKeyword;
        }

        protected BulkEntity GetBulkAdGroupNegativeSite()
        {
            // Map properties in the Bulk file to the BulkAdGroupNegativeSite
            var bulkAdGroupNegativeSite = new BulkAdGroupNegativeSite
            {
                // 'Parent Id' column header in the Bulk file
                AdGroupId = adGroupIdKey,
                // 'Ad Group' column header in the Bulk file
                AdGroupName = null,
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
                // 'Website' column header in the Bulk file
                Website = "contoso.com",
            };

            return bulkAdGroupNegativeSite;
        }

        protected BulkEntity GetBulkAdGroupNegativeSites()
        {
            // Map properties in the Bulk file to the BulkAdGroupNegativeSites
            var bulkAdGroupNegativeSites = new BulkAdGroupNegativeSites
            {
                // 'Ad Group' column header in the Bulk file
                AdGroupName = null,

                // Map properties in the Bulk file to the 
                // AdGroupNegativeSites object of the Campaign Management service.
                AdGroupNegativeSites = new AdGroupNegativeSites
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,
                    NegativeSites = new[]
                    {
                        // 'Website' column header in the Bulk file
                        "contoso.com",
                    }
                },

                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkAdGroupNegativeSites;
        }

        protected BulkEntity GetBulkAdGroupProductPartition()
        {
            // Map properties in the Bulk file to the BulkAdGroupProductPartition
            var bulkAdGroupProductPartition = new BulkAdGroupProductPartition
            {
                // Map properties in the Bulk file to the BiddableAdGroupCriterion or
                // NegativeAdGroupCriterion object of the Campaign Management service.
                // Use the BiddableAdGroupCriterion to set the 'Is Excluded' field in the Bulk file to True,
                // and otherwise use the NegativeAdGroupCriterion to set the 'Is Excluded' field to False.
                AdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,
                    Criterion = new ProductPartition
                    {
                        Condition = new ProductCondition
                        {
                            // 'Product Value 1' column header in the Bulk file
                            Attribute = null,
                            // 'Product Condition 1' column header in the Bulk file
                            Operand = "All",
                        },
                        // 'Parent Criterion Id' column header in the Bulk file
                        ParentCriterionId = null
                    },
                    CriterionBid = new FixedBid
                    {
                        // 'Bid' column header in the Bulk file is only applicable for BiddableAdGroupCriterion
                        Amount = 0
                    },
                    // 'Destination Url' column header in the Bulk file is only applicable for BiddableAdGroupCriterion
                    DestinationUrl = null,
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Paused,
                    // 'Tracking Template' column header in the Bulk file is only applicable for BiddableAdGroupCriterion
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file is only applicable for BiddableAdGroupCriterion
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
                // 'Ad Group' column header in the Bulk file
                AdGroupName = null,
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
            };

            return bulkAdGroupProductPartition;
        }

        protected BulkEntity GetBulkAdGroupRemarketingListAssociation()
        {
            // Map properties in the Bulk file to the BulkAdGroupRemarketingListAssociation
            var bulkAdGroupRemarketingListAssociation = new BulkAdGroupRemarketingListAssociation
            {
                // 'Ad Group' column header in the Bulk file
                AdGroupName = null,

                // Map properties in the Bulk file to the 
                // BiddableAdGroupCriterion object of the Campaign Management service.
                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    AdGroupId = adGroupIdKey,
                    Criterion = new AudienceCriterion
                    {
                        // 'Remarketing List Id' column header in the Bulk file
                        AudienceId = remarketingListIdKey,
                    },
                    // 'Bid Adjustment' column header in the Bulk file
                    CriterionBid = new BidMultiplier
                    {
                        Multiplier = 10
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Status' column header in the Bulk file
                    Status = AdGroupCriterionStatus.Paused
                },
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Audience' column header in the Bulk file
                RemarketingListName = "My Remarketing List " + DateTime.UtcNow,
            };

            return bulkAdGroupRemarketingListAssociation;
        }

        protected List<BulkEntity> GetBulkCampaigns()
        {
            var uploadEntities = new List<BulkEntity>();

            for (int i = 0; i < 3; i++)
            {
                // Map properties in the Bulk file to the BulkCampaign
                var bulkCampaign = new BulkCampaign
                {
                    // 'Parent Id' column header in the Bulk file
                    AccountId = 0,
                    // 'Client Id' column header in the Bulk file
                    ClientId = "ClientIdGoesHere",

                    // Map properties in the Bulk file to the 
                    // Campaign object of the Campaign Management service.
                    Campaign = new Campaign
                    {
                        // 'Bid Strategy Type' column header in the Bulk file
                        BiddingScheme = new EnhancedCpcBiddingScheme { },
                        // 'Budget Id' column header in the Bulk file
                        BudgetId = null,
                        // 'Budget Type' column header in the Bulk file
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        // 'Campaign Type' column header in the Bulk file
                        CampaignType = null,
                        // 'Budget' column header in the Bulk file
                        DailyBudget = 50,
                        // 'Description' column header in the Bulk file
                        Description = "Red shoes line.",
                        // 'Id' column header in the Bulk file
                        Id = campaignIdKey,
                        // 'Language' column header in the Bulk file
                        Languages = null,
                        // 'Campaign' column header in the Bulk file
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        // 'Bid Adjustment' column header in the Bulk file
                        NativeBidAdjustment = 10,
                        // 'Settings are not applicable for the SearchAndContent campaign type
                        Settings = null,
                        // 'Status' column header in the Bulk file
                        Status = CampaignStatus.Active,
                        // 'Time Zone' column header in the Bulk file
                        TimeZone = "PacificTimeUSCanadaTijuana",
                        // 'Tracking Template' column header in the Bulk file
                        TrackingUrlTemplate = null,
                        // 'Custom Parameter' column header in the Bulk file
                        UrlCustomParameters = new CustomParameters
                        {
                            // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                            Parameters = new[] {
                                new CustomParameter(){
                                    Key = "promoCode",
                                    Value = "PROMO1"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    },
                };

                uploadEntities.Add(bulkCampaign);
            }

            // Set properties specific to the SearchAndContent campaign type
            ((BulkCampaign)uploadEntities[0]).Campaign.Id = searchAndContentCampaignIdKey;
            ((BulkCampaign)uploadEntities[0]).Campaign.Name = "SearchAndContent " + DateTime.UtcNow;
            ((BulkCampaign)uploadEntities[0]).Campaign.CampaignType = CampaignType.SearchAndContent;

            // Set properties specific to the Shopping campaign type
            ((BulkCampaign)uploadEntities[1]).Campaign.Id = shoppingCampaignIdKey;
            ((BulkCampaign)uploadEntities[1]).Campaign.Name = "Shopping " + DateTime.UtcNow;
            // EnhancedCpcBiddingScheme is not supported for shopping campaigns.
            // 'Bid Strategy Type' column header in the Bulk file
            ((BulkCampaign)uploadEntities[1]).Campaign.BiddingScheme = null;
            ((BulkCampaign)uploadEntities[1]).Campaign.CampaignType = CampaignType.Shopping;
            ((BulkCampaign)uploadEntities[1]).Campaign.Settings = new[] {
                new ShoppingSetting {
                    // 'Priority' column header in the Bulk file
                    Priority = 0,
                    // 'Country Code' column header in the Bulk file
                    SalesCountryCode = "US",
                    // 'Store Id' column header in the Bulk file
                    StoreId = null // StoreIdGoesHere
                }
            };

            // Set properties specific to the DynamicSearchAds campaign type
            ((BulkCampaign)uploadEntities[2]).Campaign.Id = dynamicSearchAdsCampaignIdKey;
            ((BulkCampaign)uploadEntities[2]).Campaign.Name = "DynamicSearchAds " + DateTime.UtcNow;
            ((BulkCampaign)uploadEntities[2]).Campaign.CampaignType = CampaignType.DynamicSearchAds;
            ((BulkCampaign)uploadEntities[2]).Campaign.Settings = new[] {
                new DynamicSearchAdsSetting {
                    // 'Website' column header in the Bulk file
                    DomainName = "contoso.com",
                    // 'Domain Language' column header in the Bulk file
                    Language = "EN"
                }
            };

            return uploadEntities;
        }

        protected BulkEntity GetBulkCampaignAppAdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignAppAdExtension
            var bulkCampaignAppAdExtension = new BulkCampaignAppAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = appAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignAppAdExtension;
        }

        protected BulkEntity GetBulkCampaignCallAdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignCallAdExtension
            var bulkCampaignCallAdExtension = new BulkCampaignCallAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = callAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignCallAdExtension;
        }

        protected BulkEntity GetBulkCampaignCalloutAdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignCalloutAdExtension
            var bulkCampaignCalloutAdExtension = new BulkCampaignCalloutAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = calloutAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignCalloutAdExtension;
        }

        protected BulkEntity GetBulkCampaignImageAdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignImageAdExtension
            var bulkCampaignImageAdExtension = new BulkCampaignImageAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = imageAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignImageAdExtension;
        }

        protected BulkEntity GetBulkCampaignLocationAdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignLocationAdExtension
            var bulkCampaignLocationAdExtension = new BulkCampaignLocationAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = locationAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignLocationAdExtension;
        }

        protected BulkEntity GetBulkCampaignReviewAdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignReviewAdExtension
            var bulkCampaignReviewAdExtension = new BulkCampaignReviewAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = reviewAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignReviewAdExtension;
        }

        protected BulkEntity GetBulkCampaignSitelink2AdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignSitelink2AdExtension
            var bulkCampaignSitelink2AdExtension = new BulkCampaignSitelink2AdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = sitelink2AdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignSitelink2AdExtension;
        }

        protected BulkEntity GetBulkCampaignSiteLinkAdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignSiteLinkAdExtension
            var bulkCampaignSiteLinkAdExtension = new BulkCampaignSiteLinkAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = siteLinksAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignSiteLinkAdExtension;
        }

        protected BulkEntity GetBulkCampaignStructuredSnippetAdExtension()
        {
            // Map properties in the Bulk file to the BulkCampaignStructuredSnippetAdExtension
            var bulkCampaignStructuredSnippetAdExtension = new BulkCampaignStructuredSnippetAdExtension
            {
                // Map properties in the Bulk file to the 
                // AdExtensionIdToEntityIdAssociation object of the Campaign Management service.
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    // 'Id' column header in the Bulk file
                    AdExtensionId = structuredSnippetAdExtensionIdKey,
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                },

                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignStructuredSnippetAdExtension;
        }

        protected BulkEntity GetBulkCampaignNegativeDynamicSearchAdTarget()
        {
            // Map properties in the Bulk file to the BulkCampaignNegativeDynamicSearchAdTarget
            var bulkCampaignNegativeDynamicSearchAdTarget = new BulkCampaignNegativeDynamicSearchAdTarget
            {
                // Map properties in the Bulk file to the 
                // NegativeCampaignCriterion object of the Campaign Management service.
                NegativeCampaignCriterion = new NegativeCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,
                    Criterion = new Webpage
                    {
                        Parameter = new WebpageParameter
                        {
                            Conditions = new[]
                            {
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 1' column header in the Bulk file
                                    Argument = "contoso.com",
                                    // 'Dynamic Ad Target Condition 1' column header in the Bulk file
                                    Operand = WebpageConditionOperand.Url
                                },
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 2' column header in the Bulk file
                                    Argument = "US/CA/SFO",
                                    // 'Dynamic Ad Target Condition 2' column header in the Bulk file
                                    Operand = WebpageConditionOperand.Category
                                },
                                new WebpageCondition
                                {
                                    // 'Dynamic Ad Target Value 3' column header in the Bulk file
                                    Argument = "flowers",
                                    // 'Dynamic Ad Target Condition 3' column header in the Bulk file
                                    Operand = WebpageConditionOperand.PageContent
                                },
                            },
                            // 'Name' column header in the Bulk file
                            CriterionName = "Bulk Campaign Negative Dynamic Search Ad Target"
                        }
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                },
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkCampaignNegativeDynamicSearchAdTarget;
        }

        protected BulkEntity GetBulkCampaignNegativeDynamicSearchAdTargetTest()
        {
            // Map properties in the Bulk file to the BulkCampaignNegativeDynamicSearchAdTarget
            var bulkCampaignNegativeDynamicSearchAdTarget = new BulkCampaignNegativeDynamicSearchAdTarget
            {
                // Map properties in the Bulk file to the 
                // NegativeCampaignCriterion object of the Campaign Management service.
                NegativeCampaignCriterion = new NegativeCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,
                    Criterion = new Webpage
                    {
                        Parameter = new WebpageParameter
                        {
                            Conditions = null,
                            // 'Name' column header in the Bulk file
                            CriterionName = "Bulk Campaign Negative Dynamic Search Ad Target"
                        }
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                },
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkCampaignNegativeDynamicSearchAdTarget;
        }

        protected BulkEntity GetBulkCampaignNegativeKeyword()
        {
            // Map properties in the Bulk file to the BulkCampaignNegativeKeyword
            var bulkCampaignNegativeKeyword = new BulkCampaignNegativeKeyword
            {
                // 'Parent Id' column header in the Bulk file
                CampaignId = campaignIdKey,
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // NegativeKeyword object of the Campaign Management service.
                NegativeKeyword = new NegativeKeyword
                {
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Match Type' column header in the Bulk file
                    MatchType = MatchType.Exact,
                    // 'Text' column header in the Bulk file
                    Text = "shoes"
                },

                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkCampaignNegativeKeyword;
        }

        protected BulkEntity GetBulkCampaignNegativeKeywordList()
        {
            // Map properties in the Bulk file to the BulkCampaignNegativeKeywordList
            var bulkCampaignNegativeKeywordList = new BulkCampaignNegativeKeywordList
            {
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // SharedEntityAssociation object of the Campaign Management service.
                SharedEntityAssociation = new SharedEntityAssociation
                {
                    // 'Parent Id' column header in the Bulk file
                    EntityId = campaignIdKey,
                    // 'Id' column header in the Bulk file
                    SharedEntityId = negativeKeywordListIdKey,
                },

                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkCampaignNegativeKeywordList;
        }

        protected BulkEntity GetBulkNegativeKeywordList()
        {
            // Map properties in the Bulk file to the BulkNegativeKeywordList
            var bulkNegativeKeywordList = new BulkNegativeKeywordList
            {
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // NegativeKeywordList object of the Campaign Management service.
                NegativeKeywordList = new NegativeKeywordList
                {
                    // 'Id' column header in the Bulk file
                    Id = negativeKeywordListIdKey,
                    // 'Name' column header in the Bulk file
                    Name = "My Negative Keyword List " + DateTime.UtcNow,
                },

                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkNegativeKeywordList;
        }

        protected BulkEntity GetBulkSharedNegativeKeyword()
        {
            // Map properties in the Bulk file to the BulkSharedNegativeKeyword
            var bulkSharedNegativeKeyword = new BulkSharedNegativeKeyword
            {
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // NegativeKeyword object of the Campaign Management service.
                NegativeKeyword = new NegativeKeyword
                {
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Match Type' column header in the Bulk file
                    MatchType = MatchType.Exact,
                    // 'Text' column header in the Bulk file
                    Text = "shoes"
                },

                // 'Parent Id' column header in the Bulk file
                NegativeKeywordListId = negativeKeywordListIdKey,

                // 'Status' column header in the Bulk file
                Status = Status.Active
            };

            return bulkSharedNegativeKeyword;
        }

        protected BulkEntity GetBulkCampaignNegativeSite()
        {
            // Map properties in the Bulk file to the BulkCampaignNegativeSite
            var bulkCampaignNegativeSite = new BulkCampaignNegativeSite
            {
                // 'Parent Id' column header in the Bulk file
                CampaignId = campaignIdKey,
                // 'Campaign' column header in the Bulk file
                CampaignName = null,
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",
                // 'Status' column header in the Bulk file
                Status = Status.Active,
                // 'Website' column header in the Bulk file
                Website = "contoso.com",
            };

            return bulkCampaignNegativeSite;
        }

        protected BulkEntity GetBulkCampaignNegativeSites()
        {
            // Map properties in the Bulk file to the BulkCampaignNegativeSites
            var bulkCampaignNegativeSites = new BulkCampaignNegativeSites
            {
                // 'Campaign' column header in the Bulk file
                CampaignName = null,

                // Map properties in the Bulk file to the 
                // CampaignNegativeSites object of the Campaign Management service.
                CampaignNegativeSites = new CampaignNegativeSites
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,
                    NegativeSites = new[]
                    {
                        // 'Website' column header in the Bulk file
                        "contoso.com",
                    }
                },

                // 'Status' column header in the Bulk file
                Status = Status.Active,
            };

            return bulkCampaignNegativeSites;
        }

        protected BulkEntity GetBulkCampaignProductScope()
        {
            // Map properties in the Bulk file to the BulkCampaignProductScope
            var bulkCampaignProductScope = new BulkCampaignProductScope
            {
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // BiddableCampaignCriterion object of the Campaign Management service.
                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    // 'Parent Id' column header in the Bulk file
                    CampaignId = campaignIdKey,
                    Criterion = new ProductScope
                    {
                        // Conditions are mapped to Product Value 1..7 and Product Condition 1..7 columns
                        Conditions = new[]
                        {
                            new ProductCondition
                            {
                                // 'Product Value 1' column header in the Bulk file
                                Attribute = "New",
                                // 'Product Condition 1' column header in the Bulk file
                                Operand = "Condition",
                            },
                            new ProductCondition
                            {
                                // 'Product Value 2' column header in the Bulk file
                                Attribute = "MerchantDefinedCustomLabel",
                                // 'Product Condition 2' column header in the Bulk file
                                Operand = "CustomLabel0",
                            },
                        },
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Status' column header in the Bulk file
                    Status = CampaignCriterionStatus.Active
                },

                // 'Campaign' column header in the Bulk file
                CampaignName = null,
            };

            return bulkCampaignProductScope;
        }


        protected BulkEntity GetBulkKeyword()
        {
            // Map properties in the Bulk file to the BulkKeyword
            var bulkKeyword = new BulkKeyword
            {
                // 'Parent Id' column header in the Bulk file
                AdGroupId = adGroupIdKey,
                // 'Ad Group' column header in the Bulk file
                AdGroupName = "AdGroupNameHere",
                // 'Campaign' column header in the Bulk file
                CampaignName = "ParentCampaignNameGoesHere",
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // Keyword object of the Campaign Management service.
                Keyword = new Keyword
                {
                    // 'Bid' column header in the Bulk file
                    Bid = new Bid
                    {
                        Amount = 0.50,
                    },
                    // 'Bid Strategy Type' column header in the Bulk file
                    BiddingScheme = new ManualCpcBiddingScheme { },
                    // 'Destination Url' column header in the Bulk file
                    DestinationUrl = null,
                    // 'Mobile Final Url' column header in the Bulk file
                    FinalMobileUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://mobile.contoso.com/womenshoesale"
                    },
                    // 'Final Url' column header in the Bulk file
                    FinalUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://www.contoso.com/womenshoesale"
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Match Type' column header in the Bulk file
                    MatchType = MatchType.Broad,
                    // 'Param 1 column header in the Bulk file
                    Param1 = null,
                    // 'Param 2' column header in the Bulk file
                    Param2 = null,
                    // 'Param 3' column header in the Bulk file
                    Param3 = null,
                    // 'Status' column header in the Bulk file
                    Status = KeywordStatus.Active,
                    // 'Text' column header in the Bulk file
                    Text = "red shoes",
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
            };

            return bulkKeyword;
        }

        protected BulkEntity GetBulkAppInstallAd()
        {
            // Map properties in the Bulk file to the BulkAppInstallAd
            var bulkAppInstallAd = new BulkAppInstallAd
            {
                // 'Parent Id' column header in the Bulk file
                AdGroupId = adGroupIdKey,
                // 'Ad Group' column header in the Bulk file
                AdGroupName = "AdGroupNameHere",
                // 'Campaign' column header in the Bulk file
                CampaignName = "ParentCampaignNameGoesHere",
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // AppInstallAd object of the Campaign Management service.
                AppInstallAd = new AppInstallAd
                {
                    // 'App Platform' column header in the Bulk file
                    AppPlatform = "Android",
                    // 'App Id' column header in the Bulk file
                    AppStoreId = "AppStoreIdGoesHere",
                    // 'Device Preference' column header in the Bulk file
                    DevicePreference = 0,
                    // 'Final Url' column header in the Bulk file
                    FinalUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "FinalUrlGoesHere"
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Status' column header in the Bulk file
                    Status = AdStatus.Active,
                    // 'Text' column header in the Bulk file
                    Text = "Find New Customers & Increase Sales!",
                    // 'Title' column header in the Bulk file
                    Title = "Contoso Quick Setup",
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
            };

            return bulkAppInstallAd;
        }

        protected BulkEntity GetBulkDynamicSearchAd()
        {
            // Map properties in the Bulk file to the BulkDynamicSearchAd
            var bulkDynamicSearchAd = new BulkDynamicSearchAd
            {
                // 'Parent Id' column header in the Bulk file
                AdGroupId = adGroupIdKey,
                // 'Ad Group' column header in the Bulk file
                AdGroupName = "AdGroupNameHere",
                // 'Campaign' column header in the Bulk file
                CampaignName = "ParentCampaignNameGoesHere",
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // DynamicSearchAd object of the Campaign Management service.
                DynamicSearchAd = new DynamicSearchAd
                {
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Path 1' column header in the Bulk file
                    Path1 = "seattle",
                    // 'Path 2' column header in the Bulk file
                    Path2 = "shoe sale",
                    // 'Status' column header in the Bulk file
                    Status = AdStatus.Active,
                    // 'Text' column header in the Bulk file
                    Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
            };

            return bulkDynamicSearchAd;
        }

        protected BulkEntity GetBulkExpandedTextAd()
        {
            // Map properties in the Bulk file to the BulkExpandedTextAd
            var bulkExpandedTextAd = new BulkExpandedTextAd
            {
                // 'Parent Id' column header in the Bulk file
                AdGroupId = adGroupIdKey,
                // 'Ad Group' column header in the Bulk file
                AdGroupName = "AdGroupNameHere",
                // 'Campaign' column header in the Bulk file
                CampaignName = "ParentCampaignNameGoesHere",
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // ExpandedTextAd object of the Campaign Management service.
                ExpandedTextAd = new ExpandedTextAd
                {
                    // 'Ad Format Preference' column header in the Bulk file
                    AdFormatPreference = "All",
                    // 'Mobile Final Url' column header in the Bulk file
                    FinalMobileUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://mobile.contoso.com/womenshoesale"
                    },
                    // 'Final Url' column header in the Bulk file
                    FinalUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://www.contoso.com/womenshoesale"
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Path 1' column header in the Bulk file
                    Path1 = "seattle",
                    // 'Path 2' column header in the Bulk file
                    Path2 = "shoe sale",
                    // 'Status' column header in the Bulk file
                    Status = AdStatus.Active,
                    // 'Text' column header in the Bulk file
                    Text = "Find New Customers & Increase Sales! Start Advertising on Contoso Today.",
                    // 'Title Part 1' column header in the Bulk file
                    TitlePart1 = "Contoso",
                    // 'Title Part 2' column header in the Bulk file
                    TitlePart2 = "Quick & Easy Setup",
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
            };

            return bulkExpandedTextAd;
        }

        protected BulkEntity GetBulkProductAd()
        {
            // Map properties in the Bulk file to the BulkProductAd
            var bulkProductAd = new BulkProductAd
            {
                // 'Parent Id' column header in the Bulk file
                AdGroupId = adGroupIdKey,
                // 'Ad Group' column header in the Bulk file
                AdGroupName = "AdGroupNameHere",
                // 'Campaign' column header in the Bulk file
                CampaignName = "ParentCampaignNameGoesHere",
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // ProductAd object of the Campaign Management service.
                ProductAd = new ProductAd
                {
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Promotion' column header in the Bulk file
                    PromotionalText = "Find New Customers & Increase Sales!",
                    // 'Status' column header in the Bulk file
                    Status = AdStatus.Active,
                },
            };

            return bulkProductAd;
        }

        protected BulkEntity GetBulkTextAd()
        {
            // Map properties in the Bulk file to the BulkTextAd
            var bulkTextAd = new BulkTextAd
            {
                // 'Parent Id' column header in the Bulk file
                AdGroupId = adGroupIdKey,
                // 'Ad Group' column header in the Bulk file
                AdGroupName = "AdGroupNameHere",
                // 'Campaign' column header in the Bulk file
                CampaignName = "ParentCampaignNameGoesHere",
                // 'Client Id' column header in the Bulk file
                ClientId = "ClientIdGoesHere",

                // Map properties in the Bulk file to the 
                // TextAd object of the Campaign Management service.
                TextAd = new TextAd
                {
                    // 'Ad Format Preference' column header in the Bulk file
                    AdFormatPreference = "All",
                    // 'Destination Url' column header in the Bulk file
                    DestinationUrl = null,
                    // 'Device Preference' column header in the Bulk file
                    DevicePreference = 0,
                    // 'Display Url' column header in the Bulk file
                    DisplayUrl = "contoso.com",
                    // 'Mobile Final Url' column header in the Bulk file
                    FinalMobileUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://mobile.contoso.com/womenshoesale"
                    },
                    // 'Final Url' column header in the Bulk file
                    FinalUrls = new[] {
                        // Each Url is delimited by a semicolon (;) in the Bulk file
                        "http://www.contoso.com/womenshoesale"
                    },
                    // 'Id' column header in the Bulk file
                    Id = null,
                    // 'Status' column header in the Bulk file
                    Status = AdStatus.Active,
                    // 'Text' column header in the Bulk file
                    Text = "Find New Customers & Increase Sales!",
                    // 'Title' column header in the Bulk file
                    Title = "Contoso Quick Setup",
                    // 'Tracking Template' column header in the Bulk file
                    TrackingUrlTemplate = null,
                    // 'Custom Parameter' column header in the Bulk file
                    UrlCustomParameters = new CustomParameters
                    {
                        // Each custom parameter is delimited by a semicolon (;) in the Bulk file
                        Parameters = new[] {
                            new CustomParameter(){
                                Key = "promoCode",
                                Value = "PROMO1"
                            },
                            new CustomParameter(){
                                Key = "season",
                                Value = "summer"
                            },
                        }
                    },
                },
            };

            return bulkTextAd;
        }

        #endregion BulkEntityExamples
    }
}
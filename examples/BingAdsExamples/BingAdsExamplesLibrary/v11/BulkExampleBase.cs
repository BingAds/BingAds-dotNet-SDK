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
        
        protected long appAdExtensionIdKey = -11;
        protected long callAdExtensionIdKey = -12;
        protected long calloutAdExtensionIdKey = -13;
        protected long imageAdExtensionIdKey = -14;
        protected long locationAdExtensionIdKey = -15;
        protected long reviewAdExtensionIdKey = -16;
        protected long siteLinksAdExtensionIdKey = -17;
        protected long sitelink2AdExtensionIdKey = -17;
        protected long structuredSnippetAdExtensionIdKey = -18;
        protected long negativeKeywordListIdKey = -19;
        protected long budgetIdKey = -20;
        protected long campaignIdKey = -111;
        protected long adGroupIdKey = -1111;
        protected long negativeKeywordIdKey = -11111;


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
                OutputAdGroup(entity.AdGroup);

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
                    OutputBiddableAdGroupCriterion(entity.AdGroupCriterion);

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
                    OutputBiddableAdGroupCriterion(entity.AdGroupCriterion);

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
                if(entity != null)
                {
                    OutputStatusMessage("\nBulkAdGroupDeviceCriterion: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                    // Output the Campaign Management BiddableAdGroupCriterion Object
                    OutputBiddableAdGroupCriterion(entity.AdGroupCriterion);

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
                    OutputBiddableAdGroupCriterion(entity.AdGroupCriterion);

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
                    OutputBiddableAdGroupCriterion(entity.AdGroupCriterion);

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
                    OutputBiddableAdGroupCriterion(entity.AdGroupCriterion);

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
                    OutputNegativeAdGroupCriterion(entity.AdGroupCriterion);

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
                    OutputBiddableAdGroupCriterion(entity.AdGroupCriterion);

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
                    OutputNegativeKeyword(entity.NegativeKeyword);
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
                    OutputNegativeSites(entity.AdGroupNegativeSites.NegativeSites);
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
                OutputNegativeSites(new[] { entity.Website });

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
                OutputAppAdExtension(entity.AppAdExtension);

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
                OutputCallAdExtension(entity.CallAdExtension);

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
                OutputCalloutAdExtension(entity.CalloutAdExtension);
                
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
                OutputReviewAdExtension(entity.ReviewAdExtension);

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
                OutputBudget(entity.Budget);

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
                OutputCampaign(entity.Campaign);

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
                    OutputNegativeKeyword(entity.NegativeKeyword);
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
                    OutputNegativeSites(entity.CampaignNegativeSites.NegativeSites);
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
                OutputNegativeSites(new[] { entity.Website });

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
                OutputImageAdExtension(entity.ImageAdExtension);

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
                OutputKeyword(entity.Keyword);

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
                OutputLocationAdExtension(entity.LocationAdExtension);

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
                OutputNegativeKeywordList(entity.NegativeKeywordList);
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));

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
                OutputProductAd(entity.ProductAd);

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
                OutputBiddableCampaignCriterion(entity.BiddableCampaignCriterion);

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
                    OutputBiddableAdGroupCriterion(biddableAdGroupCriterion);
                    
                }
                else
                {
                    var negativeAdGroupCriterion = entity.AdGroupCriterion as NegativeAdGroupCriterion;
                    if (negativeAdGroupCriterion != null)
                    {
                        // Output the Campaign Management NegativeAdGroupCriterion
                        OutputNegativeAdGroupCriterion(negativeAdGroupCriterion);
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
                OutputNegativeKeyword(entity.NegativeKeyword);
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
                OutputSiteLinksAdExtension(entity.SiteLinksAdExtension);

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
                OutputSiteLinks(new[] { entity.SiteLink });

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
                OutputSitelink2AdExtension(entity.Sitelink2AdExtension);

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
                OutputStructuredSnippetAdExtension(entity.StructuredSnippetAdExtension);

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
                OutputExpandedTextAd(entity.ExpandedTextAd);

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
                OutputTextAd(entity.TextAd);

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
                OutputRemarketingList(entity.RemarketingList);

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
                OutputBiddableAdGroupCriterion(entity.BiddableAdGroupCriterion);

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
                    // 'Pricing Model' column header in the Bulk file
                    PricingModel = PricingModel.Cpc,
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

                CampaignCriterion = new BiddableCampaignCriterion
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

                CampaignCriterion = new BiddableCampaignCriterion
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

                    CampaignCriterion = new BiddableCampaignCriterion
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
                    CampaignCriterion = new BiddableCampaignCriterion
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
                    CampaignCriterion = new BiddableCampaignCriterion
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

                CampaignCriterion = new BiddableCampaignCriterion
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

                CampaignCriterion = new BiddableCampaignCriterion
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

                CampaignCriterion = new BiddableCampaignCriterion
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

                CampaignCriterion = new NegativeCampaignCriterion
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

                CampaignCriterion = new BiddableCampaignCriterion
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

                AdGroupCriterion = new BiddableAdGroupCriterion
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

                AdGroupCriterion = new BiddableAdGroupCriterion
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

                    AdGroupCriterion = new BiddableAdGroupCriterion
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
                    AdGroupCriterion = new BiddableAdGroupCriterion
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
                    AdGroupCriterion = new BiddableAdGroupCriterion
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

                AdGroupCriterion = new BiddableAdGroupCriterion
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

                AdGroupCriterion = new BiddableAdGroupCriterion
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

                AdGroupCriterion = new BiddableAdGroupCriterion
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

                AdGroupCriterion = new NegativeAdGroupCriterion
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

                AdGroupCriterion = new BiddableAdGroupCriterion
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

        #endregion BulkEntityExamples
    }
}

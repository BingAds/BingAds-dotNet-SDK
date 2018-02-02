using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V11.Bulk;
using Microsoft.BingAds.V11.Bulk.Entities;
using Microsoft.BingAds.V11.CampaignManagement;

namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to use targets criterions using the BulkServiceManager class.
    /// </summary>
    public class BulkTargetCriterions : BulkExampleBase
    {        
        public override string Description
        {
            get { return "Target Criterions | Bulk V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));
                
                OutputStatusMessage("Add and Update Criterions . . .\n");
                await AddUpdateDeleteCriterions().ConfigureAwait(continueOnCapturedContext: false);

            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V11.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V11.Bulk.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (BulkOperationInProgressException ex)
            {
                OutputStatusMessage("The result file for the bulk operation is not yet available for download.");
                OutputStatusMessage(ex.Message);
            }
            catch (BulkOperationCouldNotBeCompletedException<DownloadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (BulkOperationCouldNotBeCompletedException<UploadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
            finally
            {
                if (Reader != null) { Reader.Dispose(); }
                if (Writer != null) { Writer.Dispose(); }
            }
        }
        
        /// <summary>
        /// Downloads all target criterions in the account. You can use this method 
        /// to sync criterion identifiers and map them to your campaigns and ad groups.
        /// </summary>
        /// <returns>The string result of the Task is the local path to the downloaded bulk file.</returns>
        private async Task<string> DownloadTargetsAsCriterions(IList<long> campaignIds)
        {
            var downloadParameters = new DownloadParameters
            {
                CampaignIds = campaignIds,
                DownloadEntities = new List<DownloadEntity> {
                    DownloadEntity.AdGroupTargetCriterions,
                    DownloadEntity.CampaignTargetCriterions
                },
                ResultFileDirectory = FileDirectory,
                ResultFileName = DownloadFileName,
                OverwriteResultFile = true,
                LastSyncTimeInUTC = null
            };

            OutputStatusMessage("Downloading targets as criterions . . . \n");
            return await BulkServiceManager.DownloadFileAsync(downloadParameters).ConfigureAwait(continueOnCapturedContext: false);
        }

        /// <summary>
        /// Adds a new campaign and ad group with target criterions. Then some criterions are
        /// updated before all of the entities created by this example are deleted.
        /// </summary>
        /// <returns></returns>
        private async Task AddUpdateDeleteCriterions()
        {
            #region Add

            campaignIdKey = -111;
            adGroupIdKey = -1111;
            
            List<BulkEntity> uploadEntities = GetExampleBulkEntitiesToAdd();
            
            // Upload and write the output

            OutputStatusMessage("Adding campaign, ad group, and criterions . . . \n");
            Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities).ConfigureAwait(continueOnCapturedContext: false);
            var downloadEntities = Reader.ReadEntities().ToList();
            
            var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
            var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();

            var campaignAgeCriterionResults = downloadEntities.OfType<BulkCampaignAgeCriterion>().ToList();
            var campaignDayTimeCriterionResults = downloadEntities.OfType<BulkCampaignDayTimeCriterion>().ToList();
            var campaignDeviceCriterionResults = downloadEntities.OfType<BulkCampaignDeviceCriterion>().ToList();
            var campaignGenderCriterionResults = downloadEntities.OfType<BulkCampaignGenderCriterion>().ToList();
            var campaignLocationCriterionResults = downloadEntities.OfType<BulkCampaignLocationCriterion>().ToList();
            var campaignLocationIntentCriterionResults = downloadEntities.OfType<BulkCampaignLocationIntentCriterion>().ToList();
            var campaignNegativeLocationCriterionResults = downloadEntities.OfType<BulkCampaignNegativeLocationCriterion>().ToList();
            var campaignRadiusCriterionResults = downloadEntities.OfType<BulkCampaignRadiusCriterion>().ToList();

            var adGroupAgeCriterionResults = downloadEntities.OfType<BulkAdGroupAgeCriterion>().ToList();
            var adGroupDayTimeCriterionResults = downloadEntities.OfType<BulkAdGroupDayTimeCriterion>().ToList();
            var adGroupDeviceCriterionResults = downloadEntities.OfType<BulkAdGroupDeviceCriterion>().ToList();
            var adGroupGenderCriterionResults = downloadEntities.OfType<BulkAdGroupGenderCriterion>().ToList();
            var adGroupLocationCriterionResults = downloadEntities.OfType<BulkAdGroupLocationCriterion>().ToList();
            var adGroupLocationIntentCriterionResults = downloadEntities.OfType<BulkAdGroupLocationIntentCriterion>().ToList();
            var adGroupNegativeLocationCriterionResults = downloadEntities.OfType<BulkAdGroupNegativeLocationCriterion>().ToList();
            var adGroupRadiusCriterionResults = downloadEntities.OfType<BulkAdGroupRadiusCriterion>().ToList();
            
            Reader.Dispose();

            campaignIdKey = (long)campaignResults[0].Campaign.Id;
            adGroupIdKey = (long)adGroupResults[0].AdGroup.Id;
            
            await DownloadTargetsAsCriterions(new[] { campaignIdKey }).ConfigureAwait(continueOnCapturedContext: false);

            #endregion Add

            #region Update
            
            uploadEntities = new List<BulkEntity>();

            // We can also target Canada (LocationId = 32) by adding a new location criterion.
            var bulkCampaignLocationCriterion = new BulkCampaignLocationCriterion
            {
                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    CampaignId = campaignIdKey,
                    Criterion = new LocationCriterion
                    {
                        LocationId = 32,
                    },
                    CriterionBid = new BidMultiplier
                    {
                        Multiplier = 20,
                    },
                }
            };
            uploadEntities.Add(bulkCampaignLocationCriterion);

            // You cannot delete the location intent criterion from campaign or ad group.
            // This attempt will fail but will not cause other criterion updates to fail.
            var bulkCampaignLocationIntentCriterion = new BulkCampaignLocationIntentCriterion
            {
                BiddableCampaignCriterion = new BiddableCampaignCriterion
                {
                    CampaignId = campaignIdKey,
                    Id = campaignLocationIntentCriterionResults[0].BiddableCampaignCriterion.Id,
                    Status = CampaignCriterionStatus.Deleted
                }
            };
            uploadEntities.Add(bulkCampaignLocationIntentCriterion);

            // If you do not use the assigned identifier for an existing criterion bid e.g. change bid from 20 to 10 
            // for the EighteenToTwentyFour age range, the attempt will fail with code 1043 i.e. CampaignServiceEntityAlreadyExists.
            var bulkAdGroupAgeCriterion = new BulkAdGroupAgeCriterion
            {
                BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    AdGroupId = adGroupIdKey,
                    Id = null,
                    Criterion = new AgeCriterion
                    {
                        AgeRange = AgeRange.EighteenToTwentyFour,
                    },
                    CriterionBid = new BidMultiplier
                    {
                        Multiplier = 10,
                    }
                }
            };
            uploadEntities.Add(bulkAdGroupAgeCriterion);

            // By removing all ad group level device criterions, the campaign device criterions will be inherited.
            foreach (var adGroupDeviceCriterionResult in adGroupDeviceCriterionResults)
            {
                adGroupDeviceCriterionResult.BiddableAdGroupCriterion.Status = AdGroupCriterionStatus.Deleted;
                uploadEntities.Add(adGroupDeviceCriterionResult);
            }
                        
            OutputStatusMessage("Adding and updating criterions . . .\n");

            Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities).ConfigureAwait(continueOnCapturedContext: false);
            downloadEntities = Reader.ReadEntities().ToList();

            campaignAgeCriterionResults = downloadEntities.OfType<BulkCampaignAgeCriterion>().ToList();
            campaignDayTimeCriterionResults = downloadEntities.OfType<BulkCampaignDayTimeCriterion>().ToList();
            campaignDeviceCriterionResults = downloadEntities.OfType<BulkCampaignDeviceCriterion>().ToList();
            campaignGenderCriterionResults = downloadEntities.OfType<BulkCampaignGenderCriterion>().ToList();
            campaignLocationCriterionResults = downloadEntities.OfType<BulkCampaignLocationCriterion>().ToList();
            campaignLocationIntentCriterionResults = downloadEntities.OfType<BulkCampaignLocationIntentCriterion>().ToList();
            campaignNegativeLocationCriterionResults = downloadEntities.OfType<BulkCampaignNegativeLocationCriterion>().ToList();
            campaignRadiusCriterionResults = downloadEntities.OfType<BulkCampaignRadiusCriterion>().ToList();

            adGroupAgeCriterionResults = downloadEntities.OfType<BulkAdGroupAgeCriterion>().ToList();
            adGroupDayTimeCriterionResults = downloadEntities.OfType<BulkAdGroupDayTimeCriterion>().ToList();
            adGroupDeviceCriterionResults = downloadEntities.OfType<BulkAdGroupDeviceCriterion>().ToList();
            adGroupGenderCriterionResults = downloadEntities.OfType<BulkAdGroupGenderCriterion>().ToList();
            adGroupLocationCriterionResults = downloadEntities.OfType<BulkAdGroupLocationCriterion>().ToList();
            adGroupLocationIntentCriterionResults = downloadEntities.OfType<BulkAdGroupLocationIntentCriterion>().ToList();
            adGroupNegativeLocationCriterionResults = downloadEntities.OfType<BulkAdGroupNegativeLocationCriterion>().ToList();
            adGroupRadiusCriterionResults = downloadEntities.OfType<BulkAdGroupRadiusCriterion>().ToList();

            Reader.Dispose();

            #endregion Update

            #region Cleanup

            // Delete i.e. clean up the entities created in this example.
            OutputStatusMessage("Deleting campaign, ad group, and criterions . . .\n");
            await DeleteBulkCampaignsAsync((new [] { campaignIdKey }).ToList()).ConfigureAwait(continueOnCapturedContext: false);

            #endregion Cleanup

            return;
        }

        /// <summary>
        /// Delete the campaign and any contained entities such as criterions. 
        /// When you delete a BulkCampaign, the dependent entities such as  
        /// BulkCampaignDeviceCriterion are deleted without being specified explicitly.
        /// </summary>
        /// <returns></returns>
        private async Task DeleteBulkCampaignsAsync(IList<long> campaignIds)
        {             
            var uploadEntities = new List<BulkEntity>();
            foreach (var campaignId in campaignIds)
            {
                var bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
                        Id = campaignId,
                        Status = CampaignStatus.Deleted
                    }
                };

                uploadEntities.Add(bulkCampaign);
            }
            
            var bulkFileReader = await Task.Run(() => WriteEntitiesAndUploadFileAsync(uploadEntities)).ConfigureAwait(continueOnCapturedContext: false);
            bulkFileReader.Dispose();
        }


        private List<BulkEntity> GetExampleBulkEntitiesToAdd()
        {
            var uploadEntities = new List<BulkEntity>();

            // Prepare the bulk entities that you want to upload. Each bulk entity contains the corresponding Campaign Management 
            // API data object, and additional elements supported for bulk download and upload. 
                        
            // Campaign and Ad Group

            uploadEntities.Add(GetExampleBulkCampaign());
            uploadEntities.Add(GetExampleBulkAdGroup());

            // Campaign Criterions

            uploadEntities.Add(GetExampleBulkCampaignAgeCriterion());
            uploadEntities.Add(GetExampleBulkCampaignDayTimeCriterion());
            foreach (BulkCampaignDeviceCriterion bulkCampaignDeviceCriterion in GetExampleBulkCampaignDeviceCriterions())
            {
                uploadEntities.Add(bulkCampaignDeviceCriterion);
            }
            uploadEntities.Add(GetExampleBulkCampaignGenderCriterion());
            uploadEntities.Add(GetExampleBulkCampaignLocationCriterion());
            uploadEntities.Add(GetExampleBulkCampaignLocationIntentCriterion());
            uploadEntities.Add(GetExampleBulkCampaignNegativeLocationCriterion());
            uploadEntities.Add(GetExampleBulkCampaignRadiusCriterion());

            // Ad Group Criterions

            uploadEntities.Add(GetExampleBulkAdGroupAgeCriterion());
            uploadEntities.Add(GetExampleBulkAdGroupDayTimeCriterion());
            foreach (BulkAdGroupDeviceCriterion bulkAdGroupDeviceCriterion in GetExampleBulkAdGroupDeviceCriterions())
            {
                uploadEntities.Add(bulkAdGroupDeviceCriterion);
            }
            uploadEntities.Add(GetExampleBulkAdGroupGenderCriterion());
            uploadEntities.Add(GetExampleBulkAdGroupLocationCriterion());
            uploadEntities.Add(GetExampleBulkAdGroupLocationIntentCriterion());
            uploadEntities.Add(GetExampleBulkAdGroupNegativeLocationCriterion());
            uploadEntities.Add(GetExampleBulkAdGroupRadiusCriterion());
            
            return uploadEntities;
        }
    }
}

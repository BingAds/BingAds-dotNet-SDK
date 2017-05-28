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

using BulkServiceManagerV10 = Microsoft.BingAds.V10.Bulk.BulkServiceManager;
using DownloadParametersV10 = Microsoft.BingAds.V10.Bulk.DownloadParameters;
using BulkDownloadEntityV10 = Microsoft.BingAds.V10.Bulk.BulkDownloadEntity;

using ICampaignManagementServiceV10 = Microsoft.BingAds.V10.CampaignManagement.ICampaignManagementService;

using AddTargetsToLibraryRequestV10 = Microsoft.BingAds.V10.CampaignManagement.AddTargetsToLibraryRequest;
using AddTargetsToLibraryResponseV10 = Microsoft.BingAds.V10.CampaignManagement.AddTargetsToLibraryResponse;
using SetTargetToCampaignRequestV10 = Microsoft.BingAds.V10.CampaignManagement.SetTargetToCampaignRequest;

using Target = Microsoft.BingAds.V10.CampaignManagement.Target;
using DeviceOSTarget = Microsoft.BingAds.V10.CampaignManagement.DeviceOSTarget;
using DeviceOSTargetBid = Microsoft.BingAds.V10.CampaignManagement.DeviceOSTargetBid;


namespace BingAdsExamplesLibrary.V11
{
    /// <summary>
    /// This example demonstrates how to migrate from targets to criterions using the BulkServiceManager class.
    /// </summary>
    public class BulkTargetCriterions : BulkExampleBase
    {
        public static BulkServiceManagerV10 BulkServiceManagerV10;
        public static ServiceClient<ICampaignManagementServiceV10> CampaignServiceV10;
        
        public override string Description
        {
            get { return "Target Criterions | Bulk V10 to V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CampaignServiceV10 = new ServiceClient<ICampaignManagementServiceV10>(authorizationData);
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);

                BulkServiceManager = new BulkServiceManager(authorizationData);
                BulkServiceManagerV10 = new BulkServiceManagerV10(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));
                
                OutputStatusMessage("Step through the 'Migration Example A' section of the 'Upgrade Targets to Criterions' guide.\n");
                await MigrateTargetCriterionsA(authorizationData).ConfigureAwait(continueOnCapturedContext: false);

                OutputStatusMessage("Step through the 'Migration Example B' section of the 'Upgrade Targets to Criterions' guide.\n");
                await MigrateTargetCriterionsB(authorizationData).ConfigureAwait(continueOnCapturedContext: false);

                OutputStatusMessage("Step through the 'Sync Criterions' section of the 'Upgrade Targets to Criterions' guide.\n");
                await DownloadTargetsAsCriterions(null).ConfigureAwait(continueOnCapturedContext: false);

                OutputStatusMessage("Step through the 'Add or Update Criterions' section of the 'Upgrade Targets to Criterions' guide.\n");
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
        
        // Adds the specified Target object to the customer library. 
        private async Task<AddTargetsToLibraryResponseV10> AddTargetsToLibraryAsync(IList<Target> targets)
        {
            var request = new AddTargetsToLibraryRequestV10
            {
                Targets = targets
            };

            return (await CampaignServiceV10.CallAsync((s, r) => s.AddTargetsToLibraryAsync(r), request));
        }

        // Associates the specified campaign and target. 
        private async Task SetTargetToCampaignAsync(long campaignId, long targetId)
        {
            var request = new SetTargetToCampaignRequestV10
            {
                CampaignId = campaignId,
                TargetId = targetId,
                ReplaceAssociation = true
            };

            await CampaignServiceV10.CallAsync((s, r) => s.SetTargetToCampaignAsync(r), request);
        }

        /// <summary>
        /// Shares a target with multiple new campaigns. This helper function is used to setup
        /// criterion migration scenarios e.g. MigrateTargetCriterionsA and MigrateTargetCriterionsB.
        /// 
        /// This is an example of a deprecated scenario. You must no longer use the AddTargetsToLibrary, 
        /// SetTargetToCampaign, or SetTargetToAdGroup operations. Instead you will be required to use 
        /// criterions in Bing Ads API version 11. Support for targets will end no later than the sunset 
        /// of Bing Ads API version 10. 
        /// </summary>
        /// <param name="authorizationData"></param>
        /// <returns></returns>
        private async Task<IList<long>> ShareDeprecatedTargets(AuthorizationData authorizationData)
        {
            var campaigns = new[]{
                new Campaign
                {
                    Name = "Campaign One " + DateTime.UtcNow,
                    Description = "Red shoes line.",
                    DailyBudget = 20,
                    BudgetType = BudgetLimitType.DailyBudgetStandard,
                    TimeZone = "PacificTimeUSCanadaTijuana",
                },
                new Campaign
                {
                    Name = "Campaign Two " + DateTime.UtcNow,
                    Description = "Red shoes line.",
                    DailyBudget = 20,
                    BudgetType = BudgetLimitType.DailyBudgetStandard,
                    TimeZone = "PacificTimeUSCanadaTijuana",
                },
                new Campaign
                {
                    Name = "Campaign Three " + DateTime.UtcNow,
                    Description = "Red shoes line.",
                    DailyBudget = 20,
                    BudgetType = BudgetLimitType.DailyBudgetStandard,
                    TimeZone = "PacificTimeUSCanadaTijuana",
                },
            };

            AddCampaignsResponse addCampaignsResponse = await AddCampaignsAsync(authorizationData.AccountId, campaigns).ConfigureAwait(continueOnCapturedContext: false);
            IList<long> campaignIds = new List<long>();
            foreach(var campaignId in addCampaignsResponse.CampaignIds.ToArray())
            {
                campaignIds.Add((long)campaignId);
            }

            var sharedTarget = new Target
            {
                Name = "My Campaign Target",

                DeviceOS = new DeviceOSTarget
                {
                    Bids = new[]
                    {
                            new DeviceOSTargetBid
                            {
                                BidAdjustment = 20,
                                DeviceName = "Computers",
                            },
                        },
                },
            };

            var addTargetsToLibraryResponse = (await AddTargetsToLibraryAsync(new[] { sharedTarget }).ConfigureAwait(continueOnCapturedContext: false));
            var sharedTargetId = addTargetsToLibraryResponse.TargetIds[0];
            OutputStatusMessage(string.Format("Added Target Id: {0}\n", sharedTargetId));

            await SetTargetToCampaignAsync(campaignIds[0], sharedTargetId).ConfigureAwait(continueOnCapturedContext: false);
            OutputStatusMessage(string.Format("Associated CampaignId {0} with TargetId {1}.\n", campaignIds[0], sharedTargetId));
            await SetTargetToCampaignAsync(campaignIds[1], sharedTargetId).ConfigureAwait(continueOnCapturedContext: false);
            OutputStatusMessage(string.Format("Associated CampaignId {0} with TargetId {1}.\n", campaignIds[1], sharedTargetId));
            await SetTargetToCampaignAsync(campaignIds[2], sharedTargetId).ConfigureAwait(continueOnCapturedContext: false);
            OutputStatusMessage(string.Format("Associated CampaignId {0} with TargetId {1}.\n", campaignIds[2], sharedTargetId));
            
            // We can then observe how the target associations are represented using Bing Ads API version 10, Bulk file format 4.0.
            // Each sub target downloaded in this example shares the same target identifier.
            await DownloadTargets(campaignIds).ConfigureAwait(continueOnCapturedContext: false);

            // Now let's download the campaign's target criterions using Bing Ads API version 11, Bulk file format 5.0, 
            // and we can see that with the exception of location intent criterion even the underlying criterions 
            // are shared by all three of the campaigns. 
            await DownloadTargetsAsCriterions(campaignIds).ConfigureAwait(continueOnCapturedContext: false);

            return campaignIds;
        }

        /// <summary>
        /// Demonstrates the results of adding or updating target criterions 
        /// that are shared by more than one campaign.
        /// For more details see the Upgrade Targets to Criterions guide.
        /// </summary>
        /// <param name="authorizationData"></param>
        /// <returns></returns>
        private async Task MigrateTargetCriterionsA(AuthorizationData authorizationData)
        {
            IList<long> campaignIds = await ShareDeprecatedTargets(authorizationData).ConfigureAwait(continueOnCapturedContext: false);
            campaignIdKey = campaignIds[0];

            // Given the above shared target scenario, let's turn attention towards the migration process 
            // whereby the updated campaign will be assigned new criterion identifiers, and the other two campaigns 
            // will keep the original identifiers. 
            // For example let's add a day and time criterion to Campaign One.

            var uploadEntities = new List<BulkEntity>();

            var bulkCampaignDayTimeCriterion = new BulkCampaignDayTimeCriterion
            {
                CampaignCriterion = new BiddableCampaignCriterion
                {
                    CampaignId = campaignIdKey,
                    Criterion = new DayTimeCriterion
                    {
                        Day = Day.Monday,
                        FromHour = 0,
                        ToHour = 4,
                        FromMinute = Minute.Zero,
                        ToMinute = Minute.Zero
                    },
                    CriterionBid = new BidMultiplier
                    {
                        Multiplier = 20,
                    },
                }
            };
            uploadEntities.Add(bulkCampaignDayTimeCriterion);

            OutputStatusMessage("Adding campaign day and time criterion . . . \n");
            var bulkFileReader = await Task.Run(() => WriteEntitiesAndUploadFileAsync(uploadEntities)).ConfigureAwait(continueOnCapturedContext: false);
            bulkFileReader.Dispose();

            // In the result file we can see the following records:
            // 
            //   - One Campaign DeviceOS Criterion record with Status set to Deleted and Id set to 0 (zero).
            //     This record indicates that the previous Campaign DeviceOS Criterion identifiers are no longer valid 
            //     for the corresponding campaign, and you must sync the new identifiers provided below in the same bulk file.
            // 
            //   - One Campaign Location Intent Criterion record with Status set to Deleted and Id set to 0 (zero). 
            //     This record indicates that the previous Campaign Location Intent Criterion identifier is no longer valid 
            //     for the corresponding campaign, and you must sync the new identifiers provided below in the same bulk file.
            //     That said, the location intent criterion identifier is currently always equal to the campaign identifier.
            //     This is subject to change, so it is recommended that you treat it as a unique identifier and follow the 
            //     same workflow as other criterions i.e.sync the new identifier.
            // 
            //   - One Campaign DayTime Criterion record with the unique identifier for the day and time criterion that we added.
            //     Of course if we had added more criterions of any type, a result record with unique identifiers would be included 
            //     for each of them.

            // Let's download all of the criterions again and we can see that the original criterions are still shared by 
            // Campaign Two and Campaign Three. Only the criterions associated with the modified campaign (Campaign One) 
            // were migrated and assigned new identifiers. 
            await DownloadTargetsAsCriterions(campaignIds).ConfigureAwait(continueOnCapturedContext: false);
            
            // Delete i.e. clean up the entities created in this example.
            OutputStatusMessage("Deleting campaigns and criterions . . . \n");
            await DeleteBulkCampaignsAsync(campaignIds).ConfigureAwait(continueOnCapturedContext: false);
            
            return;
        }

        /// <summary>
        /// This example uses the ShareDeprecatedTargets helper method to share a target
        /// with multiple campaigns. Then the underlying criterions are downloaded and 
        /// the same file is uploaded with no changes. The Bulk service assigns new criterion identifiers 
        /// for all except one of the campaigns that it modifies. The original criterion identifiers 
        /// only remain associated to last campaign. 
        /// For more details see the Upgrade Targets to Criterions guide.
        /// </summary>
        /// <param name="authorizationData"></param>
        /// <returns></returns>
        private async Task MigrateTargetCriterionsB(AuthorizationData authorizationData)
        {
            IList<long> campaignIds = await ShareDeprecatedTargets(authorizationData).ConfigureAwait(continueOnCapturedContext: false);
            campaignIdKey = campaignIds[0];

            // This example restricts the migration to criterions of the campaign that was added
            // via ShareDeprecatedTargets. To migrate all shared target criterions in the account,
            // you can set CampaignIds = null.
            var bulkFilePath = await DownloadTargetsAsCriterions(campaignIds).ConfigureAwait(continueOnCapturedContext: false);
            
            var fileUploadParameters = new FileUploadParameters
            {
                ResultFileDirectory = FileDirectory,
                CompressUploadFile = true,
                ResultFileName = ResultFileName,
                OverwriteResultFile = true,
                // Unless you have modified the default setting, 
                // in this case the upload file path will be 'c:\bulk\download.csv'
                UploadFilePath = bulkFilePath,  
                ResponseMode = ResponseMode.ErrorsAndResults
            };
            await BulkServiceManager.UploadFileAsync(fileUploadParameters).ConfigureAwait(continueOnCapturedContext: false);

            // In the result file we can see that the criterions associated with the first two campaigns (Campaign One and Campaign Two) 
            // were migrated and assigned new identifiers.The original criterions (201, 202, and 203) are only associated with 
            // Campaign Three. The Bulk service assigns new criterions to the first entities it modifies, and the original criterion 
            // identifiers only remain associated to last campaign or ad group.

            // Delete i.e. clean up the entities created in this example.
            OutputStatusMessage("Deleting campaigns and criterions . . .\n");
            await DeleteBulkCampaignsAsync(campaignIds).ConfigureAwait(continueOnCapturedContext: false);

            return;
        }

        /// <summary>
        /// Downloads all target criterions in the account. You can use this method 
        /// to sync criterion identifiers and map them to your campaigns and ad groups.
        /// </summary>
        /// <returns>The string result of the Task is the local path to the downloaded bulk file.</returns>
        private async Task<string> DownloadTargets(IList<long> campaignIds)
        {
            var downloadParameters = new DownloadParametersV10
            {
                CampaignIds = campaignIds,
                Entities = BulkDownloadEntityV10.AdGroupTargets |
                           BulkDownloadEntityV10.CampaignTargets,
                ResultFileDirectory = FileDirectory,
                ResultFileName = DownloadFileName,
                OverwriteResultFile = true,
                LastSyncTimeInUTC = null
            };

            OutputStatusMessage("Downloading targets . . . \n");
            return await BulkServiceManagerV10.DownloadFileAsync(downloadParameters).ConfigureAwait(continueOnCapturedContext: false);
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

            // Whether you added targets or criterions you can retrieve the target representation using Bulk API version 10 format version 4,
            // and you can retrieve the criterion representation using Bulk API version 11 format version 5.

            await DownloadTargets(new[] { campaignIdKey } ).ConfigureAwait(continueOnCapturedContext: false);

            await DownloadTargetsAsCriterions(new[] { campaignIdKey }).ConfigureAwait(continueOnCapturedContext: false);

            #endregion Add

            #region Update
            
            uploadEntities = new List<BulkEntity>();

            // We can also target Canada (LocationId = 32) by adding a new location criterion.
            var bulkCampaignLocationCriterion = new BulkCampaignLocationCriterion
            {
                CampaignCriterion = new BiddableCampaignCriterion
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
                CampaignCriterion = new BiddableCampaignCriterion
                {
                    CampaignId = campaignIdKey,
                    Id = campaignLocationIntentCriterionResults[0].CampaignCriterion.Id,
                    Status = CampaignCriterionStatus.Deleted
                }
            };
            uploadEntities.Add(bulkCampaignLocationIntentCriterion);

            // If you do not use the assigned identifier for an existing criterion bid e.g. change bid from 20 to 10 
            // for the EighteenToTwentyFour age range, the attempt will fail with code 1043 i.e. CampaignServiceEntityAlreadyExists.
            var bulkAdGroupAgeCriterion = new BulkAdGroupAgeCriterion
            {
                AdGroupCriterion = new BiddableAdGroupCriterion
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
                adGroupDeviceCriterionResult.AdGroupCriterion.Status = AdGroupCriterionStatus.Deleted;
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

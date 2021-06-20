using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to associate remarketing lists with a new ad group with the Bulk service.
    /// </summary>
    public class BulkRemarketingLists : BulkExampleBase
    {
        public override string Description
        {
            get { return "Bulk Remarketing List Associations | Bulk V13"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                ApiEnvironment environment = ((OAuthDesktopMobileAuthCodeGrant)authorizationData.Authentication).Environment;

                // Used to output the Campaign Management objects within Bulk entities.
                CampaignManagementExampleHelper = new CampaignManagementExampleHelper(
                    OutputStatusMessageDefault: this.OutputStatusMessage);

                BulkServiceManager = new BulkServiceManager(
                    authorizationData: authorizationData,
                    apiEnvironment: environment);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                var tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(TimeoutInMilliseconds);

                var downloadParameters = new DownloadParameters
                {
                    DownloadEntities = new[] { DownloadEntity.RemarketingLists },
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true,
                    LastSyncTimeInUTC = null
                };

                OutputStatusMessage("-----\nDownloading all remarketing lists that the current user can associate with ad groups.");

                var bulkFilePath = await BulkServiceManager.DownloadFileAsync(
                    parameters: downloadParameters,
                    progress: progress,
                    cancellationToken: tokenSource.Token);

                OutputStatusMessage("Download results:");

                Reader = new BulkFileReader(
                    filePath: bulkFilePath,
                    resultFileType: ResultFileType.FullDownload,
                    fileFormat: FileType);
                                
                var downloadEntities = Reader.ReadEntities().ToList();

                var remarketingListResults = downloadEntities.OfType<BulkRemarketingList>().ToList();
                OutputBulkRemarketingLists(remarketingListResults);

                Reader.Dispose();

                // You must have at least one remarketing list. 

                if (remarketingListResults.Count < 1)
                {
                    OutputStatusMessage("There are no remarketing lists in the account.");
                    return;
                }

                var uploadEntities = new List<BulkEntity>();

                // Add an ad group in a campaign. The ad group will later be associated with remarketing lists.

                var bulkCampaign = new BulkCampaign
                {
                    ClientId = "YourClientIdGoesHere",
                    Campaign = new Campaign
                    {
                        Id = campaignIdKey,
                        BudgetType = BudgetLimitType.DailyBudgetStandard,
                        DailyBudget = 50,
                        Languages = new string[] { "All" },
                        Name = "Everyone's Shoes " + DateTime.UtcNow,
                        TimeZone = "PacificTimeUSCanadaTijuana",
                    }
                };
                uploadEntities.Add(bulkCampaign);

                var bulkAdGroup = new BulkAdGroup
                {
                    CampaignId = campaignIdKey,
                    AdGroup = new AdGroup
                    {
                        Id = adGroupIdKey,
                        Name = "Everyone's Red Shoe Sale",
                        StartDate = null,
                        EndDate = new Microsoft.BingAds.V13.CampaignManagement.Date
                        {
                            Month = 12,
                            Day = 31,
                            Year = DateTime.UtcNow.Year + 1
                        },
                        CpcBid = new Bid { Amount = 0.09 },
                        // Applicable for all remarketing lists that are associated with this ad group. TargetAndBid indicates 
                        // that you want to show ads only to people included in the remarketing list, with the option to change
                        // the bid amount. Ads in this ad group will only show to people included in the remarketing list.
                        Settings = new[]
                        {
                            new TargetSetting
                            {
                                Details = new []
                                {
                                    new TargetSettingDetail
                                    {
                                        CriterionTypeGroup = CriterionTypeGroup.Audience,
                                        TargetAndBid = true
                                    }
                                }
                            }
                        },
                    },
                };

                uploadEntities.Add(bulkAdGroup);
                
                // For example, associate all of the remarketing lists with the new ad group.

                foreach (var remarketingList in remarketingListResults)
                {
                    if (remarketingList.RemarketingList != null && remarketingList.RemarketingList.Id != null)
                    {
                        var bulkAdGroupRemarketingListAssociation = new BulkAdGroupRemarketingListAssociation
                        {
                            ClientId = "MyBulkAdGroupRemarketingListAssociation " + remarketingList.RemarketingList.Id,
                            BiddableAdGroupCriterion = new BiddableAdGroupCriterion
                            {
                                AdGroupId = adGroupIdKey,
                                Criterion = new AudienceCriterion
                                {
                                    AudienceId = (long)remarketingList.RemarketingList.Id,
                                    AudienceType = AudienceType.RemarketingList,
                                },
                                CriterionBid = new BidMultiplier
                                {
                                    Multiplier = 20.00,
                                },
                                Status = AdGroupCriterionStatus.Paused,
                            },
                        };
                        uploadEntities.Add(bulkAdGroupRemarketingListAssociation);
                    }
                }

                // Upload and write the output

                OutputStatusMessage("-----\nAdding campaign, ad group, and ad group remarketing list associations...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                
                OutputStatusMessage("Upload results:");

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var adGroupResults = downloadEntities.OfType<BulkAdGroup>().ToList();
                OutputBulkAdGroups(adGroupResults);

                var adGroupRemarketingListResults = downloadEntities.OfType<BulkAdGroupRemarketingListAssociation>().ToList();
                OutputBulkAdGroupRemarketingListAssociations(adGroupRemarketingListResults);

                Reader.Dispose();
                
                // Delete the campaign and everything it contains e.g., ad groups and ads.

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("-----\nDeleting the campaign and everything it contains e.g., ad groups and ads...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());
                OutputBulkNegativeKeywordLists(downloadEntities.OfType<BulkNegativeKeywordList>().ToList());

                Reader.Dispose();
            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V13.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V13.Bulk.ApiFaultDetail> ex)
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
    }
}

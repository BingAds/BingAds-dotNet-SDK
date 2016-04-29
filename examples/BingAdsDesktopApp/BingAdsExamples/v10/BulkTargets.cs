using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V10.Bulk;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;

namespace BingAdsExamples.V10
{
    /// <summary>
    /// This example demonstrates how to add and update targets using the BulkServiceManager class.
    /// </summary>
    public class BulkTargets : BulkExampleBase
    {
        public override string Description
        {
            get { return "Targets | Bulk V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                BulkService = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(String.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                #region Add

                const int targetIdKey = -1;
                const int campaignIdKey = -123;

                var uploadEntities = new List<BulkEntity>();

                // Prepare the bulk entities that you want to upload. Each bulk entity contains the corresponding campaign management object, 
                // and additional elements needed to read from and write to a bulk file. 

                var bulkCampaign = new BulkCampaign
                {
                    // ClientId may be used to associate records in the bulk upload file with records in the results file. The value of this field 
                    // is not used or stored by the server; it is simply copied from the uploaded record to the corresponding result record.
                    // Note: This bulk file Client Id is not related to an application Client Id for OAuth. 
                    ClientId = "YourClientIdGoesHere",
                    Campaign = new Campaign
                    {
                        // When using the Campaign Management service, the Id cannot be set. In the context of a BulkCampaign, the Id is optional 
                        // and may be used as a negative reference key during bulk upload. For example the same negative value set for the campaign Id 
                        // will be used when associating this new campaign with a new call ad extension in the BulkCampaignCallAdExtension object below. 
                        Id = campaignIdKey,
                        Name = "Women's Shoes " + DateTime.UtcNow,
                        Description = "Red shoes line.",
                        BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                        MonthlyBudget = 1000.00,
                        TimeZone = "PacificTimeUSCanadaTijuana",

                        // DaylightSaving is not supported in the Bulk file schema. Whether or not you specify it in a BulkCampaign,
                        // the value is not written to the Bulk file, and by default DaylightSaving is set to true.
                        DaylightSaving = true,
                    }
                };

                // Prepare targets for upload

                var bulkCampaignDayTimeTarget = new BulkCampaignDayTimeTarget
                {
                    CampaignId = campaignIdKey,
                    TargetId = targetIdKey,
                    DayTimeTarget = new DayTimeTarget
                    {
                        Bids = new List<DayTimeTargetBid>
                        {
                            new DayTimeTargetBid
                            {
                                BidAdjustment = 10,
                                Day = Day.Friday,
                                FromHour = 11,
                                FromMinute = Minute.Zero,
                                ToHour = 13,
                                ToMinute = Minute.Fifteen
                            },
                            new DayTimeTargetBid
                            {
                                BidAdjustment = 20,
                                Day = Day.Saturday,
                                FromHour = 11,
                                FromMinute = Minute.Zero,
                                ToHour = 13,
                                ToMinute = Minute.Fifteen
                            }
                        }
                    }
                };

                var bulkCampaignLocationTarget = new BulkCampaignLocationTarget
                {
                    CampaignId = campaignIdKey,
                    TargetId = targetIdKey,

                    IntentOption = IntentOption.PeopleIn,
                    CityTarget = new CityTarget
                    {
                        Bids = new List<CityTargetBid>
                        {
                            new CityTargetBid
                            {
                                BidAdjustment = 15,
                                City = "Toronto, Toronto ON CA",
                                IsExcluded = false
                            }
                        }
                    },
                    CountryTarget = new CountryTarget
                    {
                        Bids = new List<CountryTargetBid>
                        {
                            new CountryTargetBid
                            {
                                BidAdjustment = 15,
                                CountryAndRegion = "CA",
                                IsExcluded = false
                            }
                        }
                    },
                    MetroAreaTarget = new MetroAreaTarget
                    {
                        Bids = new List<MetroAreaTargetBid>
                        {
                            new MetroAreaTargetBid
                            {
                                BidAdjustment = 15,
                                MetroArea = "Seattle-Tacoma, WA, WA US",
                                IsExcluded = false
                            }
                        }
                    },
                    StateTarget = new StateTarget
                    {
                        Bids = new List<StateTargetBid>
                        {
                            new StateTargetBid
                            {
                                BidAdjustment = 15,
                                State = "US-WA",
                                IsExcluded = false
                            }
                        }
                    },
                    PostalCodeTarget = new PostalCodeTarget
                    {
                        Bids = new List<PostalCodeTargetBid>
                        {
                            new PostalCodeTargetBid
                            {
                                // Bid adjustments are not allowed for location exclusions. 
                                // If IsExcluded is true, this element will be ignored.
                                BidAdjustment = 10,
                                PostalCode = "98052, WA US",
                                IsExcluded = false
                            }
                        }
                    }
                };

                var bulkCampaignRadiusTarget = new BulkCampaignRadiusTarget
                {
                    CampaignId = campaignIdKey,
                    TargetId = targetIdKey,

                    RadiusTarget = new RadiusTarget
                    {
                        Bids = new List<RadiusTargetBid>
                        {
                            new RadiusTargetBid
                            {
                                BidAdjustment = 50,
                                LatitudeDegrees = 47.755367,
                                LongitudeDegrees = -122.091827,
                                Radius = 11,
                                RadiusUnit = DistanceUnit.Kilometers,
                                Name = "radius1"
                            },
                            new RadiusTargetBid
                            {
                                BidAdjustment = 20,
                                LatitudeDegrees = 49.755367,
                                LongitudeDegrees = -129.091827,
                                Radius = 12,
                                RadiusUnit = DistanceUnit.Kilometers,
                                Name = "radius2"
                            }
                        }
                    }
                };


                // Upload the entities created above.

                uploadEntities.Add(bulkCampaign);
                uploadEntities.Add(bulkCampaignDayTimeTarget);
                uploadEntities.Add(bulkCampaignLocationTarget);
                uploadEntities.Add(bulkCampaignRadiusTarget);

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                // Upload and write the output

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var campaignDayTimeTargetResults = downloadEntities.OfType<BulkCampaignDayTimeTarget>().ToList();
                OutputBulkCampaignDayTimeTargets(campaignDayTimeTargetResults);

                var campaignLocationTargetResults = downloadEntities.OfType<BulkCampaignLocationTarget>().ToList();
                OutputBulkCampaignLocationTargets(campaignLocationTargetResults);

                var campaignRadiusTargetResults = downloadEntities.OfType<BulkCampaignRadiusTarget>().ToList();
                OutputBulkCampaignRadiusTargets(campaignRadiusTargetResults);

                Reader.Dispose();

                #endregion Add

                #region Update

                // Update the day and time target. 
                // Do not create a BulkCampaignDayTimeTarget for update, unless you want to replace all existing DayTime target bids
                // with the specified day and time target set for the current bulk upload. 
                // Instead you should upload one or more BulkCampaignDayTimeTargetBid.

                var bulkCampaignDayTimeTargetBids = new List<BulkCampaignDayTimeTargetBid>
                {
                    new BulkCampaignDayTimeTargetBid
                    {
                        CampaignId = campaignDayTimeTargetResults[0].CampaignId,
                        TargetId = campaignDayTimeTargetResults[0].TargetId,
                        DayTimeTargetBid = new DayTimeTargetBid
                        {
                            BidAdjustment = 15,
                            Day = Day.Friday,
                            FromHour = 11,
                            FromMinute = Minute.Zero,
                            ToHour = 13,
                            ToMinute = Minute.Fifteen
                        }
                    }
                };

                // Upload the updated target

                uploadEntities = new List<BulkEntity>();

                foreach (var bulkCampaignDayTimeTargetBid in bulkCampaignDayTimeTargetBids)
                {
                    uploadEntities.Add(bulkCampaignDayTimeTargetBid);
                }

                // Upload and write the output

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);

                OutputStatusMessage("Upload Results Bulk File Path" + Reader.BulkFilePath + "\n");
                OutputStatusMessage("Updated Entities\n");

                downloadEntities = Reader.ReadEntities().ToList();

                var campaignDayTimeTargetBidResults = downloadEntities.OfType<BulkCampaignDayTimeTargetBid>().ToList();
                OutputBulkCampaignDayTimeTargetBids(campaignDayTimeTargetBidResults);

                Reader.Dispose();

                #endregion Update

                #region CleanUp

                //Delete the campaign and target associations that were previously added. 
                //You should remove this region if you want to view the added entities in the 
                //Bing Ads web application or another tool.

                //You must set the Id field to the corresponding entity identifier, and the Status field to Deleted.

                //When you delete a BulkCampaign, the dependent entities such as BulkCampaignDayTimeTarget 
                //are deleted without being specified explicitly.  

                //Deleting targets is not supported using the Bulk service.
                //To delete targets you can use the DeleteTargetsFromLibrary operation via the Campaign Management service.

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign, ad group, keywords, and ads . . .\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());

                Reader.Dispose();

                #endregion Cleanup
            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V10.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.Bulk.ApiFaultDetail> ex)
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

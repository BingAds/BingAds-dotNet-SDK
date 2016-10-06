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
using Microsoft.BingAds.CustomerManagement;

using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Entities.AdExtensions;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to add and update ad extensions using the BulkServiceManager class.
    /// </summary>
    public class BulkAdExtensions : BulkExampleBase
    {
        public static ServiceClient<ICampaignManagementService> CampaignService;
        public static ServiceClient<ICustomerManagementService> CustomerService;

        private const string SITELINK_MIGRATION = "SiteLinkAdExtension";

        public override string Description
        {
            get { return "AdExtensions | Bulk V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                BulkService = new BulkServiceManager(authorizationData);
                CustomerService = new ServiceClient<ICustomerManagementService>(authorizationData);
                CampaignService = new ServiceClient<ICampaignManagementService>(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                OutputStatusMessage(String.Format("{0} % Complete", x.PercentComplete.ToString(CultureInfo.InvariantCulture))));


                #region MigrationStatus

                // To prepare for the sitelink ad extensions migration in 2017, you will need to determine
                // whether the account has been migrated from SiteLinksAdExtension to Sitelink2AdExtension. 
                // All ad extension service operations available for both types of sitelinks; however you will 
                // need to determine which type to add, update, and retrieve.

                bool sitelinkMigrationIsCompleted = false;

                // Optionally you can find out which pilot features the customer is able to use. Even if the customer 
                // is in pilot for sitelink migrations, the accounts that it contains might not be migrated.
                var featurePilotFlags = await GetCustomerPilotFeaturesAsync(authorizationData.CustomerId);

                // The pilot flag value for Sitelink ad extension migration is 253.
                // Pilot flags apply to all accounts within a given customer; however,
                // each account goes through migration individually and has its own migration status.
                if (featurePilotFlags.Any(pilotFlag => pilotFlag == 253))
                {
                    // Account migration status below will be either NotStarted, InProgress, or Completed.
                    OutputStatusMessage("Customer is in pilot for Sitelink migration.\n");
                }
                else
                {
                    // Account migration status below will be NotInPilot.
                    OutputStatusMessage("Customer is not in pilot for Sitelink migration.\n");
                }

                // Even if you have multiple accounts per customer, each account will have its own
                // migration status. This example checks one account using the provided AuthorizationData.
                var accountMigrationStatusesInfos = (await GetAccountMigrationStatusesAsync(
                    new long[] { authorizationData.AccountId },
                    SITELINK_MIGRATION
                )).ToArray();

                foreach (var accountMigrationStatusesInfo in accountMigrationStatusesInfos)
                {
                    OutputAccountMigrationStatusesInfo(accountMigrationStatusesInfo);

                    if (accountMigrationStatusesInfo.MigrationStatusInfo.Any(
                        statusInfo =>
                        statusInfo.Status == MigrationStatus.Completed && SITELINK_MIGRATION.CompareTo(statusInfo.MigrationType) == 0))
                    {
                        sitelinkMigrationIsCompleted = true;
                    }
                }

                #endregion MigrationStatus

                #region Add

                // Prepare the bulk entities that you want to upload. Each bulk entity contains the corresponding campaign management object, 
                // and additional elements needed to read from and write to a bulk file. 

                var uploadEntities = new List<BulkEntity>();

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
                        Status = CampaignStatus.Paused,

                        // DaylightSaving is not supported in the Bulk file schema. Whether or not you specify it in a BulkCampaign,
                        // the value is not written to the Bulk file, and by default DaylightSaving is set to true.
                        DaylightSaving = true,

                        // Used with FinalUrls shown in the sitelinks that we will add below.
                        TrackingUrlTemplate =
                            "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}"
                    }
                };

                // Prepare ad extensions for upload

                var bulkAppAdExtension = new BulkAppAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    AppAdExtension = new AppAdExtension
                    {
                        AppPlatform = "Windows",
                        AppStoreId = "AppStoreIdGoesHere",
                        DestinationUrl = "DestinationUrlGoesHere",
                        DisplayText = "Contoso",
                        Id = appAdExtensionIdKey,
                    }
                };

                var bulkCallAdExtension = new BulkCallAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    CallAdExtension = new CallAdExtension
                    {
                        CountryCode = "US",
                        PhoneNumber = "2065550100",
                        IsCallOnly = false,
                        Id = callAdExtensionIdKey,
                        Scheduling = new Schedule
                        {

                            // For this example assume the call center is open Monday - Friday from 9am - 9pm
                            // in the account's time zone.

                            UseSearcherTimeZone = false,
                            DayTimeRanges = new[]
                            {
                                new DayTime
                                {
                                    Day = Day.Monday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                                new DayTime
                                {
                                    Day = Day.Tuesday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                                new DayTime
                                {
                                    Day = Day.Wednesday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                                new DayTime
                                {
                                    Day = Day.Thursday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                                new DayTime
                                {
                                    Day = Day.Friday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 21,
                                    EndMinute = Minute.Zero,
                                },
                            },
                            StartDate = null,
                            EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date
                            {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        }
                    }
                };

                var bulkCalloutAdExtension = new BulkCalloutAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    CalloutAdExtension = new CalloutAdExtension
                    {
                        Text = "Callout Text",
                        Id = calloutAdExtensionIdKey
                    }
                };

                var bulkLocationAdExtension = new BulkLocationAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    LocationAdExtension = new LocationAdExtension
                    {
                        Id = locationAdExtensionIdKey,
                        PhoneNumber = "206-555-0100",
                        CompanyName = "Contoso Shoes",
                        IconMediaId = null,
                        ImageMediaId = null,
                        Address = new Microsoft.BingAds.V10.CampaignManagement.Address
                        {
                            StreetAddress = "1234 Washington Place",
                            StreetAddress2 = "Suite 1210",
                            CityName = "Woodinville",
                            ProvinceName = "WA",
                            CountryCode = "US",
                            PostalCode = "98608"
                        },
                        Scheduling = new Schedule
                        {

                            // For this example assume you want to drive traffic every Saturday morning
                            // in the search user's time zone.

                            UseSearcherTimeZone = true,
                            DayTimeRanges = new[]
                            {
                                new DayTime
                                {
                                    Day = Day.Saturday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 12,
                                    EndMinute = Minute.Zero,
                                },
                            },
                            StartDate = null,
                            EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date
                            {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        }
                    }
                };

                var bulkReviewAdExtension = new BulkReviewAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    ReviewAdExtension = new ReviewAdExtension
                    {
                        IsExact = true,
                        Source = "Review Source Name",
                        Text = "Review Text",
                        Url = "http://review.contoso.com", // The Url of the third-party review. This is not your business Url.
                        Id = reviewAdExtensionIdKey
                    }
                };

                var bulkStructuredSnippetAdExtension = new BulkStructuredSnippetAdExtension
                {
                    AccountId = authorizationData.AccountId,
                    StructuredSnippetAdExtension = new StructuredSnippetAdExtension
                    {
                        Header = "Brands",
                        Values = new[] { "Windows", "Xbox", "Skype" },
                        Id = structuredSnippetAdExtensionIdKey
                    }
                };

                // Prepare ad extension associations for upload

                var bulkCampaignAppAdExtension = new BulkCampaignAppAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = appAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignCallAdExtension = new BulkCampaignCallAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = callAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignCalloutAdExtension = new BulkCampaignCalloutAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = calloutAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignLocationAdExtension = new BulkCampaignLocationAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = locationAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignReviewAdExtension = new BulkCampaignReviewAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = reviewAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };

                var bulkCampaignStructuredSnippetAdExtension = new BulkCampaignStructuredSnippetAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = structuredSnippetAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                };


                // Upload the entities created above.
                // Dependent entities such as BulkCampaignCallAdExtension must be written after any dependencies,  
                // for example the BulkCampaign and BulkCallAdExtension. 

                uploadEntities.Add(bulkCampaign);

                uploadEntities.Add(bulkAppAdExtension);
                uploadEntities.Add(bulkCallAdExtension);
                uploadEntities.Add(bulkCalloutAdExtension);
                uploadEntities.Add(bulkLocationAdExtension);
                uploadEntities.Add(bulkReviewAdExtension);
                uploadEntities.Add(bulkStructuredSnippetAdExtension);

                uploadEntities.Add(bulkCampaignAppAdExtension);
                uploadEntities.Add(bulkCampaignCallAdExtension);
                uploadEntities.Add(bulkCampaignCalloutAdExtension);
                uploadEntities.Add(bulkCampaignLocationAdExtension);
                uploadEntities.Add(bulkCampaignReviewAdExtension);
                uploadEntities.Add(bulkCampaignStructuredSnippetAdExtension);

                // Before migration only the deprecated BulkSiteLinkAdExtension type can be added, 
                // and after migration only the new BulkSitelink2AdExtension type can be added.
                var bulkSLExtensions = (sitelinkMigrationIsCompleted ? (BulkEntity[])
                    GetSampleBulkSitelink2AdExtensions(authorizationData.AccountId) :
                    GetSampleBulkSiteLinkAdExtensions(authorizationData.AccountId)).ToArray();

                foreach(var bulkSLExtension in bulkSLExtensions)
                {
                    uploadEntities.Add(bulkSLExtension);
                }

                OutputStatusMessage("Adding campaign, ad extensions, and associations . . .");

                // Upload and write the output

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var appAdExtensionResults = downloadEntities.OfType<BulkAppAdExtension>().ToList();
                OutputBulkAppAdExtensions(appAdExtensionResults);

                var callAdExtensionResults = downloadEntities.OfType<BulkCallAdExtension>().ToList();
                OutputBulkCallAdExtensions(callAdExtensionResults);

                var calloutAdExtensionResults = downloadEntities.OfType<BulkCalloutAdExtension>().ToList();
                OutputBulkCalloutAdExtensions(calloutAdExtensionResults);

                var imageAdExtensionResults = downloadEntities.OfType<BulkImageAdExtension>().ToList();
                OutputBulkImageAdExtensions(imageAdExtensionResults);

                var locationAdExtensionResults = downloadEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                var reviewAdExtensionResults = downloadEntities.OfType<BulkReviewAdExtension>().ToList();
                OutputBulkReviewAdExtensions(reviewAdExtensionResults);
                
                var structuredSnippetAdExtensionResults = downloadEntities.OfType<BulkStructuredSnippetAdExtension>().ToList();
                OutputBulkStructuredSnippetAdExtensions(structuredSnippetAdExtensionResults);

                // Before migration only the deprecated BulkSiteLinkAdExtension results will be returned, 
                // and after migration only the new BulkSitelink2AdExtension results will be returned.

                var siteLinkAdExtensionResults = downloadEntities.OfType<BulkSiteLinkAdExtension>().ToList();
                OutputBulkSiteLinkAdExtensions(siteLinkAdExtensionResults);
                var sitelink2AdExtensionResults = downloadEntities.OfType<BulkSitelink2AdExtension>().ToList();
                OutputBulkSitelink2AdExtensions(sitelink2AdExtensionResults);
                
                OutputBulkCampaignAdExtensionAssociations(downloadEntities.OfType<BulkCampaignAdExtensionAssociation>().ToList());

                Reader.Dispose();

                #endregion Add

                #region Update

                // Use only the location extension results and remove scheduling.

                uploadEntities = new List<BulkEntity>();

                foreach (var locationAdExtensionResult in locationAdExtensionResults)
                {
                    if (locationAdExtensionResult.LocationAdExtension.Id > 0)
                    {
                        // If you set the Scheduling element null, any existing scheduling set for the ad extension will remain unchanged. 
                        // If you set this to any non-null Schedule object, you are effectively replacing existing scheduling 
                        // for the ad extension. In this example, we will remove any existing scheduling by setting this element  
                        // to an empty Schedule object.
                        // The "delete_value" keyword will be written to the corresponding columns in the bulk file.
                        locationAdExtensionResult.LocationAdExtension.Scheduling = new Schedule();
                        uploadEntities.Add(locationAdExtensionResult);
                    }
                }
                
                OutputStatusMessage("\nRemoving scheduling from location ad extensions . . .\n");

                // Upload and write the output

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                locationAdExtensionResults = downloadEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                Reader.Dispose();

                #endregion Update

                #region CleanUp

                //Delete the campaign and ad extensions that were previously added. 
                //You should remove this region if you want to view the added entities in the 
                //Bing Ads web application or another tool.

                //You must set the Id field to the corresponding entity identifier, and the Status field to Deleted. 

                //When you delete a BulkCampaign or BulkCallAdExtension, dependent entities such as BulkCampaignCallAdExtension 
                //are deleted without being specified explicitly.  

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    if(campaignResult.Campaign != null)
                    {
                        campaignResult.Campaign.Status = CampaignStatus.Deleted;
                        uploadEntities.Add(campaignResult);
                    }
                }

                foreach (var appAdExtensionResult in appAdExtensionResults)
                {
                    //By default the sample does not successfully create any app ad extensions,
                    //because you need to provide details above such as the AppStoreId.
                    if (appAdExtensionResult.AppAdExtension.Id > 0)
                    {
                        appAdExtensionResult.AppAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(appAdExtensionResult);
                    }
                }

                foreach (var callAdExtensionResult in callAdExtensionResults)
                {
                    if (callAdExtensionResult.CallAdExtension.Id > 0)
                    {
                        callAdExtensionResult.CallAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(callAdExtensionResult);
                    }
                }

                foreach (var calloutAdExtensionResult in calloutAdExtensionResults)
                {
                    if (calloutAdExtensionResult.CalloutAdExtension.Id > 0)
                    {
                        calloutAdExtensionResult.CalloutAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(calloutAdExtensionResult);
                    }
                }

                foreach (var imageAdExtensionResult in imageAdExtensionResults)
                {
                    if (imageAdExtensionResult.ImageAdExtension.Id > 0)
                    {
                        imageAdExtensionResult.ImageAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(imageAdExtensionResult);
                    }
                }

                foreach (var locationAdExtensionResult in locationAdExtensionResults)
                {
                    locationAdExtensionResult.LocationAdExtension.Status = AdExtensionStatus.Deleted;
                    uploadEntities.Add(locationAdExtensionResult);
                }

                foreach (var reviewAdExtensionResult in reviewAdExtensionResults)
                {
                    if (reviewAdExtensionResult.ReviewAdExtension.Id > 0)
                    {
                        reviewAdExtensionResult.ReviewAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(reviewAdExtensionResult);
                    }
                }

                foreach (var siteLinkAdExtensionResult in siteLinkAdExtensionResults)
                {
                    if (siteLinkAdExtensionResult.SiteLinksAdExtension.Id > 0)
                    {
                        siteLinkAdExtensionResult.SiteLinksAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(siteLinkAdExtensionResult);
                    }
                }

                foreach (var sitelink2AdExtensionResult in sitelink2AdExtensionResults)
                {
                    if (sitelink2AdExtensionResult.Sitelink2AdExtension.Id > 0)
                    {
                        sitelink2AdExtensionResult.Sitelink2AdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(sitelink2AdExtensionResult);
                    }
                }

                foreach (var structuredSnippetAdExtensionResult in structuredSnippetAdExtensionResults)
                {
                    if (structuredSnippetAdExtensionResult.StructuredSnippetAdExtension.Id > 0)
                    {
                        structuredSnippetAdExtensionResult.StructuredSnippetAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(structuredSnippetAdExtensionResult);
                    }
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign and ad extensions . . .\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());
                OutputBulkAppAdExtensions(downloadEntities.OfType<BulkAppAdExtension>().ToList());
                OutputBulkCallAdExtensions(downloadEntities.OfType<BulkCallAdExtension>().ToList());
                OutputBulkCalloutAdExtensions(downloadEntities.OfType<BulkCalloutAdExtension>().ToList());
                OutputBulkImageAdExtensions(downloadEntities.OfType<BulkImageAdExtension>().ToList());
                OutputBulkLocationAdExtensions(downloadEntities.OfType<BulkLocationAdExtension>().ToList());
                OutputBulkReviewAdExtensions(downloadEntities.OfType<BulkReviewAdExtension>().ToList());
                OutputBulkSiteLinkAdExtensions(downloadEntities.OfType<BulkSiteLinkAdExtension>().ToList());
                OutputBulkSitelink2AdExtensions(downloadEntities.OfType<BulkSitelink2AdExtension>().ToList());
                OutputBulkStructuredSnippetAdExtensions(downloadEntities.OfType<BulkStructuredSnippetAdExtension>().ToList());
                Reader.Dispose();

                #endregion Cleanup

            }
            #region CatchExceptions
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            // Catch Customer Management service exceptions
            catch (FaultException<Microsoft.BingAds.CustomerManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.CustomerManagement.ApiFault> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
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
            #endregion CatchExceptions 
        }

        // Gets the account's migration statuses.

        private async Task<IEnumerable<AccountMigrationStatusesInfo>> GetAccountMigrationStatusesAsync(
            long[] accountIds,
            string migrationType)
        {
            var request = new GetAccountMigrationStatusesRequest
            {
                AccountIds = accountIds,
                MigrationType = migrationType
            };

            return (await CampaignService.CallAsync((s, r) => s.GetAccountMigrationStatusesAsync(r), request)).MigrationStatuses;
        }

        /// <summary>
        /// Gets the list of pilot features that the customer is able to use.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<IList<int>> GetCustomerPilotFeaturesAsync(long customerId)
        {
            var request = new GetCustomerPilotFeaturesRequest
            {
                CustomerId = customerId
            };

            return (await CustomerService.CallAsync((s, r) => s.GetCustomerPilotFeaturesAsync(r), request)).FeaturePilotFlags.ToArray();
        }

        // Gets example BulkSiteLinkAdExtension and BulkCampaignSiteLinkAdExtension objects. You can use 
        // this type of ad extension if your account has not yet been migrated to BulkSitelink2AdExtension.
        private BulkEntity[] GetSampleBulkSiteLinkAdExtensions(long accountId)
        {
            return new BulkEntity[] {
                // Note that when written to file using the BulkFileWriter, an extra Sitelink Ad Extension record with Deleted
                // status precedes the actual site link record or records that you want to upload. All bulk entities 
                // that are derived from MultiRecordBulkEntiy are preceded with a Deleted record using the BulkFileWriter. 
                // In this example it is a moot point because we are creating a new ad extension. If the specified
                // ad extension Id already exists in your account, the Deleted record effectively deletes the existing
                // extension and replaces it with the SiteLinksAdExtension specified below.
                new BulkSiteLinkAdExtension
                {
                    AccountId = accountId,
                    SiteLinksAdExtension = new SiteLinksAdExtension
                    {
                        // Note that if you do not specify a negative Id as reference key, each of SiteLinks items will
                        // be split during upload into separate sitelink ad extensions with unique ad extension identifiers.
                        Id = siteLinksAdExtensionIdKey,
                        SiteLinks = new List<SiteLink>
                        {
                            new SiteLink
                            {
                                DisplayText = "Women's Shoe Sale 1",

                                // If you are currently using Destination URLs, you must replace them with Final URLs. 
                                // Here is an example of a DestinationUrl you might have used previously. 
                                // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                                // To migrate from DestinationUrl to FinalUrls for existing sitelinks, you can set DestinationUrl
                                // to an empty string when updating the sitelink. If you are removing DestinationUrl,
                                // then FinalUrls is required.
                                // DestinationUrl = "",

                                // With FinalUrls you can separate the tracking template, custom parameters, and 
                                // landing page URLs. 
                                FinalUrls = new[] {
                                    "http://www.contoso.com/womenshoesale"
                                },
                                // Final Mobile URLs can also be used if you want to direct the user to a different page 
                                // for mobile devices.
                                FinalMobileUrls = new[] {
                                    "http://mobile.contoso.com/womenshoesale"
                                }, 
                                // You could use a tracking template which would override the campaign level
                                // tracking template. Tracking templates defined for lower level entities 
                                // override those set for higher level entities.
                                // In this example we are using the campaign level tracking template.
                                TrackingUrlTemplate = null,

                                // Set custom parameters that are specific to this sitelink, 
                                // and can be used by the sitelink, ad group, campaign, or account level tracking template. 
                                // In this example we are using the campaign level tracking template.
                                UrlCustomParameters = new CustomParameters {
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
                            new SiteLink
                            {
                                DisplayText = "Women's Shoe Sale 2",

                                // If you are currently using Destination URLs, you must replace them with Final URLs. 
                                // Here is an example of a DestinationUrl you might have used previously. 
                                // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                                // To migrate from DestinationUrl to FinalUrls for existing sitelinks, you can set DestinationUrl
                                // to an empty string when updating the sitelink. If you are removing DestinationUrl,
                                // then FinalUrls is required.
                                // DestinationUrl = "",

                                // With FinalUrls you can separate the tracking template, custom parameters, and 
                                // landing page URLs. 
                                FinalUrls = new[] {
                                    "http://www.contoso.com/womenshoesale"
                                },
                                // Final Mobile URLs can also be used if you want to direct the user to a different page 
                                // for mobile devices.
                                FinalMobileUrls = new[] {
                                    "http://mobile.contoso.com/womenshoesale"
                                }, 
                                // You could use a tracking template which would override the campaign level
                                // tracking template. Tracking templates defined for lower level entities 
                                // override those set for higher level entities.
                                // In this example we are using the campaign level tracking template.
                                TrackingUrlTemplate = null,

                                // Set custom parameters that are specific to this sitelink, 
                                // and can be used by the sitelink, ad group, campaign, or account level tracking template. 
                                // In this example we are using the campaign level tracking template.
                                UrlCustomParameters = new CustomParameters {
                                    Parameters = new[] {
                                        new CustomParameter(){
                                            Key = "promoCode",
                                            Value = "PROMO2"
                                        },
                                        new CustomParameter(){
                                            Key = "season",
                                            Value = "summer"
                                        },
                                    }
                                },
                            }
                        }
                    },
                    // Note that BulkSiteLinkAdExtension.SiteLinks is read only and only 
                    // accessible when reading results from the download or upload results file.
                    // To upload new site links for a new site links ad extension, you should specify
                    // BulkSiteLinkAdExtension.SiteLinksAdExtension.SiteLinks as shown above.
                },
                new BulkCampaignSiteLinkAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = siteLinksAdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                }
            };
        }

        // Gets example BulkSitelink2AdExtension and BulkCampaignSitelink2AdExtension objects. You can use 
        // this type of ad extension if your account has been migrated to BulkSitelink2AdExtension.
        private BulkEntity[] GetSampleBulkSitelink2AdExtensions(long accountId)
        {
            return new BulkEntity[] {
                new BulkSitelink2AdExtension
                {
                    AccountId = accountId,
                    Sitelink2AdExtension = new Sitelink2AdExtension {
                        Id = sitelink2AdExtensionIdKey,
                        Description1 = "Simple & Transparent.",
                        Description2 = "No Upfront Cost.",
                        DisplayText = "Women's Shoe Sale 1",

                        // If you are currently using Destination URLs, you must replace them with Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously. 
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // To migrate from DestinationUrl to FinalUrls, you can set DestinationUrl
                        // to an empty string when updating the ad extension. If you are removing DestinationUrl,
                        // then FinalUrls is required.
                        // DestinationUrl = "",

                        // With FinalUrls you can separate the tracking template, custom parameters, and 
                        // landing page URLs. 
                        FinalUrls = new[] {
                            "http://www.contoso.com/womenshoesale"
                        },
                        // Final Mobile URLs can also be used if you want to direct the user to a different page 
                        // for mobile devices.
                        FinalMobileUrls = new[] {
                            "http://mobile.contoso.com/womenshoesale"
                        },
                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this ad extension, 
                        // and can be used by the ad extension, ad group, campaign, or account level tracking template. 
                        // In this example we are using the campaign level tracking template.
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
                },
                new BulkSitelink2AdExtension
                {
                    AccountId = accountId,
                    Sitelink2AdExtension = new Sitelink2AdExtension
                    {
                        Id = sitelink2AdExtensionIdKey,
                        Description1 = "Do Amazing Things With Contoso.",
                        Description2 = "Read Our Case Studies.",
                        DisplayText = "Women's Shoe Sale 2",

                        // If you are currently using Destination URLs, you must replace them with Final URLs. 
                        // Here is an example of a DestinationUrl you might have used previously. 
                        // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

                        // To migrate from DestinationUrl to FinalUrls, you can set DestinationUrl
                        // to an empty string when updating the ad extension. If you are removing DestinationUrl,
                        // then FinalUrls is required.
                        // DestinationUrl = "",

                        // With FinalUrls you can separate the tracking template, custom parameters, and 
                        // landing page URLs. 
                        FinalUrls = new[] {
                            "http://www.contoso.com/womenshoesale"
                        },
                        // Final Mobile URLs can also be used if you want to direct the user to a different page 
                        // for mobile devices.
                        FinalMobileUrls = new[] {
                            "http://mobile.contoso.com/womenshoesale"
                        },

                        Scheduling = new Schedule {

                            // For this example assume you want to drive traffic every Saturday morning
                            // in the search user's time zone.

                            UseSearcherTimeZone = true,
                            DayTimeRanges = new[]
                            {
                                new DayTime
                                {
                                    Day = Day.Saturday,
                                    StartHour = 9,
                                    StartMinute = Minute.Zero,
                                    EndHour = 12,
                                    EndMinute = Minute.Zero,
                                },
                            },
                            StartDate = null,
                            EndDate = new Microsoft.BingAds.V10.CampaignManagement.Date {
                                Month = 12,
                                Day = 31,
                                Year = DateTime.UtcNow.Year + 1
                            },
                        },

                        // You could use a tracking template which would override the campaign level
                        // tracking template. Tracking templates defined for lower level entities 
                        // override those set for higher level entities.
                        // In this example we are using the campaign level tracking template.
                        TrackingUrlTemplate = null,

                        // Set custom parameters that are specific to this ad extension, 
                        // and can be used by the ad extension, ad group, campaign, or account level tracking template. 
                        // In this example we are using the campaign level tracking template.
                        UrlCustomParameters = new CustomParameters
                        {
                            Parameters = new[] {
                                new CustomParameter(){
                                    Key = "promoCode",
                                    Value = "PROMO2"
                                },
                                new CustomParameter(){
                                    Key = "season",
                                    Value = "summer"
                                },
                            }
                        },
                    }
                },
                new BulkCampaignSitelink2AdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = sitelink2AdExtensionIdKey,
                        EntityId = campaignIdKey
                    }
                }
            };
        }
    }
}

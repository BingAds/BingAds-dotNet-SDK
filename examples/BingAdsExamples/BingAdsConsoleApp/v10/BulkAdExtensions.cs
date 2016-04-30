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


namespace BingAdsConsoleApp.V10
{
    /// <summary>
    /// This example demonstrates how to add and update ad extensions using the BulkServiceManager class.
    /// </summary>
    public class BulkAdExtensions : BulkExampleBase
    {
        public override string Description
        {
            get { return "AdExtensions | Bulk V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                BulkService = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                OutputStatusMessage(String.Format("{0} % Complete", x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

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
                        Id = callAdExtensionIdKey
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
                        Address = new Address
                        {
                            StreetAddress = "1234 Washington Place",
                            StreetAddress2 = "Suite 1210",
                            CityName = "Woodinville",
                            ProvinceName = "WA",
                            CountryCode = "US",
                            PostalCode = "98608"
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

                // Note that when written to file using the BulkFileWriter, an extra Sitelink Ad Extension record with Deleted
                // status precedes the actual site link record or records that you want to upload. All bulk entities 
                // that are derived from MultiRecordBulkEntiy are preceded with a Deleted record using the BulkFileWriter. 
                // In this example it is a moot point because we are creating a new ad extension. If the specified
                // ad extension Id already exists in your account, the Deleted record effectively deletes the existing
                // extension and replaces it with the SiteLinksAdExtension specified below.

                var bulkSiteLinkAdExtension = new BulkSiteLinkAdExtension
                {
                    AccountId = authorizationData.AccountId,
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
                
                var bulkCampaignSiteLinkAdExtension = new BulkCampaignSiteLinkAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = siteLinksAdExtensionIdKey,
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
                uploadEntities.Add(bulkSiteLinkAdExtension);

                uploadEntities.Add(bulkCampaignAppAdExtension);
                uploadEntities.Add(bulkCampaignCallAdExtension);
                uploadEntities.Add(bulkCampaignCalloutAdExtension);
                uploadEntities.Add(bulkCampaignLocationAdExtension);
                uploadEntities.Add(bulkCampaignReviewAdExtension);
                uploadEntities.Add(bulkCampaignSiteLinkAdExtension);
                
                OutputStatusMessage("\nAdding campaign, ad extensions, and associations . . .\n");

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

                var locationAdExtensionResults = downloadEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                var reviewAdExtensionResults = downloadEntities.OfType<BulkReviewAdExtension>().ToList();
                OutputBulkReviewAdExtensions(reviewAdExtensionResults);

                var siteLinkAdExtensionResults = downloadEntities.OfType<BulkSiteLinkAdExtension>().ToList();
                OutputBulkSiteLinkAdExtensions(siteLinkAdExtensionResults);

                OutputBulkCampaignAppAdExtensions(downloadEntities.OfType<BulkCampaignAppAdExtension>().ToList());
                OutputBulkCampaignCallAdExtensions(downloadEntities.OfType<BulkCampaignCallAdExtension>().ToList());
                OutputBulkCampaignCalloutAdExtensions(downloadEntities.OfType<BulkCampaignCalloutAdExtension>().ToList());
                OutputBulkCampaignLocationAdExtensions(downloadEntities.OfType<BulkCampaignLocationAdExtension>().ToList());
                OutputBulkCampaignReviewAdExtensions(downloadEntities.OfType<BulkCampaignReviewAdExtension>().ToList());
                OutputBulkCampaignSiteLinkAdExtensions(downloadEntities.OfType<BulkCampaignSiteLinkAdExtension>().ToList());

                Reader.Dispose();

                #endregion Add

                #region Update

                // Update the site links ad extension. 
                // Add an additional site link, and update an existing site link

                // Do not create a BulkSiteLinkAdExtension for update, unless you want to replace all existing SiteLinks
                // with the specified SiteLinks for the specified ad extension. 
                // Instead you should upload one or more site links as a list of BulkSiteLink.

                var siteLinkAdExtensionId = siteLinkAdExtensionResults[0].SiteLinksAdExtension.Id;
                
                var bulkSiteLinks = new List<BulkSiteLink>
                {
                    siteLinkAdExtensionResults[0].SiteLinks[0],
                    new BulkSiteLink 
                    {
                        AccountId = authorizationData.AccountId,
                        AdExtensionId = siteLinkAdExtensionId,
                        Order = 3,
                        SiteLink = new SiteLink
                        {
                            
                            DisplayText = "Women's Shoe Sale 3",

                            // If you are currently using Destination URLs, you must replace them with Final URLs. 
                            // Here is an example of a DestinationUrl you might have used previously. 
                            // DestinationUrl = "http://www.contoso.com/womenshoesale/?season=spring&promocode=PROMO123",

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
                                        Value = "PROMO3"
                                    },
                                    new CustomParameter(){
                                        Key = "season",
                                        Value = "summer"
                                    },
                                }
                            },
                        }
                    },
                };

                // To remove a subset of custom parameters, specify the custom parameters that 
                // you want to keep in the Parameters element of the CustomParameters object.
                                
                bulkSiteLinks[0].SiteLink.UrlCustomParameters = new CustomParameters {
                    Parameters = new[] {
                        new CustomParameter(){
                            Key = "promoCode",
                            Value = "updatedpromo"
                        },
                    }
                };

                // Write the new site link and updated site link to the file

                uploadEntities = new List<BulkEntity>();

                foreach (var bulkSiteLink in bulkSiteLinks)
                {
                    uploadEntities.Add(bulkSiteLink);
                }

                OutputStatusMessage("\nUpdating sitelinks . . .\n");

                // Upload and write the output

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                var siteLinkResults = downloadEntities.OfType<BulkSiteLink>().ToList();
                OutputBulkSiteLinks(siteLinkResults);

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
                    campaignResult.Campaign.Status = CampaignStatus.Deleted;
                    uploadEntities.Add(campaignResult);
                }

                foreach (var appAdExtensionResult in appAdExtensionResults)
                {
                    appAdExtensionResult.AppAdExtension.Status = AdExtensionStatus.Deleted;
                    //By default the sample does not successfully create any app ad extensions,
                    //because you need to provide details such as the AppStoreId.
                    //You can uncomment the following line if you added an app ad extension above.
                    //uploadEntities.Add(appAdExtensionResult); 
                }

                foreach (var callAdExtensionResult in callAdExtensionResults)
                {
                    callAdExtensionResult.CallAdExtension.Status = AdExtensionStatus.Deleted;
                    uploadEntities.Add(callAdExtensionResult);
                }

                foreach (var calloutAdExtensionResult in calloutAdExtensionResults)
                {
                    calloutAdExtensionResult.CalloutAdExtension.Status = AdExtensionStatus.Deleted;
                    uploadEntities.Add(calloutAdExtensionResult);
                }

                foreach (var locationAdExtensionResult in locationAdExtensionResults)
                {
                    locationAdExtensionResult.LocationAdExtension.Status = AdExtensionStatus.Deleted;
                    uploadEntities.Add(locationAdExtensionResult);
                }

                foreach (var reviewAdExtensionResult in reviewAdExtensionResults)
                {
                    reviewAdExtensionResult.ReviewAdExtension.Status = AdExtensionStatus.Deleted;
                    uploadEntities.Add(reviewAdExtensionResult);
                }

                foreach (var siteLinkAdExtensionResult in siteLinkAdExtensionResults)
                {
                    siteLinkAdExtensionResult.SiteLinksAdExtension.Status = AdExtensionStatus.Deleted;
                    uploadEntities.Add(siteLinkAdExtensionResult);
                }

                // Upload and write the output

                OutputStatusMessage("\nDeleting campaign and ad extensions . . .\n");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();
                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());
                OutputBulkAppAdExtensions(downloadEntities.OfType<BulkAppAdExtension>().ToList());
                OutputBulkCallAdExtensions(downloadEntities.OfType<BulkCallAdExtension>().ToList());
                OutputBulkCalloutAdExtensions(downloadEntities.OfType<BulkCalloutAdExtension>().ToList());
                OutputBulkLocationAdExtensions(downloadEntities.OfType<BulkLocationAdExtension>().ToList());
                OutputBulkReviewAdExtensions(downloadEntities.OfType<BulkReviewAdExtension>().ToList());
                OutputBulkSiteLinkAdExtensions(downloadEntities.OfType<BulkSiteLinkAdExtension>().ToList());
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

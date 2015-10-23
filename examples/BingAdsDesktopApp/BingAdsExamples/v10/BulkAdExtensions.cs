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

                const int appAdExtensionIdKey = -11;
                const int callAdExtensionIdKey = -12;
                const int locationAdExtensionIdKey = -13;
                const int siteLinksAdExtensionIdKey = -14;
                const int campaignIdKey = -123;

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

                                            // Destination URLs are deprecated and will be sunset in March 2016. 
                                            // If you are currently using the Destination URL, you must upgrade to Final URLs. 
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

                                            // Destination URLs are deprecated and will be sunset in March 2016. 
                                            // If you are currently using the Destination URL, you must upgrade to Final URLs. 
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
                    }
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

                var bulkCampaignLocationAdExtension = new BulkCampaignLocationAdExtension
                {
                    AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                    {
                        AdExtensionId = locationAdExtensionIdKey,
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


                // Write the entities created above, to the specified file.
                // Dependent entities such as BulkCampaignCallAdExtension must be written after any dependencies,  
                // for example the BulkCampaign and BulkCallAdExtension. 

                Writer = new BulkFileWriter(FileDirectory + UploadFileName);

                Writer.WriteEntity(bulkCampaign);

                Writer.WriteEntity(bulkAppAdExtension);
                Writer.WriteEntity(bulkCallAdExtension);
                Writer.WriteEntity(bulkLocationAdExtension);
                Writer.WriteEntity(bulkSiteLinkAdExtension);

                Writer.WriteEntity(bulkCampaignAppAdExtension);
                Writer.WriteEntity(bulkCampaignCallAdExtension);
                Writer.WriteEntity(bulkCampaignLocationAdExtension);
                Writer.WriteEntity(bulkCampaignSiteLinkAdExtension);


                Writer.Dispose();

                var fileUploadParameters = new FileUploadParameters
                {
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = ResultFileName,
                    OverwriteResultFile = true,
                    UploadFilePath = FileDirectory + UploadFileName,
                    ResponseMode = ResponseMode.ErrorsAndResults
                };

                // UploadFileAsync will upload the file you finished writing and will download the results file

                OutputStatusMessage("\nAdding campaign, ad extensions, and associations . . .\n");
                var bulkFilePath = await BulkService.UploadFileAsync(fileUploadParameters, progress, CancellationToken.None);
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.Upload, FileType);
                OutputStatusMessage("Upload Results Bulk File Path: " + Reader.BulkFilePath + "\n");

                // Write the upload output

                var bulkEntities = Reader.ReadEntities().ToList();

                var campaignResults = bulkEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var appAdExtensionResults = bulkEntities.OfType<BulkAppAdExtension>().ToList();
                OutputBulkAppAdExtensions(appAdExtensionResults);

                var callAdExtensionResults = bulkEntities.OfType<BulkCallAdExtension>().ToList();
                OutputBulkCallAdExtensions(callAdExtensionResults);

                var locationAdExtensionResults = bulkEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                var siteLinkAdExtensionResults = bulkEntities.OfType<BulkSiteLinkAdExtension>().ToList();
                OutputBulkSiteLinkAdExtensions(siteLinkAdExtensionResults);

                OutputBulkCampaignAppAdExtensions(bulkEntities.OfType<BulkCampaignAppAdExtension>().ToList());
                OutputBulkCampaignCallAdExtensions(bulkEntities.OfType<BulkCampaignCallAdExtension>().ToList());
                OutputBulkCampaignLocationAdExtensions(bulkEntities.OfType<BulkCampaignLocationAdExtension>().ToList());
                OutputBulkCampaignSiteLinkAdExtensions(bulkEntities.OfType<BulkCampaignSiteLinkAdExtension>().ToList());

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

                            // Destination URLs are deprecated and will be sunset in March 2016. 
                            // If you are currently using the Destination URL, you must upgrade to Final URLs. 
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

                Writer = new BulkFileWriter(FileDirectory + UploadFileName);

                foreach (var bulkSiteLink in bulkSiteLinks)
                {
                    Writer.WriteEntity(bulkSiteLink);
                }

                Writer.Dispose();

                fileUploadParameters = new FileUploadParameters
                {
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = ResultFileName,
                    OverwriteResultFile = true,
                    UploadFilePath = FileDirectory + UploadFileName,
                    ResponseMode = ResponseMode.ErrorsAndResults
                };

                // UploadFileAsync will upload the file you finished writing and will download the results file

                OutputStatusMessage("\nUpdating sitelinks . . .\n");
                bulkFilePath = await BulkService.UploadFileAsync(fileUploadParameters, progress, CancellationToken.None);
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.Upload, FileType);
                OutputStatusMessage("Upload Results Bulk File Path: " + Reader.BulkFilePath + "\n");

                // Write any upload errors

                bulkEntities = Reader.ReadEntities().ToList();
                var siteLinkResults = bulkEntities.OfType<BulkSiteLink>().ToList();
                OutputBulkSiteLinks(siteLinkResults);

                Reader.Dispose();

                #endregion Update

                #region Delete

                // Prepare the bulk entities that you want to delete. You must set the Id field to the corresponding 
                // entity identifier, and the Status field to Deleted. 

                var campaignId = campaignResults[0].Campaign.Id;
                bulkCampaign = new BulkCampaign
                {
                    Campaign = new Campaign
                    {
                        Id = campaignId,
                        Status = CampaignStatus.Deleted
                    }
                };

                var appAdExtensionId = appAdExtensionResults[0].AppAdExtension.Id;
                bulkAppAdExtension = new BulkAppAdExtension
                {
                    AppAdExtension = new AppAdExtension
                    {
                        Id = appAdExtensionId,
                        Status = AdExtensionStatus.Deleted
                    }
                };

                var callAdExtensionId = callAdExtensionResults[0].CallAdExtension.Id;
                bulkCallAdExtension = new BulkCallAdExtension
                {
                    CallAdExtension = new CallAdExtension
                    {
                        Id = callAdExtensionId,
                        Status = AdExtensionStatus.Deleted
                    }
                };

                var locationAdExtensionId = locationAdExtensionResults[0].LocationAdExtension.Id;
                bulkLocationAdExtension = new BulkLocationAdExtension
                {
                    LocationAdExtension = new LocationAdExtension
                    {
                        Id = locationAdExtensionId,
                        Status = AdExtensionStatus.Deleted
                    }
                };


                bulkSiteLinkAdExtension = new BulkSiteLinkAdExtension
                {
                    SiteLinksAdExtension = new SiteLinksAdExtension
                    {
                        Id = siteLinkAdExtensionId,
                        Status = AdExtensionStatus.Deleted
                    }
                };

                // Write the entities that you want deleted, to the specified file.
                // Dependent entities such as BulkCampaignCallAdExtension are deleted without being specified explicitly.  
                // For example, if you delete either BulkCampaign or BulkCallAdExtension, then the equivalent of 
                // BulkCampaignCallAdExtension is effectively deleted. 

                Writer = new BulkFileWriter(FileDirectory + UploadFileName);

                Writer.WriteEntity(bulkCampaign);

                Writer.WriteEntity(bulkAppAdExtension);
                Writer.WriteEntity(bulkCallAdExtension);
                Writer.WriteEntity(bulkLocationAdExtension);
                Writer.WriteEntity(bulkSiteLinkAdExtension);


                Writer.Dispose();

                fileUploadParameters = new FileUploadParameters
                {
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = ResultFileName,
                    OverwriteResultFile = true,
                    UploadFilePath = FileDirectory + UploadFileName,
                    ResponseMode = ResponseMode.ErrorsAndResults
                };

                // UploadFileAsync will upload the file you finished writing and will download the results file

                OutputStatusMessage("\nDeleting campaign and ad extensions . . .\n");
                bulkFilePath = await BulkService.UploadFileAsync(fileUploadParameters, progress, CancellationToken.None);
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.Upload, FileType);
                OutputStatusMessage("Upload Results Bulk File Path: " + Reader.BulkFilePath + "\n");

                // Write the upload output

                bulkEntities = Reader.ReadEntities().ToList();

                campaignResults = bulkEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                appAdExtensionResults = bulkEntities.OfType<BulkAppAdExtension>().ToList();
                OutputBulkAppAdExtensions(appAdExtensionResults);

                callAdExtensionResults = bulkEntities.OfType<BulkCallAdExtension>().ToList();
                OutputBulkCallAdExtensions(callAdExtensionResults);

                locationAdExtensionResults = bulkEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                siteLinkAdExtensionResults = bulkEntities.OfType<BulkSiteLinkAdExtension>().ToList();
                OutputBulkSiteLinkAdExtensions(siteLinkAdExtensionResults);

                OutputBulkCampaignAppAdExtensions(bulkEntities.OfType<BulkCampaignAppAdExtension>().ToList());
                OutputBulkCampaignCallAdExtensions(bulkEntities.OfType<BulkCampaignCallAdExtension>().ToList());
                OutputBulkCampaignLocationAdExtensions(bulkEntities.OfType<BulkCampaignLocationAdExtension>().ToList());
                OutputBulkCampaignSiteLinkAdExtensions(bulkEntities.OfType<BulkCampaignSiteLinkAdExtension>().ToList());

                Reader.Dispose();

                #endregion Delete

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

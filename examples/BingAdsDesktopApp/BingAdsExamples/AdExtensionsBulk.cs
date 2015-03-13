using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.Bulk;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;


namespace BingAdsExamples
{
    /// <summary>
    /// This example demonstrates how to add and update ad extensions using the BulkServiceManager class.
    /// </summary>
    public class AdExtensionsBulk : BulkExampleBase
    {
        public override string Description
        {
            get { return "BulkServiceManager | Ad Extensions"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                BulkService = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                OutputStatusMessage(String.Format("{0} % Complete", x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                #region Add

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
                                DaylightSaving = true
                            }
                    };

                // Prepare ad extensions for upload

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
                                                DestinationUrl = "Contoso.com",
                                                DisplayText = "Women's Shoe Sale 1"
                                            },
                                        new SiteLink
                                            {
                                                DestinationUrl = "Contoso.com/WomenShoeSale/2",
                                                DisplayText = "Women's Shoe Sale 2"
                                            }
                                    }
                            }
                        // Note that BulkSiteLinkAdExtension.SiteLinks is read only and only 
                        // accessible when reading results from the download or upload results file.
                        // To upload new site links for a new site links ad extension, you should specify
                        // BulkSiteLinkAdExtension.SiteLinksAdExtension.SiteLinks as shown above.
                    };

                // Prepare ad extension associations for upload
                
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

                Writer.WriteEntity(bulkCallAdExtension);
                Writer.WriteEntity(bulkLocationAdExtension);
                Writer.WriteEntity(bulkSiteLinkAdExtension);

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
                
                OutputStatusMessage("Starting UploadFileAsync . . .\n");
                var bulkFilePath = await BulkService.UploadFileAsync(fileUploadParameters, progress, CancellationToken.None);
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.Upload, FileType);
                OutputStatusMessage("Upload Results Bulk File Path: " + Reader.BulkFilePath + "\n");
                OutputStatusMessage("Added Entities\n");
                
                // Write the upload output

                var bulkEntities = Reader.ReadEntities().ToList();

                var campaignResults = bulkEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var callAdExtensionResults = bulkEntities.OfType<BulkCallAdExtension>().ToList();
                OutputBulkCallAdExtensions(callAdExtensionResults);

                var locationAdExtensionResults = bulkEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                var siteLinkAdExtensionResults = bulkEntities.OfType<BulkSiteLinkAdExtension>().ToList();
                OutputBulkSiteLinkAdExtensions(siteLinkAdExtensionResults);

                Reader.Dispose();

                #endregion Add

                #region Update

                // Update the site links ad extension. 
                // Do not create a BulkSiteLinkAdExtension for update, unless you want to replace all existing SiteLinks
                // with the specified SiteLinks for the specified ad extension. 
                // Instead you should upload one or more site links as a list of BulkSiteLink.

                var bulkSiteLinks = new List<BulkSiteLink>
                    {
                        new BulkSiteLink
                            {
                                SiteLink = new SiteLink
                                    {
                                        DestinationUrl = "Contoso.com",
                                        DisplayText = "Red Shoe Sale"
                                    }
                            }
                    };

                // Add an additional site link, and update an existing site link

                if (siteLinkAdExtensionResults.ToArray().Any() &&
                    siteLinkAdExtensionResults.ToArray()[0].SiteLinks.ToArray().Any())
                {
                    var existingSiteLink = siteLinkAdExtensionResults.ToArray()[0].SiteLinks[0];
                    existingSiteLink.SiteLink.DisplayText = "Red Shoes Super Sale";

                    // Associate the new site links with the identifier of the existing site links ad extension

                    foreach (var bulkSiteLink in bulkSiteLinks)
                    {
                        bulkSiteLink.AdExtensionId = existingSiteLink.AdExtensionId;
                    }

                    bulkSiteLinks.Add(existingSiteLink);
                }

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

                OutputStatusMessage("Starting UploadFileAsync . . .\n");
                bulkFilePath = await BulkService.UploadFileAsync(fileUploadParameters, progress, CancellationToken.None);
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.Upload, FileType);
                OutputStatusMessage("Upload Results Bulk File Path: " + Reader.BulkFilePath + "\n");
                OutputStatusMessage("Updated Entities\n");
                
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


                var siteLinkAdExtensionId = siteLinkAdExtensionResults[0].SiteLinksAdExtension.Id;
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

                OutputStatusMessage("Starting UploadFileAsync . . .\n");
                bulkFilePath = await BulkService.UploadFileAsync(fileUploadParameters, progress, CancellationToken.None);
                Reader = new BulkFileReader(bulkFilePath, ResultFileType.Upload, FileType);
                OutputStatusMessage("Upload Results Bulk File Path: " + Reader.BulkFilePath + "\n");
                OutputStatusMessage("Deleted Entities\n");

                // Write the upload output

                bulkEntities = Reader.ReadEntities().ToList();

                campaignResults = bulkEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                callAdExtensionResults = bulkEntities.OfType<BulkCallAdExtension>().ToList();
                OutputBulkCallAdExtensions(callAdExtensionResults);

                locationAdExtensionResults = bulkEntities.OfType<BulkLocationAdExtension>().ToList();
                OutputBulkLocationAdExtensions(locationAdExtensionResults);

                siteLinkAdExtensionResults = bulkEntities.OfType<BulkSiteLinkAdExtension>().ToList();
                OutputBulkSiteLinkAdExtensions(siteLinkAdExtensionResults);

                Reader.Dispose();

                #endregion Delete

            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.Bulk.ApiFaultDetail> ex)
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
                if(Reader != null){ Reader.Dispose(); }
                if(Writer != null){ Writer.Dispose(); }
            }
        }
    }
}

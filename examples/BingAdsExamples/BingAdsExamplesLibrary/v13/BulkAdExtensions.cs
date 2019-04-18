using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

using Microsoft.BingAds.V13.Internal.Bulk.Entities;

namespace BingAdsExamplesLibrary.V13
{
    /// <summary>
    /// How to add and update ad extensions with the Bulk service.
    /// </summary>
    public class BulkAdExtensions : BulkExampleBase
    {
        public override string Description
        {
            get { return "AdExtensions | Bulk V13"; }
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
                
                var uploadEntities = new List<BulkEntity>();

                // Add a new campaign and associate it with ad extensions. 

                uploadEntities.Add(GetBulkCampaign());

                uploadEntities.Add(GetBulkActionAdExtension());
                uploadEntities.Add(GetBulkAppAdExtension());
                uploadEntities.Add(GetBulkCallAdExtension());
                uploadEntities.Add(GetBulkCalloutAdExtension());
                uploadEntities.Add(GetBulkLocationAdExtension());
                uploadEntities.Add(GetBulkPriceAdExtension());
                uploadEntities.Add(GetBulkReviewAdExtension());
                uploadEntities.Add(GetBulkSitelinkAdExtension());
                uploadEntities.Add(GetBulkStructuredSnippetAdExtension());

                uploadEntities.Add(GetBulkCampaignActionAdExtension());
                uploadEntities.Add(GetBulkCampaignAppAdExtension());
                uploadEntities.Add(GetBulkCampaignCallAdExtension());
                uploadEntities.Add(GetBulkCampaignCalloutAdExtension());
                uploadEntities.Add(GetBulkCampaignLocationAdExtension());
                uploadEntities.Add(GetBulkCampaignPriceAdExtension());
                uploadEntities.Add(GetBulkCampaignReviewAdExtension());
                uploadEntities.Add(GetBulkCampaignSitelinkAdExtension());
                uploadEntities.Add(GetBulkCampaignStructuredSnippetAdExtension());

                OutputStatusMessage("-----\nAdding campaign, ad extensions, and associations...");

                var Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                var downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                var campaignResults = downloadEntities.OfType<BulkCampaign>().ToList();
                OutputBulkCampaigns(campaignResults);

                var actionAdExtensionResults = downloadEntities.OfType<BulkActionAdExtension>().ToList();
                OutputBulkActionAdExtensions(actionAdExtensionResults);

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

                var priceAdExtensionResults = downloadEntities.OfType<BulkPriceAdExtension>().ToList();
                OutputBulkPriceAdExtensions(priceAdExtensionResults);

                var reviewAdExtensionResults = downloadEntities.OfType<BulkReviewAdExtension>().ToList();
                OutputBulkReviewAdExtensions(reviewAdExtensionResults);

                var structuredSnippetAdExtensionResults = downloadEntities.OfType<BulkStructuredSnippetAdExtension>().ToList();
                OutputBulkStructuredSnippetAdExtensions(structuredSnippetAdExtensionResults);

                var sitelinkAdExtensionResults = downloadEntities.OfType<BulkSitelinkAdExtension>().ToList();
                OutputBulkSitelinkAdExtensions(sitelinkAdExtensionResults);

                OutputBulkCampaignAdExtensionAssociations(downloadEntities.OfType<BulkCampaignAdExtensionAssociation>().ToList());

                Reader.Dispose();
                

                // Delete the campaign and ad extensions that were previously added.

                uploadEntities = new List<BulkEntity>();

                foreach (var campaignResult in campaignResults)
                {
                    if (campaignResult.Campaign != null)
                    {
                        campaignResult.Campaign.Status = CampaignStatus.Deleted;
                        uploadEntities.Add(campaignResult);
                    }
                }

                foreach (var actionAdExtensionResult in actionAdExtensionResults)
                {
                    if (actionAdExtensionResult.ActionAdExtension.Id > 0)
                    {
                        actionAdExtensionResult.ActionAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(actionAdExtensionResult);
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
                    if (locationAdExtensionResult.LocationAdExtension.Id > 0)
                    {
                        locationAdExtensionResult.LocationAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(locationAdExtensionResult);
                    }
                }

                foreach (var priceAdExtensionResult in priceAdExtensionResults)
                {
                    if (priceAdExtensionResult.PriceAdExtension.Id > 0)
                    {
                        priceAdExtensionResult.PriceAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(priceAdExtensionResult);
                    }
                }

                foreach (var reviewAdExtensionResult in reviewAdExtensionResults)
                {
                    if (reviewAdExtensionResult.ReviewAdExtension.Id > 0)
                    {
                        reviewAdExtensionResult.ReviewAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(reviewAdExtensionResult);
                    }
                }

                foreach (var sitelinkAdExtensionResult in sitelinkAdExtensionResults)
                {
                    if (sitelinkAdExtensionResult.SitelinkAdExtension.Id > 0)
                    {
                        sitelinkAdExtensionResult.SitelinkAdExtension.Status = AdExtensionStatus.Deleted;
                        uploadEntities.Add(sitelinkAdExtensionResult);
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

                OutputStatusMessage("-----\nDeleting campaign and ad extensions...");

                Reader = await WriteEntitiesAndUploadFileAsync(uploadEntities);
                downloadEntities = Reader.ReadEntities().ToList();

                OutputStatusMessage("Upload results:");

                OutputBulkCampaigns(downloadEntities.OfType<BulkCampaign>().ToList());
                OutputBulkActionAdExtensions(downloadEntities.OfType<BulkActionAdExtension>().ToList());
                OutputBulkAppAdExtensions(downloadEntities.OfType<BulkAppAdExtension>().ToList());
                OutputBulkCallAdExtensions(downloadEntities.OfType<BulkCallAdExtension>().ToList());
                OutputBulkCalloutAdExtensions(downloadEntities.OfType<BulkCalloutAdExtension>().ToList());
                OutputBulkImageAdExtensions(downloadEntities.OfType<BulkImageAdExtension>().ToList());
                OutputBulkLocationAdExtensions(downloadEntities.OfType<BulkLocationAdExtension>().ToList());
                OutputBulkPriceAdExtensions(downloadEntities.OfType<BulkPriceAdExtension>().ToList());
                OutputBulkReviewAdExtensions(downloadEntities.OfType<BulkReviewAdExtension>().ToList());
                OutputBulkSitelinkAdExtensions(downloadEntities.OfType<BulkSitelinkAdExtension>().ToList());
                OutputBulkStructuredSnippetAdExtensions(downloadEntities.OfType<BulkStructuredSnippetAdExtension>().ToList());

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

using System.Collections.Generic;
using Microsoft.BingAds.Bulk;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

namespace BingAdsExamples
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
        protected static BulkServiceManager BulkService;

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
        /// The name of the bulk upload file.
        /// </summary>
        protected const string UploadFileName = @"upload.csv";

        /// <summary>
        /// The name of the bulk upload file.
        /// </summary>
        protected const string ResultFileName = @"result.csv";
        
        /// <summary>
        /// The bulk file extension type.
        /// </summary>
        protected const DownloadFileType FileType = DownloadFileType.Csv;
        
        
        protected void OutputBulkCampaigns(IEnumerable<BulkCampaign> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkCampaign: \n");
                OutputStatusMessage(string.Format("Name: {0}", entity.Campaign.Name));
                OutputStatusMessage(string.Format("Id: {0}\n", entity.Campaign.Id));
                
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        protected void OutputBulkAdGroups(IEnumerable<BulkAdGroup> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkAdGroup: \n");
                OutputStatusMessage(string.Format("Name: {0}", entity.AdGroup.Name));
                OutputStatusMessage(string.Format("Id: {0}\n", entity.AdGroup.Id));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        protected void OutputBulkKeywords(IEnumerable<BulkKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkKeyword: \n");
                OutputStatusMessage(string.Format("Text: {0}", entity.Keyword.Text));
                OutputStatusMessage(string.Format("Id: {0}\n", entity.Keyword.Id));
                
                if (entity.BidSuggestions != null)
                {
                    OutputStatusMessage("Bid Suggestions Data");
                    OutputStatusMessage(string.Format("BestPosition: {0}", entity.BidSuggestions.BestPosition));
                    OutputStatusMessage(string.Format("MainLine: {0}", entity.BidSuggestions.MainLine));
                    OutputStatusMessage(string.Format("FirstPage: {0}", entity.BidSuggestions.FirstPage));
                }
                
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        protected void OutputBulkTextAds(IEnumerable<BulkTextAd> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkTextAd: \n");
                OutputStatusMessage(string.Format("Title: {0}", entity.TextAd.Title));
                OutputStatusMessage(string.Format("Id: {0}\n", entity.TextAd.Id));
                
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        protected void OutputBulkCallAdExtensions(IEnumerable<BulkCallAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkCallAdExtension: \n");
                OutputStatusMessage(string.Format("PhoneNumber: {0}", entity.CallAdExtension.PhoneNumber));
                OutputStatusMessage(string.Format("Id: {0}\n", entity.CallAdExtension.Id));
                
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        protected void OutputBulkLocationAdExtensions(IEnumerable<BulkLocationAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkLocationAdExtension: \n");
                OutputStatusMessage(string.Format("CompanyName: {0}", entity.LocationAdExtension.CompanyName));
                OutputStatusMessage(string.Format("Id: {0}\n", entity.LocationAdExtension.Id));
                
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        protected void OutputBulkSiteLinkAdExtensions(IEnumerable<BulkSiteLinkAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkSiteLinkAdExtension: \n");
                OutputStatusMessage(string.Format("Id: {0}\n", entity.SiteLinksAdExtension.Id));
                
                if (entity.SiteLinks != null && entity.SiteLinks.Count > 0)
                {
                    OutputBulkSiteLinks(entity.SiteLinks);
                }
            }
        }

        protected void OutputBulkSiteLinks(IEnumerable<BulkSiteLink> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkSiteLink: \n");
                OutputStatusMessage(string.Format("DisplayText: {0}", entity.SiteLink.DisplayText));
                OutputStatusMessage(string.Format("AdExtensionId: {0}\n", entity.AdExtensionId));
                
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        private void OutputErrors(IEnumerable<BulkError> errors)
        {
            foreach (var error in errors)
            {
                OutputStatusMessage(string.Format("Error: {0}", error.Error));
                OutputStatusMessage(string.Format("Number: {0}\n", error.Number));
            }
        }
        
        protected void OutputBulkCampaignDayTimeTargets(IEnumerable<BulkCampaignDayTimeTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignDayTimeTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignDayTimeTargetBid objects. For example if you want to print the 
                // BulkCampaignDayTimeTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.DayTimeTarget != null)
                {
                    OutputStatusMessage("BulkCampaignDayTimeTarget: \n");
                    OutputStatusMessage(string.Format("Campaign Name: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("Target Id: \n{0}", entity.TargetId));

                    foreach (var bid in entity.DayTimeTarget.Bids)
                    {
                        OutputStatusMessage("Campaign Management DayTimeTargetBid Object: ");
                        OutputStatusMessage(string.Format("Bid Adjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(string.Format("Day : {0}", bid.Day));
                        OutputStatusMessage(string.Format("From Hour : {0}", bid.FromHour));
                        OutputStatusMessage(string.Format("From Minute: {0}", bid.FromMinute));
                        OutputStatusMessage(string.Format("To Hour : {0}", bid.ToHour));
                        OutputStatusMessage(string.Format("To Minute: {0}\n", bid.ToMinute));
                    }
                }
                else
                {
                    OutputBulkCampaignDayTimeTargetBids(entity.Bids);
                }
            }
        }

        protected void OutputBulkCampaignDayTimeTargetBids(IEnumerable<BulkCampaignDayTimeTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkCampaignDayTimeTargetBid: \n");
                OutputStatusMessage(string.Format("Campaign Name: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("Target Id: {0}\n", entity.TargetId));

                OutputStatusMessage(string.Format("Bid Adjustment: {0}", entity.DayTimeTargetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Day : {0}", entity.DayTimeTargetBid.Day));
                OutputStatusMessage(string.Format("From Hour : {0}", entity.DayTimeTargetBid.FromHour));
                OutputStatusMessage(string.Format("From Minute: {0}", entity.DayTimeTargetBid.FromMinute));
                OutputStatusMessage(string.Format("To Hour : {0}", entity.DayTimeTargetBid.FromHour));
                OutputStatusMessage(string.Format("To Minute: {0}\n", entity.DayTimeTargetBid.FromMinute));
                
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        protected void OutputBulkCampaignRadiusTargets(IEnumerable<BulkCampaignRadiusTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignRadiusTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignRadiusTargetBid objects. For example if you want to print the 
                // BulkCampaignRadiusTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.RadiusTarget != null)
                {
                    OutputStatusMessage("BulkCampaignRadiusTarget: \n");
                    OutputStatusMessage(string.Format("Campaign Name: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("Target Id: \n{0}", entity.TargetId));

                    foreach (var bid in entity.RadiusTarget.Bids)
                    {
                        OutputStatusMessage("Campaign Management RadiusTargetBid Object: ");
                        OutputStatusMessage(string.Format("Bid Adjustment: {0}", bid.BidAdjustment));
                        OutputStatusMessage(string.Format("Name : {0}", bid.Name));
                        OutputStatusMessage(string.Format("Radius : {0}", bid.Radius));
                        var radiusUnit = bid.RadiusUnit == DistanceUnit.Kilometers ? "Kilometers" : "Miles";
                        OutputStatusMessage(string.Format("Radius Unit: {0}", radiusUnit));
                    }
                }
                else
                {
                    OutputBulkCampaignRadiusTargetBids(entity.Bids);
                }
            }
        }

        protected void OutputBulkCampaignRadiusTargetBids(IEnumerable<BulkCampaignRadiusTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkCampaignRadiusTargetBid: \n");
                OutputStatusMessage(string.Format("Campaign Name: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("Target Id: {0}\n", entity.TargetId));

                OutputStatusMessage(string.Format("Bid Adjustment: {0}", entity.RadiusTargetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Name : {0}", entity.RadiusTargetBid.Name));
                OutputStatusMessage(string.Format("Radius : {0}", entity.RadiusTargetBid.Radius));
                var radiusUnit = entity.RadiusTargetBid.RadiusUnit == DistanceUnit.Kilometers ? "Kilometers" : "Miles";
                OutputStatusMessage(string.Format("Radius Unit: {0}\n", radiusUnit));

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        protected void OutputBulkCampaignLocationTargets(IEnumerable<BulkCampaignLocationTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignLocationTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignLocationTargetBid objects. For example if you want to print the 
                // BulkCampaignLocationTarget prior to upload.
                if (entity.Bids.Count == 0)
                {
                    OutputStatusMessage("BulkCampaignLocationTarget: \n");
                    OutputStatusMessage(string.Format("Campaign Name: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("Target Id: {0}", entity.TargetId));
                    OutputStatusMessage(string.Format("Intent Option: {0}\n", entity.IntentOption));

                    if (entity.CityTarget != null)
                    {
                        foreach (var bid in entity.CityTarget.Bids)
                        {
                            OutputStatusMessage("Campaign Management CityTargetBid Object: ");
                            OutputStatusMessage(string.Format("Bid Adjustment: {0}", bid.BidAdjustment));
                            OutputStatusMessage(string.Format("City : {0}", bid.City));
                            var isExcluded = bid.IsExcluded ? "True" : "False";
                            OutputStatusMessage(string.Format("Location Is Excluded: {0}", isExcluded));
                        }
                    }
                    if (entity.CountryTarget != null)
                    {
                        foreach (var bid in entity.CountryTarget.Bids)
                        {
                            OutputStatusMessage("Campaign Management CountryTargetBid Object: ");
                            OutputStatusMessage(string.Format("Bid Adjustment: {0}", bid.BidAdjustment));
                            OutputStatusMessage(string.Format("CountryAndRegion : {0}", bid.CountryAndRegion));
                            var isExcluded = bid.IsExcluded ? "True" : "False";
                            OutputStatusMessage(string.Format("Location Is Excluded: {0}", isExcluded));
                        }
                    }
                    if (entity.MetroAreaTarget != null)
                    {
                        foreach (var bid in entity.MetroAreaTarget.Bids)
                        {
                            OutputStatusMessage("Campaign Management MetroAreaTargetBid Object: ");
                            OutputStatusMessage(string.Format("Bid Adjustment: {0}", bid.BidAdjustment));
                            OutputStatusMessage(string.Format("MetroArea : {0}", bid.MetroArea));
                            var isExcluded = bid.IsExcluded ? "True" : "False";
                            OutputStatusMessage(string.Format("Location Is Excluded: {0}", isExcluded));
                        }
                    }
                    if (entity.PostalCodeTarget != null)
                    {
                        foreach (var bid in entity.PostalCodeTarget.Bids)
                        {
                            OutputStatusMessage("Campaign Management PostalCodeTargetBid Object: ");
                            OutputStatusMessage(string.Format("Bid Adjustment: {0}", bid.BidAdjustment));
                            OutputStatusMessage(string.Format("PostalCode : {0}", bid.PostalCode));
                            var isExcluded = bid.IsExcluded ? "True" : "False";
                            OutputStatusMessage(string.Format("Location Is Excluded: {0}", isExcluded));
                        }
                    }
                    if (entity.StateTarget != null)
                    {
                        foreach (var bid in entity.StateTarget.Bids)
                        {
                            OutputStatusMessage("Campaign Management StateTargetBid Object: ");
                            OutputStatusMessage(string.Format("Bid Adjustment: {0}", bid.BidAdjustment));
                            OutputStatusMessage(string.Format("State : {0}", bid.State));
                            var isExcluded = bid.IsExcluded ? "True" : "False";
                            OutputStatusMessage(string.Format("Location Is Excluded: {0}", isExcluded));
                        }
                    }
                }
                else
                {
                    OutputBulkCampaignLocationTargetBids(entity.Bids);
                }
            }
        }

        protected void OutputBulkCampaignLocationTargetBids(IEnumerable<BulkCampaignLocationTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("BulkCampaignLocationTargetBid: \n");
                OutputStatusMessage(string.Format("Campaign Name: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("Target Id: {0}\n", entity.TargetId));


                OutputStatusMessage(string.Format("Bid Adjustment: {0}", entity.BidAdjustment));
                OutputStatusMessage(string.Format("Location Type : {0}", entity.LocationType));
                OutputStatusMessage(string.Format("Location : {0}\n", entity.Location));

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }
    }
}

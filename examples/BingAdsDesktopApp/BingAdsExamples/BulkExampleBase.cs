using System;
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

        protected const int targetIdKey = -1;
        protected const int appAdExtensionIdKey = -11;
        protected const int callAdExtensionIdKey = -12;
        protected const int imageAdExtensionIdKey = -13;
        protected const int locationAdExtensionIdKey = -14;
        protected const int siteLinksAdExtensionIdKey = -15;
        protected const int negativeKeywordListIdKey = -16;
        protected const int campaignIdKey = -111;
        protected const int adGroupIdKey = -1111;
        protected const int negativeKeywordIdKey = -11111;


        /// <summary>
        /// Outputs the list of BulkAccount
        /// </summary>
        protected void OutputBulkAccounts(IEnumerable<BulkAccount> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAccount: \n");
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("CustomerId: {0}", entity.CustomerId));
                OutputStatusMessage(string.Format("Id: {0}", entity.Id));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("SyncTime: {0}", entity.SyncTime));
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroup that can be written as an Ad Group record in a Bulk file. 
        /// </summary>
        protected BulkAdGroup GetExampleBulkAdGroup()
        {
            var adGroup = GetExampleAdGroup();
            adGroup.Id = adGroupIdKey;

            return new BulkAdGroup
            {
                CampaignId = campaignIdKey,
                AdGroup = adGroup,
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroup.
        /// </summary>
        protected void OutputBulkAdGroups(IEnumerable<BulkAdGroup> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroup: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("IsExpired: {0}", entity.IsExpired));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);
                OutputBulkQualityScoreData(entity.QualityScoreData);

                // Output the Campaign Management AdGroup Object
                OutputAdGroup(entity.AdGroup);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupAgeTarget that can be written as an Ad Group Age Target record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupAgeTarget GetExampleBulkAdGroupAgeTarget()
        {
            return new BulkAdGroupAgeTarget
            {
                AdGroupId = adGroupIdKey,
                TargetId = targetIdKey,
                AgeTarget = GetExampleAgeTarget()
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupAgeTarget.
        /// </summary>
        protected void OutputBulkAdGroupAgeTargets(IEnumerable<BulkAdGroupAgeTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupAgeTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupAgeTargetBid objects. For example if you want to print the 
                // BulkAdGroupAgeTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.AgeTarget != null)
                {
                    OutputStatusMessage("\nBulkAdGroupAgeTarget: \n");
                    OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    foreach (var bid in entity.AgeTarget.Bids)
                    {
                        // Output the Campaign Management AgeTargetBid Object
                        OutputAgeTargetBid(bid);
                    }
                }
                else
                {
                    OutputBulkAdGroupAgeTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupAgeTargetBid that can be written as an Ad Group Age Target record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupAgeTargetBid GetExampleBulkAdGroupAgeTargetBid()
        {
            return new BulkAdGroupAgeTargetBid
            {
                ClientId = "BulkAdGroupAgeTargetBid",
                AdGroupId = adGroupIdKey,
                TargetId = targetIdKey,
                AgeTargetBid = GetExampleAgeTargetBid()
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupAgeTargetBid.
        /// </summary>
        protected void OutputBulkAdGroupAgeTargetBids(IEnumerable<BulkAdGroupAgeTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupAgeTargetBid: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                // Output the Campaign Management AgeTargetBid Object
                OutputAgeTargetBid(entity.AgeTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupAppAdExtension that can be written as an Ad Group App Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupAppAdExtension GetExampleBulkAdGroupAppAdExtension()
        {
            return new BulkAdGroupAppAdExtension
            {
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    AdExtensionId = appAdExtensionIdKey,
                    EntityId = adGroupIdKey,
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupAppAdExtension.
        /// </summary>
        protected void OutputBulkAdGroupAppAdExtensions(IEnumerable<BulkAdGroupAppAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupAppAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupDayTimeTarget that can be written as an Ad Group DayTime Target record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupDayTimeTarget GetExampleBulkAdGroupDayTimeTarget()
        {
            return new BulkAdGroupDayTimeTarget
            {
                AdGroupId = adGroupIdKey,
                TargetId = targetIdKey,
                DayTimeTarget = GetExampleDayTimeTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupDayTimeTarget.
        /// </summary>
        protected void OutputBulkAdGroupDayTimeTargets(IEnumerable<BulkAdGroupDayTimeTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupDayTimeTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupDayTimeTargetBid objects. For example if you want to print the 
                // BulkAdGroupDayTimeTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.DayTimeTarget != null)
                {
                    OutputStatusMessage("\nBulkAdGroupDayTimeTarget: \n");
                    OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    foreach (var bid in entity.DayTimeTarget.Bids)
                    {
                        // Output the Campaign Management DayTimeTarget Object
                        OutputDayTimeTargetBid(bid);
                    }
                }
                else
                {
                    OutputBulkAdGroupDayTimeTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupDayTimeTargetBid.
        /// </summary>
        protected void OutputBulkAdGroupDayTimeTargetBids(IEnumerable<BulkAdGroupDayTimeTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupDayTimeTargetBid: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                // Output the Campaign Management DayTimeTarget Object
                OutputDayTimeTargetBid(entity.DayTimeTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupDeviceOsTarget that can be written as an Ad Group DeviceOS Target record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupDeviceOsTarget GetExampleBulkAdGroupDeviceOsTarget()
        {
            return new BulkAdGroupDeviceOsTarget
            {
                AdGroupId = adGroupIdKey,
                TargetId = targetIdKey,
                DeviceOsTarget = GetExampleDeviceOSTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupDeviceOsTarget.
        /// </summary>
        protected void OutputBulkAdGroupDeviceOsTargets(IEnumerable<BulkAdGroupDeviceOsTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupDeviceOsTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupDeviceOsTargetBid objects. For example if you want to print the 
                // BulkAdGroupDeviceOsTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.DeviceOsTarget != null)
                {
                    OutputStatusMessage("\nBulkAdGroupDeviceOsTarget: \n");
                    OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    foreach (var bid in entity.DeviceOsTarget.Bids)
                    {
                        // Output the Campaign Management DeviceOSTarget Object
                        OutputDeviceOSTargetBid(bid);
                    }
                }
                else
                {
                    OutputBulkAdGroupDeviceOsTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupDeviceOsTargetBid.
        /// </summary>
        protected void OutputBulkAdGroupDeviceOsTargetBids(IEnumerable<BulkAdGroupDeviceOsTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupDeviceOsTargetBid: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                // Output the Campaign Management DeviceOSTarget Object
                OutputDeviceOSTargetBid(entity.DeviceOsTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupGenderTarget that can be written as an Ad Group Gender Target record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupGenderTarget GetExampleBulkAdGroupGenderTarget()
        {
            return new BulkAdGroupGenderTarget
            {
                AdGroupId = adGroupIdKey,
                TargetId = targetIdKey,
                GenderTarget = GetExampleGenderTarget()
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupGenderTarget.
        /// </summary>
        protected void OutputBulkAdGroupGenderTargets(IEnumerable<BulkAdGroupGenderTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupGenderTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupGenderTargetBid objects. For example if you want to print the 
                // BulkAdGroupGenderTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.GenderTarget != null)
                {
                    OutputStatusMessage("\nBulkAdGroupGenderTarget: \n");
                    OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    foreach (var bid in entity.GenderTarget.Bids)
                    {
                        // Output the Campaign Management GenderTarget Object
                        OutputGenderTargetBid(bid);
                    }
                }
                else
                {
                    OutputBulkAdGroupGenderTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupGenderTargetBid.
        /// </summary>
        protected void OutputBulkAdGroupGenderTargetBids(IEnumerable<BulkAdGroupGenderTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupGenderTargetBid: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                // Output the Campaign Management GenderTarget Object
                OutputGenderTargetBid(entity.GenderTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupImageAdExtension that can be written as an Ad Group Image Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupImageAdExtension GetExampleBulkAdGroupImageAdExtension()
        {
            return new BulkAdGroupImageAdExtension
            {
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    AdExtensionId = imageAdExtensionIdKey,
                    EntityId = adGroupIdKey,
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupImageAdExtension.
        /// </summary>
        protected void OutputBulkAdGroupImageAdExtensions(IEnumerable<BulkAdGroupImageAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupImageAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupLocationTarget that can be written as an Ad Group Location Target record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupLocationTarget GetExampleBulkAdGroupLocationTarget()
        {
            /* 
             * If you set the IsExcluded element for any target bids within BulkEntity derived objects, 
             * it will be ignored. To exclude a location, upload either a BulkAdGroupNegativeLocationTarget or
             * BulkCampaignNegativeLocationTarget instance, and to include a location upload either a 
             * BulkAdGroupLocationTarget or BulkCampaignLocationTarget instance. 
             */
            return new BulkAdGroupLocationTarget
            {
                AdGroupId = adGroupIdKey,
                TargetId = targetIdKey,
                IntentOption = IntentOption.PeopleIn,
                CityTarget = GetExampleCityTarget(),
                CountryTarget = GetExampleCountryTarget(),
                MetroAreaTarget = GetExampleMetroAreaTarget(),
                StateTarget = GetExampleStateTarget(),
                PostalCodeTarget = GetExamplePostalCodeTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupLocationTarget.
        /// </summary>
        protected void OutputBulkAdGroupLocationTargets(IEnumerable<BulkAdGroupLocationTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupLocationTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupLocationTargetBid objects. For example if you want to print the 
                // BulkAdGroupLocationTarget prior to upload.
                if (entity.Bids.Count == 0)
                {
                    OutputStatusMessage("\nBulkAdGroupLocationTarget: \n");
                    OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("IntentOption: {0}", entity.IntentOption));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    if (entity.CityTarget != null)
                    {
                        foreach (var bid in entity.CityTarget.Bids)
                        {
                            // Output the Campaign Management CityTargetBid Object
                            OutputCityTargetBid(bid);
                        }
                    }
                    if (entity.CountryTarget != null)
                    {
                        foreach (var bid in entity.CountryTarget.Bids)
                        {
                            // Output the Campaign Management CountryTargetBid Object
                            OutputCountryTargetBid(bid);
                        }
                    }
                    if (entity.MetroAreaTarget != null)
                    {
                        foreach (var bid in entity.MetroAreaTarget.Bids)
                        {
                            // Output the Campaign Management MetroAreaTargetBid Object
                            OutputMetroAreaTargetBid(bid);
                        }
                    }
                    if (entity.PostalCodeTarget != null)
                    {
                        foreach (var bid in entity.PostalCodeTarget.Bids)
                        {
                            // Output the Campaign Management PostalCodeTargetBid Object
                            OutputPostalCodeTargetBid(bid);
                        }
                    }
                    if (entity.StateTarget != null)
                    {
                        foreach (var bid in entity.StateTarget.Bids)
                        {
                            // Output the Campaign Management StateTargetBid Object
                            OutputStateTargetBid(bid);
                        }
                    }
                }
                else
                {
                    OutputBulkAdGroupLocationTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupLocationTargetBid.
        /// </summary>
        protected void OutputBulkAdGroupLocationTargetBids(IEnumerable<BulkAdGroupLocationTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupLocationTargetBid: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("BidAdjustment: {0}", entity.BidAdjustment));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("IntentOption: {0}", entity.IntentOption));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Location: {0}", entity.Location));
                OutputStatusMessage(string.Format("LocationType: {0}", entity.LocationType));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupNegativeKeyword that can be written as an Ad Group Negative Keyword record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupNegativeKeyword GetExampleBulkAdGroupNegativeKeyword()
        {
            return new BulkAdGroupNegativeKeyword
            {
                AdGroupId = adGroupIdKey,
                NegativeKeyword = GetExampleNegativeKeyword(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeKeyword.
        /// </summary>
        protected void OutputBulkAdGroupNegativeKeywords(IEnumerable<BulkAdGroupNegativeKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupNegativeKeyword: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.NegativeKeyword != null)
                {
                    // Output the Campaign Management NegativeKeyword Object
                    OutputNegativeKeyword(entity.NegativeKeyword);
                }
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupNegativeLocationTarget that can be written as an Ad Group Negative Location Target record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupNegativeLocationTarget GetExampleBulkAdGroupNegativeLocationTarget()
        {
            /*
             * Bid adjustments are not allowed for location exclusions. For a BulkAdGroupNegativeLocationTarget or
             * BulkCampaignNegativeLocationTarget instance this element will be ignored.
             *
             * If you set the IsExcluded element for any target bids within BulkEntity derived objects, 
             * it will be ignored. To exclude a location, upload either a BulkAdGroupNegativeLocationTarget or
             * BulkCampaignNegativeLocationTarget instance, and to include a location upload either a 
             * BulkAdGroupLocationTarget or BulkCampaignLocationTarget instance. 
             */
            return new BulkAdGroupNegativeLocationTarget
            {
                AdGroupId = adGroupIdKey,
                TargetId = targetIdKey,
                CityTarget = GetExampleCityTarget(),
                CountryTarget = GetExampleCountryTarget(),
                MetroAreaTarget = GetExampleMetroAreaTarget(),
                StateTarget = GetExampleStateTarget(),
                PostalCodeTarget = GetExamplePostalCodeTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeLocationTarget.
        /// </summary>
        protected void OutputBulkAdGroupNegativeLocationTargets(IEnumerable<BulkAdGroupNegativeLocationTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupNegativeLocationTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupNegativeLocationTargetBid objects. For example if you want to print the 
                // BulkAdGroupNegativeLocationTarget prior to upload.
                if (entity.Bids.Count == 0)
                {
                    OutputStatusMessage("\nBulkAdGroupNegativeLocationTarget: \n");
                    OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    if (entity.CityTarget != null)
                    {
                        foreach (var bid in entity.CityTarget.Bids)
                        {
                            // Output the Campaign Management CityTargetBid Object
                            OutputCityTargetBid(bid);
                        }
                    }
                    if (entity.CountryTarget != null)
                    {
                        foreach (var bid in entity.CountryTarget.Bids)
                        {
                            // Output the Campaign Management CountryTargetBid Object
                            OutputCountryTargetBid(bid);
                        }
                    }
                    if (entity.MetroAreaTarget != null)
                    {
                        foreach (var bid in entity.MetroAreaTarget.Bids)
                        {
                            // Output the Campaign Management MetroAreaTargetBid Object
                            OutputMetroAreaTargetBid(bid);
                        }
                    }
                    if (entity.PostalCodeTarget != null)
                    {
                        foreach (var bid in entity.PostalCodeTarget.Bids)
                        {
                            // Output the Campaign Management PostalCodeTargetBid Object
                            OutputPostalCodeTargetBid(bid);
                        }
                    }
                    if (entity.StateTarget != null)
                    {
                        foreach (var bid in entity.StateTarget.Bids)
                        {
                            // Output the Campaign Management StateTargetBid Object
                            OutputStateTargetBid(bid);
                        }
                    }
                }
                else
                {
                    OutputBulkAdGroupNegativeLocationTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeLocationTargetBid.
        /// </summary>
        protected void OutputBulkAdGroupNegativeLocationTargetBids(IEnumerable<BulkAdGroupNegativeLocationTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupNegativeLocationTargetBid: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Location: {0}", entity.Location));
                OutputStatusMessage(string.Format("LocationType: {0}", entity.LocationType));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupNegativeSites that can be written as an Ad Group Negative Site record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupNegativeSites GetExampleBulkAdGroupNegativeSites()
        {
            return new BulkAdGroupNegativeSites
            {
                AdGroupNegativeSites = new AdGroupNegativeSites
                {
                    AdGroupId = adGroupIdKey,
                    NegativeSites = GetExampleNegativeSites(),
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeSites.
        /// </summary>
        protected void OutputBulkAdGroupNegativeSites(IEnumerable<BulkAdGroupNegativeSites> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupNegativeSites object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupNegativeSites.NegativeSites objects. For example if you want to print the 
                // BulkAdGroupNegativeSites prior to upload.
                if (entity.NegativeSites.Count == 0)
                {
                    OutputStatusMessage("\nBulkAdGroupNegativeSites: \n");
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputNegativeSites(entity.AdGroupNegativeSites.NegativeSites);
                }
                else
                {
                    OutputBulkAdGroupNegativeSite(entity.NegativeSites);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupNegativeSite.
        /// </summary>
        protected void OutputBulkAdGroupNegativeSite(IEnumerable<BulkAdGroupNegativeSite> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupNegativeSite: \n");
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputNegativeSites(new[] { entity.Website });

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupRadiusTarget that can be written as an Ad Group Radius Target record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupRadiusTarget GetExampleBulkAdGroupRadiusTarget()
        {
            return new BulkAdGroupRadiusTarget
            {
                AdGroupId = adGroupIdKey,
                TargetId = targetIdKey,
                RadiusTarget = GetExampleRadiusTarget2(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupRadiusTarget.
        /// </summary>
        protected void OutputBulkAdGroupRadiusTargets(IEnumerable<BulkAdGroupRadiusTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkAdGroupRadiusTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkAdGroupRadiusTargetBid objects. For example if you want to print the 
                // BulkAdGroupRadiusTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.RadiusTarget != null)
                {
                    OutputStatusMessage("\nBulkAdGroupRadiusTarget: \n");
                    OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                    OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));
                    OutputStatusMessage(string.Format("IntentOption: {0}", entity.IntentOption));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                    foreach (var bid in entity.RadiusTarget.Bids)
                    {
                        // Output the Campaign Management RadiusTargetBid2 Object
                        OutputRadiusTargetBid2(bid);
                    }
                }
                else
                {
                    OutputBulkAdGroupRadiusTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupRadiusTargetBid.
        /// </summary>
        protected void OutputBulkAdGroupRadiusTargetBids(IEnumerable<BulkAdGroupRadiusTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupRadiusTargetBid: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));
                OutputStatusMessage(string.Format("IntentOption: {0}", entity.IntentOption));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                // Output the Campaign Management RadiusTargetBid2 Object
                OutputRadiusTargetBid2(entity.RadiusTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupSiteLinkAdExtension that can be written as an Ad Group Sitelink Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkAdGroupSiteLinkAdExtension GetExampleBulkAdGroupSiteLinkAdExtension()
        {
            return new BulkAdGroupSiteLinkAdExtension
            {
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    AdExtensionId = siteLinksAdExtensionIdKey,
                    EntityId = adGroupIdKey,
                },
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupSiteLinkAdExtension.
        /// </summary>
        protected void OutputBulkAdGroupSiteLinkAdExtensions(IEnumerable<BulkAdGroupSiteLinkAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupSiteLinkAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkAdGroupTarget that can be written as a one or more ad group target records in a Bulk file.
        /// </summary>
        protected BulkAdGroupTarget GetExampleBulkAdGroupTarget()
        {
            var target2 = GetExampleTarget2();
            target2.Id = targetIdKey;

            return new BulkAdGroupTarget
            {
                AdGroupId = adGroupIdKey,
                Target = target2,
            };
        }

        /// <summary>
        /// Outputs the list of BulkAdGroupTarget.
        /// </summary>
        protected void OutputBulkAdGroupTargets(IEnumerable<BulkAdGroupTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAdGroupTarget: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                // Output the Campaign Management Target2 Object
                OutputTarget2(entity.Target);

                OutputBulkAdGroupAgeTargets(new[] { entity.AgeTarget });
                OutputBulkAdGroupDayTimeTargets(new[] { entity.DayTimeTarget });
                OutputBulkAdGroupDeviceOsTargets(new[] { entity.DeviceOsTarget });
                OutputBulkAdGroupGenderTargets(new[] { entity.GenderTarget });
                OutputBulkAdGroupLocationTargets(new[] { entity.LocationTarget });
                OutputBulkAdGroupNegativeLocationTargets(new[] { entity.NegativeLocationTarget });
                OutputBulkAdGroupRadiusTargets(new[] { entity.RadiusTarget });
            }
        }

        /// <summary>
        /// Gets an example BulkAppAdExtension that can be written as a App Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkAppAdExtension GetExampleBulkAppAdExtension(long accountId)
        {
            var appAdExtension = GetExampleAppAdExtension();
            appAdExtension.Id = appAdExtensionIdKey;

            return new BulkAppAdExtension
            {
                AccountId = accountId,
                AppAdExtension = appAdExtension,
            };
        }

        /// <summary>
        /// Outputs the list of BulkAppAdExtension.
        /// </summary>
        protected void OutputBulkAppAdExtensions(IEnumerable<BulkAppAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkAppAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management AppAdExtension Object
                OutputAppAdExtension(entity.AppAdExtension);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }


        /// <summary>
        /// Gets an example BulkCallAdExtension that can be written as a Call Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkCallAdExtension GetExampleBulkCallAdExtension(long accountId)
        {
            var callAdExtension = GetExampleCallAdExtension();
            callAdExtension.Id = callAdExtensionIdKey;

            return new BulkCallAdExtension
            {
                AccountId = accountId,
                CallAdExtension = callAdExtension,
            };
        }

        /// <summary>
        /// Outputs the list of BulkCallAdExtension.
        /// </summary>
        protected void OutputBulkCallAdExtensions(IEnumerable<BulkCallAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCallAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management CallAdExtension Object
                OutputCallAdExtension(entity.CallAdExtension);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaign that can be written as a Campaign record in a Bulk file. 
        /// </summary>
        protected BulkCampaign GetExampleBulkCampaign()
        {

            var campaign = GetExampleCampaign();
            campaign.Id = campaignIdKey;

            return new BulkCampaign
            {
                // ClientId may be used to associate records in the bulk upload file with records in the results file. The value of this field 
                // is not used or stored by the server; it is simply copied from the uploaded record to the corresponding result record.
                // Note: This bulk file Client Id is not related to an application Client Id for OAuth. 
                ClientId = "YourClientIdGoesHere",
                Campaign = campaign,
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaign.
        /// </summary>
        protected void OutputBulkCampaigns(IEnumerable<BulkCampaign> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaign: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);
                OutputBulkQualityScoreData(entity.QualityScoreData);

                // Output the Campaign Management Campaign Object
                OutputCampaign(entity.Campaign);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignAgeTarget that can be written as a Campaign Age Target record in a Bulk file. 
        /// </summary>
        protected BulkCampaignAgeTarget GetExampleBulkCampaignAgeTarget()
        {
            return new BulkCampaignAgeTarget
            {
                CampaignId = campaignIdKey,
                TargetId = targetIdKey,
                AgeTarget = GetExampleAgeTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignAgeTarget.
        /// </summary>
        protected void OutputBulkCampaignAgeTargets(IEnumerable<BulkCampaignAgeTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignAgeTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignAgeTargetBid objects. For example if you want to print the 
                // BulkCampaignAgeTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.AgeTarget != null)
                {
                    OutputStatusMessage("\nBulkCampaignAgeTarget: \n");
                    OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    foreach (var bid in entity.AgeTarget.Bids)
                    {
                        // Output the Campaign Management AgeTargetBid Object
                        OutputAgeTargetBid(bid);
                    }
                }
                else
                {
                    OutputBulkCampaignAgeTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignAgeTargetBid.
        /// </summary>
        protected void OutputBulkCampaignAgeTargetBids(IEnumerable<BulkCampaignAgeTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignAgeTargetBid: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                // Output the Campaign Management AgeTargetBid Object
                OutputAgeTargetBid(entity.AgeTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignAppAdExtension that can be written as a Campaign App Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkCampaignAppAdExtension GetExampleBulkCampaignAppAdExtension()
        {
            return new BulkCampaignAppAdExtension
            {
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    AdExtensionId = appAdExtensionIdKey,
                    EntityId = campaignIdKey,
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignAppAdExtension.
        /// </summary>
        protected void OutputBulkCampaignAppAdExtensions(IEnumerable<BulkCampaignAppAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignAppAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignCallAdExtension that can be written as a Campaign Call Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkCampaignCallAdExtension GetExampleBulkCampaignCallAdExtension()
        {
            return new BulkCampaignCallAdExtension
            {
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    AdExtensionId = callAdExtensionIdKey,
                    EntityId = campaignIdKey,
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignCallAdExtension.
        /// </summary>
        protected void OutputBulkCampaignCallAdExtensions(IEnumerable<BulkCampaignCallAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignCallAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignDayTimeTarget that can be written as a Campaign DayTime Target record in a Bulk file. 
        /// </summary>
        protected BulkCampaignDayTimeTarget GetExampleBulkCampaignDayTimeTarget()
        {
            return new BulkCampaignDayTimeTarget
            {
                CampaignId = campaignIdKey,
                TargetId = targetIdKey,
                DayTimeTarget = GetExampleDayTimeTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignDayTimeTarget.
        /// </summary>
        protected void OutputBulkCampaignDayTimeTargets(IEnumerable<BulkCampaignDayTimeTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignDayTimeTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignDayTimeTargetBid objects. For example if you want to print the 
                // BulkCampaignDayTimeTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.DayTimeTarget != null)
                {
                    OutputStatusMessage("\nBulkCampaignDayTimeTarget: \n");
                    OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    foreach (var bid in entity.DayTimeTarget.Bids)
                    {
                        // Output the Campaign Management DayTimeTarget Object
                        OutputDayTimeTargetBid(bid);
                    }
                }
                else
                {
                    OutputBulkCampaignDayTimeTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignDayTimeTargetBid.
        /// </summary>
        protected void OutputBulkCampaignDayTimeTargetBids(IEnumerable<BulkCampaignDayTimeTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignDayTimeTargetBid: \n");
                OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                // Output the Campaign Management DayTimeTarget Object
                OutputDayTimeTargetBid(entity.DayTimeTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignDeviceOsTarget that can be written as a Campaign DeviceOS Target record in a Bulk file. 
        /// </summary>
        protected BulkCampaignDeviceOsTarget GetExampleBulkCampaignDeviceOsTarget()
        {
            return new BulkCampaignDeviceOsTarget
            {
                CampaignId = campaignIdKey,
                TargetId = targetIdKey,
                DeviceOsTarget = GetExampleDeviceOSTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignDeviceOsTarget.
        /// </summary>
        protected void OutputBulkCampaignDeviceOsTargets(IEnumerable<BulkCampaignDeviceOsTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignDeviceOsTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignDeviceOsTargetBid objects. For example if you want to print the 
                // BulkCampaignDeviceOsTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.DeviceOsTarget != null)
                {
                    OutputStatusMessage("\nBulkCampaignDeviceOsTarget: \n");
                    OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    foreach (var bid in entity.DeviceOsTarget.Bids)
                    {
                        // Output the Campaign Management DeviceOSTarget Object
                        OutputDeviceOSTargetBid(bid);
                    }
                }
                else
                {
                    OutputBulkCampaignDeviceOsTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignDeviceOsTargetBid.
        /// </summary>
        protected void OutputBulkCampaignDeviceOsTargetBids(IEnumerable<BulkCampaignDeviceOsTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignDeviceOsTargetBid: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                // Output the Campaign Management DeviceOSTarget Object
                OutputDeviceOSTargetBid(entity.DeviceOsTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignGenderTarget that can be written as a Campaign Gender Target record in a Bulk file. 
        /// </summary>
        protected BulkCampaignGenderTarget GetExampleBulkCampaignGenderTarget()
        {
            return new BulkCampaignGenderTarget
            {
                CampaignId = campaignIdKey,
                TargetId = targetIdKey,
                GenderTarget = GetExampleGenderTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignGenderTarget.
        /// </summary>
        protected void OutputBulkCampaignGenderTargets(IEnumerable<BulkCampaignGenderTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignGenderTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignGenderTargetBid objects. For example if you want to print the 
                // BulkCampaignGenderTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.GenderTarget != null)
                {
                    OutputStatusMessage("\nBulkAdGroupGenderTarget: \n");
                    OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    foreach (var bid in entity.GenderTarget.Bids)
                    {
                        // Output the Campaign Management GenderTarget Object
                        OutputGenderTargetBid(bid);
                    }
                }
                else
                {
                    OutputBulkCampaignGenderTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignGenderTargetBid.
        /// </summary>
        protected void OutputBulkCampaignGenderTargetBids(IEnumerable<BulkCampaignGenderTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignGenderTargetBid: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                // Output the Campaign Management GenderTarget Object
                OutputGenderTargetBid(entity.GenderTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignImageAdExtension that can be written as a Campaign Image Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkCampaignImageAdExtension GetExampleBulkCampaignImageAdExtension()
        {
            return new BulkCampaignImageAdExtension
            {
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    AdExtensionId = imageAdExtensionIdKey,
                    EntityId = campaignIdKey,
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignImageAdExtension.
        /// </summary>
        protected void OutputBulkCampaignImageAdExtensions(IEnumerable<BulkCampaignImageAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignImageAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignLocationAdExtension that can be written as a Campaign Location Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkCampaignLocationAdExtension GetExampleBulkCampaignLocationAdExtension()
        {
            return new BulkCampaignLocationAdExtension
            {
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    AdExtensionId = locationAdExtensionIdKey,
                    EntityId = campaignIdKey,
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignLocationAdExtension.
        /// </summary>
        protected void OutputBulkCampaignLocationAdExtensions(IEnumerable<BulkCampaignLocationAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignLocationAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Ad Group Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignLocationTarget that can be written as a Campaign Location Target record in a Bulk file. 
        /// </summary>
        protected BulkCampaignLocationTarget GetExampleBulkCampaignLocationTarget()
        {
            /* 
             * If you set the IsExcluded element for any target bids within BulkEntity derived objects, 
             * it will be ignored. To exclude a location, upload either a BulkAdGroupNegativeLocationTarget or
             * BulkCampaignNegativeLocationTarget instance, and to include a location upload either a 
             * BulkAdGroupLocationTarget or BulkCampaignLocationTarget instance. 
             */
            return new BulkCampaignLocationTarget
            {
                CampaignId = campaignIdKey,
                TargetId = targetIdKey,
                IntentOption = IntentOption.PeopleIn,
                CityTarget = GetExampleCityTarget(),
                CountryTarget = GetExampleCountryTarget(),
                MetroAreaTarget = GetExampleMetroAreaTarget(),
                StateTarget = GetExampleStateTarget(),
                PostalCodeTarget = GetExamplePostalCodeTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignLocationTarget.
        /// </summary>
        protected void OutputBulkCampaignLocationTargets(IEnumerable<BulkCampaignLocationTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignLocationTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignLocationTargetBid objects. For example if you want to print the 
                // BulkCampaignLocationTarget prior to upload.
                if (entity.Bids.Count == 0)
                {
                    OutputStatusMessage("\nBulkCampaignLocationTarget: \n");
                    OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("IntentOption: {0}", entity.IntentOption));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    if (entity.CityTarget != null)
                    {
                        foreach (var bid in entity.CityTarget.Bids)
                        {
                            // Output the Campaign Management CityTargetBid Object
                            OutputCityTargetBid(bid);
                        }
                    }
                    if (entity.CountryTarget != null)
                    {
                        foreach (var bid in entity.CountryTarget.Bids)
                        {
                            // Output the Campaign Management CountryTargetBid Object
                            OutputCountryTargetBid(bid);
                        }
                    }
                    if (entity.MetroAreaTarget != null)
                    {
                        foreach (var bid in entity.MetroAreaTarget.Bids)
                        {
                            // Output the Campaign Management MetroAreaTargetBid Object
                            OutputMetroAreaTargetBid(bid);
                        }
                    }
                    if (entity.PostalCodeTarget != null)
                    {
                        foreach (var bid in entity.PostalCodeTarget.Bids)
                        {
                            // Output the Campaign Management PostalCodeTargetBid Object
                            OutputPostalCodeTargetBid(bid);
                        }
                    }
                    if (entity.StateTarget != null)
                    {
                        foreach (var bid in entity.StateTarget.Bids)
                        {
                            // Output the Campaign Management StateTargetBid Object
                            OutputStateTargetBid(bid);
                        }
                    }
                }
                else
                {
                    OutputBulkCampaignLocationTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignLocationTargetBid.
        /// </summary>
        protected void OutputBulkCampaignLocationTargetBids(IEnumerable<BulkCampaignLocationTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignLocationTargetBid: \n");
                OutputStatusMessage(string.Format("Campaign Name: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("Campaign Id: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("Target Id: {0}", entity.TargetId));
                OutputStatusMessage(string.Format("IntentOption: {0}", entity.IntentOption));

                OutputStatusMessage(string.Format("Bid Adjustment: {0}", entity.BidAdjustment));
                OutputStatusMessage(string.Format("Location Type : {0}", entity.LocationType));
                OutputStatusMessage(string.Format("Location : {0}", entity.Location));

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignNegativeKeyword that can be written as a Campaign Negative Keyword record in a Bulk file. 
        /// </summary>
        protected BulkCampaignNegativeKeyword GetExampleBulkCampaignNegativeKeyword()
        {
            return new BulkCampaignNegativeKeyword
            {
                CampaignId = campaignIdKey,
                NegativeKeyword = GetExampleNegativeKeyword(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeKeyword.
        /// </summary>
        protected void OutputBulkCampaignNegativeKeywords(IEnumerable<BulkCampaignNegativeKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignNegativeKeyword: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.NegativeKeyword != null)
                {
                    // Output the Campaign Management NegativeKeyword Object
                    OutputNegativeKeyword(entity.NegativeKeyword);
                }
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignNegativeKeywordList that can be written as a Campaign Negative Keyword List record in a Bulk file. 
        /// </summary>
        protected BulkCampaignNegativeKeywordList GetExampleBulkCampaignNegativeKeywordList()
        {
            return new BulkCampaignNegativeKeywordList
            {
                SharedEntityAssociation = new SharedEntityAssociation
                {
                    EntityId = campaignIdKey,
                    EntityType = "Campaign",
                    SharedEntityId = negativeKeywordListIdKey,
                    SharedEntityType = "Negative Keyword List"
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeKeywordList.
        /// </summary>
        protected void OutputBulkCampaignNegativeKeywordLists(IEnumerable<BulkCampaignNegativeKeywordList> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignNegativeKeywordList: \n");
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.SharedEntityAssociation != null)
                {
                    OutputStatusMessage(string.Format("EntityId: {0}",
                        entity.SharedEntityAssociation.EntityId));
                    OutputStatusMessage(string.Format("EntityType: {0}",
                        entity.SharedEntityAssociation.EntityType));
                    OutputStatusMessage(string.Format("SharedEntityId: {0}",
                        entity.SharedEntityAssociation.SharedEntityId));
                    OutputStatusMessage(string.Format("SharedEntityType: {0}",
                        entity.SharedEntityAssociation.SharedEntityType));
                }

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignNegativeLocationTarget that can be written as a Campaign Negative Location Target record in a Bulk file. 
        /// </summary>
        protected BulkCampaignNegativeLocationTarget GetExampleBulkCampaignNegativeLocationTarget()
        {
            return new BulkCampaignNegativeLocationTarget
            {
                CampaignId = campaignIdKey,
                TargetId = targetIdKey,
                CityTarget = GetExampleCityTarget(),
                CountryTarget = GetExampleCountryTarget(),
                MetroAreaTarget = GetExampleMetroAreaTarget(),
                StateTarget = GetExampleStateTarget(),
                PostalCodeTarget = GetExamplePostalCodeTarget(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeLocationTarget.
        /// </summary>
        protected void OutputBulkCampaignNegativeLocationTargets(IEnumerable<BulkCampaignNegativeLocationTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignNegativeLocationTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignNegativeLocationTargetBid objects. For example if you want to print the 
                // BulkCampaignNegativeLocationTarget prior to upload.
                if (entity.Bids.Count == 0)
                {
                    OutputStatusMessage("\nBulkAdGroupNegativeLocationTarget: \n");
                    OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                    if (entity.CityTarget != null)
                    {
                        foreach (var bid in entity.CityTarget.Bids)
                        {
                            // Output the Campaign Management CityTargetBid Object
                            OutputCityTargetBid(bid);
                        }
                    }
                    if (entity.CountryTarget != null)
                    {
                        foreach (var bid in entity.CountryTarget.Bids)
                        {
                            // Output the Campaign Management CountryTargetBid Object
                            OutputCountryTargetBid(bid);
                        }
                    }
                    if (entity.MetroAreaTarget != null)
                    {
                        foreach (var bid in entity.MetroAreaTarget.Bids)
                        {
                            // Output the Campaign Management MetroAreaTargetBid Object
                            OutputMetroAreaTargetBid(bid);
                        }
                    }
                    if (entity.PostalCodeTarget != null)
                    {
                        foreach (var bid in entity.PostalCodeTarget.Bids)
                        {
                            // Output the Campaign Management PostalCodeTargetBid Object
                            OutputPostalCodeTargetBid(bid);
                        }
                    }
                    if (entity.StateTarget != null)
                    {
                        foreach (var bid in entity.StateTarget.Bids)
                        {
                            // Output the Campaign Management StateTargetBid Object
                            OutputStateTargetBid(bid);
                        }
                    }
                }
                else
                {
                    OutputBulkCampaignNegativeLocationTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeLocationTargetBid.
        /// </summary>
        protected void OutputBulkCampaignNegativeLocationTargetBids(IEnumerable<BulkCampaignNegativeLocationTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignNegativeLocationTargetBid: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Location: {0}", entity.Location));
                OutputStatusMessage(string.Format("LocationType: {0}", entity.LocationType));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignNegativeSites that can be written as a Campaign Negative Site record in a Bulk file. 
        /// </summary>
        protected BulkCampaignNegativeSites GetExampleBulkCampaignNegativeSites()
        {
            return new BulkCampaignNegativeSites
            {
                CampaignNegativeSites = new CampaignNegativeSites
                {
                    CampaignId = campaignIdKey,
                    NegativeSites = GetExampleNegativeSites(),
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeSites.
        /// </summary>
        protected void OutputBulkCampaignNegativeSites(IEnumerable<BulkCampaignNegativeSites> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignNegativeSites object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignNegativeSites.NegativeSites objects. For example if you want to print the 
                // BulkCampaignNegativeSites prior to upload.
                if (entity.NegativeSites.Count == 0)
                {
                    OutputStatusMessage("\nBulkCampaignNegativeSites: \n");
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                    OutputNegativeSites(entity.CampaignNegativeSites.NegativeSites);
                }
                else
                {
                    OutputBulkCampaignNegativeSite(entity.NegativeSites);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignNegativeSite.
        /// </summary>
        protected void OutputBulkCampaignNegativeSite(IEnumerable<BulkCampaignNegativeSite> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignNegativeSite: \n");
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputNegativeSites(new[] { entity.Website });

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignRadiusTarget that can be written as a Campaign Radius Target record in a Bulk file. 
        /// </summary>
        protected BulkCampaignRadiusTarget GetExampleBulkCampaignRadiusTarget()
        {
            return new BulkCampaignRadiusTarget
            {
                CampaignId = campaignIdKey,
                TargetId = targetIdKey,
                RadiusTarget = GetExampleRadiusTarget2(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignRadiusTarget.
        /// </summary>
        protected void OutputBulkCampaignRadiusTargets(IEnumerable<BulkCampaignRadiusTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                // If the BulkCampaignRadiusTarget object was created by the application, and not read from a bulk file, 
                // then there will be no BulkCampaignRadiusTargetBid objects. For example if you want to print the 
                // BulkCampaignRadiusTarget prior to upload.
                if (entity.Bids.Count == 0 && entity.RadiusTarget != null)
                {
                    OutputStatusMessage("\nBulkCampaignRadiusTarget: \n");
                    OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                    OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                    OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));
                    OutputStatusMessage(string.Format("IntentOption: {0}", entity.IntentOption));
                    OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                    OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                    foreach (var bid in entity.RadiusTarget.Bids)
                    {
                        // Output the Campaign Management RadiusTargetBid2 Object
                        OutputRadiusTargetBid2(bid);
                    }
                }
                else
                {
                    OutputBulkCampaignRadiusTargetBids(entity.Bids);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkCampaignRadiusTargetBid.
        /// </summary>
        protected void OutputBulkCampaignRadiusTargetBids(IEnumerable<BulkCampaignRadiusTargetBid> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignRadiusTargetBid: \n");
                OutputStatusMessage(string.Format("CampaignId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("TargetId: {0}", entity.TargetId));
                OutputStatusMessage(string.Format("IntentOption: {0}", entity.IntentOption));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                // Output the Campaign Management RadiusTargetBid2 Object
                OutputRadiusTargetBid2(entity.RadiusTargetBid);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignSiteLinkAdExtension that can be written as a Campaign Sitelink Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkCampaignSiteLinkAdExtension GetExampleBulkCampaignSiteLinkAdExtension()
        {
            return new BulkCampaignSiteLinkAdExtension
            {
                AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation
                {
                    AdExtensionId = siteLinksAdExtensionIdKey,
                    EntityId = campaignIdKey,
                }
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignSiteLinkAdExtension.
        /// </summary>
        protected void OutputBulkCampaignSiteLinkAdExtensions(IEnumerable<BulkCampaignSiteLinkAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignSiteLinkAdExtension: \n");
                if (entity.AdExtensionIdToEntityIdAssociation != null)
                {
                    OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionIdToEntityIdAssociation.AdExtensionId));
                    OutputStatusMessage(string.Format("EntityId (Campaign Id): {0}", entity.AdExtensionIdToEntityIdAssociation.EntityId));
                }
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", entity.EditorialStatus));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkCampaignTarget that can be written as a one or more campaign target records in a Bulk file.
        /// </summary>
        protected BulkCampaignTarget GetExampleBulkCampaignTarget()
        {
            var target2 = GetExampleTarget2();
            target2.Id = targetIdKey;

            return new BulkCampaignTarget
            {
                CampaignId = campaignIdKey,
                Target = target2,
            };
        }

        /// <summary>
        /// Outputs the list of BulkCampaignTarget.
        /// </summary>
        protected void OutputBulkCampaignTargets(IEnumerable<BulkCampaignTarget> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkCampaignTarget: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.CampaignId));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                // Output the Campaign Management Target2 Object
                OutputTarget2(entity.Target);

                OutputBulkCampaignAgeTargets(new[] { entity.AgeTarget });
                OutputBulkCampaignDayTimeTargets(new[] { entity.DayTimeTarget });
                OutputBulkCampaignDeviceOsTargets(new[] { entity.DeviceOsTarget });
                OutputBulkCampaignGenderTargets(new[] { entity.GenderTarget });
                OutputBulkCampaignLocationTargets(new[] { entity.LocationTarget });
                OutputBulkCampaignNegativeLocationTargets(new[] { entity.NegativeLocationTarget });
                OutputBulkCampaignRadiusTargets(new[] { entity.RadiusTarget });
            }
        }

        /// <summary>
        /// Gets an example BulkImageAdExtension that can be written as an Image Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkImageAdExtension GetExampleBulkImageAdExtension(long accountId, long imageMediaId)
        {
            var imageAdExtension = GetExampleImageAdExtension(imageMediaId);
            imageAdExtension.Id = imageAdExtensionIdKey;

            return new BulkImageAdExtension
            {
                AccountId = accountId,
                ImageAdExtension = imageAdExtension,
            };
        }

        /// <summary>
        /// Outputs the list of BulkImageAdExtension.
        /// </summary>
        protected void OutputBulkImageAdExtensions(IEnumerable<BulkImageAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkImageAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management ImageAdExtension Object
                OutputImageAdExtension(entity.ImageAdExtension);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkKeyword that can be written as a Keyword record in a Bulk file. 
        /// </summary>
        protected BulkKeyword GetExampleBulkKeyword()
        {
            return new BulkKeyword
            {
                AdGroupId = adGroupIdKey,
                Keyword = GetExampleKeyword(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkKeyword.
        /// </summary>
        protected void OutputBulkKeywords(IEnumerable<BulkKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkKeyword: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);
                OutputBulkQualityScoreData(entity.QualityScoreData);
                OutputBulkBidSuggestions(entity.BidSuggestions);

                // Output the Campaign Management Keyword Object
                OutputKeyword(entity.Keyword);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkLocationAdExtension that can be written as a Location Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkLocationAdExtension GetExampleBulkLocationAdExtension(long accountId)
        {
            var locationAdExtension = GetExampleLocationAdExtension();
            locationAdExtension.Id = locationAdExtensionIdKey;

            return new BulkLocationAdExtension
            {
                AccountId = accountId,
                LocationAdExtension = locationAdExtension,
            };
        }

        /// <summary>
        /// Outputs the list of BulkLocationAdExtension.
        /// </summary>
        protected void OutputBulkLocationAdExtensions(IEnumerable<BulkLocationAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkLocationAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management LocationAdExtension Object
                OutputLocationAdExtension(entity.LocationAdExtension);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkMobileAd that can be written as a Mobile Ad record in a Bulk file. 
        /// </summary>
        protected BulkMobileAd GetExampleBulkMobileAd()
        {
            return new BulkMobileAd
            {
                AdGroupId = adGroupIdKey,
                MobileAd = GetExampleMobileAd(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkMobileAd.
        /// </summary>
        protected void OutputBulkMobileAds(IEnumerable<BulkMobileAd> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkMobileAd: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);

                // Output the Campaign Management MobileAd Object
                OutputMobileAd(entity.MobileAd);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkNegativeKeywordList that can be written as a Negative Keyword List record in a Bulk file. 
        /// </summary>
        protected BulkNegativeKeywordList GetExampleBulkNegativeKeywordList()
        {
            var negativeKeywordList = GetExampleNegativeKeywordList();
            negativeKeywordList.Id = negativeKeywordListIdKey;

            return new BulkNegativeKeywordList
            {
                NegativeKeywordList = negativeKeywordList,
            };
        }

        /// <summary>
        /// Outputs the list of BulkNegativeKeywordList.
        /// </summary>
        protected void OutputBulkNegativeKeywordLists(IEnumerable<BulkNegativeKeywordList> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkNegativeKeywordList: \n");
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputNegativeKeywordList(entity.NegativeKeywordList);
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.Status));

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkProductAd that can be written as a Product Ad record in a Bulk file. 
        /// </summary>
        protected BulkProductAd GetExampleBulkProductAd()
        {
            return new BulkProductAd
            {
                AdGroupId = adGroupIdKey,
                ProductAd = GetExampleProductAd(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkProductAd.
        /// </summary>
        protected void OutputBulkProductAds(IEnumerable<BulkProductAd> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkProductAd: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);

                // Output the Campaign Management ProductAd Object
                OutputProductAd(entity.ProductAd);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkSharedNegativeKeyword that can be written as a Shared Negative Keyword record in a Bulk file. 
        /// </summary>
        protected BulkSharedNegativeKeyword GetExampleBulkSharedNegativeKeyword()
        {
            return new BulkSharedNegativeKeyword
            {
                NegativeKeyword = GetExampleNegativeKeyword(),
                NegativeKeywordListId = negativeKeywordListIdKey,
            };
        }

        /// <summary>
        /// Outputs the list of BulkSharedNegativeKeyword.
        /// </summary>
        protected void OutputBulkSharedNegativeKeywords(IEnumerable<BulkSharedNegativeKeyword> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkSharedNegativeKeyword: \n");
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputNegativeKeyword(entity.NegativeKeyword);
                OutputStatusMessage(string.Format("NegativeKeywordListId: {0}", entity.NegativeKeywordListId));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkSiteLinkAdExtension that can be written as a Sitelink Ad Extension record in a Bulk file. 
        /// </summary>
        protected BulkSiteLinkAdExtension GetExampleBulkSiteLinkAdExtension(long accountId)
        {
            // Note that if you do not specify a negative Id as reference key, each of SiteLinks items will
            // be split during upload into separate sitelink ad extensions with unique ad extension identifiers.
            var siteLinksAdExtension = GetExampleSiteLinksAdExtension();
            siteLinksAdExtension.Id = siteLinksAdExtensionIdKey;

            return new BulkSiteLinkAdExtension
            {
                AccountId = accountId,
                SiteLinksAdExtension = siteLinksAdExtension,
            };
            // Note that BulkSiteLinkAdExtension.SiteLinks is read only and only 
            // accessible when reading results from the download or upload results file.
            // To upload new site links for a new site links ad extension, you should specify
            // BulkSiteLinkAdExtension.SiteLinksAdExtension.SiteLinks as shown above.
        }

        /// <summary>
        /// Outputs the list of BulkSiteLinkAdExtension.
        /// </summary>
        protected void OutputBulkSiteLinkAdExtensions(IEnumerable<BulkSiteLinkAdExtension> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkSiteLinkAdExtension: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                // Output the Campaign Management SiteLinksAdExtension Object
                OutputSiteLinksAdExtension(entity.SiteLinksAdExtension);

                if (entity.SiteLinks != null && entity.SiteLinks.Count > 0)
                {
                    OutputBulkSiteLinks(entity.SiteLinks);
                }
            }
        }

        /// <summary>
        /// Outputs the list of BulkSiteLink.
        /// </summary>
        protected void OutputBulkSiteLinks(IEnumerable<BulkSiteLink> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkSiteLink: \n");
                OutputStatusMessage(string.Format("AccountId: {0}", entity.AccountId));
                OutputStatusMessage(string.Format("AdExtensionId: {0}", entity.AdExtensionId));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));
                OutputStatusMessage(string.Format("Order: {0}", entity.Order));
                OutputStatusMessage(string.Format("Status: {0}", entity.Status));
                OutputStatusMessage(string.Format("Version: {0}", entity.Version));

                // Output the Campaign Management SiteLink Object
                OutputSiteLinks(new[] { entity.SiteLink });

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Gets an example BulkTextAd that can be written as a Text Ad record in a Bulk file. 
        /// </summary>
        protected BulkTextAd GetExampleBulkTextAd()
        {
            return new BulkTextAd
            {
                AdGroupId = adGroupIdKey,
                TextAd = GetExampleTextAd(),
            };
        }

        /// <summary>
        /// Outputs the list of BulkTextAd.
        /// </summary>
        protected void OutputBulkTextAds(IEnumerable<BulkTextAd> bulkEntities)
        {
            foreach (var entity in bulkEntities)
            {
                OutputStatusMessage("\nBulkTextAd: \n");
                OutputStatusMessage(string.Format("AdGroupId: {0}", entity.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupName: {0}", entity.AdGroupName));
                OutputStatusMessage(string.Format("CampaignName: {0}", entity.CampaignName));
                OutputStatusMessage(string.Format("ClientId: {0}", entity.ClientId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", entity.LastModifiedTime));

                OutputBulkPerformanceData(entity.PerformanceData);

                // Output the Campaign Management TextAd Object
                OutputTextAd(entity.TextAd);

                if (entity.HasErrors)
                {
                    OutputErrors(entity.Errors);
                }
            }
        }

        /// <summary>
        /// Outputs the PerformanceData
        /// </summary>
        private void OutputBulkPerformanceData(PerformanceData performanceData)
        {
            if (performanceData != null)
            {
                OutputStatusMessage("PerformanceData: ");
                OutputStatusMessage(string.Format("AverageCostPerClick: {0}", performanceData.AverageCostPerClick));
                OutputStatusMessage(string.Format("AverageCostPerThousandImpressions: {0}", performanceData.AverageCostPerThousandImpressions));
                OutputStatusMessage(string.Format("AveragePosition: {0}", performanceData.AveragePosition));
                OutputStatusMessage(string.Format("Clicks: {0}", performanceData.Clicks));
                OutputStatusMessage(string.Format("ClickThroughRate: {0}", performanceData.ClickThroughRate));
                OutputStatusMessage(string.Format("Conversions: {0}", performanceData.Conversions));
                OutputStatusMessage(string.Format("CostPerConversion: {0}", performanceData.CostPerConversion));
                OutputStatusMessage(string.Format("Impressions: {0}", performanceData.Impressions));
                OutputStatusMessage(string.Format("Spend: {0}", performanceData.Spend));
            }
        }

        /// <summary>
        /// Outputs the PerformanceData
        /// </summary>
        private void OutputBulkQualityScoreData(QualityScoreData qualityScoreData)
        {
            if (qualityScoreData != null)
            {
                OutputStatusMessage("QualityScoreData: ");
                OutputStatusMessage(string.Format("KeywordRelevance: {0}", qualityScoreData.KeywordRelevance));
                OutputStatusMessage(string.Format("LandingPageRelevance: {0}", qualityScoreData.LandingPageRelevance));
                OutputStatusMessage(string.Format("LandingPageUserExperience: {0}", qualityScoreData.LandingPageUserExperience));
                OutputStatusMessage(string.Format("QualityScore: {0}", qualityScoreData.QualityScore));
            }
        }

        /// <summary>
        /// Outputs the PerformanceData
        /// </summary>
        private void OutputBulkBidSuggestions(BidSuggestionData bidSuggestions)
        {
            if (bidSuggestions != null)
            {
                OutputStatusMessage("BidSuggestions: ");
                OutputStatusMessage(string.Format("BestPosition: {0}", bidSuggestions.BestPosition));
                OutputStatusMessage(string.Format("MainLine: {0}", bidSuggestions.MainLine));
                OutputStatusMessage(string.Format("FirstPage: {0}", bidSuggestions.FirstPage));
            }
        }

        /// <summary>
        /// Outputs the list of BulkError.
        /// </summary>
        /// <param name="errors"></param>
        private void OutputErrors(IEnumerable<BulkError> errors)
        {
            foreach (var error in errors)
            {
                OutputStatusMessage(string.Format("Error: {0}", error.Error));
                OutputStatusMessage(string.Format("Number: {0}", error.Number));
            }
        }

    }
}

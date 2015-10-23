using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.CampaignManagement;

namespace BingAdsExamples.V9
{
    /// <summary>
    /// Provides a base class for extending and experimenting with Bing Ads examples. 
    /// </summary>
    public abstract class ExampleBase : BingAdsExamples.ExampleBase
    {
        /// <summary>
        /// Initializes a new instance of the ExampleBase class, and sets the default output status message.
        /// </summary>
        protected ExampleBase()
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }

        /// <summary>
        /// Write to the console by default, if the example does not implement its own OutputStatusMessage method.
        /// </summary>
        /// <param name="msg">The message sent to console output.</param>
        private void OutputStatusMessageDefault(String msg)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Gets an example Campaign. 
        /// </summary>
        protected Campaign GetExampleCampaign()
        {
            return new Campaign
            {
                Id = null,
                Name = "Women's Shoes " + DateTime.UtcNow,
                Description = "Red shoes line.",
                BudgetType = BudgetLimitType.MonthlyBudgetSpendUntilDepleted,
                MonthlyBudget = 1000.00,
                TimeZone = "PacificTimeUSCanadaTijuana",
                DaylightSaving = true,
            };
        }

        /// <summary>
        /// Outputs the Campaign.
        /// </summary>
        protected void OutputCampaign(Campaign campaign)
        {
            if (campaign != null)
            {
                OutputStatusMessage(string.Format("BudgetType: {0}", campaign.BudgetType));
                OutputStatusMessage(string.Format("CampaignType: {0}", campaign.CampaignType));
                OutputStatusMessage(string.Format("DailyBudget: {0}", campaign.DailyBudget));
                OutputStatusMessage(string.Format("Description: {0}", campaign.Description));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (campaign.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in campaign.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", campaign.Id));
                OutputStatusMessage(string.Format("MonthlyBudget: {0}", campaign.MonthlyBudget));
                OutputStatusMessage(string.Format("Name: {0}", campaign.Name));
                OutputStatusMessage("Settings: \n");
                if (campaign.Settings != null)
                {
                    foreach (var setting in campaign.Settings)
                    {
                        var shoppingSetting = setting as ShoppingSetting;
                        if (shoppingSetting != null)
                        {
                            OutputStatusMessage("ShoppingSetting: \n");
                            OutputStatusMessage(string.Format("Priority: {0}", shoppingSetting.Priority));
                            OutputStatusMessage(string.Format("SalesCountryCode: {0}", shoppingSetting.SalesCountryCode));
                            OutputStatusMessage(string.Format("StoreId: {0}", shoppingSetting.StoreId));
                        }
                    }
                }
                OutputStatusMessage(string.Format("Status: {0}", campaign.Status));
                OutputStatusMessage(string.Format("TimeZone: {0}", campaign.TimeZone));
            }
        }

        /// <summary>
        /// Gets an example AdGroup. 
        /// </summary>
        protected AdGroup GetExampleAdGroup()
        {
            return new AdGroup
            {
                Name = "Women's Red Shoe Sale " + DateTime.UtcNow,
                AdDistribution = AdDistribution.Search,
                PricingModel = PricingModel.Cpc,
                StartDate = null,
                EndDate = new Microsoft.BingAds.CampaignManagement.Date { Month = 12, Day = 31, Year = DateTime.UtcNow.Year },
                ExactMatchBid = new Bid { Amount = 0.09 },
                PhraseMatchBid = new Bid { Amount = 0.07 },
                Language = "English",
            };
        }

        /// <summary>
        /// Outputs the AdGroup.
        /// </summary>
        protected void OutputAdGroup(AdGroup adGroup)
        {
            if (adGroup != null)
            {
                OutputStatusMessage(string.Format("AdDistribution: {0}", adGroup.AdDistribution));
                OutputStatusMessage(string.Format("AdRotation Type: {0}",
                    adGroup.AdRotation != null ? adGroup.AdRotation.Type : null));
                OutputStatusMessage(string.Format("BiddingModel: {0}", adGroup.BiddingModel));
                OutputStatusMessage(string.Format("BroadMatchBid: {0}",
                    adGroup.BroadMatchBid != null ? adGroup.BroadMatchBid.Amount : 0));
                OutputStatusMessage(string.Format("ContentMatchBid: {0}", adGroup.ContentMatchBid));
                if (adGroup.EndDate != null)
                {
                    OutputStatusMessage(string.Format("EndDate: {0}/{1}/{2}",
                    adGroup.EndDate.Month,
                    adGroup.EndDate.Day,
                    adGroup.EndDate.Year));
                }
                OutputStatusMessage(string.Format("ExactMatchBid: {0}",
                    adGroup.ExactMatchBid != null ? adGroup.ExactMatchBid.Amount : 0));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (adGroup.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in adGroup.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", adGroup.Id));
                OutputStatusMessage(string.Format("Language: {0}", adGroup.Language));
                OutputStatusMessage(string.Format("Name: {0}", adGroup.Name));
                OutputStatusMessage(string.Format("NativeBidAdjustment: {0}", adGroup.NativeBidAdjustment));
                OutputStatusMessage(string.Format("Network: {0}", adGroup.Network));
                OutputStatusMessage(string.Format("PhraseMatchBid: {0}",
                        adGroup.PhraseMatchBid != null ? adGroup.PhraseMatchBid.Amount : 0));
                OutputStatusMessage(string.Format("PricingModel: {0}", adGroup.PricingModel));
                if (adGroup.StartDate != null)
                {
                    OutputStatusMessage(string.Format("StartDate: {0}/{1}/{2}",
                    adGroup.StartDate.Month,
                    adGroup.StartDate.Day,
                    adGroup.StartDate.Year));
                }
                OutputStatusMessage(string.Format("Status: {0}", adGroup.Status));
            }
        }

        /// <summary>
        /// Gets an example Keyword. 
        /// </summary>
        protected Keyword GetExampleKeyword()
        {
            return new Keyword
            {
                Bid = new Bid { Amount = 0.47 },
                Param2 = "10% Off",
                MatchType = MatchType.Phrase,
                Text = "Brand-A Shoes"
            };
        }

        /// <summary>
        /// Outputs the Keyword.
        /// </summary>
        protected void OutputKeyword(Keyword keyword)
        {
            if (keyword != null)
            {
                if (keyword.Bid != null)
                {
                    OutputStatusMessage(string.Format("Bid.Amount: {0}", keyword.Bid.Amount));
                }

                OutputStatusMessage(string.Format("DestinationUrl: {0}", keyword.DestinationUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", keyword.EditorialStatus));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (keyword.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in keyword.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", keyword.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", keyword.MatchType));
                OutputStatusMessage(string.Format("Param1: {0}", keyword.Param1));
                OutputStatusMessage(string.Format("Param2: {0}", keyword.Param2));
                OutputStatusMessage(string.Format("Param3: {0}", keyword.Param3));
                OutputStatusMessage(string.Format("Status: {0}", keyword.Status));
                OutputStatusMessage(string.Format("Text: {0}", keyword.Text));
            }
        }

        /// <summary>
        /// Gets an example MobileAd. 
        /// </summary>
        protected MobileAd GetExampleMobileAd()
        {
            return new MobileAd
            {
                BusinessName = "Contoso",
                DestinationUrl = "http://www.contoso.com/womenshoesale",
                DisplayUrl = "Contoso.com",
                PhoneNumber = "206-555-0100",
                Text = "Huge Savings.",
                Title = "Women's Shoe Sale"
            };
        }

        /// <summary>
        /// Outputs the MobileAd.
        /// </summary>
        protected void OutputMobileAd(MobileAd ad)
        {
            if (ad != null)
            {
                OutputStatusMessage(string.Format("BusinessName: {0}", ad.BusinessName));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", ad.DestinationUrl));
                OutputStatusMessage(string.Format("DevicePreference: {0}", ad.DevicePreference));
                OutputStatusMessage(string.Format("DisplayUrl: {0}", ad.DisplayUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", ad.EditorialStatus));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (ad.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in ad.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", ad.Id));
                OutputStatusMessage(string.Format("PhoneNumber: {0}", ad.PhoneNumber));
                OutputStatusMessage(string.Format("Status: {0}", ad.Status));
                OutputStatusMessage(string.Format("Text: {0}", ad.Text));
                OutputStatusMessage(string.Format("Title: {0}", ad.Title));
            }
        }

        /// <summary>
        /// Gets an example ProductAd. 
        /// </summary>
        protected ProductAd GetExampleProductAd()
        {
            return new ProductAd
            {
                PromotionalText = "Free shipping on $99 purchases."
            };
        }

        /// <summary>
        /// Outputs the ProductAd.
        /// </summary>
        protected void OutputProductAd(ProductAd ad)
        {
            if (ad != null)
            {
                OutputStatusMessage(string.Format("DevicePreference: {0}", ad.DevicePreference));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", ad.EditorialStatus));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (ad.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in ad.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", ad.Id));
                OutputStatusMessage(string.Format("PromotionalText: {0}", ad.PromotionalText));
                OutputStatusMessage(string.Format("Status: {0}", ad.Status));
            }
        }

        /// <summary>
        /// Gets an example TextAd. 
        /// </summary>
        protected TextAd GetExampleTextAd()
        {
            return new TextAd
            {
                DestinationUrl = "http://www.contoso.com/womenshoesale",
                DisplayUrl = "Contoso.com",
                Text = "Huge Savings on red shoes.",
                Title = "Women's Shoe Sale"
            };
        }

        /// <summary>
        /// Outputs the TextAd.
        /// </summary>
        protected void OutputTextAd(TextAd ad)
        {
            if (ad != null)
            {
                OutputStatusMessage(string.Format("DestinationUrl: {0}", ad.DestinationUrl));
                OutputStatusMessage(string.Format("DevicePreference: {0}", ad.DevicePreference));
                OutputStatusMessage(string.Format("DisplayUrl: {0}", ad.DisplayUrl));
                OutputStatusMessage(string.Format("EditorialStatus: {0}", ad.EditorialStatus));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (ad.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in ad.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", ad.Id));
                OutputStatusMessage(string.Format("Status: {0}", ad.Status));
                OutputStatusMessage(string.Format("Text: {0}", ad.Text));
                OutputStatusMessage(string.Format("Title: {0}", ad.Title));
            }
        }

        /// <summary>
        /// Gets an example ProductPartition. 
        /// </summary>
        protected ProductPartition GetExampleProductPartition()
        {
            return new ProductPartition
            {
                // This is an example of a root node, because it does not have a parent.
                ParentCriterionId = null,
                Condition = new ProductCondition
                {
                    Operand = "All",
                    Attribute = null
                },
                PartitionType = ProductPartitionType.Unit
            };
        }

        /// <summary>
        /// Outputs the ProductPartition.
        /// </summary>
        protected void OutputProductPartition(ProductPartition productPartition)
        {
            if (productPartition != null)
            {
                OutputStatusMessage(string.Format("ParentCriterionId: {0}", productPartition.ParentCriterionId));
                OutputStatusMessage(string.Format("PartitionType: {0}", productPartition.PartitionType));
                if (productPartition.Condition != null)
                {
                    OutputStatusMessage(string.Format("Condition: "));
                    OutputStatusMessage(string.Format("\tOperand: {0}", productPartition.Condition.Operand));
                    OutputStatusMessage(string.Format("\tAttribute: {0}", productPartition.Condition.Attribute));
                }
            }
        }

        /// <summary>
        /// Gets an example FixedBid. 
        /// </summary>
        protected FixedBid GetExampleFixedBid()
        {
            return new FixedBid
            {
                Bid = new Bid()
                {
                    Amount = 0.35
                },
            };
        }

        /// <summary>
        /// Outputs the FixedBid.
        /// </summary>
        protected void OutputFixedBid(FixedBid fixedBid)
        {
            if (fixedBid != null && fixedBid.Bid != null)
            {
                OutputStatusMessage(string.Format("Bid Amount: {0}", fixedBid.Bid.Amount));
            }
        }

        /// <summary>
        /// Gets an example CampaignCriterion that contains ProductPartition. 
        /// </summary>
        protected AdGroupCriterion GetExampleAdGroupCriterionWithProductPartition()
        {
            return new BiddableAdGroupCriterion
            {
                Criterion = GetExampleProductPartition(),
                CriterionBid = GetExampleFixedBid(),
            };
        }

        /// <summary>
        /// Outputs the AdGroupCriterion that contains a ProductPartition.
        /// </summary>
        protected void OutputAdGroupCriterionWithProductPartition(AdGroupCriterion adGroupCriterion)
        {
            if (adGroupCriterion != null)
            {
                OutputStatusMessage(string.Format("AdGroupId: {0}", adGroupCriterion.AdGroupId));
                OutputStatusMessage(string.Format("AdGroupCriterion Id: {0}", adGroupCriterion.Id));
                OutputStatusMessage(string.Format("AdGroupCriterion Type: {0}", adGroupCriterion.Type));

                var biddableAdGroupCriterion = adGroupCriterion as BiddableAdGroupCriterion;
                if (biddableAdGroupCriterion != null)
                {
                    OutputStatusMessage(string.Format("DestinationUrl: {0}", biddableAdGroupCriterion.DestinationUrl));

                    // Output the Campaign Management FixedBid Object
                    OutputFixedBid((FixedBid)biddableAdGroupCriterion.CriterionBid);
                }
                else
                {
                    var negativeAdGroupCriterion = adGroupCriterion as NegativeAdGroupCriterion;
                    if (negativeAdGroupCriterion != null)
                    {
                    }
                }

                // Output the Campaign Management ProductPartition Object
                OutputProductPartition((ProductPartition)adGroupCriterion.Criterion);
            }
        }

        /// <summary>
        /// Gets an example ProductScope. 
        /// </summary>
        protected ProductScope GetExampleProductScope()
        {
            return new ProductScope
            {
                Conditions = new ProductCondition[] {
                    new ProductCondition {
                        Operand = "Condition",
                        Attribute = "New"
                    },
                    new ProductCondition {
                        Operand = "CustomLabel0",
                        Attribute = "MerchantDefinedCustomLabel"
                    },
                }
            };
        }

        /// <summary>
        /// Outputs the ProductScope.
        /// </summary>
        protected void OutputProductScope(ProductScope productScope)
        {
            if (productScope != null)
            {
                OutputStatusMessage(string.Format("Product Conditions: \n"));
                foreach (var condition in productScope.Conditions)
                {
                    OutputStatusMessage(string.Format("Operand: {0}", condition.Operand));
                    OutputStatusMessage(string.Format("Attribute: {0}", condition.Attribute));
                }
            }
        }

        /// <summary>
        /// Gets an example CampaignCriterion that contains ProductScope. 
        /// </summary>
        protected CampaignCriterion GetExampleCampaignCriterionWithProductScope()
        {
            return new CampaignCriterion
            {
                BidAdjustment = 0,
                Criterion = GetExampleProductScope(),
            };
        }

        /// <summary>
        /// Outputs the CampaignCriterion that contains a Product Scope.
        /// </summary>
        protected void OutputCampaignCriterionWithProductScope(CampaignCriterion campaignCriterion)
        {
            if (campaignCriterion != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", campaignCriterion.BidAdjustment));
                OutputStatusMessage(string.Format("CampaignId: {0}", campaignCriterion.CampaignId));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (campaignCriterion.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in campaignCriterion.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("CampaignCriterion Id: {0}", campaignCriterion.Id));
                OutputStatusMessage(string.Format("CampaignCriterion Type: {0}", campaignCriterion.Type));

                // Output the Campaign Management ProductScope Object
                OutputProductScope((ProductScope)campaignCriterion.Criterion);
            }
        }

        /// <summary>
        /// Gets an example AgeTarget. 
        /// </summary>
        protected AgeTarget GetExampleAgeTarget()
        {
            return new AgeTarget
            {
                Bids = new List<AgeTargetBid> 
                {
                    new AgeTargetBid{
                        Age = AgeRange.ThirtyFiveToFifty,
                        BidAdjustment = 10,
                    }
                }
            };
        }

        /// <summary>
        /// Gets an example AgeTargetBid. 
        /// </summary>
        protected AgeTargetBid GetExampleAgeTargetBid()
        {
            return new AgeTargetBid
            {
                Age = AgeRange.ThirtyFiveToFifty,
                BidAdjustment = 10,
            };
        }

        /// <summary>
        /// Outputs the AgeTargetBid.
        /// </summary>
        protected void OutputAgeTargetBid(AgeTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Age : {0}", targetBid.Age));
            }
        }

        /// <summary>
        /// Gets an example GenderTarget. 
        /// </summary>
        protected GenderTarget GetExampleGenderTarget()
        {
            return new GenderTarget
            {
                Bids = new List<GenderTargetBid> 
                {
                    new GenderTargetBid 
                    {
                        BidAdjustment = 10,
                        Gender = GenderType.Female,
                    },
                }
            };
        }

        /// <summary>
        /// Outputs the GenderTargetBid.
        /// </summary>
        protected void OutputGenderTargetBid(GenderTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Gender : {0}", targetBid.Gender));
            }
        }

        /// <summary>
        /// Gets an example DayTimeTarget. 
        /// </summary>
        protected DayTimeTarget GetExampleDayTimeTarget()
        {
            return new DayTimeTarget
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
                }
            };
        }

        /// <summary>
        /// Outputs the DayTimeTargetBid.
        /// </summary>
        protected void OutputDayTimeTargetBid(DayTimeTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Day : {0}", targetBid.Day));
                OutputStatusMessage(string.Format("FromHour : {0}", targetBid.FromHour));
                OutputStatusMessage(string.Format("FromMinute: {0}", targetBid.FromMinute));
                OutputStatusMessage(string.Format("ToHour : {0}", targetBid.ToHour));
                OutputStatusMessage(string.Format("ToMinute: {0}", targetBid.ToMinute));
            }
        }

        /// <summary>
        /// Gets an example DeviceOSTarget. 
        /// </summary>
        protected DeviceOSTarget GetExampleDeviceOSTarget()
        {
            return new DeviceOSTarget
            {
                Bids = new List<DeviceOSTargetBid> 
                {
                    new DeviceOSTargetBid
                    {
                        BidAdjustment = 10,
                        DeviceName = "Tablets"
                    },
                }
            };
        }

        /// <summary>
        /// Outputs the DeviceOSTargetBid.
        /// </summary>
        protected void OutputDeviceOSTargetBid(DeviceOSTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("DeviceName : {0}", targetBid.DeviceName));
            }
        }

        /// <summary>
        /// Gets an example Target2. 
        /// </summary>
        protected Target2 GetExampleTarget2()
        {
            return new Target2()
            {
                Id = null,
                Age = GetExampleAgeTarget(),
                DayTime = GetExampleDayTimeTarget(),
                DeviceOS = GetExampleDeviceOSTarget(),
                Gender = GetExampleGenderTarget(),
                Location = GetExampleLocationTarget2(),
            };
        }

        /// <summary>
        /// Outputs the Target2.
        /// </summary>
        protected void OutputTarget2(Target2 target)
        {
            if (target != null)
            {
                OutputStatusMessage(string.Format("Target.Id: {0}", target.Id));
                if (target.Age != null)
                {
                    foreach (var bid in target.Age.Bids)
                    {
                        OutputAgeTargetBid(bid);
                    }
                }
                if (target.DayTime != null)
                {
                    foreach (var bid in target.DayTime.Bids)
                    {
                        OutputDayTimeTargetBid(bid);
                    }
                }
                if (target.DeviceOS != null)
                {
                    foreach (var bid in target.DeviceOS.Bids)
                    {
                        OutputDeviceOSTargetBid(bid);
                    }
                }
                if (target.DeviceOS != null)
                {
                    foreach (var bid in target.Gender.Bids)
                    {
                        OutputGenderTargetBid(bid);
                    }
                }
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (target.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in target.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputLocationTarget2(target.Location);
            }
        }

        /// <summary>
        /// Gets an example LocationTarget2. 
        /// </summary>
        protected LocationTarget2 GetExampleLocationTarget2()
        {
            return new LocationTarget2
            {
                IntentOption = IntentOption.PeopleSearchingForOrViewingPages,
                CityTarget = GetExampleCityTarget(),
                CountryTarget = GetExampleCountryTarget(),
                MetroAreaTarget = GetExampleMetroAreaTarget(),
                StateTarget = GetExampleStateTarget(),
                PostalCodeTarget = GetExamplePostalCodeTarget(),
                RadiusTarget = null
            };
        }

        /// <summary>
        /// Outputs the LocationTarget2.
        /// </summary>
        protected void OutputLocationTarget2(LocationTarget2 locationTarget)
        {
            if (locationTarget != null)
            {
                OutputStatusMessage(string.Format("IntentOption: {0}", locationTarget.IntentOption));
                if (locationTarget.CityTarget != null)
                {
                    foreach (var bid in locationTarget.CityTarget.Bids)
                    {
                        OutputCityTargetBid(bid);
                    }
                }
                if (locationTarget.CountryTarget != null)
                {
                    foreach (var bid in locationTarget.CountryTarget.Bids)
                    {
                        OutputCountryTargetBid(bid);
                    }
                }
                if (locationTarget.MetroAreaTarget != null)
                {
                    foreach (var bid in locationTarget.MetroAreaTarget.Bids)
                    {
                        OutputMetroAreaTargetBid(bid);
                    }
                }
                if (locationTarget.PostalCodeTarget != null)
                {
                    foreach (var bid in locationTarget.PostalCodeTarget.Bids)
                    {
                        OutputPostalCodeTargetBid(bid);
                    }
                }
                if (locationTarget.RadiusTarget != null)
                {
                    foreach (var bid in locationTarget.RadiusTarget.Bids)
                    {
                        OutputRadiusTargetBid2(bid);
                    }
                }
                if (locationTarget.StateTarget != null)
                {
                    foreach (var bid in locationTarget.StateTarget.Bids)
                    {
                        OutputStateTargetBid(bid);
                    }
                }
            }
        }

        /// <summary>
        /// Gets an example CityTarget. 
        /// </summary>
        protected CityTarget GetExampleCityTarget()
        {
            return new CityTarget
            {
                Bids = new List<CityTargetBid>
                {
                    new CityTargetBid
                    {
                        BidAdjustment = 15,
                        City = "Toronto, Toronto ON CA",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the CityTargetBid.
        /// </summary>
        protected void OutputCityTargetBid(CityTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("City : {0}", targetBid.City));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example CountryTarget. 
        /// </summary>
        protected CountryTarget GetExampleCountryTarget()
        {
            return new CountryTarget
            {
                Bids = new List<CountryTargetBid>
                {
                    new CountryTargetBid
                    {
                        BidAdjustment = 15,
                        CountryAndRegion = "CA",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the CountryTargetBid.
        /// </summary>
        protected void OutputCountryTargetBid(CountryTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("CountryAndRegion : {0}", targetBid.CountryAndRegion));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example MetroAreaTarget. 
        /// </summary>
        protected MetroAreaTarget GetExampleMetroAreaTarget()
        {
            return new MetroAreaTarget
            {
                Bids = new List<MetroAreaTargetBid>
                {
                    new MetroAreaTargetBid
                    {
                        BidAdjustment = 15,
                        MetroArea = "Seattle-Tacoma, WA, WA US",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the MetroAreaTargetBid.
        /// </summary>
        protected void OutputMetroAreaTargetBid(MetroAreaTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("MetroArea : {0}", targetBid.MetroArea));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example StateTarget. 
        /// </summary>
        protected StateTarget GetExampleStateTarget()
        {
            return new StateTarget
            {
                Bids = new List<StateTargetBid>
                {
                    new StateTargetBid
                    {
                        BidAdjustment = 15,
                        State = "US-WA",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the StateTargetBid.
        /// </summary>
        protected void OutputStateTargetBid(StateTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("State : {0}", targetBid.State));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example PostalCodeTarget. 
        /// </summary>
        protected PostalCodeTarget GetExamplePostalCodeTarget()
        {
            return new PostalCodeTarget
            {
                Bids = new List<PostalCodeTargetBid>
                {
                    new PostalCodeTargetBid
                    {
                        BidAdjustment = 10,
                        PostalCode = "98052, WA US",
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the PostalCodeTargetBid.
        /// </summary>
        protected void OutputPostalCodeTargetBid(PostalCodeTargetBid targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("PostalCode : {0}", targetBid.PostalCode));
                var isExcluded = targetBid.IsExcluded ? "True" : "False";
                OutputStatusMessage(string.Format("IsExcluded: {0}", isExcluded));
            }
        }

        /// <summary>
        /// Gets an example RadiusTarget2. 
        /// </summary>
        protected RadiusTarget2 GetExampleRadiusTarget2()
        {
            return new RadiusTarget2
            {
                Bids = new List<RadiusTargetBid2>
                {
                    new RadiusTargetBid2
                    {
                        BidAdjustment = 50,
                        LatitudeDegrees = 47.755367,
                        LongitudeDegrees = -122.091827,
                        Radius = 11,
                        RadiusUnit = DistanceUnit.Kilometers
                    }
                }
            };
        }

        /// <summary>
        /// Outputs the RadiusTargetBid2.
        /// </summary>
        protected void OutputRadiusTargetBid2(RadiusTargetBid2 targetBid)
        {
            if (targetBid != null)
            {
                OutputStatusMessage(string.Format("BidAdjustment: {0}", targetBid.BidAdjustment));
                OutputStatusMessage(string.Format("Name : {0}", targetBid.Name));
                OutputStatusMessage(string.Format("Radius : {0}", targetBid.Radius));
                var radiusUnit = targetBid.RadiusUnit == DistanceUnit.Kilometers ? "Kilometers" : "Miles";
                OutputStatusMessage(string.Format("RadiusUnit: {0}", radiusUnit));
            }
        }

        /// <summary>
        /// Gets an example NegativeKeyword. 
        /// </summary>
        protected NegativeKeyword GetExampleNegativeKeyword()
        {
            return new NegativeKeyword
            {
                Id = null,
                MatchType = MatchType.Exact,
                Text = "auto",
                Type = "NegativeKeyword"
            };
        }

        /// <summary>
        /// Outputs the NegativeKeyword.
        /// </summary>
        protected void OutputNegativeKeyword(NegativeKeyword negativeKeyword)
        {
            if (negativeKeyword != null)
            {
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (negativeKeyword.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in negativeKeyword.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", negativeKeyword.Id));
                OutputStatusMessage(string.Format("MatchType: {0}", negativeKeyword.MatchType));
                OutputStatusMessage(string.Format("Text: {0}", negativeKeyword.Text));
                OutputStatusMessage(string.Format("Type: {0}", negativeKeyword.Type));
            }
        }

        /// <summary>
        /// Gets an example NegativeKeywordList. 
        /// </summary>
        protected NegativeKeywordList GetExampleNegativeKeywordList()
        {
            return new NegativeKeywordList
            {
                Id = null,
                Name = "My Negative Keyword List" + DateTime.UtcNow,
                Type = "NegativeKeywordList"
            };
        }

        /// <summary>
        /// Outputs the NegativeKeywordList.
        /// </summary>
        protected void OutputNegativeKeywordList(NegativeKeywordList negativeKeywordList)
        {
            if (negativeKeywordList != null)
            {
                OutputStatusMessage(string.Format("AssociationCount: {0}", negativeKeywordList.AssociationCount));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (negativeKeywordList.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in negativeKeywordList.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", negativeKeywordList.Id));
                OutputStatusMessage(string.Format("ItemCount: {0}", negativeKeywordList.ItemCount));
                OutputStatusMessage(string.Format("Name: {0}", negativeKeywordList.Name));
            }
        }

        /// <summary>
        /// Gets a list of example negative websites. 
        /// </summary>
        protected List<string> GetExampleNegativeSites()
        {
            return new List<string>
            {
                "contoso.com/negativesite"
            };
        }

        /// <summary>
        /// Outputs the negative websites.
        /// </summary>
        protected void OutputNegativeSites(IList<string> negativeSites)
        {
            foreach (var negativeSite in negativeSites)
            {
                OutputStatusMessage(string.Format("NegativeSite: {0}", negativeSite));
            }
        }

        /// <summary>
        /// Gets an example CallAdExtension. 
        /// </summary>
        protected AppAdExtension GetExampleAppAdExtension()
        {
            return new AppAdExtension
            {
                AppPlatform = "Windows",
                AppStoreId = "AppStoreIdGoesHere",
                DestinationUrl = "DestinationUrlGoesHere",
                DevicePreference = 0,
                DisplayText = "Contoso",
                Id = null,
            };
        }

        /// <summary>
        /// Outputs the AppAdExtension.
        /// </summary>
        protected void OutputAppAdExtension(AppAdExtension extension)
        {
            if (extension != null)
            {
                OutputStatusMessage(string.Format("AppPlatform: {0}", extension.AppPlatform));
                OutputStatusMessage(string.Format("AppStoreId: {0}", extension.AppStoreId));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", extension.DestinationUrl));
                OutputStatusMessage(string.Format("DevicePreference: {0}", extension.DevicePreference));
                OutputStatusMessage(string.Format("DisplayText: {0}", extension.DisplayText));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (extension.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in extension.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", extension.Id));
                OutputStatusMessage(string.Format("Status: {0}", extension.Status));
                OutputStatusMessage(string.Format("Version: {0}", extension.Version));
            }
        }

        /// <summary>
        /// Gets an example CallAdExtension. 
        /// </summary>
        protected CallAdExtension GetExampleCallAdExtension()
        {
            return new CallAdExtension
            {
                CountryCode = "US",
                PhoneNumber = "2065550100",
                IsCallOnly = false,
                Id = null
            };
        }

        /// <summary>
        /// Outputs the CallAdExtension.
        /// </summary>
        protected void OutputCallAdExtension(CallAdExtension extension)
        {
            if (extension != null)
            {
                OutputStatusMessage(string.Format("CountryCode: {0}", extension.CountryCode));
                OutputStatusMessage(string.Format("DevicePreference: {0}", extension.DevicePreference));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (extension.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in extension.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", extension.Id));
                OutputStatusMessage(string.Format("IsCallOnly: {0}", extension.IsCallOnly));
                OutputStatusMessage(string.Format("IsCallTrackingEnabled: {0}", extension.IsCallTrackingEnabled));
                OutputStatusMessage(string.Format("PhoneNumber: {0}", extension.PhoneNumber));
                OutputStatusMessage(string.Format("RequireTollFreeTrackingNumber: {0}", extension.RequireTollFreeTrackingNumber));
                OutputStatusMessage(string.Format("Status: {0}", extension.Status));
                OutputStatusMessage(string.Format("Version: {0}", extension.Version));
            }
        }

        /// <summary>
        /// Gets an example ImageAdExtension. 
        /// </summary>
        protected ImageAdExtension GetExampleImageAdExtension(long imageMediaId)
        {
            return new ImageAdExtension
            {
                AlternativeText = "Image Alternative Text",
                ImageMediaId = imageMediaId,
                Id = null
            };
        }

        /// <summary>
        /// Outputs the ImageAdExtension.
        /// </summary>
        protected void OutputImageAdExtension(ImageAdExtension extension)
        {
            if (extension != null)
            {
                OutputStatusMessage(string.Format("AlternativeText: {0}", extension.AlternativeText));
                OutputStatusMessage(string.Format("Description: {0}", extension.Description));
                OutputStatusMessage(string.Format("DestinationUrl: {0}", extension.DestinationUrl));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (extension.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in extension.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", extension.Id));
                OutputStatusMessage(string.Format("ImageMediaId: {0}", extension.ImageMediaId));
                OutputStatusMessage(string.Format("Status: {0}", extension.Status));
                OutputStatusMessage(string.Format("Version: {0}", extension.Version));
            }
        }

        /// <summary>
        /// Gets an example LocationAdExtension. 
        /// </summary>
        protected LocationAdExtension GetExampleLocationAdExtension()
        {
            return new LocationAdExtension
            {
                Id = null,
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
            };
        }

        /// <summary>
        /// Outputs the LocationAdExtension.
        /// </summary>
        protected void OutputLocationAdExtension(LocationAdExtension extension)
        {
            if (extension != null)
            {
                if (extension.Address != null)
                {
                    OutputStatusMessage(string.Format("CityName: {0}", extension.Address.CityName));
                    OutputStatusMessage(string.Format("CountryCode: {0}", extension.Address.CountryCode));
                    OutputStatusMessage(string.Format("PostalCode: {0}", extension.Address.PostalCode));
                    OutputStatusMessage(string.Format("ProvinceCode: {0}", extension.Address.ProvinceCode));
                    OutputStatusMessage(string.Format("ProvinceName: {0}", extension.Address.ProvinceName));
                    OutputStatusMessage(string.Format("StreetAddress: {0}", extension.Address.StreetAddress));
                    OutputStatusMessage(string.Format("StreetAddress2: {0}", extension.Address.StreetAddress2));
                }

                OutputStatusMessage(string.Format("CompanyName: {0}", extension.CompanyName));
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (extension.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in extension.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("GeoCodeStatus: {0}", extension.GeoCodeStatus));
                if (extension.GeoPoint != null)
                {
                    OutputStatusMessage("GeoPoint: ");
                    OutputStatusMessage(string.Format("LatitudeInMicroDegrees: {0}", extension.GeoPoint.LatitudeInMicroDegrees));
                    OutputStatusMessage(string.Format("LongitudeInMicroDegrees: {0}", extension.GeoPoint.LongitudeInMicroDegrees));
                }
                OutputStatusMessage(string.Format("IconMediaId: {0}", extension.IconMediaId));
                OutputStatusMessage(string.Format("Id: {0}", extension.Id));
                OutputStatusMessage(string.Format("ImageMediaId: {0}", extension.ImageMediaId));
                OutputStatusMessage(string.Format("PhoneNumber: {0}", extension.PhoneNumber));
                OutputStatusMessage(string.Format("Status: {0}", extension.Status));
                OutputStatusMessage(string.Format("Version: {0}", extension.Version));
            }
        }

        /// <summary>
        /// Gets an example SiteLinksAdExtension. 
        /// </summary>
        protected SiteLinksAdExtension GetExampleSiteLinksAdExtension()
        {
            return new SiteLinksAdExtension
            {
                Id = null,
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
            };
        }

        /// <summary>
        /// Outputs the SiteLinksAdExtension.
        /// </summary>
        protected void OutputSiteLinksAdExtension(SiteLinksAdExtension extension)
        {
            if (extension != null)
            {
                OutputStatusMessage("ForwardCompatibilityMap: ");
                if (extension.ForwardCompatibilityMap != null)
                {
                    foreach (var pair in extension.ForwardCompatibilityMap)
                    {
                        OutputStatusMessage(string.Format("Key: {0}", pair.Key));
                        OutputStatusMessage(string.Format("Value: {0}", pair.Value));
                    }
                }
                OutputStatusMessage(string.Format("Id: {0}", extension.Id));
                OutputStatusMessage(string.Format("Status: {0}", extension.Status));
                OutputStatusMessage(string.Format("Version: {0}", extension.Version));
                OutputSiteLinks(extension.SiteLinks);
            }
        }

        /// <summary>
        /// Outputs the list of SiteLink.
        /// </summary>
        protected void OutputSiteLinks(IList<SiteLink> siteLinks)
        {
            if (siteLinks != null)
            {
                foreach (var siteLink in siteLinks)
                {
                    OutputStatusMessage(string.Format("Description1: {0}", siteLink.Description1));
                    OutputStatusMessage(string.Format("Description2: {0}", siteLink.Description2));
                    OutputStatusMessage(string.Format("DestinationUrl: {0}", siteLink.DestinationUrl));
                    OutputStatusMessage(string.Format("DevicePreference: {0}", siteLink.DevicePreference));
                    OutputStatusMessage(string.Format("DisplayText: {0}", siteLink.DisplayText));
                }
            }
        }
    }
}

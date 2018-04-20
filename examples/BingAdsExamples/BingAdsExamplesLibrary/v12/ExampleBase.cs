using System;
using System.Collections.Generic;
using Microsoft.BingAds.V12.CampaignManagement;

namespace BingAdsExamplesLibrary.V12
{
    /// <summary>
    /// Provides a base class for extending and experimenting with Bing Ads examples. 
    /// </summary>
    public abstract class ExampleBase : BingAdsExamplesLibrary.ExampleBase
    {     
        protected static CampaignType AllCampaignTypes = 
            CampaignType.DynamicSearchAds |
            CampaignType.Search |
            CampaignType.Shopping;

        protected static List<AdType> AllAdTypes = new List<AdType>
        {
            AdType.AppInstall,
            AdType.DynamicSearch,
            AdType.ExpandedText,
            AdType.Product,
            AdType.Text
        };

        protected static CampaignCriterionType AllTargetCampaignCriterionTypes = 
            CampaignCriterionType.Age |
            CampaignCriterionType.DayTime |
            CampaignCriterionType.Device |
            CampaignCriterionType.Gender |
            CampaignCriterionType.Location |
            CampaignCriterionType.LocationIntent |
            CampaignCriterionType.Radius;

        protected static AdGroupCriterionType AllTargetAdGroupCriterionTypes = 
            AdGroupCriterionType.Age |
            AdGroupCriterionType.DayTime |
            AdGroupCriterionType.Device |
            AdGroupCriterionType.Gender |
            AdGroupCriterionType.Location |
            AdGroupCriterionType.LocationIntent |
            AdGroupCriterionType.Radius;

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

        protected List<long> GetNonNullableIds(IList<long?> nullableIds)
        {
            List<long> ids = new List<long>();
            foreach (var nullableId in nullableIds)
            {
                ids.Add((long)nullableId);
            }
            return ids;
        }

        /// <summary>
        /// Set the read-only properties of an ad extension to null. This operation can be useful between calls to
        /// GetAdExtensionsByIds and UpdateAdExtensions. The update operation would fail if you send certain read-only
        /// fields.
        /// </summary>
        /// <param name="extension">The ad extension whose read-only properties you want to nullify.</param>
        /// <returns></returns>
        protected AdExtension SetReadOnlyAdExtensionElementsToNull(AdExtension extension)
        {
            if (extension == null)
            {
                return extension;
            }
            else
            {
                // Set to null for all extension types.
                extension.Version = null;

                var locationAdExtension = extension as LocationAdExtension;
                if (locationAdExtension != null)
                {
                    locationAdExtension.GeoCodeStatus = null;
                    return locationAdExtension;
                }
                else
                {
                    return extension;
                }
            }
        }
    }
}

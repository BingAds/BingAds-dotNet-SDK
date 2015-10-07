using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents one or more negative sites that are assigned to an ad group. Each negative site can be read or written in a bulk file. 
    /// This class exposes properties that can be read and written as fields of the Ad Group Negative Site record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511539">Ad Group Negative Site</see>. </para>
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkAdGroupNegativeSites"/> has one or more <see cref="BulkAdGroupNegativeSite"/>. Each <see cref="BulkAdGroupNegativeSite"/> instance 
    /// corresponds to one Ad Group Negative Site record in the bulk file. If you upload a <see cref="BulkAdGroupNegativeSites"/>, 
    /// then you are effectively replacing any existing negative sites assigned to the ad group. 
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupNegativeSites : BulkNegativeSites<BulkAdGroupNegativeSite, BulkAdGroupNegativeSitesIdentifier>
    {
        /// <summary>
        /// The AdGroupNegativeSites Data Object of the Campaign Management Service. A subset of AdGroupNegativeSites properties are available 
        /// in the Ad Group Negative Site record. For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511539">Ad Group Negative Site</see>.
        /// </summary>
        public AdGroupNegativeSites AdGroupNegativeSites { get; set; }

        /// <summary>
        /// The name of the ad group that the negative site is assigned.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; set; }

        /// <summary>
        /// The name of the campaign that the negative site is assigned.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// Initializes a new instance of the BulkAdGroupNegativeSites class.
        /// </summary>
        public BulkAdGroupNegativeSites()
        {
        }

        internal BulkAdGroupNegativeSites(BulkAdGroupNegativeSite site)
            : base(site)
        {
            SetDataFromIdentifier(site.Identifier);
        }

        internal BulkAdGroupNegativeSites(BulkAdGroupNegativeSitesIdentifier identifier)
            : base(identifier)
        {
            SetDataFromIdentifier(identifier);
        }

        private void SetDataFromIdentifier(BulkAdGroupNegativeSitesIdentifier identifier)
        {
            AdGroupNegativeSites = new AdGroupNegativeSites
            {
                AdGroupId = identifier.AdGroupId
            };

            AdGroupName = identifier.AdGroupName;
            CampaignName = identifier.CampaignName;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IEnumerable<BulkAdGroupNegativeSite> ConvertApiToBulkNegativeSites()
        {
            ValidateListNotNullOrEmpty(AdGroupNegativeSites.NegativeSites, "AdGroupNegativeSites.NegativeSites");

            return AdGroupNegativeSites.NegativeSites.Select(s => new BulkAdGroupNegativeSite
            {
                AdGroupId = AdGroupNegativeSites.AdGroupId,
                Website = s,
                AdGroupName = AdGroupName,
                CampaignName = CampaignName
            });
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructApiObjects()
        {
            AdGroupNegativeSites.NegativeSites = NegativeSites.Select(s => s.Website).ToList();
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override BulkAdGroupNegativeSitesIdentifier CreateIdentifier()
        {
            return new BulkAdGroupNegativeSitesIdentifier
            {
                AdGroupId = AdGroupNegativeSites.AdGroupId,
                AdGroupName = AdGroupName,
                CampaignName = CampaignName
            };
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            ValidatePropertyNotNull(AdGroupNegativeSites, "AdGroupNegativeSites");
        }
    }
}

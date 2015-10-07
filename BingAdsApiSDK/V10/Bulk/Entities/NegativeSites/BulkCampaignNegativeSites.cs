using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents one or more negative sites that are assigned to a campaign. Each negative site can be read or written in a bulk file. 
    /// This class exposes properties that can be read and written as fields of the Campaign Negative Site record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511524">Campaign Negative Site</see>. </para>
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkCampaignNegativeSites"/> has one or more <see cref="BulkCampaignNegativeSite"/>. Each <see cref="BulkCampaignNegativeSite"/> instance 
    /// corresponds to one Campaign Negative Site record in the bulk file. If you upload a <see cref="BulkCampaignNegativeSites"/>, 
    /// then you are effectively replacing any existing negative sites assigned to the campaign. 
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignNegativeSites : BulkNegativeSites<BulkCampaignNegativeSite, BulkCampaignNegativeSitesIdentifier>
    {
        /// <summary>
        /// The CampaignNegativeSites Data Object of the Campaign Management Service. A subset of CampaignNegativeSites properties are available 
        /// in the Campaign Negative Site record. For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511524">Campaign Negative Site</see>. 
        /// </summary>
        public CampaignNegativeSites CampaignNegativeSites { get; set; }

        /// <summary>
        /// The name of the campaign that the negative site is assigned.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// Initializes a new instance of the BulkCampaignNegativeSites class.
        /// </summary>
        public BulkCampaignNegativeSites()
        {
        }

        internal BulkCampaignNegativeSites(BulkCampaignNegativeSite site)
            : base(site)
        {
            SetDataFromIdentifier(site.Identifier);
        }

        internal BulkCampaignNegativeSites(BulkCampaignNegativeSitesIdentifier identifier)
            : base(identifier)
        {
            SetDataFromIdentifier(identifier);
        }

        private void SetDataFromIdentifier(BulkCampaignNegativeSitesIdentifier identifier)
        {
            CampaignNegativeSites = new CampaignNegativeSites
            {
                CampaignId = identifier.CampaignId
            };

            CampaignName = identifier.CampaignName;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IEnumerable<BulkCampaignNegativeSite> ConvertApiToBulkNegativeSites()
        {
            ValidateListNotNullOrEmpty(CampaignNegativeSites.NegativeSites, "CampaignNegativeSites.NegativeSites");

            return CampaignNegativeSites.NegativeSites.Select(s => new BulkCampaignNegativeSite
            {
                CampaignId = CampaignNegativeSites.CampaignId,
                Website = s,
                CampaignName = CampaignName
            });
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructApiObjects()
        {
            CampaignNegativeSites.NegativeSites = NegativeSites.Select(s => s.Website).ToList();       
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override BulkCampaignNegativeSitesIdentifier CreateIdentifier()
        {
            return new BulkCampaignNegativeSitesIdentifier
            {
                CampaignId = CampaignNegativeSites.CampaignId,
                CampaignName = CampaignName
            };
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            ValidatePropertyNotNull(CampaignNegativeSites, "CampaignNegativeSites");
        }
    }
}

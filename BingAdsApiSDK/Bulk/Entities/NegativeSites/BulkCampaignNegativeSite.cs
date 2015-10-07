using Microsoft.BingAds.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a negative site that is assigned to a campaign. Each negative site can be read or written in a bulk file. 
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
    public class BulkCampaignNegativeSite : BulkNegativeSite<BulkCampaignNegativeSitesIdentifier>
    {
        /// <summary>
        /// The identifier of the campaign that the negative site is assigned.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long CampaignId
        {
            get { return Identifier.CampaignId; }
            set { Identifier.CampaignId = value; }
        }

        /// <summary>
        /// The name of the campaign that the negative site is assigned.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return Identifier.CampaignName; }
            set { Identifier.CampaignName = value; }
        }

        /// <summary>
        /// Initializes a new instance of the BulkCampaignNegativeSite class.
        /// </summary>
        public BulkCampaignNegativeSite()
            : base(new BulkCampaignNegativeSitesIdentifier())
        {
            
        }        
        
        internal override MultiRecordBulkEntity CreateNegativeSitesWithThisNegativeSite()
        {
            return new BulkCampaignNegativeSites(this);
        }
    }
}

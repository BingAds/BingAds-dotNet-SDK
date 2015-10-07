using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents one sub location negative target bid within a negative location target that is associated with a campaign. 
    /// This class exposes properties that can be read and written as fields of the Campaign Negative Location Target record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511526">Campaign Negative Location Target</see>. </para>
    /// </summary>
    /// <remarks>
    /// <para>Each negative location sub type contains a list of bids. For example <see cref="BulkLocationTargetWithStringLocation{TBid}.CityTarget"/> contains a list of <see cref="CityTargetBid"/>. 
    /// Each <see cref="CityTargetBid"/> instance 
    /// corresponds to one Campaign Negative Location Target record in the bulk file. If you upload a <see cref="BulkLocationTargetWithStringLocation{TBid}.CityTarget"/>, 
    /// then you are effectively replacing any existing city bids for the corresponding negative location target.</para>
    /// <para>
    /// The <see cref="BulkLocationTargetBidWithStringLocation.LocationType"/> property determines the geographical location sub type.
    /// </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignNegativeLocationTargetBid : BulkNegativeLocationTargetBid
    {
        /// <summary>
        /// The identifier of the campaign that the target is associated.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? CampaignId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        /// <summary>
        /// The name of the campaign that the target is associated.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return EntityName; }
            set { EntityName = value; }
        }

        /// <summary>
        /// Initializes a new instanced of the BulkCampaignNegativeLocationTargetBid class. 
        /// </summary>
        public BulkCampaignNegativeLocationTargetBid()
            : base(new BulkCampaignTargetIdentifier(typeof(BulkCampaignNegativeLocationTargetBid))) { }        
    }
}

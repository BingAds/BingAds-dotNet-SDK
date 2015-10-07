using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>    
    /// Represents a negative geographical location target that is associated with a campaign. 
    /// This class exposes the <see cref="BulkLocationTargetWithStringLocation{TBid}.CityTarget"/>, <see cref="BulkLocationTargetWithStringLocation{TBid}.CountryTarget"/>, 
    /// <see cref="BulkLocationTargetWithStringLocation{TBid}.MetroAreaTarget"/>, <see cref="BulkLocationTargetWithStringLocation{TBid}.PostalCodeTarget"/>, and <see cref="BulkLocationTargetWithStringLocation{TBid}.StateTarget"/> properties 
    /// that represent geographical location sub types. Each sub type can be read and written as fields of the Campaign Negative Location Target record in a bulk file.       
    /// </summary>
    /// <remarks>
    /// <para>Each negative location sub type contains a list of bids. For example <see cref="BulkLocationTargetWithStringLocation{TBid}.CityTarget"/> contains a list of <see cref="CityTargetBid"/>. 
    /// Each <see cref="CityTargetBid"/> instance 
    /// corresponds to one Campaign Negative Location Target record in the bulk file. If you upload a <see cref="BulkLocationTargetWithStringLocation{TBid}.CityTarget"/>, 
    /// then you are effectively replacing any existing city bids for the corresponding negative location target.</para>
    /// <para>
    /// The <see cref="BulkLocationTargetBidWithStringLocation.LocationType"/> property determines the geographical location sub type.
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511526">Campaign Negative Location Target</see>. </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkCampaignNegativeLocationTarget : BulkNegativeLocationTarget<BulkCampaignNegativeLocationTargetBid>
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
        /// Reserved for internal use.
        /// </summary>
        protected internal override BulkCampaignNegativeLocationTargetBid CreateBid()
        {
            return new BulkCampaignNegativeLocationTargetBid();
        }
    }
}

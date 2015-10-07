
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>    
    /// Represents a geographical location target that is associated with an ad group. 
    /// This class exposes the <see cref="BulkLocationTargetWithStringLocation{TBid}.CityTarget"/>, <see cref="BulkLocationTargetWithStringLocation{TBid}.CountryTarget"/>, 
    /// <see cref="BulkLocationTargetWithStringLocation{TBid}.MetroAreaTarget"/>, <see cref="BulkLocationTargetWithStringLocation{TBid}.PostalCodeTarget"/>, and <see cref="BulkLocationTargetWithStringLocation{TBid}.StateTarget"/> properties 
    /// that represent geographical location sub types. Each sub type can be read and written as fields of the Ad Group Location Target record in a bulk file.     
    /// </summary>
    /// <remarks>
    /// <para>Each location sub type contains a list of bids. For example <see cref="BulkLocationTargetWithStringLocation{TBid}.CityTarget"/> contains a list of <see cref="CityTargetBid"/>. 
    /// Each <see cref="CityTargetBid"/> instance 
    /// corresponds to one Ad Group Location Target record in the bulk file. If you upload a <see cref="BulkLocationTargetWithStringLocation{TBid}.CityTarget"/>, 
    /// then you are effectively replacing any existing city bids for the corresponding location target.</para>
    /// <para>
    /// The <see cref="BulkLocationTargetBidWithStringLocation.LocationType"/> property determines the geographical location sub type.
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511541">Ad Group Location Target</see>. </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupLocationTarget : BulkLocationTarget<BulkAdGroupLocationTargetBid>
    {
        /// <summary>
        /// The identifier of the ad group that the target is associated.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? AdGroupId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        /// <summary>
        /// The name of the ad group that the target is associated.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName
        {
            get { return EntityName; }
            set { EntityName = value; }
        }

        /// <summary>
        /// The name of the ad group that target is associated.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return ParentEntityName; }
            set { ParentEntityName = value; }
        }

        /// <summary>
        /// Defines the possible intent options for location targeting.
        /// </summary>
        public IntentOption? IntentOption
        {
            get { return GetLocationProperty(x => x.IntentOption); }
            set { SetLocationProperty(x => x.IntentOption = value); }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal override BulkAdGroupLocationTargetBid CreateBid()
        {
            return new BulkAdGroupLocationTargetBid();
        }
    }
}
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents one day and time target bid within a day and time target that is associated with an ad group. 
    /// This class exposes the <see cref="BulkDayTimeTargetBid.DayTimeTargetBid"/> property that can be read and written as fields of the Ad Group DayTime Target record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=512015">Ad Group DayTime Target</see>. </para>
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkAdGroupDayTimeTarget"/> exposes a read only list of <see cref="BulkAdGroupDayTimeTargetBid"/>. Each <see cref="BulkAdGroupDayTimeTargetBid"/> instance 
    /// corresponds to one Ad Group DayTime Target record in the bulk file. If you upload a <see cref="BulkAdGroupDayTimeTarget"/>, 
    /// then you are effectively replacing any existing bids for the corresponding day and time target. 
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupDayTimeTargetBid : BulkDayTimeTargetBid
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
        /// The name of the ad group that target is associated.
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
        /// Initializes a new instanced of the BulkAdGroupDayTimeTargetBid class. 
        /// </summary>
        public BulkAdGroupDayTimeTargetBid()
            : base(new BulkAdGroupTargetIdentifier(typeof(BulkAdGroupDayTimeTargetBid)))
        {

        }
    }
}

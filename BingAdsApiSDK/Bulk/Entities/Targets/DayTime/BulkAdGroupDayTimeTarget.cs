using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>    
    /// Represents a day and time target that is associated with an ad group. 
    /// This class exposes the <see cref="BulkDayTimeTarget{TBid}.DayTimeTarget"/> property that can be read and written as fields of the Ad Group DayTime Target record in a bulk file.         
    /// </summary>
    /// <remarks>
    /// <para>
    /// One <see cref="BulkAdGroupDayTimeTarget"/> exposes a read only list of <see cref="BulkAdGroupDayTimeTargetBid"/>. Each <see cref="BulkAdGroupDayTimeTargetBid"/> instance 
    /// corresponds to one Ad Group DayTime Target record in the bulk file. If you upload a <see cref="BulkAdGroupDayTimeTarget"/>, 
    /// then you are effectively replacing any existing bids for the corresponding day and time target. 
    /// </para>
    /// <para>Properties of this class and of classes that it is derived from, correspond to fields of the Ad Group DayTime Target record in a bulk file.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=512015">Ad Group DayTime Target</see>. </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupDayTimeTarget : BulkDayTimeTarget<BulkAdGroupDayTimeTargetBid>
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
        /// Reserved for internal use.
        /// </summary>
        protected internal override BulkAdGroupDayTimeTargetBid CreateBid()
        {
            return new BulkAdGroupDayTimeTargetBid();
        }
    }
}

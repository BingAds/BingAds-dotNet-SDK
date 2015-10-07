using Microsoft.BingAds.V10.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk target bid classes, for example <see cref="BulkAdGroupDayTimeTargetBid"/>.
    /// </summary>
    public abstract class BulkTargetBid : SingleRecordBulkEntity
    {
        /// <summary>
        /// The status of the target bid.
        /// The value is Active if the target bid is available in the target. 
        /// The value is Deleted if the target bid is deleted from the target, or should be deleted in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status
        {
            get { return Identifier.Status; }
            set { Identifier.Status = value; }
        }

        internal BulkTargetIdentifier Identifier { get; set; }

        /// <summary>
        /// The identifier of the target that contains this target bid. 
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? TargetId
        {
            get { return Identifier.TargetId; }
            set { Identifier.TargetId = value; }
        }

        internal long? EntityId
        {
            get { return Identifier.EntityId; }
            set { Identifier.EntityId = value; }
        }

        internal string EntityName
        {
            get { return Identifier.EntityName; }
            set { Identifier.EntityName = value; }
        }

        internal string ParentEntityName
        {
            get { return Identifier.ParentEntityName; }
            set { Identifier.ParentEntityName = value; }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="identitifier">Reserved for internal use.</param>
        protected BulkTargetBid(BulkTargetIdentifier identitifier)
        {
            Identifier = identitifier;
        }               

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Identifier.ReadFromRowValues(values);            
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {            
            Identifier.WriteToRowValues(values, excludeReadonlyData);            
        }
        
        internal override bool CanEncloseInMultilineEntity
        {
            get { return true; }
        }

        internal override MultiRecordBulkEntity EncloseInMultilineEntity()
        {
            if (Identifier is BulkCampaignTargetIdentifier)
            {
                return new BulkCampaignTarget(this);
            }

            return new BulkAdGroupTarget(this);
        }
    }
}

using System;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    public abstract class BulkTargetIdentifier : BulkEntityIdentifier
    {   
        internal Status? Status { get; set; }
        
        internal long? TargetId { get; set; }
        
        internal long? EntityId { get; set; }
        
        internal string EntityName { get; set; }

        internal string ParentEntityName { get; set; }
        
        internal Type TargetBidType { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal abstract string EntityColumnName { get; }

        private static readonly IBulkMapping<BulkTargetIdentifier>[] Mappings =
        {
            new SimpleBulkMapping<BulkTargetIdentifier>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),
            
            new SimpleBulkMapping<BulkTargetIdentifier>(StringTable.Id,
                c => c.TargetId.ToBulkString(),
                (v, c) => c.TargetId = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkTargetIdentifier>(StringTable.ParentId,
                c => c.EntityId.ToBulkString(),
                (v, c) => c.EntityId = v.ParseOptional<long>()
            ),

            new DynamicColumnNameMapping<BulkTargetIdentifier>(c => c.EntityColumnName,
                c => c.EntityName,
                (v, c) => c.EntityName = v
            ),           
 
            new ConditionalBulkMapping<BulkTargetIdentifier>(StringTable.Campaign, c => c is BulkAdGroupTargetIdentifier,
                c => c.ParentEntityName,
                (v, c) => c.ParentEntityName = v                
           )
        };

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected BulkTargetIdentifier(Type targetBidType)
        {
            TargetBidType = targetBidType;
        }

        internal override void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public override bool Equals(BulkEntityIdentifier other)
        {
            var otherIdentifier = other as BulkTargetIdentifier;

            if (otherIdentifier == null)
            {
                return false;
            }
            var isNameNotEmpty = !string.IsNullOrEmpty(EntityName) && !string.IsNullOrEmpty(ParentEntityName);

            return
                GetType() == other.GetType() &&
                (EntityId == otherIdentifier.EntityId ||
                 (isNameNotEmpty &&
                  EntityName == otherIdentifier.EntityName &&
                  ParentEntityName == otherIdentifier.ParentEntityName));
        }

        internal override bool IsDeleteRow
        {
            get { return Status == BingAds.Bulk.Entities.Status.Deleted; }
        }
    }
}
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// <para>This abstract class provides properties that are shared by all bulk ad extension association classes.</para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.</para>
    /// </summary>
    public abstract class BulkAdExtensionAssociation : SingleRecordBulkEntity
    {
        /// <summary>
        /// The status of the ad extension association.
        /// The value is Active if the EntityId and AdExtensionId are associated. The value is Deleted if the association is removed. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>   
        public Status? Status { get; set; }        

        /// <summary>
        /// Defines an association relationship between an ad extension and a supported entity, for example a campaign or ad group.
        /// </summary>
        public AdExtensionIdToEntityIdAssociation AdExtensionIdToEntityIdAssociation { get; set; }        

        /// <summary>
        /// The editorial status of the ad extension and associated entity. For more information, see <see href="http://go.microsoft.com/fwlink/?LinkId=511866">AdExtensionEditorialStatus</see>.
        /// Corresponds to the 'Editorial Status' field in the bulk file. 
        /// </summary>
        public AdExtensionEditorialStatus? EditorialStatus { get; internal set; }

        /// <summary>
        /// The historical performance data for the ad extension association.
        /// </summary>
        public PerformanceData PerformanceData { get; private set; }

        private static readonly IBulkMapping<BulkAdExtensionAssociation>[] Mappings =
        {     
            new SimpleBulkMapping<BulkAdExtensionAssociation>(StringTable.Status,
                c => c.Status.ToBulkString(),
                (v, c) => c.Status = v.ParseOptional<Status>()
            ),

            new SimpleBulkMapping<BulkAdExtensionAssociation>(StringTable.Id,
                c => c.AdExtensionIdToEntityIdAssociation.AdExtensionId.ToBulkString(),
                (v, c) => c.AdExtensionIdToEntityIdAssociation.AdExtensionId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAdExtensionAssociation>(StringTable.ParentId,
                c => c.AdExtensionIdToEntityIdAssociation.EntityId.ToBulkString(),
                (v, c) => c.AdExtensionIdToEntityIdAssociation.EntityId = v.Parse<long>()
            ),            

            new SimpleBulkMapping<BulkAdExtensionAssociation>(StringTable.EditorialStatus,
                c => c.EditorialStatus.ToBulkString(),
                (v, c) => c.EditorialStatus = v.ParseOptional<AdExtensionEditorialStatus>()
            ),
        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(AdExtensionIdToEntityIdAssociation, "AdExtensionIdToEntityIdAssociation");

            this.ConvertToValues(values, Mappings);

            if (!excludeReadonlyData)
            {
                PerformanceData.WriteToRowValuesIfNotNull(PerformanceData, values);
            }
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            AdExtensionIdToEntityIdAssociation = new AdExtensionIdToEntityIdAssociation();            

            values.ConvertToEntity(this, Mappings);

            PerformanceData = PerformanceData.ReadFromRowValuesOrNull(values);
        }
    }
}

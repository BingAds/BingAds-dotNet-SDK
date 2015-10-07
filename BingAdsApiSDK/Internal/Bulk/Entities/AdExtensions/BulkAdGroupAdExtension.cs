using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace


namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// <para>This abstract class provides properties that are shared by all bulk ad group with ad extension association classes.</para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.</para>
    /// </summary>
    public abstract class BulkAdGroupAdExtension : BulkAdExtensionAssociation
    {
        /// <summary>
        /// The name of the ad group that is associated with the ad extension.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName
        {
            get { return ParentName; }
            set { ParentName = value; }
        }

        /// <summary>
        /// The name of the parent campaign which contains the ad group that is associated with the ad extension.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// The identifier of the ad group that is associated with the ad extension.
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? AdGroupId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        private static readonly IBulkMapping<BulkAdGroupAdExtension>[] Mappings =
        {
            new SimpleBulkMapping<BulkAdGroupAdExtension>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            )
        };

        internal override void ProcessMappingsToRowValues(RowValues values)
        {
            base.ProcessMappingsToRowValues(values);

            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override string ParentColumnName
        {
            get { return StringTable.AdGroup; }
        }        
    }
}

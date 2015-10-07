using Microsoft.BingAds.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.Internal.Bulk.Entities.AdExtensions
{
    /// <summary>
    /// This abstract class provides properties that are shared by all bulk ad group ad extension association classes.
    /// </summary>
    public abstract class BulkAdGroupAdExtensionAssociation : BulkAdExtensionAssociation
    {
        /// <summary>
        /// The name of the ad group that the ad extension is associated.
        /// Corresponds to the 'AdGroup' field in the bulk file. 
        /// </summary>
        public string AdGroupName { get; private set; }

        /// <summary>
        /// The name of the campaign containing the ad group that the ad extension is associated.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; private set; }

        private static readonly IBulkMapping<BulkAdGroupAdExtensionAssociation>[] Mappings =
        {     
            new SimpleBulkMapping<BulkAdGroupAdExtensionAssociation>(StringTable.AdGroup,                
                c => c.AdGroupName,
                (v, c) => c.AdGroupName = v
            ),

            new SimpleBulkMapping<BulkAdGroupAdExtensionAssociation>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            ),
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }
    }
}

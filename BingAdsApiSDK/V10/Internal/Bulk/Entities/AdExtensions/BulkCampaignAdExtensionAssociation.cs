using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract class provides properties that are shared by all bulk campaign ad extension association classes.
    /// </summary>
    public abstract class BulkCampaignAdExtensionAssociation : BulkAdExtensionAssociation
    {
        /// <summary>
        /// The name of the campaign that the ad extension is associated.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; private set; }

        private static readonly IBulkMapping<BulkCampaignAdExtensionAssociation>[] Mappings =
        {     
            new SimpleBulkMapping<BulkCampaignAdExtensionAssociation>(StringTable.Campaign,                
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            )            
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }
    }
}

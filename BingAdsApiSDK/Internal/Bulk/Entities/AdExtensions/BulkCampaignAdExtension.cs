using Microsoft.BingAds.Bulk.Entities;
// ReSharper disable once CheckNamespace


namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// <para>This abstract class provides properties that are shared by all bulk campaign with ad extension association classes.</para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.</para>
    /// </summary>
    public abstract class BulkCampaignAdExtension : BulkAdExtensionAssociation
    {
        /// <summary>
        /// The name of the campaign that is associated with the ad extension.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return ParentName; }
            set { ParentName = value; }
        }

        /// <summary>
        /// The identifier of the campaign that is associated with the ad extension.
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? CampaignId
        {
            get { return EntityId; }
            set { EntityId = value; }
        }

        protected override string ParentColumnName
        {
            get { return StringTable.Campaign; }
        }
    }
}

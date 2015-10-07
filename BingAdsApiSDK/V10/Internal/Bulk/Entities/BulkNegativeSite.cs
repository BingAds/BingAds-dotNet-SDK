using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class for the bulk negative sites that are assigned individually to a campaign or ad group entity.
    /// </summary>
    /// <seealso cref="BulkAdGroupNegativeSite"/>
    /// <seealso cref="BulkCampaignNegativeSite"/>
    public abstract class BulkNegativeSite<TIdentifier> : SingleRecordBulkEntity
        where TIdentifier: BulkNegativeSiteIdentifier
    {
        internal TIdentifier Identifier { get; set; }

        /// <summary>
        /// The status of the negative site association.
        /// The value is Active if the negative site is assigned to the parent entity identified by the EntityId. 
        /// The value is Deleted if the negative site is removed from the parent entity, or should be removed in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status
        {
            get { return Identifier.Status; }
            set { Identifier.Status = value; }
        }        

        /// <summary>
        /// The URL of a website on which you do not want your ads displayed.
        /// Corresponds to the 'Website' field in the bulk file.  
        /// </summary>
        public string Website { get; set; }

        internal BulkNegativeSite(TIdentifier identifier)
        {
            Identifier = identifier;
        }

        private static readonly IBulkMapping<BulkNegativeSite<TIdentifier>>[] Mappings =
        {            
            new SimpleBulkMapping<BulkNegativeSite<TIdentifier>>(StringTable.Website,
                c => c.Website,
                (v, c) => c.Website = v
            )
        };
        
        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            Identifier.ReadFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            Identifier.WriteToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }

        internal override bool CanEncloseInMultilineEntity
        {
            get { return true; }
        }

        internal override MultiRecordBulkEntity EncloseInMultilineEntity()
        {
            return CreateNegativeSitesWithThisNegativeSite();
        }

        internal abstract MultiRecordBulkEntity CreateNegativeSitesWithThisNegativeSite();
    }
}

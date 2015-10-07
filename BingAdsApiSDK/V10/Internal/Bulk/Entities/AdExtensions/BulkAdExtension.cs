using System.Collections.Generic;
using Microsoft.BingAds.V10.Bulk.Entities;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// <para>This abstract class provides properties that are shared by all bulk ad extension classes.</para>
    /// </summary>
    /// <typeparam name="T">The type of ad extension from the <see cref="Microsoft.BingAds.V10.CampaignManagement"/> namespace, for example a <see cref="CallAdExtension"/> object.</typeparam>
    /// <seealso cref="BulkCallAdExtension"/>
    /// <seealso cref="BulkImageAdExtension"/>
    /// <seealso cref="BulkLocationAdExtension"/>
    /// <seealso cref="BulkSiteLinkAdExtension"/>
    public abstract class BulkAdExtensionBase<T> : SingleRecordBulkEntity
        where T: AdExtension
    {
        /// <summary>
        /// The ad extension's parent account identifier. 
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId { get; set; }                

        /// <summary>
        /// The type of ad extension from the <see cref="Microsoft.BingAds.V10.CampaignManagement"/> namespace, for example a <see cref="CallAdExtension"/> object.
        /// </summary>
        protected T AdExtension { get; set; }     

        private static readonly IEnumerable<IBulkMapping<BulkAdExtensionBase<T>>> Mappings = new IBulkMapping<BulkAdExtensionBase<T>>[]
        {
            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.Status,
                c => c.AdExtension.Status.ToBulkString(),
                (v, c) => c.AdExtension.Status = v.ParseOptional<AdExtensionStatus>()
            ), 

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.Id,
                c => c.AdExtension.Id.ToBulkString(),
                (v, c) => c.AdExtension.Id = v.ParseOptional<long>()
            ),

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.ParentId,
                c => c.AccountId.ToBulkString(),
                (v, c) => c.AccountId = v.Parse<long>()
            ),

            new SimpleBulkMapping<BulkAdExtensionBase<T>>(StringTable.Version,
                c => c.AdExtension.Version.ToBulkString(),
                (v, c) => c.AdExtension.Version = v.ParseOptional<int>()
            ), 
        };

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            this.ConvertToValues(values, Mappings);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);
        }
    }
}

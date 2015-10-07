﻿using Microsoft.BingAds.V10.Internal;
using Microsoft.BingAds.V10.Internal.Bulk;
using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Mappings;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a negative keyword that is assigned to an ad group. Each negative keyword can be read or written in a bulk file. 
    /// This class exposes the <see cref="BulkNegativeKeyword.NegativeKeyword"/> property that can be read and written as fields of the Ad Group Negative Keyword record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620253">Ad Group Negative Keyword</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupNegativeKeyword : BulkEntityNegativeKeyword
    {
        /// <summary>
        /// The identifier of the ad group that the negative keyword is assigned.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long? AdGroupId
        {
            get { return ParentId; }
            set { ParentId = value; }
        }

        /// <summary>
        /// The name of the ad group that the negative keyword is assigned.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName
        {
            get { return EntityName; }
            set { EntityName = value; }
        }

        /// <summary>
        /// The name of the campaign that the negative keyword is assigned.
        /// Corresponds to the 'Campaign' field in the bulk file. 
        /// </summary>
        public string CampaignName { get; set; }

        private static readonly IBulkMapping<BulkAdGroupNegativeKeyword>[] Mappings =
        {
            new SimpleBulkMapping<BulkAdGroupNegativeKeyword>(StringTable.Campaign,
                c => c.CampaignName,
                (v, c) => c.CampaignName = v
            )
        };

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override string EntityColumnName
        {
            get { return StringTable.AdGroup; }
        }
    }
}

using Microsoft.BingAds.V10.Internal.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a negative site that is assigned to an ad group. Each negative site can be read or written in a bulk file. 
    /// This class exposes properties that can be read and written as fields of the Ad Group Negative Site record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620254">Ad Group Negative Site</see>. </para>
    /// </summary>
    /// <remarks>
    /// One <see cref="BulkAdGroupNegativeSites"/> exposes a read only list of <see cref="BulkAdGroupNegativeSite"/>. Each <see cref="BulkAdGroupNegativeSite"/> instance 
    /// corresponds to one Ad Group Negative Site record in the bulk file. If you upload a <see cref="BulkAdGroupNegativeSites"/>, 
    /// then you are effectively replacing any existing negative sites assigned to the ad group. 
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupNegativeSite : BulkNegativeSite<BulkAdGroupNegativeSitesIdentifier>
    {
        /// <summary>
        /// The identifier of the ad group that the negative site is assigned.
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AdGroupId
        {
            get { return Identifier.AdGroupId; }
            set { Identifier.AdGroupId = value; }
        }

        /// <summary>
        /// The name of the ad group that the negative site is assigned.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string AdGroupName
        {
            get { return Identifier.AdGroupName; }
            set { Identifier.AdGroupName = value; }
        }

        /// <summary>
        /// The name of the ad group that the negative site is assigned.
        /// Corresponds to the 'Ad Group' field in the bulk file. 
        /// </summary>
        public string CampaignName
        {
            get { return Identifier.CampaignName; }
            set { Identifier.CampaignName = value; }
        }   

        /// <summary>
        /// Initializes a new instance of the BulkAdGroupNegativeSite class.
        /// </summary>
        public BulkAdGroupNegativeSite()
            : base(new BulkAdGroupNegativeSitesIdentifier())
        {

        }

        internal override MultiRecordBulkEntity CreateNegativeSitesWithThisNegativeSite()
        {
            return new BulkAdGroupNegativeSites(this);
        }
    }
}

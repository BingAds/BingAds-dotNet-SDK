using System.Globalization;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Mappings;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a sitelink. 
    /// This class exposes the <see cref="BulkSiteLink.SiteLink"/> property that can be read and written 
    /// as fields of the Sitelink Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511517">Sitelink Ad Extension</see>. </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Sitelink Ad Extension record includes the distinct properties of the <see cref="BulkSiteLink"/> class, combined with 
    /// the commmon properties of the <see cref="BulkSiteLinkAdExtension"/> class, for example <see cref="AccountId"/> and <see cref="SiteLinksAdExtension"/>.
    /// </para>
    /// <para>
    /// One <see cref="BulkSiteLinkAdExtension"/> has one or more <see cref="BulkSiteLink"/>. Each <see cref="BulkSiteLink"/> instance 
    /// corresponds to one Sitelink Ad Extension record in the bulk file. If you upload a <see cref="BulkSiteLinkAdExtension"/>, 
    /// then you are effectively replacing any existing site links for the sitelink ad extension. 
    /// </para>
    /// </remarks>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkSiteLink : SingleRecordBulkEntity
    {        
        internal SiteLinkAdExtensionIdentifier Identifier { get; private set; }

        /// <summary>
        /// The identifier of the ad extension. 
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? AdExtensionId
        {
            get { return Identifier.AdExtensionId; }
            set { Identifier.AdExtensionId = value; }
        }

        /// <summary>
        /// The ad extension's parent account identifier. 
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId
        {
            get { return Identifier.AccountId; }
            set { Identifier.AccountId = value; }
        }

        /// <summary>
        /// The status of the ad extension. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public AdExtensionStatus? Status
        {
            get { return Identifier.Status; }
            set { Identifier.Status = value; }
        }

        /// <summary>
        /// The version of the ad extension. 
        /// Corresponds to the 'Version' field in the bulk file. 
        /// </summary>
        public int? Version
        {
            get { return Identifier.Version; }
            set { Identifier.Version = value; }
        }

        /// <summary>
        /// The order of the sitelink displayed to a search user in the ad. 
        /// Corresponds to the 'Sitelink Extension Order' field in the bulk file. 
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The sitelink.
        /// </summary>
        public SiteLink SiteLink { get; set; }

        /// <summary>
        /// Initializes a new instance of the BulkSiteLink class. 
        /// </summary>
        public BulkSiteLink()
        {            
            Identifier = new SiteLinkAdExtensionIdentifier();
        }

        private static readonly IBulkMapping<BulkSiteLink>[] Mappings =
        {
            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkExtensionOrder,
                c => c.Order.ToString(CultureInfo.InvariantCulture),
                (v, c) => c.Order = int.Parse(v)
            ),

            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkDisplayText,
                c => c.SiteLink.DisplayText,
                (v, c) => c.SiteLink.DisplayText = v
            ),
 
            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkDestinationUrl,
                c => c.SiteLink.DestinationUrl,
                (v, c) => c.SiteLink.DestinationUrl = v
            ),

            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkDescription1,
                c => c.SiteLink.Description1,
                (v, c) => c.SiteLink.Description1 = v
            ), 

            new SimpleBulkMapping<BulkSiteLink>(StringTable.SiteLinkDescription2,
                c => c.SiteLink.Description2,
                (v, c) => c.SiteLink.Description2 = v
            ), 

            // TODO: figure out the exact implementation for SiteLink DevicePreference. Seems different from TextAd DevicePreference
            new SimpleBulkMapping<BulkSiteLink>(StringTable.DevicePreference,
                c => c.SiteLink.DevicePreference.ToDevicePreferenceBulkString(),
                (v, c) => c.SiteLink.DevicePreference = v.ParseDevicePreference()
            ), 
        };
        
        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            SiteLink = new SiteLink();

            Identifier.ReadFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(SiteLink, "SiteLink");
            
            Identifier.WriteToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }

        internal override bool CanEncloseInMultilineEntity
        {
            get { return true; }
        }

        internal override MultiRecordBulkEntity EncloseInMultilineEntity()
        {
            return new BulkSiteLinkAdExtension(this);
        }
    }
}

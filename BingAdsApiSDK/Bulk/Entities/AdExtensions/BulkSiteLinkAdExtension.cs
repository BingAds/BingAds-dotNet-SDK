using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.BingAds.Internal;
using Microsoft.BingAds.Internal.Bulk;
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a sitelink ad extension. 
    /// This class exposes the <see cref="BulkSiteLinkAdExtension.SiteLinksAdExtension"/> property that can be read and written 
    /// as fields of the Sitelink Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511517">Sitelink Ad Extension</see>. </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Sitelink Ad Extension record includes the distinct properties of the <see cref="BulkSiteLink"/> class, combined with 
    /// the commmon properties of the <see cref="BulkSiteLinkAdExtension"/> class, for example <see cref="AccountId"/> and <see cref="CampaignManagement.SiteLinksAdExtension"/>.
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
    public class BulkSiteLinkAdExtension : MultiRecordBulkEntity
    {
        /// <summary>
        /// The ad extension's parent account identifier. 
        /// Corresponds to the 'Parent Id' field in the bulk file. 
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Defines an ad extension that specifies one or more sitelinks to add to a text add.
        /// </summary>
        public SiteLinksAdExtension SiteLinksAdExtension { get; set; }

        private readonly List<BulkSiteLink> _bulkSiteLinkResults = new List<BulkSiteLink>();

        /// <summary>
        /// The list of <see cref="BulkSiteLink"/> are represented by multiple Sitelink Ad Extension records in the file.
        /// Each item in the list corresponds to a separate Sitelink Ad Extension record that includes the distinct properties of the <see cref="BulkSiteLink"/> class, combined with 
        /// the commmon properties of the <see cref="BulkSiteLinkAdExtension"/> class, for example <see cref="AccountId"/> and <see cref="SiteLinksAdExtension"/>.
        /// </summary>
        public IReadOnlyList<BulkSiteLink> SiteLinks
        {
            get { return _bulkSiteLinkResults; }
        }

        internal override IReadOnlyList<BulkEntity> ChildEntities
        {
            get { return SiteLinks; }
        }

        private readonly SiteLinkAdExtensionIdentifier _identifier;

        private bool _hasDeleteAllRow;

        /// <summary>
        /// Initializes a new instance of the BulkSiteLinkAdExtension class. 
        /// </summary>
        public BulkSiteLinkAdExtension()
        {

        }

        internal BulkSiteLinkAdExtension(SiteLinkAdExtensionIdentifier identifier)
            : this()
        {
            _identifier = identifier;

            _hasDeleteAllRow = identifier.Status == AdExtensionStatus.Deleted;

            SiteLinksAdExtension = new SiteLinksAdExtension { Type = "SiteLinksAdExtension" };

            SiteLinksAdExtension.Id = identifier.AdExtensionId;

            SiteLinksAdExtension.Status = identifier.Status;

            SiteLinksAdExtension.Version = identifier.Version;

            AccountId = identifier.AccountId;
        }

        internal BulkSiteLinkAdExtension(BulkSiteLink firstSiteLink)
            : this(firstSiteLink.Identifier)
        {
            _bulkSiteLinkResults.Add(firstSiteLink);
        }

        internal override void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(SiteLinksAdExtension, "SiteLinksAdExtension");

            if (SiteLinksAdExtension.Status != AdExtensionStatus.Deleted)
            {
                ValidateListNotNullOrEmpty(SiteLinksAdExtension.SiteLinks, "SiteLinksAdExtension.SiteLinks");
            }

            if (SiteLinksAdExtension.Id == null)
            {
                throw new InvalidOperationException(ErrorMessages.SiteLinkAdExtensionIdMustBeSet);
            }

            rowWriter.WriteObjectRow(new SiteLinkAdExtensionIdentifier
            {
                Status = AdExtensionStatus.Deleted,
                AccountId = AccountId,
                AdExtensionId = SiteLinksAdExtension.Id
            }, excludeReadonlyData);

            if (SiteLinksAdExtension.Status == AdExtensionStatus.Deleted)
            {
                return;
            }

            foreach (var bulkSiteLink in ConvertRawToBulkSiteLinks())
            {
                bulkSiteLink.WriteToStream(rowWriter, excludeReadonlyData);
            }
        }

        internal override void ReadRelatedDataFromStream(IBulkStreamReader reader)
        {
            var hasMoreRows = true;

            while (hasMoreRows)
            {
                BulkSiteLink nextSiteLink;

                SiteLinkAdExtensionIdentifier identitifier;

                if (reader.TryRead(x => x.Identifier.Equals(_identifier), out nextSiteLink))
                {
                    _bulkSiteLinkResults.Add(nextSiteLink);
                }
                else if (reader.TryRead(x => x.Equals(_identifier) && x.IsDeleteRow, out identitifier))
                {
                    _hasDeleteAllRow = true;
                }
                else
                {
                    hasMoreRows = false;
                }
            }

            if (_bulkSiteLinkResults.Count > 0)
            {
                SiteLinksAdExtension.SiteLinks = _bulkSiteLinkResults.OrderBy(s => s.Order).Select(s => s.SiteLink).ToList();
                SiteLinksAdExtension.Status = AdExtensionStatus.Active;
            }
            else
            {
                SiteLinksAdExtension.Status = AdExtensionStatus.Deleted;
            }
        }

        private IEnumerable<BulkSiteLink> ConvertRawToBulkSiteLinks()
        {
            return SiteLinksAdExtension.SiteLinks.Select((s, i) => new BulkSiteLink
            {
                SiteLink = s,
                AccountId = AccountId,
                AdExtensionId = SiteLinksAdExtension.Id,
                Order = i + 1,
                Version = SiteLinksAdExtension.Version
            });
        }

        internal override bool AllChildrenArePresent
        {
            get { return _hasDeleteAllRow; }
        }
    }
}

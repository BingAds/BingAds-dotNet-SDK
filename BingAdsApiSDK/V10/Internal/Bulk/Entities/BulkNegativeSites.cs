using System.Collections.Generic;
using Microsoft.BingAds.V10.Bulk.Entities;

namespace Microsoft.BingAds.V10.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class for the bulk negative sites that assigned in sets to a campaign or ad group entity.
    /// </summary>
    /// <seealso cref="BulkAdGroupNegativeSites"/>
    /// <seealso cref="BulkCampaignNegativeSites"/>
    public abstract class BulkNegativeSites<TNegativeSite, TIdentifier> : MultiRecordBulkEntity
        where TNegativeSite : BulkNegativeSite<TIdentifier>
        where TIdentifier : BulkNegativeSiteIdentifier
    {
        private readonly List<TNegativeSite> _bulkNegativeSites = new List<TNegativeSite>();

        private readonly TIdentifier _firstRowIdentifier;

        private bool _hasDeleteAllRow;

        /// <summary>
        /// The status of the negative site association.
        /// The value is Active if the negative site is assigned to the parent entity. 
        /// The value is Deleted if the negative site is removed from the parent entity, or should be removed in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public IReadOnlyList<TNegativeSite> NegativeSites
        {
            get { return _bulkNegativeSites; }
        }

        internal override IReadOnlyList<BulkEntity> ChildEntities
        {
            get { return NegativeSites; }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected BulkNegativeSites()
        {
        }

        internal BulkNegativeSites(TNegativeSite site)
            : this(site.Identifier)
        {
            _bulkNegativeSites.Add(site);
        }

        internal BulkNegativeSites(TIdentifier identifier)
        {
            _firstRowIdentifier = identifier;

            _hasDeleteAllRow = identifier.IsDeleteRow;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected abstract TIdentifier CreateIdentifier();

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected abstract void ValidatePropertiesNotNull();

        internal override void WriteToStream(IBulkObjectWriter streamWriter, bool excludeReadonlyData)
        {
            ValidatePropertiesNotNull();

            var deleteRow = CreateIdentifier();

            deleteRow.Status = V10.Bulk.Entities.Status.Deleted;

            streamWriter.WriteObjectRow(deleteRow, excludeReadonlyData);

            if (Status == V10.Bulk.Entities.Status.Deleted)
            {
                return;
            }

            foreach (var site in ConvertApiToBulkNegativeSites())
            {
                site.WriteToStream(streamWriter, excludeReadonlyData);
            }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected abstract IEnumerable<TNegativeSite> ConvertApiToBulkNegativeSites();

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected abstract void ReconstructApiObjects();

        internal override void ReadRelatedDataFromStream(IBulkStreamReader reader)
        {
            var hasMoreRows = true;

            while (hasMoreRows)
            {
                TNegativeSite nextSite;

                TIdentifier identifier;

                if (reader.TryRead(x => x.Identifier.Equals(_firstRowIdentifier), out nextSite))
                {
                    _bulkNegativeSites.Add(nextSite);
                }
                else if (reader.TryRead(x => x.Equals(_firstRowIdentifier) && x.IsDeleteRow, out identifier))
                {
                    _hasDeleteAllRow = true;
                }
                else
                {
                    hasMoreRows = false;
                }
            }

            ReconstructApiObjects();

            Status = _bulkNegativeSites.Count > 0
                ? V10.Bulk.Entities.Status.Active
                : V10.Bulk.Entities.Status.Deleted;
        }

        internal override bool AllChildrenArePresent
        {
            get { return _hasDeleteAllRow; }
        }
    }
}
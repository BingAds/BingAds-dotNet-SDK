//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 13.0
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MS-PL License
// 
// This license governs use of the accompanying software. If you use the software, you accept this license. 
//  If you do not accept the license, do not use the software.
// 
// 1. Definitions
// 
// The terms reproduce, reproduction, derivative works, and distribution have the same meaning here as under U.S. copyright law. 
//  A contribution is the original software, or any additions or changes to the software. 
//  A contributor is any person that distributes its contribution under this license. 
//  Licensed patents  are a contributor's patent claims that read directly on its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, 
//  prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, 
//  each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, 
//  sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License - This license does not grant you rights to use any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, 
//  your patent license from such contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, 
//  and attribution notices that are present in the software.
// 
// (D) If you distribute any portion of the software in source code form, 
//  you may do so only under this license by including a complete copy of this license with your distribution. 
//  If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
// 
// (E) The software is licensed *as-is.* You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
//  You may have additional consumer rights under your local laws which this license cannot change. 
//  To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, 
//  fitness for a particular purpose and non-infringement.
//=====================================================================================================================================================

using Microsoft.BingAds.V13.Bulk.Entities;

namespace Microsoft.BingAds.V13.Internal.Bulk.Entities
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

            deleteRow.Status = V13.Bulk.Entities.Status.Deleted;

            streamWriter.WriteObjectRow(deleteRow, excludeReadonlyData);

            if (Status == V13.Bulk.Entities.Status.Deleted)
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

        internal override void ReadRelatedData(IBulkRecordReader reader)
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
                ? V13.Bulk.Entities.Status.Active
                : V13.Bulk.Entities.Status.Deleted;
        }

        internal override bool AllChildrenArePresent
        {
            get { return _hasDeleteAllRow; }
        }
    }
}

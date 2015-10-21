//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 10.4
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

using System;
using System.Collections.Generic;
using Microsoft.BingAds.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk sub target classes.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkTargetBid"/></typeparam>
    public abstract class BulkSubTarget<TBid> : MultiRecordBulkEntity
        where TBid : BulkTargetBid
    {
        /// <summary>
        /// The identifier of the target. 
        /// Corresponds to the 'Id' field in the bulk file. 
        /// </summary>
        public long? TargetId { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal long? EntityId { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal string EntityName { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal string ParentEntityName { get; set; }

        private BulkTargetIdentifier _identifier;

        private bool _hasDeleteAllRow;

        private readonly List<TBid> _bids = new List<TBid>();

        /// <summary>
        /// The list of target bids corresponding the this sub target type.
        /// </summary>
        public IReadOnlyList<TBid> Bids
        {
            get { return _bids; }
        }

        internal void SetBids(IEnumerable<TBid> bids)
        {
            _bids.AddRange(bids);

            ReconstructSubTargets();

            Status = _bids.Count > 0 
                ? BingAds.Bulk.Entities.Status.Active 
                : BingAds.Bulk.Entities.Status.Deleted;
        }

        internal void SetIdentifier(BulkTargetIdentifier identifier)
        {
            if (identifier.TargetBidType != typeof(TBid))
            {
                throw new Exception("Invalid bulk target identifier.");
            }

            _identifier = identifier;

            _hasDeleteAllRow = identifier.IsDeleteRow;

            EntityId = _identifier.EntityId;

            TargetId = _identifier.TargetId;

            EntityName = _identifier.EntityName;

            ParentEntityName = _identifier.ParentEntityName;
        }

        internal override IReadOnlyList<BulkEntity> ChildEntities
        {
            get { return Bids; }
        }

        /// <summary>
        /// The status of the target.
        /// The value is Active if the target is available in the customer's shared library. 
        /// The value is Deleted if the target is deleted from the customer's shared library, or should be deleted in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        internal bool IsBeingWrittenAsPartOfParentTarget;

        internal override void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            // If the sub-target (for example BulkAgeTarget) is being written as part of BulkTarget, 
            // AgeTarget may be null, which means no Age bids should be written.
            // Otherwise, if BulkAgeTarget is written individually, AgeTarget must be set.
            // Also, if target is being deleted, don't require SubTarget API properties to be set
            if (!IsBeingWrittenAsPartOfParentTarget && Status != BingAds.Bulk.Entities.Status.Deleted)
            {
                ValidatePropertiesNotNull();
            }

            // In any case, for non-empty targets Bids list need to be validated. API doesn't allow passing null or empty list of bids, so shouldn't SDK
            ValidateBidsNotNullOrEmpty();

            var identifier = CreateBid().Identifier;

            identifier.Status = BingAds.Bulk.Entities.Status.Deleted;
            identifier.TargetId = TargetId;
            identifier.EntityId = EntityId;
            identifier.EntityName = EntityName;
            identifier.ParentEntityName = ParentEntityName;

            identifier.WriteToStream(rowWriter, excludeReadonlyData);

            if (Status == BingAds.Bulk.Entities.Status.Deleted)
            {
                return;
            }

            foreach (var subTarget in ConvertApiToBulkBids())
            {
                subTarget.WriteToStream(rowWriter, excludeReadonlyData);
            }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected abstract void ReconstructSubTargets();

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <returns><see cref="IReadOnlyList{T}"/></returns>
        protected abstract IReadOnlyList<TBid> ConvertApiToBulkBids();

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected virtual void ValidatePropertiesNotNull()
        {

        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected virtual void ValidateBidsNotNullOrEmpty()
        {
            
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal abstract TBid CreateBid();

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected internal TBid CreateAndPopulateBid(Action<TBid> setAdditionalPropertes)
        {
            var bid = CreateBid();

            bid.Status = Status;
            bid.TargetId = TargetId;
            bid.EntityId = EntityId;
            bid.EntityName = EntityName;
            bid.ParentEntityName = ParentEntityName;             

            setAdditionalPropertes(bid);

            return bid;
        }

        internal override bool AllChildrenArePresent
        {
            get { return _hasDeleteAllRow; }
        }
    }
}

//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 9.3
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
using System.Linq;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Bulk.Entities;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// This abstract base class provides properties that are shared by all bulk target classes, for example <see cref="BulkAdGroupDayTimeTarget"/>.
    /// </summary>
    /// <typeparam name="TIdentifier">The common target identifier accross all sub types for a given target.</typeparam>
    /// <typeparam name="TAgeBid">An age target bid.</typeparam>
    /// <typeparam name="TAge">An age target.</typeparam>
    /// <typeparam name="TGenderBid">A gender target bid.</typeparam>
    /// <typeparam name="TGender">A gender target.</typeparam>
    /// <typeparam name="TDayTimeBid">A day and time target bid.</typeparam>
    /// <typeparam name="TDayTime">A day and time target.</typeparam>
    /// <typeparam name="TLocationBid">A location target bid.</typeparam>
    /// <typeparam name="TLocation">A location target</typeparam>
    /// <typeparam name="TNegativeLocationBid">A negative location target bid.</typeparam>
    /// <typeparam name="TNegativeLocation">A negative location target.</typeparam>
    /// <typeparam name="TRadiusTargetBid">A radius target bid.</typeparam>
    /// <typeparam name="TRadius">A radius target.</typeparam>
    /// <typeparam name="TDeviceOsBid">A device OS target bid.</typeparam>
    /// <typeparam name="TDeviceOs">A device OS target.</typeparam>
    public abstract class BulkTarget<TIdentifier, TAgeBid, TAge, TGenderBid, TGender, TDayTimeBid, TDayTime, TLocationBid, TLocation, TNegativeLocationBid, TNegativeLocation, TRadiusTargetBid, TRadius, TDeviceOsBid, TDeviceOs> : MultiRecordBulkEntity
        where TIdentifier : BulkTargetIdentifier
        where TAgeBid : BulkAgeTargetBid where TAge: BulkAgeTarget<TAgeBid>
        where TGenderBid: BulkGenderTargetBid where TGender: BulkGenderTarget<TGenderBid>
        where TDayTimeBid: BulkDayTimeTargetBid where TDayTime: BulkDayTimeTarget<TDayTimeBid>
        where TLocationBid: BulkLocationTargetBid where TLocation: BulkLocationTarget<TLocationBid>
        where TNegativeLocationBid: BulkNegativeLocationTargetBid where TNegativeLocation: BulkNegativeLocationTarget<TNegativeLocationBid>
        where TRadiusTargetBid: BulkRadiusTargetBid where TRadius: BulkRadiusTarget<TRadiusTargetBid>
        where TDeviceOsBid: BulkDeviceOsTargetBid where TDeviceOs: BulkDeviceOsTarget<TDeviceOsBid>
    {
        private readonly TIdentifier _originalIdentifier;

        /// <summary>
        /// The status of the target.
        /// The value is Active if the target is available in the customer's shared library. 
        /// The value is Deleted if the target is deleted from the customer's shared library, or should be deleted in a subsequent upload operation. 
        /// Corresponds to the 'Status' field in the bulk file. 
        /// </summary>
        public Status? Status { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected long? EntityId { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected string EntityName { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected string ParentEntityName { get; set; }

        /// <summary>
        /// The associated target. 
        /// </summary>
        public Target2 Target { get; set; }

        /// <summary>
        /// The <see cref="BulkLocationTarget{TBid}"/> contains multiple <see cref="BulkLocationTargetBid"/>.
        /// </summary>
        public TLocation LocationTarget { get; private set; }

        /// <summary>
        /// The <see cref="BulkAgeTarget{TBid}"/> contains multiple <see cref="BulkAgeTargetBid"/>.
        /// </summary>
        public TAge AgeTarget { get; private set; }

        /// <summary>
        /// The <see cref="BulkGenderTarget{TBid}"/> contains multiple <see cref="BulkGenderTargetBid"/>.
        /// </summary>
        public TGender GenderTarget { get; private set; }

        /// <summary>
        /// The <see cref="BulkDayTimeTarget{TBid}"/> contains multiple <see cref="BulkDayTimeTargetBid"/>.
        /// </summary>
        public TDayTime DayTimeTarget { get; private set; }

        /// <summary>
        /// The <see cref="BulkDeviceOsTarget{TBid}"/> contains multiple <see cref="BulkDeviceOsTargetBid"/>.
        /// </summary>
        public TDeviceOs DeviceOsTarget { get; private set; }

        /// <summary>
        /// The <see cref="BulkNegativeLocationTarget{TBid}"/> contains multiple <see cref="BulkNegativeLocationTargetBid"/>.
        /// </summary>
        public TNegativeLocation NegativeLocationTarget { get; private set; }

        /// <summary>
        /// The <see cref="BulkRadiusTarget{TBid}"/> contains multiple <see cref="BulkRadiusTargetBid"/>.
        /// </summary>
        public TRadius RadiusTarget { get; private set; }

        /// <summary>
        /// The list of sub targets that the target contains can include LocationTarget, AgeTarget, GenderTarget, DayTimeTarget, DeviceOsTarget, NegativeLocationTarget, and RadiusTarget. 
        /// </summary>
        public IReadOnlyList<BulkEntity> SubTargets
        {
            get
            {
                return new BulkEntity[]
                {
                    LocationTarget,
                    AgeTarget,
                    GenderTarget,
                    DayTimeTarget,
                    DeviceOsTarget,
                    NegativeLocationTarget,
                    RadiusTarget
                };
            }
        }

        internal override IReadOnlyList<BulkEntity> ChildEntities
        {
            get { return SubTargets; }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="bid">Reserved for internal use.</param>
        /// <param name="location">Reserved for internal use.</param>
        /// <param name="age">Reserved for internal use.</param>
        /// <param name="gender">Reserved for internal use.</param>
        /// <param name="dayTime">Reserved for internal use.</param>
        /// <param name="deviceOs">Reserved for internal use.</param>
        /// <param name="negativeLocation">Reserved for internal use.</param>
        /// <param name="radius">Reserved for internal use.</param>
        protected BulkTarget(BulkTargetBid bid, TLocation location, TAge age, TGender gender, TDayTime dayTime, TDeviceOs deviceOs, TNegativeLocation negativeLocation, TRadius radius)
            : this((TIdentifier)bid.Identifier, location, age, gender, dayTime, deviceOs, negativeLocation, radius)
        {
            _bids.Add(bid);
        }

        internal BulkTarget(TIdentifier identifier, TLocation location, TAge age, TGender gender, TDayTime dayTime, TDeviceOs deviceOs, TNegativeLocation negativeLocation, TRadius radius)
            : this(location, age, gender, dayTime, deviceOs, negativeLocation, radius)
        {
            Target = new Target2();

            Target.Id = identifier.TargetId;

            EntityId = identifier.EntityId;

            EntityName = identifier.EntityName;

            ParentEntityName = identifier.ParentEntityName;

            if (identifier.IsDeleteRow)
            {
                _deleteAllRows.Add(identifier);
            }

            _originalIdentifier = identifier;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="location">Reserved for internal use.</param>
        /// <param name="age">Reserved for internal use.</param>
        /// <param name="gender">Reserved for internal use.</param>
        /// <param name="dayTime">Reserved for internal use.</param>
        /// <param name="deviceOs">Reserved for internal use.</param>
        /// <param name="negativeLocation">Reserved for internal use.</param>
        /// <param name="radius">Reserved for internal use.</param>
        protected BulkTarget(TLocation location, TAge age, TGender gender, TDayTime dayTime, TDeviceOs deviceOs, TNegativeLocation negativeLocation, TRadius radius)
        {            
            LocationTarget = location;
            AgeTarget = age;
            GenderTarget = gender;
            DayTimeTarget = dayTime;
            DeviceOsTarget = deviceOs;
            NegativeLocationTarget = negativeLocation;
            RadiusTarget = radius;
        }

        internal override bool AllChildrenArePresent
        {
            get { return SubTargets.Cast<MultiRecordBulkEntity>().All(x => x.AllChildrenArePresent); }
        }

        internal override void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            if (Status != Microsoft.BingAds.Bulk.Entities.Status.Deleted)
            {
                ValidatePropertyNotNull(Target, "Target");
            }

            if (Target != null)
            {
                if (Target.Location == null && Target.Age == null && Target.Gender == null && Target.DayTime == null && Target.DeviceOS == null && Target.Location == null)
                {
                    throw new InvalidOperationException(ErrorMessages.AtLeastOneSubTargetMustNotBeNull);
                }
            }

            if (Target != null)
            {
                LocationTarget.Location = Target.Location;
                AgeTarget.AgeTarget = Target.Age;
                GenderTarget.GenderTarget = Target.Gender;
                DayTimeTarget.DayTimeTarget = Target.DayTime;
                DeviceOsTarget.DeviceOsTarget = Target.DeviceOS;
                NegativeLocationTarget.Location = Target.Location;
                RadiusTarget.Location = Target.Location;
            }

            SetDefaultIdentifier(LocationTarget);
            SetDefaultIdentifier(AgeTarget);
            SetDefaultIdentifier(GenderTarget);
            SetDefaultIdentifier(DayTimeTarget);
            SetDefaultIdentifier(DeviceOsTarget);
            SetDefaultIdentifier(NegativeLocationTarget);
            SetDefaultIdentifier(RadiusTarget);

            LocationTarget.IsBeingWrittenAsPartOfParentTarget = true;
            AgeTarget.IsBeingWrittenAsPartOfParentTarget = true;
            GenderTarget.IsBeingWrittenAsPartOfParentTarget = true;
            DayTimeTarget.IsBeingWrittenAsPartOfParentTarget = true;
            DeviceOsTarget.IsBeingWrittenAsPartOfParentTarget = true;
            NegativeLocationTarget.IsBeingWrittenAsPartOfParentTarget = true;
            RadiusTarget.IsBeingWrittenAsPartOfParentTarget = true;

            foreach (var childEntity in SubTargets)
            {                
                childEntity.WriteToStream(rowWriter, excludeReadonlyData);
            }
        }

        readonly List<BulkTargetBid> _bids = new List<BulkTargetBid>();

        readonly List<TIdentifier> _deleteAllRows = new List<TIdentifier>();

        internal override void ReadRelatedDataFromStream(IBulkStreamReader reader)
        {
            var hasMoreRows = true;

            while (hasMoreRows)
            {
                BulkTargetBid bidRow;

                TIdentifier identifierRow;

                if (reader.TryRead(x => x.Identifier.Equals(_originalIdentifier), out bidRow))
                {
                    _bids.Add(bidRow);
                }
                else if (reader.TryRead(x => x.Equals(_originalIdentifier) && x.IsDeleteRow, out identifierRow))
                {
                    _deleteAllRows.Add(identifierRow);
                }
                else
                {
                    hasMoreRows = false;
                }

                // Delta download sends delete-all rows first, which don't have targetId. Have to look at all rows and set first non-null Id.
                if (Target.Id == null && bidRow != null && bidRow.TargetId != null)
                {
                    Target.Id = bidRow.TargetId;
                }
            }

            Status = _bids.Count > 0 ? BingAds.Bulk.Entities.Status.Active : BingAds.Bulk.Entities.Status.Deleted;

            var bidGroups = _bids.GroupBy(r => r.GetType()).ToDictionary(x => x.Key, x => x.ToList());

            var location = new LocationTarget2();

            LocationTarget.Location = location;
            NegativeLocationTarget.Location = location;
            RadiusTarget.Location = location;

            PopulateChildTargetBids(LocationTarget, bidGroups);
            PopulateChildTargetBids(AgeTarget, bidGroups);
            PopulateChildTargetBids(GenderTarget, bidGroups);
            PopulateChildTargetBids(DayTimeTarget, bidGroups);
            PopulateChildTargetBids(DeviceOsTarget, bidGroups);
            PopulateChildTargetBids(NegativeLocationTarget, bidGroups);
            PopulateChildTargetBids(RadiusTarget, bidGroups);

            var deleteAllGroups = _deleteAllRows.GroupBy(r => r.TargetBidType).ToDictionary(x => x.Key, x => x.ToList());

            PopulateChildTargetIdentities(LocationTarget, deleteAllGroups);
            PopulateChildTargetIdentities(AgeTarget, deleteAllGroups);
            PopulateChildTargetIdentities(GenderTarget, deleteAllGroups);
            PopulateChildTargetIdentities(DayTimeTarget, deleteAllGroups);
            PopulateChildTargetIdentities(DeviceOsTarget, deleteAllGroups);
            PopulateChildTargetIdentities(NegativeLocationTarget, deleteAllGroups);
            PopulateChildTargetIdentities(RadiusTarget, deleteAllGroups);

            if (new object[] { location.CityTarget, location.MetroAreaTarget, location.StateTarget, location.CountryTarget, location.PostalCodeTarget, location.RadiusTarget }.Any(x => x != null))
            {
                Target.Location = location;
            }

            Target.Age = AgeTarget.AgeTarget;
            Target.Gender = GenderTarget.GenderTarget;
            Target.DayTime = DayTimeTarget.DayTimeTarget;
            Target.DeviceOS = DeviceOsTarget.DeviceOsTarget;
        }

        private void PopulateChildTargetBids<T>(BulkSubTarget<T> target, Dictionary<Type, List<BulkTargetBid>> groups)
            where T : BulkTargetBid
        {
            if (!groups.ContainsKey(typeof(T)))
            {         
                target.Status = BingAds.Bulk.Entities.Status.Deleted;
                return;
            }

            var bids = groups[typeof(T)].Cast<T>();

            target.SetBids(bids);
        }

        private void PopulateChildTargetIdentities<T>(BulkSubTarget<T> target, Dictionary<Type, List<TIdentifier>> groups)
    where T : BulkTargetBid
        {
            // no delete all row for this target bid type
            if (!groups.ContainsKey(typeof(T)))
            {
                SetDefaultIdentifier(target);

                return;
            }

            var identities = groups[typeof(T)];

            // should have only one delete all row at most
            foreach (var identifier in identities)
            {
                target.SetIdentifier(identifier);
            }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="bidType">Reserved for internal use.</param>
        /// <returns>Reserved for internal use.</returns>
        protected internal abstract TIdentifier CreateIdentifier(Type bidType);

        private void SetDefaultIdentifier<TBid>(BulkSubTarget<TBid> target) where TBid : BulkTargetBid
        {
            var identifier = CreateIdentifier(typeof(TBid));
            
            identifier.EntityId = EntityId;

            if (Target != null)
            {
                identifier.TargetId = Target.Id;
            }

            identifier.EntityName = EntityName;

            identifier.ParentEntityName = ParentEntityName;

            if (Status == Microsoft.BingAds.Bulk.Entities.Status.Deleted)
            {
                target.Status = BingAds.Bulk.Entities.Status.Deleted;
            }
    
            target.SetIdentifier(identifier);
        }       
    }
}

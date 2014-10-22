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
    /// This abstract base class provides properties that are shared by all bulk negative location target classes, for example <see cref="BulkAdGroupNegativeLocationTarget"/>.
    /// </summary>
    /// <typeparam name="TBid"><see cref="BulkNegativeLocationTargetBid"/></typeparam>
    public abstract class BulkNegativeLocationTarget<TBid> : BulkSubTarget<TBid>
        where TBid : BulkNegativeLocationTargetBid
    {
        /// <summary>
        /// Defines a list of cities to target with bid adjustments.
        /// </summary>
        public CityTarget CityTarget
        {
            get { return GetLocationProperty(x => x.CityTarget); }
            set { SetLocationProperty(x => x.CityTarget = value); }
        }

        /// <summary>
        /// Defines a list of metro areas to target with bid adjustments.
        /// </summary>
        public MetroAreaTarget MetroAreaTarget
        {
            get { return GetLocationProperty(x => x.MetroAreaTarget); }
            set { SetLocationProperty(x => x.MetroAreaTarget = value); }
        }

        /// <summary>
        /// Defines a list of states to target with bid adjustments.
        /// </summary>
        public StateTarget StateTarget
        {
            get { return GetLocationProperty(x => x.StateTarget); }
            set { SetLocationProperty(x => x.StateTarget = value); }
        }

        /// <summary>
        /// Defines a list of countries to target with bid adjustments.
        /// </summary>
        public CountryTarget CountryTarget
        {
            get { return GetLocationProperty(x => x.CountryTarget); }
            set { SetLocationProperty(x => x.CountryTarget = value); }
        }

        /// <summary>
        /// Defines a list of postal codes to target with bid adjustments.
        /// </summary>
        public PostalCodeTarget PostalCodeTarget
        {
            get { return GetLocationProperty(x => x.PostalCodeTarget); }
            set { SetLocationProperty(x => x.PostalCodeTarget = value); }
        }

        /// <summary>
        /// Defines the possible intent options for location targeting. 
        /// </summary>
        public IntentOption? IntentOption
        {
            get { return GetLocationProperty(x => x.IntentOption); }
            set { SetLocationProperty(x => x.IntentOption = value); }
        }

        internal LocationTarget2 Location { get; set; }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ReconstructSubTargets()
        {
            ReconstructApiBids(
                LocationTargetType.City,
                t => new CityTargetBid { IsExcluded = true, City = t.Location },
                () => Location.CityTarget,
                _ => Location.CityTarget = _,
                () => Location.CityTarget.Bids,
                _ => Location.CityTarget.Bids = _
            );

            ReconstructApiBids(
                LocationTargetType.MetroArea,
                t => new MetroAreaTargetBid() { IsExcluded = true, MetroArea = t.Location },
                () => Location.MetroAreaTarget,
                _ => Location.MetroAreaTarget = _,
                () => Location.MetroAreaTarget.Bids,
                _ => Location.MetroAreaTarget.Bids = _
            );

            ReconstructApiBids(
                LocationTargetType.State,
                t => new StateTargetBid() { IsExcluded = true, State = t.Location },
                () => Location.StateTarget,
                _ => Location.StateTarget = _,
                () => Location.StateTarget.Bids,
                _ => Location.StateTarget.Bids = _
            );

            ReconstructApiBids(
                LocationTargetType.Country,
                t => new CountryTargetBid() { IsExcluded = true, CountryAndRegion = t.Location },
                () => Location.CountryTarget,
                _ => Location.CountryTarget = _,
                () => Location.CountryTarget.Bids,
                _ => Location.CountryTarget.Bids = _
            );

            ReconstructApiBids(
                LocationTargetType.PostalCode,
                t => new PostalCodeTargetBid() { IsExcluded = true, PostalCode = t.Location },
                () => Location.PostalCodeTarget,
                _ => Location.PostalCodeTarget = _,
                () => Location.PostalCodeTarget.Bids,
                _ => Location.PostalCodeTarget.Bids = _
            );
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override IReadOnlyList<TBid> ConvertApiToBulkBids()
        {
            var bids = new List<TBid>();

            var intentOption = Location.IntentOption;

            AddBids(bids, () => Location.CityTarget, _ => _.Bids, LocationTargetType.City, _ => _.City, _ => _.IsExcluded, intentOption);
            AddBids(bids, () => Location.MetroAreaTarget, _ => _.Bids, LocationTargetType.MetroArea, _ => _.MetroArea, _ => _.IsExcluded, intentOption);
            AddBids(bids, () => Location.StateTarget, _ => _.Bids, LocationTargetType.State, _ => _.State, _ => _.IsExcluded, intentOption);
            AddBids(bids, () => Location.CountryTarget, _ => _.Bids, LocationTargetType.Country, _ => _.CountryAndRegion, _ => _.IsExcluded, intentOption);
            AddBids(bids, () => Location.PostalCodeTarget, _ => _.Bids, LocationTargetType.PostalCode, _ => _.PostalCode, _ => _.IsExcluded, intentOption);

            return bids;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidatePropertiesNotNull()
        {
            if (CityTarget == null && MetroAreaTarget == null && StateTarget == null && CountryTarget == null && PostalCodeTarget == null)
            {
                throw new InvalidOperationException(ErrorMessages.AtLeastOneLocationSubTargetMustNotBeNull);
            }
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        protected override void ValidateBidsNotNullOrEmpty()
        {
            if (CityTarget != null)
            {
                ValidateListNotNullOrEmpty(CityTarget.Bids, "CityTarget.Bids");
            }

            if (MetroAreaTarget != null)
            {
                ValidateListNotNullOrEmpty(MetroAreaTarget.Bids, "MetroAreaTarget.Bids");
            }

            if (StateTarget != null)
            {
                ValidateListNotNullOrEmpty(StateTarget.Bids, "StateTarget.Bids");
            }

            if (CountryTarget != null)
            {
                ValidateListNotNullOrEmpty(CountryTarget.Bids, "CountryTarget.Bids");
            }

            if (PostalCodeTarget != null)
            {
                ValidateListNotNullOrEmpty(PostalCodeTarget.Bids, "PostalCodeTarget.Bids");
            }
        }

        private void ReconstructApiBids<TApiBid, TTarget>(LocationTargetType locationType, Func<TBid, TApiBid> createBid, Func<TTarget> getTarget, Action<TTarget> setTarget, Func<IList<TApiBid>> getBids, Action<IList<TApiBid>> setBids)
            where TApiBid : new()
            where TTarget : class, new()
        {
            var bidsFromFile = Bids.Where(t => t.LocationType == locationType).Select(createBid).ToList();

            if (bidsFromFile.Count > 0)
            {
                if (getTarget() == null)
                {
                    setTarget(new TTarget());

                    setBids(new List<TApiBid>());
                }

                getBids().AddRange(bidsFromFile);
            }
        }

        private void AddBids<T, TTarget>(IList<TBid> bids, Func<TTarget> getTarget, Func<TTarget, IList<T>> getBids, LocationTargetType locationType, Func<T, string> location, Func<T, bool> isExcluded, IntentOption? intentOption)
        {
            var target = getTarget();

            if (target == null)
            {
                return;
            }

            var v9Bids = getBids(target);

            if (v9Bids == null)
            {
                return;
            }

            bids.AddRange(v9Bids.Where(isExcluded).Select(b => CreateAndPopulateBid(x =>
            {             
                x.Location = location(b);
                x.LocationType = locationType;                
            })));
        }

        private T GetLocationProperty<T>(Func<LocationTarget2, T> getFunc)            
        {
            if (Location == null)
            {
                return default(T);
            }

            return getFunc(Location);
        }

        private void SetLocationProperty(Action<LocationTarget2> setAction)
        {
            if (Location == null)
            {
                Location = new LocationTarget2();
            }

            setAction(Location);
        }
    }
}

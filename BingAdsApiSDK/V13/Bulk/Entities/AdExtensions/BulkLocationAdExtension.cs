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

using Microsoft.BingAds.V13.Internal.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;
using Microsoft.BingAds.V13.Internal.Bulk.Entities;
using Microsoft.BingAds.V13.CampaignManagement;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V13.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an location ad extension. 
    /// This class exposes the <see cref="BulkLocationAdExtension.LocationAdExtension"/> property that can be read and written 
    /// as fields of the Location Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Location Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkLocationAdExtension : BulkAdExtensionBase<LocationAdExtension>
    {
        /// <summary>
        /// The location ad extension.
        /// </summary>
        public LocationAdExtension LocationAdExtension
        {
            get { return AdExtension; }
            set { AdExtension = value; }
        }

        private static readonly IEnumerable<IBulkMapping<BulkLocationAdExtension>> Mappings = new IBulkMapping<BulkLocationAdExtension>[]
        {
            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.BusinessName,
                c => c.LocationAdExtension.CompanyName,
                (v, c) => c.LocationAdExtension.CompanyName = v
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.PhoneNumber,
                c => c.LocationAdExtension.PhoneNumber.ToOptionalBulkString(c.LocationAdExtension.Id),
                (v, c) => c.LocationAdExtension.PhoneNumber = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.GeoCodeStatus,
                c => c.LocationAdExtension.GeoCodeStatus.ToBulkString(),
                (v, c) => c.LocationAdExtension.GeoCodeStatus = v.ParseOptional<BusinessGeoCodeStatus>()
            ),

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.AddressLine1,
                c => GetAddressPart(c, x => x.StreetAddress),
                (v, c) => SetAddressPart(c, x => x.StreetAddress = v)
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.AddressLine2,
                c => GetAddressPart(c, x => x.StreetAddress2.ToOptionalBulkString(c.LocationAdExtension.Id)),
                (v, c) => SetAddressPart(c, x => x.StreetAddress2 = v.GetValueOrEmptyString())
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.City,
                c =>  GetAddressPart(c, x => x.CityName),
                (v, c) => SetAddressPart(c, x => x.CityName = v)
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.ProvinceName,
                c => GetAddressPart(c, x => x.ProvinceName),
                (v, c) => SetAddressPart(c, x => x.ProvinceName = v)
            ), 
            
            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.StateOrProvince,
                c => GetAddressPart(c, x => x.ProvinceCode),
                (v, c) => SetAddressPart(c, x => x.ProvinceCode = v)
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.PostalCode,
                c => GetAddressPart(c, x => x.PostalCode),
                (v, c) => SetAddressPart(c, x => x.PostalCode = v)
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.CountryCode,
                c => GetAddressPart(c, x => x.CountryCode),
                (v, c) => SetAddressPart(c, x => x.CountryCode = v)
            ),

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.Latitude,
                c => GetGeoPointPart(c, x => (x.LatitudeInMicroDegrees / 1000000.0).ToBulkString()),
                (v, c) => SetGeoPointPart(c, (x, latitude) => x.LatitudeInMicroDegrees = (int)Math.Round(latitude.Parse<double>() * 1000000), v)
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.Longitude,
                c => GetGeoPointPart(c, x => (x.LongitudeInMicroDegrees / 1000000.0).ToBulkString()),
                (v, c) => SetGeoPointPart(c, (x, longitude) => x.LongitudeInMicroDegrees = (int)Math.Round(longitude.Parse<double>() * 1000000), v)
            )
        };

        private static string GetAddressPart(BulkLocationAdExtension adExtension, Func<Address, string> getFunc)
        {
            return adExtension.LocationAdExtension.Address != null ? getFunc(adExtension.LocationAdExtension.Address) : null;
        }

        private static void SetAddressPart(BulkLocationAdExtension adExtension, Action<Address> setFunc)
        {
            if (adExtension.LocationAdExtension.Address == null)
            {
                adExtension.LocationAdExtension.Address = new Address();
            }

            setFunc(adExtension.LocationAdExtension.Address);
        }

        private static string GetGeoPointPart(BulkLocationAdExtension adExtension, Func<GeoPoint, string> getFunc)
        {
            return adExtension.LocationAdExtension.GeoPoint != null ? getFunc(adExtension.LocationAdExtension.GeoPoint) : null;
        }

        private static void SetGeoPointPart(BulkLocationAdExtension adExtension, Action<GeoPoint, string> setFunc, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            if (adExtension.LocationAdExtension.GeoPoint == null)
            {
                adExtension.LocationAdExtension.GeoPoint = new GeoPoint();
            }

            setFunc(adExtension.LocationAdExtension.GeoPoint, value);
        }

        internal override void ProcessMappingsFromRowValues(RowValues values)
        {
            LocationAdExtension = new LocationAdExtension { Type = "LocationAdExtension" };

            if (!string.IsNullOrEmpty(values[StringTable.Latitude]) || !string.IsNullOrEmpty(values[StringTable.Longitude]))
            {
                LocationAdExtension.GeoPoint = new GeoPoint();
            }

            base.ProcessMappingsFromRowValues(values);

            values.ConvertToEntity(this, Mappings);
        }

        internal override void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData)
        {
            ValidatePropertyNotNull(LocationAdExtension, "LocationAdExtension");

            base.ProcessMappingsToRowValues(values, excludeReadonlyData);

            this.ConvertToValues(values, Mappings);
        }
    }
}

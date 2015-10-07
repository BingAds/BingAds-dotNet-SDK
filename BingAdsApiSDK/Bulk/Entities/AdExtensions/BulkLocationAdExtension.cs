using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
    /// Represents an location ad extension. 
    /// This class exposes the <see cref="BulkLocationAdExtension.LocationAdExtension"/> property that can be read and written 
    /// as fields of the Location Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511515">Location Ad Extension</see>. </para>
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
                c => c.LocationAdExtension.PhoneNumber.ToOptionalBulkString(),
                (v, c) => c.LocationAdExtension.PhoneNumber = v.GetValueOrEmptyString()
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.GeoCodeStatus,
                c => c.LocationAdExtension.GeoCodeStatus.ToBulkString(),
                (v, c) => c.LocationAdExtension.GeoCodeStatus = v.ParseOptional<BusinessGeoCodeStatus>()
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.IconMediaId,
                c => c.LocationAdExtension.IconMediaId.ToBulkString(),
                (v, c) => c.LocationAdExtension.IconMediaId = v.ParseOptional<long>()
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.AddressLine1,
                c => GetAddressPart(c, x => x.StreetAddress),
                (v, c) => SetAddressPart(c, x => x.StreetAddress = v)
            ), 

            new SimpleBulkMapping<BulkLocationAdExtension>(StringTable.AddressLine2,
                c => GetAddressPart(c, x => x.StreetAddress2.ToOptionalBulkString()),
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

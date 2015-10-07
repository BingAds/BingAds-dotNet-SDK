using System;
using System.Globalization;
using Microsoft.BingAds.CampaignManagement;
using Microsoft.BingAds.Bulk.Entities;

namespace Microsoft.BingAds.Internal
{
    internal static class StringExtensions
    {
        private const string DeleteValue = "delete_value";

        private static readonly CultureInfo ParsingCulture = new CultureInfo("en-US");

        public static PricingModel? ParseOptionalPricingModel(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return (PricingModel)Enum.Parse(typeof(PricingModel), s, ignoreCase: true);
        }

        public static string ToPricingModelBulkString(this PricingModel? pricingModel)
        {
            if (pricingModel == null)
            {
                return null;
            }

            return pricingModel.Value.ToBulkString().ToUpper();
        }

        public static T Parse<T>(this string s, bool returnDefaultValueOnNullOrEmpty = false)
            where T : struct
        {
            if (returnDefaultValueOnNullOrEmpty && string.IsNullOrEmpty(s))
            {
                return default(T);
            }

            if (typeof(T).IsEnum)
            {
                return (T)(Enum.Parse(typeof(T), s));
            }

            return (T)Convert.ChangeType(s, typeof(T), ParsingCulture);
        }

        public static T Parse<T>(this string s, T nullValue)
            where T : struct
        {
            if (string.IsNullOrEmpty(s))
            {
                return nullValue;
            }

            return Parse<T>(s);
        }

        public static T? ParseOptional<T>(this string s)
            where T : struct
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            if (typeof(T).IsEnum)
            {
                return (T?)(Enum.Parse(typeof(T), s));
            }

            return (T?)Convert.ChangeType(s, typeof(T), ParsingCulture);
        }

        public static string ToBulkString<T>(this T? value)
            where T : struct, IFormattable
        {
            if (value == null)
            {
                return null;
            }

            return value.Value.ToBulkString();
        }

        public static string ToBulkString<T>(this T value, bool returnNullForDefaultValue = false)
            where T : struct, IFormattable
        {
            if (returnNullForDefaultValue && value.Equals(default(T)))
            {
                return null;
            }

            return value.ToString(null, ParsingCulture);
        }        

        public static Date ParseDate(this string s)
        {
            var dateTime = ParseOptional<DateTime>(s);

            if (dateTime == null)
            {
                return null;
            }

            return new Date { Year = dateTime.Value.Year, Month = dateTime.Value.Month, Day = dateTime.Value.Day };
        }

        public static string ToDateBulkString(this Date date)
        {
            if (date == null)
            {
                return null;
            }

            return string.Format("{0}/{1}/{2}", date.Month, date.Day, date.Year);
        }

        public static string ToAdRotationBulkString(this AdRotation adRotation)
        {
            if (adRotation == null)
            {
                return null;
            }

            if (adRotation.Type == null)
            {
                return DeleteValue;
            }

            return adRotation.Type.ToBulkString();
        }

        public static AdRotation ParseAdRotation(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return new AdRotation { Type = (s == DeleteValue) ? (AdRotationType?)null : s.Parse<AdRotationType>() };
        }

        public static string ToSearchAdDistributionBulkString(this AdDistribution? adDistribution)
        {
            if (adDistribution == null)
            {
                return null;
            }

            return adDistribution.Value.HasFlag(AdDistribution.Search) ? "On" : "Off";
        }

        public static AdDistribution ParseSearchAdDistribution(this string s)
        {
            return (s == "On") ? AdDistribution.Search : 0;
        }

        public static string ToContentAdDistributionBulkString(this AdDistribution? adDistribution)
        {
            if (adDistribution == null)
            {
                return null;
            }

            return adDistribution.Value.HasFlag(AdDistribution.Content) ? "On" : "Off";
        }

        public static AdDistribution ParseContentAdDistribution(this string s)
        {
            return (s == "On") ? AdDistribution.Content : 0;
        }

        public static string ToAdGroupBidBulkString(this Bid bid)
        {
            if (bid == null || bid.Amount == null)
            {
                return null;
            }

            return bid.Amount.ToBulkString();
        }

        public static Bid ParseAdGroupBid(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return new Bid { Amount = s.Parse<double>() };
        }

        public static string ToKeywordBidBulkString(this Bid bid)
        {
            if (bid == null)
            {
                return null;
            }

            if (bid.Amount == null)
            {
                return DeleteValue;
            }

            return bid.Amount.ToBulkString();
        }

        public static Bid ParseKeywordBid(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return new Bid() { Amount = null };
            }

            return new Bid { Amount = s.Parse<double>() };
        }

        public static long? ParseDevicePreference(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            switch (s)
            {
                case "All":
                    return 0;
                case "Mobile":
                    return 30001;
                default:
                    throw new ArgumentException("Unknown device preference");
            }
        }

        public static string ToDevicePreferenceBulkString(this long? devicePreference)
        {
            switch (devicePreference)
            {
                case null:
                    return null;
                case 0:
                    return "All";
                case 30001:
                    return "Mobile";
            }

            throw new ArgumentException("Unknown device preference");
        }

        public static Minute ParseMinute(this string s)
        {
            var minuteNumber = int.Parse(s);

            switch (minuteNumber)
            {
                case 0:
                    return Minute.Zero;
                case 15:
                    return Minute.Fifteen;
                case 30:
                    return Minute.Thirty;
                case 45:
                    return Minute.FortyFive;
                default: throw new ArgumentException("Unknown minute");
            }
        }

        public static string ToMinuteBulkString(this Minute minute)
        {
            switch (minute)
            {
                case Minute.Zero:
                    return "0";
                case Minute.Fifteen:
                    return "15";
                case Minute.Thirty:
                    return "30";
                case Minute.FortyFive:
                    return "45";
                default: throw new ArgumentException("Unknown minute");
            }
        }

        public static LocationTargetType ParseLocationTargetType(this string s)
        {
            switch (s)
            {
                case "Metro Area":
                    return LocationTargetType.MetroArea;
                case "Postal Code":
                    return LocationTargetType.PostalCode;
                default:
                    return s.Parse<LocationTargetType>();
            }
        }

        public static string ToLocationTargetTypeBulkString(this LocationTargetType locationTargetType)
        {
            switch (locationTargetType)
            {
                case LocationTargetType.MetroArea:
                    return "Metro Area";
                case LocationTargetType.PostalCode:
                    return "Postal Code";
                default:
                    return locationTargetType.ToBulkString();
            }
        }

        public static string ToOptionalBulkString(this string sourceString)
        {
            if (sourceString == null)
            {
                return null;
            }

            if (sourceString == string.Empty)
            {
                return DeleteValue;
            }

            return sourceString;
        }

        public static string GetValueOrEmptyString(this string bulkString)
        {
            if (string.IsNullOrEmpty(bulkString))
            {
                return string.Empty;
            }

            return bulkString;
        }
    }
}

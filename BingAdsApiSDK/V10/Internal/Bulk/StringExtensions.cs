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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds.V10.Bulk.Entities;

namespace Microsoft.BingAds.V10.Internal.Bulk
{
    internal static class StringExtensions
    {
        private const string DeleteValue = "delete_value";

        private static readonly CultureInfo ParsingCulture = new CultureInfo("en-US");

        private static readonly Regex UrlSplitter = new Regex(@";\s*(?=https?://)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex CustomParameterSplitter = new Regex(@"(?<!\\);\s*", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex CustomKvPattern = new Regex(@"^{_(.*?)}=(.*$)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static PricingModel? ParseOptionalPricingModel(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return (PricingModel) Enum.Parse(typeof (PricingModel), s, ignoreCase: true);
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

            if (typeof (T).IsEnum)
            {
                return (T) (Enum.Parse(typeof (T), s));
            }

            return (T) Convert.ChangeType(s, typeof (T), ParsingCulture);
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

            if (typeof (T).IsEnum)
            {
                return (T?) (Enum.Parse(typeof (T), s));
            }

            return (T?) Convert.ChangeType(s, typeof (T), ParsingCulture);
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

            return new Date {Year = dateTime.Value.Year, Month = dateTime.Value.Month, Day = dateTime.Value.Day};
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

            return new AdRotation {Type = (s == DeleteValue) ? (AdRotationType?) null : s.Parse<AdRotationType>()};
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

            return new Bid {Amount = s.Parse<double>()};
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
                return new Bid() {Amount = null};
            }

            return new Bid {Amount = s.Parse<double>()};
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
                default:
                    throw new ArgumentException("Unknown minute");
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
                default:
                    throw new ArgumentException("Unknown minute");
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

        public static string WriteUrls(this IList<string> urls, string seperator)
        {
            if (urls == null)
            {
                return null;
            }

            if (urls.Count == 0)
            {
                return DeleteValue;
            }

            var text = string.Join(seperator, urls);

            return text;
        }

        public static IList<string> ParseUrls(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            var urls =
                UrlSplitter.Split(s)
                    .Where(token => !string.IsNullOrWhiteSpace(token) && token != ";")
                    .ToList();

            return urls;
        }

        public static string ToBulkString(this CustomParameters parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            if (parameters.Parameters == null || parameters.Parameters.Count == 0)
            {
                return DeleteValue;
            }

            return string.Join("; ",
                parameters.Parameters.Select(
                    entry =>
                        string.Format(CultureInfo.InvariantCulture, "{{_{0}}}={1}", entry.Key,
                            EscapeParameterText(entry.Value))));
        }

        private static string EscapeParameterText(string text)
        {
            var buffer = new StringBuilder(text.Length * 2);

            foreach (var c in text)
            {
                if (c == ';' || c == '\\')
                {
                    buffer.Append('\\');
                }

                buffer.Append(c);
            }

            return buffer.ToString();
        }

        public static CustomParameters ParseCustomParameters(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            return new CustomParameters
            {
                Parameters = CustomParameterSplitter.Split(s).Select(token =>
                {
                    if (string.IsNullOrWhiteSpace(token))
                    {
                        return null;
                    }

                    token = token.Trim();

                    var match = CustomKvPattern.Match(token);

                    if (!match.Success)
                    {
                        throw new Exception(string.Format("Bad format for CustomParameters: {0}", s));
                    }

                    return new CustomParameter
                    {
                        Key = match.Groups[1].Value,
                        Value = UnescapeParameterText(match.Groups[2].Value)
                    };
                }).Where(p => p != null).ToList()
            };
        }

        private static string UnescapeParameterText(string text)
        {
            var buffer = new StringBuilder(text.Length);

            var length = text.Length;

            for (var i = 0; i < length; i++)
            {
                if (text[i] == '\\')
                {
                    i++;
                }

                if (i < length)
                {
                    buffer.Append(text[i]);
                }
            }

            var result = buffer.ToString();

            return result;
        }

        public static string GetValueOrEmptyString(this string bulkString)
        {
            if (string.IsNullOrEmpty(bulkString))
            {
                return string.Empty;
            }

            return bulkString;
        }

        public static string ToNativePreferenceBulkString(this IList<KeyValuePair<string, string>> parameters)
        {
            if (parameters == null)
                return null;

            foreach (var keyValuePair in parameters)
            {
                if (keyValuePair.Key.Equals("NativePreference"))
                {
                    var value = keyValuePair.Value.ToLower();

                    if (value.Equals("true"))
                    {
                        return "Native";
                    } 
                    else if (value.Equals("false"))
                    {
                        return "All";
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Unkonwn value for Native Preference : {0}", value));
                    }
                }
            }
            return null;
        }
		
        public static BiddingScheme ParseBiddingScheme(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            switch (s)
            {
                case "EnhancedCpc":
                    return new EnhancedCpcBiddingScheme { Type = "EnhancedCpc" };
                case "InheritFromParent":
                    return new InheritFromParentBiddingScheme { Type = "InheritFromParent" };
                case "ManualCpc":
                    return new ManualCpcBiddingScheme { Type = "ManualCpc" };
                case "MaxClicks":
                    return new MaxClicksBiddingScheme { Type = "MaxClicks" };
                case "MaxConversions":
                    return new MaxConversionsBiddingScheme { Type = "MaxConversions" };
                case "TargetCpa":
                    return new TargetCpaBiddingScheme { Type = "TargetCpa" };
                default:
                    throw new ArgumentException(string.Format("Unknown value for Bid Strategy Type : %s", s));
            }
        }

        public static string ToBiddingSchemeBulkString(this BiddingScheme biddingScheme)
        {
            if (biddingScheme == null)
            {
                return null;
            }

            var enhancedCpcBiddingScheme = biddingScheme as EnhancedCpcBiddingScheme;
            if (enhancedCpcBiddingScheme != null)
                return "EnhancedCpc";
            var inheritFromParentBiddingScheme = biddingScheme as InheritFromParentBiddingScheme;
            if (inheritFromParentBiddingScheme != null)
                return "InheritFromParent";
            var manualCpcBiddingScheme = biddingScheme as ManualCpcBiddingScheme;
            if (manualCpcBiddingScheme != null)
                return "ManualCpc";
            var maxClicksBiddingScheme = biddingScheme as MaxClicksBiddingScheme;
            if (maxClicksBiddingScheme != null)
                return "MaxClicks";
            var maxConversionsBiddingScheme = biddingScheme as MaxConversionsBiddingScheme;
            if (maxConversionsBiddingScheme != null)
                return "MaxConversions";
            var targetCpaBiddingScheme = biddingScheme as TargetCpaBiddingScheme;
            if (targetCpaBiddingScheme != null)
                return "TargetCpa";

            throw new ArgumentException("Unknown bidding scheme");
        }
    }
}
//=====================================================================================================================================================
// Bing Ads .NET SDK ver. 11.5
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
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.BingAds.V11.CampaignManagement;
using Microsoft.BingAds.V11.Bulk.Entities;

namespace Microsoft.BingAds.V11.Internal.Bulk
{
    internal static class StringExtensions
    {
        private const string DeleteValue = "delete_value";

        private static readonly CultureInfo ParsingCulture = new CultureInfo("en-US");

        private static readonly Regex UrlSplitter = new Regex(@";\s*(?=https?://)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex CustomParameterSplitter = new Regex(@"(?<!\\);\s*", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex CustomKvPattern = new Regex(@"^{_(.*?)}=(.*$)", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex DayTimeRangesPattern = new Regex(@"^\((Sunday|Monday|Tuesday|Wednesday|Thursday|Friday|Saturday)\[(\d\d?)\:(\d\d)\-(\d\d?)\:(\d\d)\]\)$", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex PageRulePattern = new Regex(@"^(Url|ReferrerUrl|None) (Equals|Contains|BeginsWith|EndsWith|NotEquals|DoesNotContain|DoesNotBeginWith|DoesNotEndWith) ([^()]*)$", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex OperandPattern = new Regex(@"^(Category|Action|Label|Value) ([^()]*)$", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex StringOperatorPattern = new Regex("^(Equals|Contains|BeginsWith|EndsWith|NotEquals|DoesNotContain|DoesNotBeginWith|DoesNotEndWith) ([^()]*)$", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static readonly Regex NumberOperatorPattern = new Regex("^(Equals|GreaterThan|LessThan|GreaterThanEqualTo|LessThanEqualTo) ([^()]*)$", RegexOptions.CultureInvariant | RegexOptions.Compiled);

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

        public static DateTime? ParseOptionalDateTime(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }
            DateTime dateValue;
            if (DateTime.TryParseExact(s, StringTable.LocalDateTimeFormats, ParsingCulture, DateTimeStyles.None, out dateValue))
            {
                return dateValue;
            }
            else
            {
                return ParseOptional<DateTime>(s);
            }

        }

        public static string ToBulkString<T>(this T? value)
            where T : struct, IFormattable
        {
            return value?.ToBulkString();
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
            var dateTime = ParseOptionalDateTime(s);

            if (dateTime == null)
            {
                return null;
            }

            return new Date {Year = dateTime.Value.Year, Month = dateTime.Value.Month, Day = dateTime.Value.Day};
        }

        public static DateTime ParseDateTime(this String s)
        {
            var dateTime = ParseOptionalDateTime(s);
            if (dateTime == null)
            {
                return default(DateTime);
            }
            return dateTime.Value;
        }

        public static string ToDateBulkString(this Date date)
        {
            if (date == null)
            {
                return null;
            }

            return string.Format("{0}/{1}/{2}", date.Month, date.Day, date.Year);
        }

        public static string ToScheduleDateBulkString(this Date date)
        {
            if (date == null || (date.Month == 0 && date.Day == 0 && date.Year == 0))
            {
                return DeleteValue;
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

        public static string ToAdGroupCriterionBidBulkString(this Bid bid)
        {
            if (bid == null || bid.Amount == null)
            {
                return null;
            }

            return bid.Amount.ToBulkString();
        }

        public static Bid ParseAdGroupCriterionBid(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return new Bid { Amount = s.Parse<double>() };
        }

        public static string ToAdGroupCriterionFixedBidBulkString(this FixedBid bid)
        {
            if (bid == null)
            {
                return null;
            }

            return bid.Amount.ToBulkString();
        }

        public static FixedBid ParseAdGroupCriterionFixedBid(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return new FixedBid { Amount = s.Parse<double>() };
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

        public static string ToBidBulkString(this Bid bid)
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

        public static Bid ParseBid(this string s)
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

        public static string ToAdFormatPreference(this string adFormatPreference)
        {

            if (string.IsNullOrEmpty(adFormatPreference))
            {
                return null;
            }
            else if ("True".Equals(adFormatPreference))
            {
                return "Native";
            }
            else if ("False".Equals(adFormatPreference))
            {
                return "All";
            }
            throw new ArgumentException(string.Format("Unsupported value for Native Preference : {0}", adFormatPreference));
        }

        public static string ParseAdFormatPreference(this string s)
        {
            if (string.IsNullOrEmpty(s) || s.Equals("All"))
            {
                return "False";
            }
            else if (s.Equals("Native"))
            {
                return "True";
            }
            throw new ArgumentException(string.Format("Unsupported value for Native Preference : {0}", s));
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

        public static string ToMinuteBulkString(this Minute? minute)
        {
            if (minute == null)
            {
                return null;
            }
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

        public static Day ParseDay(this string s)
        {
            switch (s.ToLower())
            {
                case "sunday":
                    return Day.Sunday;
                case "monday":
                    return Day.Monday;
                case "tuesday":
                    return Day.Tuesday;
                case "wednesday":
                    return Day.Wednesday;
                case "thursday":
                    return Day.Thursday;
                case "friday":
                    return Day.Friday;
                case "saturday":
                    return Day.Saturday;
                default:
                    throw new ArgumentException("Unkonwn day");
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

        public static string WriteCampaignLanguages(this IList<string> languages, string seperator)
        {
            if (languages == null)
            {
                return null;
            }

            if (languages.Count == 0)
            {
                return DeleteValue;
            }

            var text = string.Join(seperator, languages);

            return text;
        }

        public static IList<string> ParseCampaignLanguages(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            var languages = s.Split(new string[] { ";" }, StringSplitOptions.None).ToList();
            
            return languages;
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
                    throw new ArgumentException(string.Format("Unknown value for Bid Strategy Type : {0}", s));
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

        public static List<string> ParseStructuredSnippetValues(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            var values = s.Split(';')
                    .Where(token => !string.IsNullOrWhiteSpace(token) && token != ";")
                    .ToList();

            return values;           
        }

        public static string WriteStructuredSnippetValues(this IList<string> values, string seperator)
        {
            if (values == null || values.Count == 0)
            {
                return null;
            }

            var text = string.Join(seperator, values);

            return text;
        }

        public static string ToDayTimeRangesBulkString(this IList<DayTime> dayTimeRanges)
        {
            if (dayTimeRanges == null || dayTimeRanges.Count == 0)
            {
                return DeleteValue;
            }

            return string.Join(";",
                dayTimeRanges.Select(
                    dayTime =>
                        string.Format(CultureInfo.InvariantCulture, "({0}[{1:00}:{2:00}-{3:00}:{4:00}])", dayTime.Day,
                            dayTime.StartHour, int.Parse(ToMinuteBulkString(dayTime.StartMinute)), dayTime.EndHour, int.Parse(ToMinuteBulkString(dayTime.EndMinute)))));
        }

        public static List<DayTime> ParseDayTimeRanges(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return 
                s.Split(';').Select(token =>
                {
                    if (string.IsNullOrWhiteSpace(token))
                    {
                        return null;
                    }

                    token = token.Trim();

                    var match = DayTimeRangesPattern.Match(token);

                    if (!match.Success)
                    {
                        throw new Exception(string.Format("Bad format for DayTimeRanges: {0}", s));
                    }

                    return new DayTime
                    {
                        Day = ParseDay(match.Groups[1].Value),
                        StartHour = int.Parse(match.Groups[2].Value),
                        StartMinute = ParseMinute(match.Groups[3].Value),
                        EndHour = int.Parse(match.Groups[4].Value),
                        EndMinute = ParseMinute(match.Groups[5].Value),
                    };
                }).Where(p => p != null).ToList()
            ;
        }

        public static string ToUseSearcherTimeZoneBulkString(this bool? useSearcherTimeZone)
        {
            if (!useSearcherTimeZone.HasValue)
            {
                return "delete_value";
            }
            return useSearcherTimeZone.Value ? "true" : "false";
        }

        public static bool? ParseUseSearcherTimeZone(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            var result = false;

            if (!bool.TryParse(s, out result))
            {
                throw new ArgumentException(string.Format("Unknown values for Use Searcher Time Zone: {0}", s));
            }
            return result;
        }

        public static string ToCriterionNameBulkString(this WebpageParameter webpageParameter)
        {
            if (webpageParameter == null || webpageParameter.CriterionName == null)
            {
                return null;
            }

            if (webpageParameter.CriterionName.Length == 0)
            {
                return DeleteValue;
            }

            return webpageParameter.CriterionName;
        }

        public static string ParseCriterionName(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return s;
        }

        public static string ToRemarketingRuleBulkString(this RemarketingRule remarketingRule)
        {
            if (remarketingRule == null)
            {
                return null;
            }

            if (remarketingRule is CustomEventsRule)
            {
                return string.Format("CustomEvents{0}", GetCustomEventsRule((CustomEventsRule)remarketingRule));
            }

            if (remarketingRule is PageVisitorsRule)
            {
                return string.Format("PageVisitors{0}",
                    GetRuleItemGroups(((PageVisitorsRule) remarketingRule).RuleItemGroups));
            }

            if (remarketingRule is PageVisitorsWhoVisitedAnotherPageRule)
            {
                return string.Format("PageVisitorsWhoVisitedAnotherPage({0}) and ({1})",
                    GetRuleItemGroups(((PageVisitorsWhoVisitedAnotherPageRule)remarketingRule).RuleItemGroups),
                    GetRuleItemGroups(((PageVisitorsWhoVisitedAnotherPageRule)remarketingRule).AnotherRuleItemGroups));
            }

            if (remarketingRule is PageVisitorsWhoDidNotVisitAnotherPageRule)
            {
                return string.Format("PageVisitorsWhoDidNotVisitAnotherPage({0}) and not ({1})",
                    GetRuleItemGroups(((PageVisitorsWhoDidNotVisitAnotherPageRule)remarketingRule).IncludeRuleItemGroups),
                    GetRuleItemGroups(((PageVisitorsWhoDidNotVisitAnotherPageRule)remarketingRule).ExcludeRuleItemGroups));
            }

            if (remarketingRule is RemarketingRule)
            {
                return null;
            }

            throw new ArgumentException("Invalid Remarketing Rule");
        }

        private static string GetCustomEventsRule(CustomEventsRule rule)
        {
            if (rule == null)
            {
                return null;
            }

            var rules = new List<string>();

            if (rule.Category != null)
            {
                rules.Add(string.Format("Category {0} {1}", rule.CategoryOperator, rule.Category));
            }

            if (rule.Action != null)
            {
                rules.Add(string.Format("Action {0} {1}", rule.ActionOperator, rule.Action));
            }

            if (rule.Label != null)
            {
                rules.Add(string.Format("Label {0} {1}", rule.LabelOperator, rule.Label));
            }

            if (rule.Value != null)
            {
                rules.Add(string.Format("Value {0} {1}", rule.ValueOperator, rule.Value));
            }

            if (rules.Count == 0)
            {
                throw new ArgumentException("Invalid Custom Events Rule");
            }

            return string.Join(" and ", rules.Select(i => "(" + i + ")"));;
        }


        private static string GetRuleItemGroups(ICollection<RuleItemGroup> ruleItemGroups)
        {
            if (ruleItemGroups == null || ruleItemGroups.Count == 0)
            {
                return null;
            }

            return string.Join(" or ", ruleItemGroups.Select(i => "(" + GetRuleItems(i.Items) + ")"));
        }

        private static string GetRuleItems(ICollection<RuleItem> ruleItems)
        {
            if (ruleItems == null || ruleItems.Count == 0)
            {
                return null;
            }

            return string.Join(" and ", ruleItems.Select(i => "(" + GetRuleItem(i) + ")"));
        }

        private static string GetRuleItem(RuleItem ruleItem)
        {
            var item = ruleItem as StringRuleItem;

            return item != null ? string.Format("{0} {1} {2}", item.Operand, item.Operator, item.Value) : null;
        }

        public static RemarketingRule ParseRemarketingRule(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            var pos = s.IndexOf('(');

            if (pos == -1)
            {
                throw new ArgumentException(string.Format("Invalid Remarketing Rule {0}", s));
            }

            var type = s.Substring(0, pos);

            var ruleStr = s.Substring(pos + 1, s.Length - pos - 1);

            switch (type.ToLower())
            {
                case "pagevisitors":
                    return ParsePageVisitorsRule(ruleStr);

                case "pagevisitorswhovisitedanotherpage":
                    return ParsePageVisitorsWhoVisitedAnotherPageRule(ruleStr);

                case "pagevisitorswhodidnotvisitanotherpage":
                    return ParsePageVisitorsWhoDidNotVisitAnotherPageRule(ruleStr);

                case "customevents":
                    return ParseCustomEventsRule(ruleStr);

                default:
                    throw new ArgumentException(string.Format("Invalid Remarketing Rule Type: {0}", type));
            }
        }

        private static RemarketingRule ParsePageVisitorsRule(string ruleStr)
        {
            if (string.IsNullOrEmpty(ruleStr))
            {
                return null;
            }

            return new PageVisitorsRule()
            {
                Type = "PageVisitors",

                RuleItemGroups = ParserRuleItemGroups(ruleStr)
            };
        }

        private static RemarketingRule ParsePageVisitorsWhoVisitedAnotherPageRule(string ruleStr)
        {
            if (string.IsNullOrEmpty(ruleStr))
            {
                return null;
            }

            var groups = ruleStr.Split(new[] {@"))) and ((("}, StringSplitOptions.None);

            if (groups == null || groups.Length != 2)
            {
                throw new ArgumentException(string.Format("Invalid Remarketing Rule: {0}", ruleStr));
            }

            return new PageVisitorsWhoVisitedAnotherPageRule()
            {
                Type = "PageVisitorsWhoVisitedAnotherPage",

                RuleItemGroups = ParserRuleItemGroups(groups[0]),

                AnotherRuleItemGroups = ParserRuleItemGroups(groups[1])
            };
        }

        private static RemarketingRule ParsePageVisitorsWhoDidNotVisitAnotherPageRule(string ruleStr)
        {
            if (string.IsNullOrEmpty(ruleStr))
            {
                return null;
            }

            var groups = ruleStr.Split(new[] { @"))) and not (((" }, StringSplitOptions.None);

            if (groups == null || groups.Length != 2)
            {
                throw new ArgumentException(string.Format("Invalid Remarketing Rule: {0}", ruleStr));
            }

            return new PageVisitorsWhoDidNotVisitAnotherPageRule()
            {
                Type = "PageVisitorsWhoDidNotVisitAnotherPage",

                IncludeRuleItemGroups = ParserRuleItemGroups(groups[0]),

                ExcludeRuleItemGroups = ParserRuleItemGroups(groups[1])
            };
        }

        private static RemarketingRule ParseCustomEventsRule(string ruleStr)
        {
            if (string.IsNullOrEmpty(ruleStr))
            {
                return null;
            }

            var rule = new CustomEventsRule { Type = "CustomEvents" };

            var ruleItemStrs = ruleStr.Split(new[] { @") and (" }, StringSplitOptions.None);

            foreach (var ruleItemStr in ruleItemStrs)
            {
                var temp = ruleItemStr.Replace("(", "").Replace(")", "");

                var match = OperandPattern.Match(temp);

                if (!match.Success)
                {
                    throw new ArgumentException(string.Format("Invalid Custom Events Rule Item: {0}", ruleStr));
                }

                var operand = match.Groups[1].Value.ToLower();

                var operatorStr = match.Groups[2].Value;

                if (operand.Equals("value"))
                {
                    var numberOperator = NumberOperatorPattern.Match(operatorStr);

                    if (!numberOperator.Success)
                    {
                        throw new ArgumentException(string.Format("Invalid Custom Events Rule Item Value Operator: {0}", operatorStr));
                    }

                    rule.ValueOperator = ParseNumberOperator(numberOperator.Groups[1].Value);

                    rule.Value = decimal.Parse(numberOperator.Groups[2].Value);
                }
                else
                {
                    var stringOperator = StringOperatorPattern.Match(operatorStr);

                    if (!stringOperator.Success)
                    {
                        throw new ArgumentException(string.Format("Invalid Custom Events Rule Item String Operator: {0}", operatorStr));
                    }

                    switch(operand)
                    {
                        case "category":
                            rule.CategoryOperator = ParseStringOperator(stringOperator.Groups[1].Value);
                            rule.Category = stringOperator.Groups[2].Value;
                            break;

                        case "label":
                            rule.LabelOperator = ParseStringOperator(stringOperator.Groups[1].Value);
                            rule.Label = stringOperator.Groups[2].Value;
                            break;

                        case "action":
                            rule.ActionOperator = ParseStringOperator(stringOperator.Groups[1].Value);
                            rule.Action = stringOperator.Groups[2].Value;
                            break;

                        default:
                            throw new ArgumentException(string.Format("Invalid Custom Events Rule Item Operand: {0}", operatorStr));
                    }
                }
            }
            return rule;
        }

        private static List<RuleItemGroup> ParserRuleItemGroups(string groups)
        {
            var groupItems = groups.Split(new[] { @")) or ((" }, StringSplitOptions.None);

            return new List<RuleItemGroup>(groupItems.Select(ParseRuleItemGroup));
        }

        private static RuleItemGroup ParseRuleItemGroup(string group)
        {
            var ruleItems = group.Split(new[] { @") and (" }, StringSplitOptions.None);

            var ruleItemGroup = new RuleItemGroup
            {
                Items = new List<RuleItem>()
            };

            foreach (var ruleItem in ruleItems.Select(ParseRuleItem).Where(ruleItem => ruleItem != null))
            {
                ruleItemGroup.Items.Add(ruleItem);
            }

            return ruleItemGroup;
        }

        private static StringRuleItem ParseRuleItem(string ruleItemStr)
        {
            if (string.IsNullOrEmpty(ruleItemStr))
            {
                return null;
            }

            ruleItemStr = ruleItemStr.Replace("(", "").Replace(")", "");

            var match = PageRulePattern.Match(ruleItemStr);

            if (!match.Success)
            {
                throw new ArgumentException(string.Format("Invalid Rule Item: {0}", ruleItemStr));
            }

            return new StringRuleItem
            {
                Type = "String",

                Operand = match.Groups[1].Value,

                Operator = ParseStringOperator(match.Groups[2].Value),

                Value = match.Groups[3].Value
            };
        }

        private static NumberOperator ParseNumberOperator(string numOperator)
        {
            numOperator = numOperator.ToLower();

            switch (numOperator)
            {
                case "equals":
                    return NumberOperator.Equals;

                case "greaterthan":
                    return NumberOperator.GreaterThan;

                case "greaterthanequalto":
                    return NumberOperator.GreaterThanEqualTo;

                case "lessthan":
                    return NumberOperator.LessThan;

                case "lessthanequalto":
                    return NumberOperator.LessThanEqualTo;

                default:
                    throw new ArgumentException(string.Format("Invalid Number Rule Item Operator: {0}", numOperator));
            }
        }

        private static StringOperator ParseStringOperator(string strOperator)
        {
            strOperator = strOperator.ToLower();

            switch (strOperator)
            {
                case "equals":
                    return StringOperator.Equals;

                case "contains":
                    return StringOperator.Contains;

                case "beginswith":
                    return StringOperator.BeginsWith;

                case "endswith":
                    return StringOperator.EndsWith;

                case "notequals":
                    return StringOperator.NotEquals;

                case "doesnotcontain":
                    return StringOperator.DoesNotContain;

                case "doesnotbeginwith":
                    return StringOperator.DoesNotBeginWith;

                case "doesnotendwith":
                    return StringOperator.DoesNotEndWith;

                default:
                    throw new ArgumentException(string.Format("Invalid String Rule Item Operator: {0}", strOperator));
            }
        }
    }
}
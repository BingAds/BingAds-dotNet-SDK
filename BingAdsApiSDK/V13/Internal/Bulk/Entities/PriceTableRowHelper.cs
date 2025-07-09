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

using Microsoft.BingAds.V13.CampaignManagement;

namespace Microsoft.BingAds.V13.Internal.Bulk.Entities
{
    internal static class PriceTableRowHelper
    {
        public const int MaxNumberOfProductTableRows = 8;

        public static void AddPriceTableRowsFromRowValues(RowValues values, IList<PriceTableRow> priceTableRows)
        {
            var currencyCodePrefix = StringTable.CurrencyCode1.Remove(StringTable.CurrencyCode1.Length - 1);
            var priceDescriptionPrefix = StringTable.PriceDescription1.Remove(StringTable.PriceDescription1.Length - 1);
            var headerPrefix = StringTable.Header1.Remove(StringTable.Header1.Length - 1);
            var finalMobileUrlPrefix = StringTable.FinalMobileUrl1.Remove(StringTable.FinalMobileUrl1.Length - 1);
            var finalUrlPrefix = StringTable.FinalUrl1.Remove(StringTable.FinalUrl1.Length - 1);
            var pricePrefix = StringTable.Price1.Remove(StringTable.Price1.Length - 1);
            var priceQualifierPrefix = StringTable.PriceQualifier1.Remove(StringTable.PriceQualifier1.Length - 1);
            var priceUnitPrefix = StringTable.PriceUnit1.Remove(StringTable.PriceUnit1.Length - 1);
            var termsAndConditionsPrefix = StringTable.TermsAndConditions1.Remove(StringTable.TermsAndConditions1.Length - 1);
            var termsAndConditionsUrlPrefix = StringTable.TermsAndConditionsUrl1.Remove(StringTable.TermsAndConditionsUrl1.Length - 1);
            
            for (int i = 1; i <= MaxNumberOfProductTableRows; i++)
            {
                string currencyCode;
                string priceDescription;
                string header;
                string finalMobileUrl;
                string finalUrl;
                string price;
                string priceQualifier;
                string priceUnit;
                string termsAndConditions;
                string termsAndConditionsUrl;

                values.TryGetValue(currencyCodePrefix + i, out currencyCode);
                values.TryGetValue(priceDescriptionPrefix + i, out priceDescription);
                values.TryGetValue(headerPrefix + i, out header);
                values.TryGetValue(finalMobileUrlPrefix + i, out finalMobileUrl);
                values.TryGetValue(finalUrlPrefix + i, out finalUrl);
                values.TryGetValue(pricePrefix + i, out price);
                values.TryGetValue(priceQualifierPrefix + i, out priceQualifier);
                values.TryGetValue(priceUnitPrefix + i, out priceUnit);
                values.TryGetValue(termsAndConditionsPrefix + i, out termsAndConditions);
                values.TryGetValue(termsAndConditionsUrlPrefix + i, out termsAndConditionsUrl);

                if (!string.IsNullOrEmpty(currencyCode) || 
                    !string.IsNullOrEmpty(priceDescription) ||
                    !string.IsNullOrEmpty(header) ||
                    !string.IsNullOrEmpty(finalMobileUrl) ||
                    !string.IsNullOrEmpty(finalUrl) ||
                    !string.IsNullOrEmpty(price) ||
                    !string.IsNullOrEmpty(priceQualifier) ||
                    !string.IsNullOrEmpty(priceUnit) ||
                    !string.IsNullOrEmpty(termsAndConditions) ||
                    !string.IsNullOrEmpty(termsAndConditionsUrl))
                {
                    priceTableRows.Add(
                        new PriceTableRow
                        {
                            CurrencyCode = currencyCode,
                            Description = priceDescription,
                            Header = header,
                            FinalMobileUrls = new string[]
                            {
                                finalMobileUrl
                            },
                            FinalUrls = new string[]
                            {
                                finalUrl
                            },
                            Price = price.Parse<double>(),
                            PriceQualifier = priceQualifier.Parse<PriceQualifier>(),
                            PriceUnit = priceUnit.Parse<PriceUnit>(),
                            TermsAndConditions = termsAndConditions,
                            TermsAndConditionsUrl = termsAndConditionsUrl
                        }
                    );
                }
            }
        }

        public static void AddRowValuesFromPriceTableRows(IList<PriceTableRow> priceTableRows, RowValues rowValues)
        {
            var currencyCodePrefix = StringTable.CurrencyCode1.Remove(StringTable.CurrencyCode1.Length - 1);
            var priceDescriptionPrefix = StringTable.PriceDescription1.Remove(StringTable.PriceDescription1.Length - 1);
            var headerPrefix = StringTable.Header1.Remove(StringTable.Header1.Length - 1);
            var finalMobileUrlPrefix = StringTable.FinalMobileUrl1.Remove(StringTable.FinalMobileUrl1.Length - 1);
            var finalUrlPrefix = StringTable.FinalUrl1.Remove(StringTable.FinalUrl1.Length - 1);
            var pricePrefix = StringTable.Price1.Remove(StringTable.Price1.Length - 1);
            var priceQualifierPrefix = StringTable.PriceQualifier1.Remove(StringTable.PriceQualifier1.Length - 1);
            var priceUnitPrefix = StringTable.PriceUnit1.Remove(StringTable.PriceUnit1.Length - 1);
            var termsAndConditionsPrefix = StringTable.TermsAndConditions1.Remove(StringTable.TermsAndConditions1.Length - 1);
            var termsAndConditionsUrlPrefix = StringTable.TermsAndConditionsUrl1.Remove(StringTable.TermsAndConditionsUrl1.Length - 1);

            for (var i = 1; i <= priceTableRows.Count; i++)
            {
                rowValues[currencyCodePrefix + i] = priceTableRows[i - 1].CurrencyCode;
                rowValues[priceDescriptionPrefix + i] = priceTableRows[i - 1].Description;
                rowValues[headerPrefix + i] = priceTableRows[i - 1].Header;
                if(priceTableRows[i - 1].FinalMobileUrls != null && priceTableRows[i - 1].FinalMobileUrls.Count > 0)
                {
                    rowValues[finalMobileUrlPrefix + i] = priceTableRows[i - 1].FinalMobileUrls[0];
                }
                if (priceTableRows[i - 1].FinalUrls != null && priceTableRows[i - 1].FinalUrls.Count > 0)
                {
                    rowValues[finalUrlPrefix + i] = priceTableRows[i - 1].FinalUrls[0];
                }
                rowValues[pricePrefix + i] = priceTableRows[i - 1].Price.ToBulkString();
                rowValues[priceQualifierPrefix + i] = priceTableRows[i - 1].PriceQualifier.ToBulkString();
                rowValues[priceUnitPrefix + i] = priceTableRows[i - 1].PriceUnit.ToBulkString();
                rowValues[termsAndConditionsPrefix + i] = priceTableRows[i - 1].TermsAndConditions;
                rowValues[termsAndConditionsUrlPrefix + i] = priceTableRows[i - 1].TermsAndConditionsUrl;
            }
        }
    }
}

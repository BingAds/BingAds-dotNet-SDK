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

using Microsoft.BingAds.V13.Bulk;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V13.Internal.Bulk
{
    internal static class MappingExtensions
    {
        public static void ConvertToEntity<T>(this RowValues values, T entity, IEnumerable<IBulkMapping<T>> mappings)
        {
            foreach (var mapping in mappings)
            {
                try
                {
                    mapping.ConvertToEntity(values, entity);
                }
                catch (InvalidCastException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (FormatException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (OverflowException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (ArgumentNullException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (ArgumentException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
                catch (NullReferenceException ex)
                {
                    throw CreateEntityParsingException(values, mapping, ex);
                }
            }
        }

        private static Exception CreateEntityParsingException<T>(RowValues values, IBulkMapping<T> mapping, Exception ex)
        {
            var entityType = typeof (T).Name;

            var simpleMapping = mapping as SimpleBulkMapping<T>;

            var message = simpleMapping != null
                ? string.Format("Couldn't parse column {0} of {1} entity: {2}", simpleMapping.CsvHeader, entityType,
                    ex.Message)
                : string.Format("Couldn't parse {0} entity: {1}", entityType, ex.Message);

            message += " See ColumnValues for detailed row information and InnerException for error details.";

            return new EntityReadException(message, values.ToDebugString(), ex);
        }

        public static void ConvertToValues<T>(this T entity, RowValues values, IEnumerable<IBulkMapping<T>> mappings)
        {
            foreach (var mapping in mappings)
            {
                try
                {
                    mapping.ConvertToCsv(entity, values);
                }
                catch (ArgumentException ex)
                {
                    throw CreateEntityWriteException(mapping, ex);
                }
                catch (NullReferenceException ex)
                {
                    throw CreateEntityWriteException(mapping, ex);
                }
            }
        }

        private static Exception CreateEntityWriteException<T>(IBulkMapping<T> mapping, Exception ex)
        {
            var entityType = typeof (T).Name;

            var simpleMapping = mapping as SimpleBulkMapping<T>;

            var message = simpleMapping != null
                ? string.Format("Couldn't write column {0} of {1} entity: {2}", simpleMapping.CsvHeader, entityType,
                    ex.Message)
                : string.Format("Couldn't write {0} entity: {1}", entityType, ex.Message);

            message += " See InnerException for error details.";

            return new EntityWriteException(message, ex);
        }
    }
}

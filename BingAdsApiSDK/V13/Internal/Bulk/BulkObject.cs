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

using Microsoft.BingAds.V13.Internal.Bulk.Entities;

namespace Microsoft.BingAds.V13.Internal.Bulk
{
    /// <summary>
    /// The abstract base class for all bulk objects that can be read and written in a file 
    /// that conforms to the Bing Ad Bulk File Schema. 
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bulk File Schema</see>.
    /// </summary>
    public abstract class BulkObject
    {
        /// <summary>
        /// Read object data from a single row.                
        /// </summary>
        /// <example>
        /// SingleLineBulkEntity: reads entity fields.
        /// BulkError: reads error fields.
        /// BulkEntityIdentifier: reads identifier fields (Id, status etc.).
        /// </example>
        /// <param name="values"></param>
        internal virtual void ReadFromRowValues(RowValues values)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Writes object data to a single row.        
        /// </summary>
        /// <example>
        /// SingleLineBulkEntity: writes entity fields.
        /// BulkEntityIdentifier: writes identifier fields (Id, status etc.)
        /// </example>
        /// <param name="values"></param>
        /// <param name="excludeReadonlyData"></param>
        internal virtual void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Reads object data from consecutive rows.        
        /// </summary>
        /// <example>
        /// SingleLineBulkEntity: reads entity errors.
        /// MultilineBulkEntity: reads child entities.
        /// </example>
        /// <param name="reader"></param>
        internal virtual void ReadRelatedData(IBulkRecordReader reader) { }

        /// <summary>
        /// Writes object data to consecutive rows.        
        /// </summary>
        /// <example>
        /// SingleLineBulkEntity: writes entity.
        /// MultilineBulkEntity: writes child entities.
        /// BulkEntityIdentifier: writes identifier information (Id, status etc.)
        /// </example>
        /// <param name="rowWriter"></param>
        /// <param name="excludeReadonlyData"></param>
        internal virtual void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns true if the entity is part of multiline entity, false otherwise
        /// </summary>
        /// <example>
        /// BulkSiteLinkAdExtension: returns true
        /// BulkCampaignTarget: returns true
        /// BulkAdGroup: returns false
        /// BulkKeyword: returns false
        /// </example>
        internal virtual bool CanEncloseInMultilineEntity
        {
            get { return false; }
        }

        /// <summary>
        /// Creates a multiline entity containing this entity
        /// </summary>
        /// <example>
        /// BulkSiteLink: returns BulkSiteLinkAdExtension containing this BulkSiteLink
        /// BulkCampaignAgeTargetBid: return BulkCampaignTarget containing this BulkCampaignAgeTargetBid
        /// </example>
        internal virtual MultiRecordBulkEntity EncloseInMultilineEntity()
        {
            throw new NotSupportedException();
        }
    }
}

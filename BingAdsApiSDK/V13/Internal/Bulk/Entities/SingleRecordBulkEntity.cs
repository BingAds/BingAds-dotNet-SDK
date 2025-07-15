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

using Microsoft.BingAds.V13.Bulk.Entities;
using Microsoft.BingAds.V13.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.V13.Internal.Bulk.Entities
{
    /// <summary>
    /// Bulk entity that has its data in a single record within the bulk file.
    /// For example, <see cref="BulkCampaign"/> and <see cref="BulkSiteLink"/> are single record bulk entities.
    /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bulk File Schema</see>.
    /// </summary>
    public abstract class SingleRecordBulkEntity : BulkEntity
    {
        /// <summary>
        /// Used to associate records in the bulk upload file with records in the results file. 
        /// The value of this field is not used or stored by the server; it is simply copied from the uploaded record to the corresponding result record. 
        /// It may be any valid string to up 100 in length.
        /// Corresponds to the 'Client Id' field in the bulk file. 
        /// </summary>
        public string ClientId { get; set; }

        private DateTime? _lastModifiedTime;

        /// <summary>
        /// Gets the last modified time for the entity.
        /// </summary>
        public override DateTime? LastModifiedTime
        {
            get { return _lastModifiedTime; }                        
        }

        /// <summary>
        /// A read only list of <see cref="BulkError"/> details in a seperate bulk record that corresponds to the record of a <see cref="BulkEntity"/> derived instance. 
        /// For more information, see <see href="https://go.microsoft.com/fwlink/?linkid=846127">Bulk File Schema</see>.
        /// </summary>
        public IReadOnlyList<BulkError> Errors { get; private set; }

        /// <summary>
        /// Indicates whether or not the Errors property is null or empty. 
        /// If true, Errors contains the details of one or more <see cref="BulkError"/> objects. 
        /// </summary>
        public override bool HasErrors
        {
            get { return Errors != null && Errors.Count > 0; }
        }

        /// <summary>
        /// Mappings shared by all single line entities
        /// </summary>
        private static readonly IBulkMapping<SingleRecordBulkEntity>[] Mappings =
        {
            new SimpleBulkMapping<SingleRecordBulkEntity>(StringTable.ClientId,
                c => c.ClientId,
                (v, c) => c.ClientId = v
            ),

            new SimpleBulkMapping<SingleRecordBulkEntity>(StringTable.LastModifiedTime,
                c => c.LastModifiedTime.ToBulkString(),
                (v, c) => c._lastModifiedTime = v.ParseOptionalDateTime()
            )
        };

        /// <summary>
        /// Reads common mappings and calls abstract method to read entity-specific mappings. This is done through abstract method to avoid having to do base.ReadFromRowValues in each child.
        /// </summary>
        /// <param name="values">CSV row values</param>
        internal override void ReadFromRowValues(RowValues values)
        {
            values.ConvertToEntity(this, Mappings);

            ProcessMappingsFromRowValues(values);
        }

        /// <summary>
        /// Writes common mappings and calls abstract method to read entity-specific mappings. This is done through abstract method to avoid having to do base.WriteToRowValues in each child.
        /// </summary>
        /// <param name="values">CSV row values</param>
        /// <param name="excludeReadonlyData"></param>
        internal override void WriteToRowValues(RowValues values, bool excludeReadonlyData)
        {            
            this.ConvertToValues(values, Mappings);

            ProcessMappingsToRowValues(values, excludeReadonlyData);
        }

        /// <summary>
        /// Reads additional data for entity (if any) and errors
        /// </summary>
        /// <param name="reader">Reader object, allowing to read consecutive bulk rows</param>
        internal sealed override void ReadRelatedData(IBulkRecordReader reader)
        {
            ReadAdditionalData(reader);

            ReadErrors(reader);
        }

        /// <summary>
        /// Writes entity data to bulk file
        /// </summary>
        /// <param name="rowWriter">Writer object, allowing to write consecutive bulk rows</param>
        /// <param name="excludeReadonlyData"></param>
        internal override void WriteToStream(IBulkObjectWriter rowWriter, bool excludeReadonlyData)
        {
            rowWriter.WriteObjectRow(this, excludeReadonlyData);

            if (!excludeReadonlyData)
            {
                WriteAdditionalData(rowWriter);

                WriteErrors(rowWriter);
            }
        } 

        /// <summary>
        /// Process specific entity mappings to CSV values. Must be implemented by each entity
        /// </summary>
        /// <param name="values">Row values</param>
        /// <param name="excludeReadonlyData"></param>
        internal abstract void ProcessMappingsToRowValues(RowValues values, bool excludeReadonlyData);

        /// <summary>
        /// Process specific entity mappings from CSV values. Must be implemented by each entity
        /// </summary>
        /// <param name="values">Row values</param>
        internal abstract void ProcessMappingsFromRowValues(RowValues values);

        /// <summary>
        /// Reads additional data for entity if any.
        /// </summary>
        /// <param name="reader">Reader object, allowing to read consecutive bulk rows</param>
        internal virtual void ReadAdditionalData(IBulkRecordReader reader) { }

        internal virtual void WriteAdditionalData(IBulkObjectWriter writer) { }

        /// <summary>
        /// Reads errors immediately after the current row
        /// </summary>
        /// <param name="reader">Reader object, allowing to read consecutive bulk rows</param>
        /// <remarks>
        /// No checks are made for the error type. It's assumed that an entity row can only be followed by errors of the same type
        /// </remarks>
        private void ReadErrors(IBulkRecordReader reader)
        {
            var errors = new List<BulkError>();

            BulkError error;

            while (reader.TryRead(out error))
            {
                errors.Add(error);
            }

            Errors = errors;
        }

        private void WriteErrors(IBulkObjectWriter writer)
        {
            if (HasErrors)
            {
                foreach (var bulkError in Errors)
                {
                    writer.WriteObjectRow(bulkError);                    
                }
            }
        }
    }
}

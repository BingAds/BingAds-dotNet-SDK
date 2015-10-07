using System;
using System.Collections.Generic;
using Microsoft.BingAds.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Mappings;

namespace Microsoft.BingAds.Internal.Bulk.Entities
{
    /// <summary>
    /// Bulk entity that has its data in a single record within the bulk file.
    /// For example, <see cref="BulkCampaign"/> and <see cref="BulkSiteLink"/> are single record bulk entities.
    /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.
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
        /// For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511639">Bulk File Schema</see>.
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
                (v, c) => c._lastModifiedTime = v.ParseOptional<DateTime>()
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
        internal sealed override void ReadRelatedDataFromStream(IBulkStreamReader reader)
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
        internal virtual void ReadAdditionalData(IBulkStreamReader reader) { }

        internal virtual void WriteAdditionalData(IBulkObjectWriter writer) { }

        /// <summary>
        /// Reads errors immediately after the current row
        /// </summary>
        /// <param name="reader">Reader object, allowing to read consecutive bulk rows</param>
        /// <remarks>
        /// No checks are made for the error type. It's assumed that an entity row can only be followed by errors of the same type
        /// </remarks>
        private void ReadErrors(IBulkStreamReader reader)
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

using System;
using System.Runtime.Serialization;

namespace Microsoft.BingAds.V10.Bulk
{    
    /// <summary>
    /// This exception is thrown when attempting to read entities from a bulk file using <see cref="BulkFileReader.ReadEntities"/>.
    /// </summary>
    [Serializable]
    public class EntityReadException : Exception
    {
        /// <summary>
        /// The comma seperated column value of the record that was read.
        /// </summary>
        public string ColumnValues { get; private set; }

        /// <summary>
        /// Initializes a new instance of the EntityReadException class.
        /// </summary>
        public EntityReadException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the EntityReadException class with the specified message and column values.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="columnValues">The column values.</param>
        public EntityReadException(string message, string columnValues) : base(message)
        {
            ColumnValues = columnValues;
        }

        /// <summary>
        /// Initializes a new instance of the EntityReadException class with the specified message, column values, and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="columnValues">The column values.</param>
        /// <param name="inner">The details of the exception from the bulk service operation.</param>
        public EntityReadException(string message, string columnValues, Exception inner) : base(message, inner)
        {
            ColumnValues = columnValues;
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        /// <param name="info">Reserved for internal use.</param>
        /// <param name="context">Reserved for internal use.</param>
        protected EntityReadException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
            ColumnValues = (string)info.GetValue("ColumnValues", typeof(string));
        }

        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ColumnValues", ColumnValues);
        }
    }
}

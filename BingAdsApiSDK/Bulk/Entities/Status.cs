namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// Provides possible association status values for bulk entities. 
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The bulk entity is associated with the entity identified by the bulk record's 'Parent Id'.
        /// </summary>
        Active,

        /// <summary>
        /// The bulk entity is not associated with the entity identified by the bulk record's 'Parent Id'. 
        /// In a bulk upload file, you can use this value to delete the association of an entity from the parent entity.
        /// </summary>
        Deleted
    }
}

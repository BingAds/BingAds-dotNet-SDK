using Microsoft.BingAds.Internal.Bulk.Entities;
// ReSharper disable once CheckNamespace


namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// Defines the possibile values for a geographical location sub type.
    /// </summary>
    /// <seealso cref="BulkLocationTarget{TBid}"/>
    /// <seealso cref="BulkLocationTargetBid"/>
    /// <seealso cref="BulkNegativeLocationTarget{TBid}"/>
    /// <seealso cref="BulkNegativeLocationTargetBid"/>
    public enum LocationTargetType
    {
        /// <summary>
        /// The location represents a postal code.
        /// </summary>
        PostalCode,
        /// <summary>
        /// The location represents a city.
        /// </summary>
        City,
        /// <summary>
        /// The location represents a metro area.
        /// </summary>
        MetroArea,
        /// <summary>
        /// The location represents a state or province.
        /// </summary>
        State,
        /// <summary>
        /// The location represents a country.
        /// </summary>
        Country
    }
}

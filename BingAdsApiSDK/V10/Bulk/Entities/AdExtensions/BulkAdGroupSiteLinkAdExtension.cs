using Microsoft.BingAds.V10.Internal.Bulk.Entities;
using Microsoft.BingAds.V10.Internal.Bulk.Entities.AdExtensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.V10.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an ad group level sitelink ad extension. 
    /// This class exposes properties that can be read and written 
    /// as fields of the Ad Group Sitelink Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=620262">Ad Group Sitelink Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupSiteLinkAdExtension : BulkAdGroupAdExtensionAssociation
    {
    }
}

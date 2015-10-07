using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Entities.AdExtensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents a ad group level app ad extension. 
    /// This class exposes properties that can be read and written 
    /// as fields of the Ad Group App Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=515902">Ad Group App Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupAppAdExtension : BulkAdGroupAdExtensionAssociation
    {
    }
}

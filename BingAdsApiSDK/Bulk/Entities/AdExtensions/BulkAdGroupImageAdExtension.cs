
using Microsoft.BingAds.Internal.Bulk.Entities;
using Microsoft.BingAds.Internal.Bulk.Entities.AdExtensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.BingAds.Bulk.Entities
{
    /// <summary>
    /// <para>
    /// Represents an ad group level image ad extension. 
    /// This class exposes properties that can be read and written 
    /// as fields of the Ad Group Image Ad Extension record in a bulk file. 
    /// </para>
    /// <para>For more information, see <see href="http://go.microsoft.com/fwlink/?LinkID=511551">Ad Group Image Ad Extension</see>. </para>
    /// </summary>
    /// <seealso cref="BulkServiceManager"/>
    /// <seealso cref="BulkOperation{TStatus}"/>
    /// <seealso cref="BulkFileReader"/>
    /// <seealso cref="BulkFileWriter"/>
    public class BulkAdGroupImageAdExtension : BulkAdGroupAdExtensionAssociation
    {
    }
}

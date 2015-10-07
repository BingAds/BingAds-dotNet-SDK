using System.Threading.Tasks;
using Microsoft.BingAds.V10.Bulk;

namespace Microsoft.BingAds.V10.Internal.Bulk.Operations
{
    internal interface IBulkOperationTracker<TStatus>
    {
        Task<BulkOperationStatus<TStatus>> TrackResultFileAsync();
    }
}
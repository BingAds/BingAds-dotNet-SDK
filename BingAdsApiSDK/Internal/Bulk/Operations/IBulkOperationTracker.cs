using System.Threading.Tasks;
using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk.Operations
{
    internal interface IBulkOperationTracker<TStatus>
    {
        Task<BulkOperationStatus<TStatus>> TrackResultFileAsync();
    }
}
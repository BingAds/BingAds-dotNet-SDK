
using System.Threading.Tasks;
using Microsoft.BingAds.Bulk;

namespace Microsoft.BingAds.Internal.Bulk.Operations
{
    internal interface IBulkOperationStatusProvider<TStatus>
    {
        Task<BulkOperationStatus<TStatus>> GetCurrentStatus(ServiceClient<IBulkService> bulkServiceClient);

        bool IsFinalStatus(BulkOperationStatus<TStatus> status);

        bool IsSuccessStatus(TStatus status);
    }
}

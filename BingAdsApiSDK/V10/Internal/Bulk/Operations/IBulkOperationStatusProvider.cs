
using System.Threading.Tasks;
using Microsoft.BingAds.V10.Bulk;

namespace Microsoft.BingAds.V10.Internal.Bulk.Operations
{
    internal interface IBulkOperationStatusProvider<TStatus>
    {
        Task<BulkOperationStatus<TStatus>> GetCurrentStatus(ServiceClient<IBulkService> bulkServiceClient);

        bool IsFinalStatus(BulkOperationStatus<TStatus> status);

        bool IsSuccessStatus(TStatus status);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V11.CustomerBilling;

namespace BingAdsExamplesLibrary.V11
{
    public class CustomerBillingExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | CustomerBilling V11"; }
        }
        public ServiceClient<ICustomerBillingService> CustomerBillingService;
        public CustomerBillingExampleHelper(SendMessageDelegate OutputStatusMessageDefault)
        {
            OutputStatusMessage = OutputStatusMessageDefault;
        }
        public async Task<AddInsertionOrderResponse> AddInsertionOrderAsync(
            InsertionOrder insertionOrder)
        {
            var request = new AddInsertionOrderRequest
            {
                InsertionOrder = insertionOrder
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.AddInsertionOrderAsync(r), request));
        }
        public async Task<GetAccountMonthlySpendResponse> GetAccountMonthlySpendAsync(
            long accountId,
            DateTime monthYear)
        {
            var request = new GetAccountMonthlySpendRequest
            {
                AccountId = accountId,
                MonthYear = monthYear
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.GetAccountMonthlySpendAsync(r), request));
        }
        public async Task<GetBillingDocumentsResponse> GetBillingDocumentsAsync(
            IList<long> documentIds,
            DataType type)
        {
            var request = new GetBillingDocumentsRequest
            {
                DocumentIds = documentIds,
                Type = type
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.GetBillingDocumentsAsync(r), request));
        }
        public async Task<GetBillingDocumentsInfoResponse> GetBillingDocumentsInfoAsync(
            IList<long> accountIds,
            DateTime startDate,
            DateTime? endDate)
        {
            var request = new GetBillingDocumentsInfoRequest
            {
                AccountIds = accountIds,
                StartDate = startDate,
                EndDate = endDate
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.GetBillingDocumentsInfoAsync(r), request));
        }
        public async Task<GetInsertionOrdersByAccountResponse> GetInsertionOrdersByAccountAsync(
            long accountId,
            IList<long> insertionOrderIds)
        {
            var request = new GetInsertionOrdersByAccountRequest
            {
                AccountId = accountId,
                InsertionOrderIds = insertionOrderIds
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.GetInsertionOrdersByAccountAsync(r), request));
        }
        public async Task<SearchInsertionOrdersResponse> SearchInsertionOrdersAsync(
            IList<Predicate> predicates,
            IList<OrderBy> ordering,
            Paging pageInfo)
        {
            var request = new SearchInsertionOrdersRequest
            {
                Predicates = predicates,
                Ordering = ordering,
                PageInfo = pageInfo
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.SearchInsertionOrdersAsync(r), request));
        }
        public async Task<UpdateInsertionOrderResponse> UpdateInsertionOrderAsync(
            InsertionOrder insertionOrder)
        {
            var request = new UpdateInsertionOrderRequest
            {
                InsertionOrder = insertionOrder
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.UpdateInsertionOrderAsync(r), request));
        }
        public void OutputAdApiError(AdApiError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfAdApiError(dataObject.Errors);
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputAdApiFaultDetail(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApiBatchFault(ApiBatchFault dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfBatchError(dataObject.BatchErrors);
            }
        }
        public void OutputArrayOfApiBatchFault(IList<ApiBatchFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApiBatchFault(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApiFault(ApiFault dataObject)
        {
            if (null != dataObject)
            {
                OutputArrayOfOperationError(dataObject.OperationErrors);
                var apibatchfault = dataObject as ApiBatchFault;
                if(apibatchfault != null)
                {
                    OutputApiBatchFault((ApiBatchFault)dataObject);
                }
            }
        }
        public void OutputArrayOfApiFault(IList<ApiFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApiFault(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(adapifaultdetail != null)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifault = dataObject as ApiFault;
                if(apifault != null)
                {
                    OutputApiFault((ApiFault)dataObject);
                }
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputApplicationFault(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBatchError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBillingDocument(BillingDocument dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Data: {0}", dataObject.Data));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
            }
        }
        public void OutputArrayOfBillingDocument(IList<BillingDocument> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBillingDocument(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputBillingDocumentInfo(BillingDocumentInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("AccountName: {0}", dataObject.AccountName));
                OutputStatusMessage(string.Format("AccountNumber: {0}", dataObject.AccountNumber));
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("DocumentDate: {0}", dataObject.DocumentDate));
                OutputStatusMessage(string.Format("DocumentId: {0}", dataObject.DocumentId));
            }
        }
        public void OutputArrayOfBillingDocumentInfo(IList<BillingDocumentInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputBillingDocumentInfo(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputInsertionOrder(InsertionOrder dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("BalanceAmount: {0}", dataObject.BalanceAmount));
                OutputStatusMessage(string.Format("BookingCountryCode: {0}", dataObject.BookingCountryCode));
                OutputStatusMessage(string.Format("Comment: {0}", dataObject.Comment));
                OutputStatusMessage(string.Format("EndDate: {0}", dataObject.EndDate));
                OutputStatusMessage(string.Format("InsertionOrderId: {0}", dataObject.InsertionOrderId));
                OutputStatusMessage(string.Format("LastModifiedByUserId: {0}", dataObject.LastModifiedByUserId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", dataObject.LastModifiedTime));
                OutputStatusMessage(string.Format("NotificationThreshold: {0}", dataObject.NotificationThreshold));
                OutputStatusMessage(string.Format("ReferenceId: {0}", dataObject.ReferenceId));
                OutputStatusMessage(string.Format("SpendCapAmount: {0}", dataObject.SpendCapAmount));
                OutputStatusMessage(string.Format("StartDate: {0}", dataObject.StartDate));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("PurchaseOrder: {0}", dataObject.PurchaseOrder));
                OutputStatusMessage(string.Format("ChangePendingReview: {0}", dataObject.ChangePendingReview));
            }
        }
        public void OutputArrayOfInsertionOrder(IList<InsertionOrder> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputInsertionOrder(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOperationError(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputOrderBy(OrderBy dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Field: {0}", dataObject.Field));
                OutputStatusMessage(string.Format("Order: {0}", dataObject.Order));
            }
        }
        public void OutputArrayOfOrderBy(IList<OrderBy> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputOrderBy(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPaging(Paging dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Size: {0}", dataObject.Size));
            }
        }
        public void OutputArrayOfPaging(IList<Paging> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPaging(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputPredicate(Predicate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage(string.Format("Field: {0}", dataObject.Field));
                OutputStatusMessage(string.Format("Operator: {0}", dataObject.Operator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfPredicate(IList<Predicate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputPredicate(dataObject);
                    OutputStatusMessage("\n");
                }
            }
        }
        public void OutputDataType(DataType valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(DataType)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfDataType(IList<DataType> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputDataType(valueSet);
                }
            }
        }
        public void OutputInsertionOrderStatus(InsertionOrderStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(InsertionOrderStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfInsertionOrderStatus(IList<InsertionOrderStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputInsertionOrderStatus(valueSet);
                }
            }
        }
        public void OutputPredicateOperator(PredicateOperator valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(PredicateOperator)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfPredicateOperator(IList<PredicateOperator> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputPredicateOperator(valueSet);
                }
            }
        }
        public void OutputOrderByField(OrderByField valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(OrderByField)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfOrderByField(IList<OrderByField> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputOrderByField(valueSet);
                }
            }
        }
        public void OutputSortOrder(SortOrder valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(SortOrder)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfSortOrder(IList<SortOrder> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputSortOrder(valueSet);
                }
            }
        }
        public void OutputArrayOfString(IList<string> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the string: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the long: {0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable long: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the int: {0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("Value of the nillable int: {0}", item));
                }
            }
        }
        public void OutputKeyValuePairOfstringstring(KeyValuePair<string,string> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringstring(IList<KeyValuePair<string,string>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringstring(dataObject);
                }
            }
        }
    }
}
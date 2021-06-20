using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V13.CustomerBilling;

namespace BingAdsExamplesLibrary.V13
{
    public class CustomerBillingExampleHelper : BingAdsExamplesLibrary.ExampleBase
    {
        public override string Description
        {
            get { return "Sample Helper | CustomerBilling V13"; }
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
        public async Task<DispatchCouponsResponse> DispatchCouponsAsync(
            IList<String> sendToEmails,
            long customerId,
            String couponClassName)
        {
            var request = new DispatchCouponsRequest
            {
                SendToEmails = sendToEmails,
                CustomerId = customerId,
                CouponClassName = couponClassName
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.DispatchCouponsAsync(r), request));
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
            IList<BillingDocumentInfo> billingDocumentsInfo,
            DataType type)
        {
            var request = new GetBillingDocumentsRequest
            {
                BillingDocumentsInfo = billingDocumentsInfo,
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
        public async Task<RedeemCouponResponse> RedeemCouponAsync(
            long accountId,
            String couponCode)
        {
            var request = new RedeemCouponRequest
            {
                AccountId = accountId,
                CouponCode = couponCode
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.RedeemCouponAsync(r), request));
        }
        public async Task<SearchCouponsResponse> SearchCouponsAsync(
            IList<Predicate> predicates,
            IList<OrderBy> ordering,
            Paging pageInfo)
        {
            var request = new SearchCouponsRequest
            {
                Predicates = predicates,
                Ordering = ordering,
                PageInfo = pageInfo
            };

            return (await CustomerBillingService.CallAsync((s, r) => s.SearchCouponsAsync(r), request));
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
                OutputStatusMessage("* * * Begin OutputAdApiError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Detail: {0}", dataObject.Detail));
                OutputStatusMessage(string.Format("ErrorCode: {0}", dataObject.ErrorCode));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputAdApiError * * *");
            }
        }
        public void OutputArrayOfAdApiError(IList<AdApiError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiError(dataObject);
                    }
                }
            }
        }
        public void OutputAdApiFaultDetail(AdApiFaultDetail dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputAdApiFaultDetail * * *");
                OutputStatusMessage("Errors:");
                OutputArrayOfAdApiError(dataObject.Errors);
                OutputStatusMessage("* * * End OutputAdApiFaultDetail * * *");
            }
        }
        public void OutputArrayOfAdApiFaultDetail(IList<AdApiFaultDetail> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputAdApiFaultDetail(dataObject);
                    }
                }
            }
        }
        public void OutputApiBatchFault(ApiBatchFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApiBatchFault * * *");
                OutputStatusMessage("BatchErrors:");
                OutputArrayOfBatchError(dataObject.BatchErrors);
                OutputStatusMessage("* * * End OutputApiBatchFault * * *");
            }
        }
        public void OutputArrayOfApiBatchFault(IList<ApiBatchFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApiBatchFault(dataObject);
                    }
                }
            }
        }
        public void OutputApiFault(ApiFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApiFault * * *");
                OutputStatusMessage("OperationErrors:");
                OutputArrayOfOperationError(dataObject.OperationErrors);
                var apibatchfault = dataObject as ApiBatchFault;
                if(null != apibatchfault)
                {
                    OutputApiBatchFault((ApiBatchFault)dataObject);
                }
                OutputStatusMessage("* * * End OutputApiFault * * *");
            }
        }
        public void OutputArrayOfApiFault(IList<ApiFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApiFault(dataObject);
                    }
                }
            }
        }
        public void OutputApplicationFault(ApplicationFault dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputApplicationFault * * *");
                OutputStatusMessage(string.Format("TrackingId: {0}", dataObject.TrackingId));
                var adapifaultdetail = dataObject as AdApiFaultDetail;
                if(null != adapifaultdetail)
                {
                    OutputAdApiFaultDetail((AdApiFaultDetail)dataObject);
                }
                var apifault = dataObject as ApiFault;
                if(null != apifault)
                {
                    OutputApiFault((ApiFault)dataObject);
                }
                OutputStatusMessage("* * * End OutputApplicationFault * * *");
            }
        }
        public void OutputArrayOfApplicationFault(IList<ApplicationFault> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputApplicationFault(dataObject);
                    }
                }
            }
        }
        public void OutputBatchError(BatchError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBatchError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputBatchError * * *");
            }
        }
        public void OutputArrayOfBatchError(IList<BatchError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBatchError(dataObject);
                    }
                }
            }
        }
        public void OutputBillingDocument(BillingDocument dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBillingDocument * * *");
                OutputStatusMessage(string.Format("Data: {0}", dataObject.Data));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("Type: {0}", dataObject.Type));
                OutputStatusMessage("* * * End OutputBillingDocument * * *");
            }
        }
        public void OutputArrayOfBillingDocument(IList<BillingDocument> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBillingDocument(dataObject);
                    }
                }
            }
        }
        public void OutputBillingDocumentInfo(BillingDocumentInfo dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputBillingDocumentInfo * * *");
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("AccountName: {0}", dataObject.AccountName));
                OutputStatusMessage(string.Format("AccountNumber: {0}", dataObject.AccountNumber));
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("DocumentDate: {0}", dataObject.DocumentDate));
                OutputStatusMessage(string.Format("DocumentId: {0}", dataObject.DocumentId));
                OutputStatusMessage(string.Format("CustomerId: {0}", dataObject.CustomerId));
                OutputStatusMessage("* * * End OutputBillingDocumentInfo * * *");
            }
        }
        public void OutputArrayOfBillingDocumentInfo(IList<BillingDocumentInfo> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputBillingDocumentInfo(dataObject);
                    }
                }
            }
        }
        public void OutputCoupon(Coupon dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCoupon * * *");
                OutputStatusMessage(string.Format("CouponCode: {0}", dataObject.CouponCode));
                OutputStatusMessage(string.Format("ClassName: {0}", dataObject.ClassName));
                OutputStatusMessage(string.Format("CouponType: {0}", dataObject.CouponType));
                OutputStatusMessage(string.Format("Amount: {0}", dataObject.Amount));
                OutputStatusMessage(string.Format("SpendThreshold: {0}", dataObject.SpendThreshold));
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("PercentOff: {0}", dataObject.PercentOff));
                OutputStatusMessage(string.Format("ActiveDuration: {0}", dataObject.ActiveDuration));
                OutputStatusMessage(string.Format("ExpirationDate: {0}", dataObject.ExpirationDate));
                OutputStatusMessage(string.Format("StartDate: {0}", dataObject.StartDate));
                OutputStatusMessage(string.Format("EndDate: {0}", dataObject.EndDate));
                OutputStatusMessage(string.Format("SendToEmail: {0}", dataObject.SendToEmail));
                OutputStatusMessage(string.Format("SendToDate: {0}", dataObject.SendToDate));
                OutputStatusMessage(string.Format("IsRedeemed: {0}", dataObject.IsRedeemed));
                OutputStatusMessage("RedemptionInfo:");
                OutputCouponRedemption(dataObject.RedemptionInfo);
                OutputStatusMessage("* * * End OutputCoupon * * *");
            }
        }
        public void OutputArrayOfCoupon(IList<Coupon> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCoupon(dataObject);
                    }
                }
            }
        }
        public void OutputCouponRedemption(CouponRedemption dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputCouponRedemption * * *");
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("AccountNumber: {0}", dataObject.AccountNumber));
                OutputStatusMessage(string.Format("SpendToThreshold: {0}", dataObject.SpendToThreshold));
                OutputStatusMessage(string.Format("Balance: {0}", dataObject.Balance));
                OutputStatusMessage(string.Format("CurrencyCode: {0}", dataObject.CurrencyCode));
                OutputStatusMessage(string.Format("RedemptionDate: {0}", dataObject.RedemptionDate));
                OutputStatusMessage(string.Format("ExpirationDate: {0}", dataObject.ExpirationDate));
                OutputStatusMessage(string.Format("ActivationDate: {0}", dataObject.ActivationDate));
                OutputStatusMessage("* * * End OutputCouponRedemption * * *");
            }
        }
        public void OutputArrayOfCouponRedemption(IList<CouponRedemption> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputCouponRedemption(dataObject);
                    }
                }
            }
        }
        public void OutputInsertionOrder(InsertionOrder dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputInsertionOrder * * *");
                OutputStatusMessage(string.Format("AccountId: {0}", dataObject.AccountId));
                OutputStatusMessage(string.Format("BookingCountryCode: {0}", dataObject.BookingCountryCode));
                OutputStatusMessage(string.Format("Comment: {0}", dataObject.Comment));
                OutputStatusMessage(string.Format("EndDate: {0}", dataObject.EndDate));
                OutputStatusMessage(string.Format("Id: {0}", dataObject.Id));
                OutputStatusMessage(string.Format("LastModifiedByUserId: {0}", dataObject.LastModifiedByUserId));
                OutputStatusMessage(string.Format("LastModifiedTime: {0}", dataObject.LastModifiedTime));
                OutputStatusMessage(string.Format("NotificationThreshold: {0}", dataObject.NotificationThreshold));
                OutputStatusMessage(string.Format("ReferenceId: {0}", dataObject.ReferenceId));
                OutputStatusMessage(string.Format("SpendCapAmount: {0}", dataObject.SpendCapAmount));
                OutputStatusMessage(string.Format("StartDate: {0}", dataObject.StartDate));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("Status: {0}", dataObject.Status));
                OutputStatusMessage(string.Format("PurchaseOrder: {0}", dataObject.PurchaseOrder));
                OutputStatusMessage("PendingChanges:");
                OutputInsertionOrderPendingChanges(dataObject.PendingChanges);
                OutputStatusMessage(string.Format("AccountNumber: {0}", dataObject.AccountNumber));
                OutputStatusMessage(string.Format("BudgetRemaining: {0}", dataObject.BudgetRemaining));
                OutputStatusMessage(string.Format("BudgetSpent: {0}", dataObject.BudgetSpent));
                OutputStatusMessage(string.Format("BudgetRemainingPercent: {0}", dataObject.BudgetRemainingPercent));
                OutputStatusMessage(string.Format("BudgetSpentPercent: {0}", dataObject.BudgetSpentPercent));
                OutputStatusMessage(string.Format("SeriesName: {0}", dataObject.SeriesName));
                OutputStatusMessage(string.Format("IsInSeries: {0}", dataObject.IsInSeries));
                OutputStatusMessage(string.Format("SeriesFrequencyType: {0}", dataObject.SeriesFrequencyType));
                OutputStatusMessage("* * * End OutputInsertionOrder * * *");
            }
        }
        public void OutputArrayOfInsertionOrder(IList<InsertionOrder> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputInsertionOrder(dataObject);
                    }
                }
            }
        }
        public void OutputInsertionOrderPendingChanges(InsertionOrderPendingChanges dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputInsertionOrderPendingChanges * * *");
                OutputStatusMessage(string.Format("Comment: {0}", dataObject.Comment));
                OutputStatusMessage(string.Format("EndDate: {0}", dataObject.EndDate));
                OutputStatusMessage(string.Format("RequestedByUserId: {0}", dataObject.RequestedByUserId));
                OutputStatusMessage(string.Format("ModifiedDateTime: {0}", dataObject.ModifiedDateTime));
                OutputStatusMessage(string.Format("NotificationThreshold: {0}", dataObject.NotificationThreshold));
                OutputStatusMessage(string.Format("ReferenceId: {0}", dataObject.ReferenceId));
                OutputStatusMessage(string.Format("SpendCapAmount: {0}", dataObject.SpendCapAmount));
                OutputStatusMessage(string.Format("StartDate: {0}", dataObject.StartDate));
                OutputStatusMessage(string.Format("Name: {0}", dataObject.Name));
                OutputStatusMessage(string.Format("PurchaseOrder: {0}", dataObject.PurchaseOrder));
                OutputStatusMessage(string.Format("ChangeStatus: {0}", dataObject.ChangeStatus));
                OutputStatusMessage("* * * End OutputInsertionOrderPendingChanges * * *");
            }
        }
        public void OutputArrayOfInsertionOrderPendingChanges(IList<InsertionOrderPendingChanges> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputInsertionOrderPendingChanges(dataObject);
                    }
                }
            }
        }
        public void OutputOperationError(OperationError dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOperationError * * *");
                OutputStatusMessage(string.Format("Code: {0}", dataObject.Code));
                OutputStatusMessage(string.Format("Details: {0}", dataObject.Details));
                OutputStatusMessage(string.Format("Message: {0}", dataObject.Message));
                OutputStatusMessage("* * * End OutputOperationError * * *");
            }
        }
        public void OutputArrayOfOperationError(IList<OperationError> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOperationError(dataObject);
                    }
                }
            }
        }
        public void OutputOrderBy(OrderBy dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputOrderBy * * *");
                OutputStatusMessage(string.Format("Field: {0}", dataObject.Field));
                OutputStatusMessage(string.Format("Order: {0}", dataObject.Order));
                OutputStatusMessage("* * * End OutputOrderBy * * *");
            }
        }
        public void OutputArrayOfOrderBy(IList<OrderBy> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputOrderBy(dataObject);
                    }
                }
            }
        }
        public void OutputPaging(Paging dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPaging * * *");
                OutputStatusMessage(string.Format("Index: {0}", dataObject.Index));
                OutputStatusMessage(string.Format("Size: {0}", dataObject.Size));
                OutputStatusMessage("* * * End OutputPaging * * *");
            }
        }
        public void OutputArrayOfPaging(IList<Paging> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPaging(dataObject);
                    }
                }
            }
        }
        public void OutputPredicate(Predicate dataObject)
        {
            if (null != dataObject)
            {
                OutputStatusMessage("* * * Begin OutputPredicate * * *");
                OutputStatusMessage(string.Format("Field: {0}", dataObject.Field));
                OutputStatusMessage(string.Format("Operator: {0}", dataObject.Operator));
                OutputStatusMessage(string.Format("Value: {0}", dataObject.Value));
                OutputStatusMessage("* * * End OutputPredicate * * *");
            }
        }
        public void OutputArrayOfPredicate(IList<Predicate> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    if (null != dataObject)
                    {
                        OutputPredicate(dataObject);
                    }
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
        public void OutputInsertionOrderPendingChangesStatus(InsertionOrderPendingChangesStatus valueSet)
        {
            OutputStatusMessage(string.Format("Values in {0}", valueSet.GetType()));
            foreach (var value in Enum.GetValues(typeof(InsertionOrderPendingChangesStatus)))
            {
                OutputStatusMessage(value.ToString());
            }
        }
        public void OutputArrayOfInsertionOrderPendingChangesStatus(IList<InsertionOrderPendingChangesStatus> valueSets)
        {
            if (null != valueSets)
            {
                foreach (var valueSet in valueSets)
                {
                    OutputInsertionOrderPendingChangesStatus(valueSet);
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
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfLong(IList<long?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
                }
            }
        }
        public void OutputArrayOfInt(IList<int?> items)
        {
            if (null != items)
            {
                foreach (var item in items)
                {
                    OutputStatusMessage(string.Format("{0}", item));
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
        public void OutputKeyValuePairOflonglong(KeyValuePair<long,long> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOflonglong(IList<KeyValuePair<long,long>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOflonglong(dataObject);
                }
            }
        }
        public void OutputKeyValuePairOfstringbase64Binary(KeyValuePair<string,byte[]> dataObject)
        {
            if (null != dataObject.Key)
            {
                OutputStatusMessage(string.Format("key: {0}", dataObject.Key));
                OutputStatusMessage(string.Format("value: {0}", dataObject.Value));
            }
        }
        public void OutputArrayOfKeyValuePairOfstringbase64Binary(IList<KeyValuePair<string,byte[]>> dataObjects)
        {
            if (null != dataObjects)
            {
                foreach (var dataObject in dataObjects)
                {
                    OutputKeyValuePairOfstringbase64Binary(dataObject);
                }
            }
        }
    }
}
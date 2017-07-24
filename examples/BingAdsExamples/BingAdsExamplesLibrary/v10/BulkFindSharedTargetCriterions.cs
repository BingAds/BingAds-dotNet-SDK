using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds;
using Microsoft.BingAds.V10.Bulk;
using Microsoft.BingAds.CustomerManagement;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// Find out whether any targets are shared for the customer.
    /// </summary>
    public class BulkFindSharedTargetCriterions : BulkExampleBase
    {
        protected static ServiceClient<ICustomerManagementService> CustomerService;

        public override string Description
        {
            get { return "Find Shared Targets | Bulk V10 to V11"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                CustomerService = new ServiceClient<ICustomerManagementService>(authorizationData);
                BulkService = new BulkServiceManager(authorizationData);

                var progress = new Progress<BulkOperationProgressInfo>(x =>
                    OutputStatusMessage(string.Format("{0} % Complete",
                        x.PercentComplete.ToString(CultureInfo.InvariantCulture))));

                Dictionary<long, List<long>> targetToEntities = new Dictionary<long, List<long>>();
                Dictionary<long, List<Dictionary<long, KeyValuePair<long, string>>>> targetToEntities2 = new Dictionary<long, List<Dictionary<long, KeyValuePair<long, string>>>>();

                var downloadParameters = new DownloadParameters
                {
                    CampaignIds = null,
                    Entities = BulkDownloadEntity.AdGroupTargets |
                           BulkDownloadEntity.CampaignTargets,
                    ResultFileDirectory = FileDirectory,
                    ResultFileName = DownloadFileName,
                    OverwriteResultFile = true,
                    LastSyncTimeInUTC = null
                };

                // Search for the Bing Ads accounts that the user can access.

                var getUserResponse = await GetUserAsync(null);
                var user = getUserResponse.User;
                var accounts = await SearchAccountsByUserIdAsync(user.Id);
                
                foreach (var account in accounts)
                {
                    var linkToUI = string.Format("https://ui.bingads.microsoft.com/Campaign/Campaigns?cid={0}&aid={1}#/customer/{0}/account/{1}/campaign",
                                        authorizationData.CustomerId,
                                        account.Id);
                    OutputStatusMessage(string.Format("Downloading targets for account {0} \n", account.Number));
                    OutputStatusMessage(linkToUI);

                    authorizationData.AccountId = (long)account.Id;
                    BulkService = new BulkServiceManager(authorizationData);
                    var downloadEntities = (await BulkService.DownloadEntitiesAsync(downloadParameters)).ToList();

                    var adGroupTargetResults = downloadEntities.OfType<Microsoft.BingAds.V10.Bulk.Entities.BulkAdGroupTarget>().ToList();
                    foreach (var entity in adGroupTargetResults)
                    {
                        MapTargetToEntity(targetToEntities, (long)entity.Target.Id, (long)entity.AdGroupId);
                        MapTargetToEntity2(
                            targetToEntities2,
                            authorizationData.AccountId,
                            (long)entity.Target.Id,
                            (long)entity.AdGroupId,
                            "AdGroup"
                        );
                    }

                    var campaignTargetResults = downloadEntities.OfType<Microsoft.BingAds.V10.Bulk.Entities.BulkCampaignTarget>().ToList();
                    foreach (var entity in campaignTargetResults)
                    {
                        MapTargetToEntity(targetToEntities, (long)entity.Target.Id, (long)entity.CampaignId);
                        MapTargetToEntity2(
                            targetToEntities2,
                            authorizationData.AccountId,
                            (long)entity.Target.Id,
                            (long)entity.CampaignId,
                            "Campaign"
                        );
                    }
                }

                OutputStatusMessage("\nView 1: By Target Id:");

                foreach (var dict in targetToEntities)
                {
                    if (dict.Value.Count() > 1)
                    {
                        OutputStatusMessage(string.Format("\nTargetId {0} is shared by the following entities:", dict.Key));
                        OutputStatusMessage(string.Join("\r\n", dict.Value.Select(id => string.Format("{0}", id))));
                    }
                }

                OutputStatusMessage("\nView 2: With Account Detail:");
                OutputStatusMessage("\nTargetId, AccountId, EntityId, EntityType");

                foreach (var targetDictionary in targetToEntities2)
                {
                    if (targetDictionary.Value.Count() > 1)
                    {
                        foreach (var accountDictionary in targetDictionary.Value)
                        {
                            foreach (var accountIdKey in accountDictionary.Keys)
                            {
                                OutputStatusMessage(
                                    string.Format("{0}, {1}, {2}, {3}",
                                    targetDictionary.Key,
                                    accountIdKey,
                                    accountDictionary[accountIdKey].Key, // EntityId
                                    accountDictionary[accountIdKey].Value // EntityType e.g. Campaign or AdGroup
                                ));
                            }
                        }
                    }
                }
            }
            // Catch Microsoft Account authorization exceptions.
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Bulk service exceptions
            catch (FaultException<Microsoft.BingAds.V10.Bulk.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (BulkOperationInProgressException ex)
            {
                OutputStatusMessage("The result file for the bulk operation is not yet available for download.");
                OutputStatusMessage(ex.Message);
            }
            catch (BulkOperationCouldNotBeCompletedException<DownloadStatus> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
            finally
            {
                if (Reader != null) { Reader.Dispose(); }
                if (Writer != null) { Writer.Dispose(); }
            }
        }

        private void MapTargetToEntity(
            Dictionary<long, List<long>> targetToEntities,
            long targetId,
            long entityId)
        {
            // Map the entity ID to the target ID only if the target ID exists 
            // and the entity ID is not already mapped.
            if (targetToEntities.ContainsKey(targetId) &&
                !targetToEntities[targetId].Contains(entityId))
            {
                targetToEntities[targetId].Add(entityId);
            }
            // Map the entity ID to the target ID since neither are yet mapped 
            else
            {
                targetToEntities.Add(targetId, new List<long> { entityId });
            }
        }

        private void MapTargetToEntity2(
            Dictionary<long, List<Dictionary<long, KeyValuePair<long, string>>>> targetToEntities,
            long accountId,
            long targetId,
            long entityId,
            string entityType)
        {
            var entityPair = new KeyValuePair<long, string>(entityId, entityType);
            Dictionary<long, KeyValuePair<long, string>> dict = new Dictionary<long, KeyValuePair<long, string>>();
            dict.Add(accountId, entityPair);
            // Map the entity ID to the target ID if the target ID exists
            if (targetToEntities.ContainsKey(targetId))
            {
                targetToEntities[targetId].Add(dict);
            }
            // Map the entity ID to the target ID since neither are yet mapped 
            else
            {
                targetToEntities.Add(targetId, new List<Dictionary<long, KeyValuePair<long, string>>> { dict });
            }
        }

        protected async Task<GetUserResponse> GetUserAsync(long? userId)
        {
            var request = new GetUserRequest
            {
                UserId = userId
            };

            return (await CustomerService.CallAsync((s, r) => s.GetUserAsync(r), request));
        }

        private async Task<Account[]> SearchAccountsByUserIdAsync(long? userId)
        {
            var predicate = new Predicate
            {
                Field = "UserId",
                Operator = PredicateOperator.Equals,
                Value = userId.ToString()
            };

            var paging = new Paging
            {
                Index = 0,
                Size = 10
            };

            var request = new SearchAccountsRequest
            {
                Ordering = null,
                PageInfo = paging,
                Predicates = new[] { predicate }
            };

            return (await CustomerService.CallAsync((s, r) => s.SearchAccountsAsync(r), request)).Accounts.ToArray();
        }

        private async Task<Account[]> SearchAccountsByAccountIdAsync(long? accountId)
        {
            var predicate = new Predicate
            {
                Field = "AccountId",
                Operator = PredicateOperator.Equals,
                Value = accountId.ToString()
            };

            var paging = new Paging
            {
                Index = 0,
                Size = 10
            };

            var request = new SearchAccountsRequest
            {
                Ordering = null,
                PageInfo = paging,
                Predicates = new[] { predicate }
            };

            return (await CustomerService.CallAsync((s, r) => s.SearchAccountsAsync(r), request)).Accounts.ToArray();
        }
    }
}

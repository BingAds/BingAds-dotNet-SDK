using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.BingAds.V10.CampaignManagement;
using Microsoft.BingAds;

namespace BingAdsExamplesLibrary.V10
{
    /// <summary>
    /// This example demonstrates how to determine whether your account supports multiple sitelinks
    /// per ad extension or a single sitelink per ad extension. During calendar year 2017, Bing Ads 
    /// will migrate all SiteLinksAdExtension objects (contains multiple sitelinks per ad extension) 
    /// to Sitelink2AdExtension objects (contains one sitelink per ad extension). 
    /// </summary>
    public class SitelinkMigration : ExampleBase
    {
        public static ServiceClient<ICampaignManagementService> Service;

        public override string Description
        {
            get { return "Migrating to One Sitelink per Ad Extension | Campaign Management V10"; }
        }

        public async override Task RunAsync(AuthorizationData authorizationData)
        {
            try
            {
                Service = new ServiceClient<ICampaignManagementService>(authorizationData);

                bool sitelinkMigrationIsCompleted = false;

                // Even if you have multiple accounts per customer, each account will have
                // its own migration status. 
                var accountMigrationStatusesInfos = (await GetAccountMigrationStatusesAsync(
                    new long[] { authorizationData.AccountId },
                    "SiteLinkAdExtension"
                )).ToArray();

                do
                {
                    foreach (var accountMigrationStatusesInfo in accountMigrationStatusesInfos)
                    {
                        OutputAccountMigrationStatusesInfo(accountMigrationStatusesInfo);

                        if (accountMigrationStatusesInfo.MigrationStatusInfo.SingleOrDefault(
                            statusInfo =>
                            statusInfo.Status == MigrationStatus.NotStarted && "SiteLinkAdExtension".CompareTo(statusInfo.MigrationType) == 0)
                            != null)
                        {
                            sitelinkMigrationIsCompleted = true;
                            OutputStatusMessage("found\n");
                        }
                    }
                } while (!sitelinkMigrationIsCompleted);
                                

                // If migration has been completed, then you should request the Sitelink2AdExtension objects.
                // You can always request both types; however, before migration only the deprecated SiteLinksAdExtension
                // type will be returned, and after migration only the new Sitelink2AdExtension type will be returned.
                AdExtensionsTypeFilter adExtensionsTypeFilter =
                    sitelinkMigrationIsCompleted ? AdExtensionsTypeFilter.Sitelink2AdExtension : AdExtensionsTypeFilter.SiteLinksAdExtension;

                var adGroupAdExtensionIds = await GetAdExtensionIdsByAccountIdAsync(
                    authorizationData.AccountId, 
                    AssociationType.AdGroup, 
                    adExtensionsTypeFilter
                );

                var campaignAdExtensionIds = await GetAdExtensionIdsByAccountIdAsync(
                    authorizationData.AccountId,
                    AssociationType.Campaign,
                    adExtensionsTypeFilter
                );

                var adExtensions = new List<AdExtension>();

                if(adGroupAdExtensionIds != null && adGroupAdExtensionIds.ToList().Count > 0)
                {
                    adExtensions.Concat(await GetAdExtensionsByIdsAsync(
                        authorizationData.AccountId,
                        adGroupAdExtensionIds.ToList(),
                        adExtensionsTypeFilter
                    ));
                }

                if (campaignAdExtensionIds != null && campaignAdExtensionIds.ToList().Count > 0)
                {
                    adExtensions.Concat((AdExtension[])await GetAdExtensionsByIdsAsync(
                        authorizationData.AccountId,
                        campaignAdExtensionIds.ToList(),
                        adExtensionsTypeFilter
                    ));
                }

                OutputAdExtensionsWithEditorialReasons(adExtensions, null);
            }
            // Catch authentication exceptions
            catch (OAuthTokenRequestException ex)
            {
                OutputStatusMessage(string.Format("Couldn't get OAuth tokens. Error: {0}. Description: {1}", ex.Details.Error, ex.Details.Description));
            }
            // Catch Campaign Management service exceptions
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.AdApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.Errors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.ApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (FaultException<Microsoft.BingAds.V10.CampaignManagement.EditorialApiFaultDetail> ex)
            {
                OutputStatusMessage(string.Join("; ", ex.Detail.OperationErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
                OutputStatusMessage(string.Join("; ", ex.Detail.BatchErrors.Select(error => string.Format("{0}: {1}", error.Code, error.Message))));
            }
            catch (Exception ex)
            {
                OutputStatusMessage(ex.Message);
            }
        }

        // Gets the specified ad extension type(s) from the account's extension library.

        private async Task<IEnumerable<AccountMigrationStatusesInfo>> GetAccountMigrationStatusesAsync(
            long[] accountIds,
            string migrationType)
        {
            var request = new GetAccountMigrationStatusesRequest
            {
                AccountIds = accountIds,
                MigrationType = migrationType
            };

            return (await Service.CallAsync((s, r) => s.GetAccountMigrationStatusesAsync(r), request)).MigrationStatuses;
        }

        // Gets the specified ad extension type(s) from the account's extension library.

        private async Task<IEnumerable<long>> GetAdExtensionIdsByAccountIdAsync(
            long accountId, 
            AssociationType associationType, 
            AdExtensionsTypeFilter adExtensionsTypeFilter)
        {
            var request = new GetAdExtensionIdsByAccountIdRequest
            {
                AccountId = accountId,
                AdExtensionType = adExtensionsTypeFilter,
                AssociationType = associationType
            };

            return (await Service.CallAsync((s, r) => s.GetAdExtensionIdsByAccountIdAsync(r), request)).AdExtensionIds;
        }

        // Gets the identified ad extensions of the specified ad extension type(s) from the account's extension library.

        private async Task<IEnumerable<AdExtension>> GetAdExtensionsByIdsAsync(
            long accountId, 
            IList<long> adExtensionIds, 
            AdExtensionsTypeFilter adExtensionsTypeFilter)
        {
            var request = new GetAdExtensionsByIdsRequest
            {
                AccountId = accountId,
                AdExtensionIds = adExtensionIds,
                AdExtensionType = adExtensionsTypeFilter
            };

            return (await Service.CallAsync((s, r) => s.GetAdExtensionsByIdsAsync(r), request)).AdExtensions;
        }

        // Gets the reasons why the specified extension failed editorial validation when 
        // in the context of an associated campaign or ad group.

        private async Task<IList<AdExtensionEditorialReasonCollection>> GetAdExtensionsEditorialReasons(
            long accountId,
            IList<AdExtensionIdToEntityIdAssociation> associations,
            AssociationType associationType)
        {
            var request = new GetAdExtensionsEditorialReasonsRequest
            {
                AccountId = accountId,
                AdExtensionIdToEntityIdAssociations = associations,
                AssociationType = associationType
            };

            return (await Service.CallAsync(
                (s, r) => s.GetAdExtensionsEditorialReasonsAsync(r), request)).EditorialReasons;
        }
    }
}

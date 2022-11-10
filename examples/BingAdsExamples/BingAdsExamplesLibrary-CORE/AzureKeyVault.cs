using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BingAdsExamplesLibrary_CORE
{
    public static class KeyVault
    {
        private static SecretClient secretClient;
        private static string _KeyVaultValue = null;
        /// <summary>
        /// REPLACE WITH YOUR AZURE KEY VAULT URL
        /// https://YourVaultNameHere.vault.azure.net/
        /// </summary>
        private static string _vaultURI = "";

        public static string GetValeuFromKey(string key)
        {
            secretClient = new SecretClient(new Uri(_vaultURI), credential: new DefaultAzureCredential());
            try
            {
                _KeyVaultValue = secretClient.GetSecret(key).Value.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return _KeyVaultValue;
        }

        public static void SetValueFromKey(string key, string value)
        {
            try
            {
                secretClient.SetSecret(key, value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}


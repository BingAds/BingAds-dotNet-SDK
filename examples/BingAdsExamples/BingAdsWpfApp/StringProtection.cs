using System;
using System.Security.Cryptography;
using System.Text;

namespace BingAdsWpfApp
{
    /// <summary>
    /// Used to access and protect a string.
    /// </summary>
    public static class StringProtection
    {
        public static string Protect(this string sourceString)
        {
            var sourceBytes = Encoding.Unicode.GetBytes(sourceString);

            var encryptedBytes = ProtectedData.Protect(sourceBytes, null, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Unprotect(this string protectedString)
        {
            var protectedBytes = Convert.FromBase64String(protectedString);

            var unprotectedBytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);

            return Encoding.Unicode.GetString(unprotectedBytes);
        }        
    }
}

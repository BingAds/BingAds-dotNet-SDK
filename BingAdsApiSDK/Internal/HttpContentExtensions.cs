using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.BingAds.Internal
{
    /// <summary>
    /// Reserved for internal use.
    /// </summary>
    internal static class HttpContentExtensions
    {
        /// <summary>
        /// Reserved for internal use.
        /// </summary>
        public static Task ReadAsFileAsync(this HttpContent content, string filename, bool overwrite)
        {
            string pathname = Path.GetFullPath(filename);
            if (!overwrite && File.Exists(filename))
            {
                throw new InvalidOperationException(string.Format("File {0} already exists.", pathname));
            }

            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(pathname, FileMode.Create, FileAccess.Write, FileShare.None);

                return content.CopyToAsync(fileStream).ContinueWith(_ => fileStream.Close());
            }
            catch
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }

                throw;
            }
        }
    }
}

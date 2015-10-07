using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BingAds.Internal.Utilities
{
    internal interface IFileSystem
    {
        void DeleteFile(string filePath);

        void RenameFile(string oldName, string newName);

        void CreateDirectoryIfDoesntExist(string directoryName);

        string[] GetFilesFromDirectory(string directoryName);
    }
}

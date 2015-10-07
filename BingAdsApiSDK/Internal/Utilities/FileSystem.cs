using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.BingAds.Internal.Utilities
{
    internal class FileSystem : IFileSystem
    {
        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        public void RenameFile(string oldName, string newName)
        {
            File.Move(oldName, newName);
        }

        public void CreateDirectoryIfDoesntExist(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        public string[] GetFilesFromDirectory(string directoryName)
        {
            return Directory.GetFiles(directoryName);
        }
    }
}

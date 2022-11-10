using Azure.Data.Tables;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace BingAdsExamplesLibrary_CORE
{
    public static class BlobStore
    {
        public static void GetBlobServiceClient(ref BlobContainerClient blobServiceClient, string uri)
        {
            blobServiceClient = new BlobContainerClient(new Uri(uri), new DefaultAzureCredential());
        }

        public static void UploadFile(BlobContainerClient containerClient, string localFilePath)
        {
            try
            {
                string fileName = Path.GetFileName(localFilePath);
                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                blobClient.Upload(localFilePath, true);
                Console.WriteLine($"File Uplaoded From: {fileName} TO THIS BLOB {blobClient.Name}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public static void GetTableServiceClient(ref TableServiceClient t, string uri)
        {
            t = new TableServiceClient(new Uri(uri), new DefaultAzureCredential());
        }

        public static void UploadToAzureTable<T>(ref TableServiceClient t, string tableName, List<T> tableRows) where T : ITableEntity
        {
            TableClient tc = t.GetTableClient(tableName: tableName);
            tc.CreateIfNotExists();
            foreach (ITableEntity tableRow in tableRows)
            {
                Console.WriteLine($"PROCESSING PartitionKey:{tableRow.PartitionKey}, RowKey:{tableRow.RowKey}");
                try
                {
                    tc.UpsertEntity(tableRow);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e); ;
                }
            }
        }
    }
}

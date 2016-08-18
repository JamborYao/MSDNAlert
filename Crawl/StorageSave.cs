using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Log;

namespace Crawl
{
    public class StorageSave
    {
        private readonly string azureStorageConnecionstring = InitSettings.Storage.ConnectionString;
        private CloudStorageAccount account = null;
        private CloudBlobClient blobClient = null;
        private CloudBlobContainer container = null;
     

        public StorageSave(string containerName)
        {
            account = CloudStorageAccount.Parse(azureStorageConnecionstring);
            blobClient = account.CreateCloudBlobClient();
            container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
        }
        /// <summary>
        /// save to current file blob
        /// </summary>
        /// <param name="threads"></param>
        /// <param name="current"></param>
        public void SaveToCurrentFile(List<Crawl.Thread> threads,string current)
        {
            string content = Newtonsoft.Json.JsonConvert.SerializeObject(threads);
            UploadToStorage(content, current);          
        }

        public string CompareFiles(string history, string current)
        {
            var historyContent = ReadStorageFile(history);
            var currentContent = ReadStorageFile(current);
            List<Crawl.Thread> historyThreads = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Crawl.Thread>>(historyContent);
            List<Crawl.Thread> currentThreads = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Crawl.Thread>>(currentContent);
            if (historyThreads != null)
            {
               
                var query = from item in currentThreads where !historyThreads.Any(x=>x.Title==item.Title) select item;
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(query);              
                return result;
            }
            else
            {
                LogHelper.LogMessage("no history result");
                return "no history result";
            }
           
        }
        /// <summary>
        /// copy current.txt to history. let the current data as the history data
        /// </summary>
        public void CopyToHistory(string history,string current)
        {
            var targetBlob = container.GetBlockBlobReference(history);
            var sourceBlob = container.GetBlockBlobReference(current);
            targetBlob.StartCopy(sourceBlob);
          
        }

        /// <summary>
        /// upload content to azure storage
        /// </summary>
        /// <param name="content"></param>
        /// <param name="blobName"></param>
        public void UploadToStorage(string content, string blobName)
        {
            var blob = container.GetBlockBlobReference(blobName);
            blob.UploadText(content);
        }

        /// <summary>
        /// read azure storage file content
        /// </summary>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public string ReadStorageFile(string blobName)
        {
            string content = string.Empty;
            var blob = container.GetBlockBlobReference(blobName);
        

            if (blob.Exists())
            {
                using (var stream = blob.OpenRead())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                return content;
            }
            else
            {
               
                return "";
            }
           
        }

        public void InsertToQueue(string message)
        {
            account = CloudStorageAccount.Parse(azureStorageConnecionstring);
            var queueClient = account.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("task");
            queue.CreateIfNotExists();
            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            queue.AddMessage(queueMessage);
        }
    }
}

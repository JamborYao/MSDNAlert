using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public class Settings
    {
        [JsonProperty(PropertyName = "azureStorage")]
        public StorageSettings Storage { get; set; }
        [JsonProperty(PropertyName = "sendgridSettings")]
        public SendGridInit SendInit { get; set; }
        [JsonProperty(PropertyName = "msdn_zhcn")]
        public Forum[] ForumName { get; set; }
    }
    public class Forum
    {
        [JsonProperty(PropertyName = "emailsettings")]
        public EmailSetting EmailSet { get; set; }
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
        [JsonProperty(PropertyName = "container")]
        public string Container { get; set; }
        [JsonProperty(PropertyName = "blob")]
        public Blobs Blob { get; set; }
    }
    public class EmailSetting {
        [JsonProperty(PropertyName = "from")]
        public string[] From { get; set; }
        [JsonProperty(PropertyName = "to")]

        public string[] To { get; set; }
        [JsonProperty(PropertyName = "cc")]
        public string[] CC { get; set; }
    }


    public class StorageSettings {
        [JsonProperty(PropertyName = "account")]
        public string Account { get; set; }
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "connectionString")]
        public string ConnectionString { get; set; }
       
        [JsonProperty(PropertyName = "queue")]
        public string  Queue { get; set; }
    }
    public class SendGridInit {
        [JsonProperty(PropertyName="account")]
        public string  Account { get; set; }
        [JsonProperty(PropertyName = "key")]
        public string  Key { get; set; }

    }
    public class Blobs
    {
        [JsonProperty(PropertyName = "history")]
        public string History { get; set; }
        [JsonProperty(PropertyName = "current")]
        public string Current { get; set; }
    }
}

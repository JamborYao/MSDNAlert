using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public class InitSettings
    {
        public InitSettings()
        {

            var ass = Assembly.GetExecutingAssembly();
            string path = System.IO.Path.GetDirectoryName(ass.Location);
            string filepath = path + "\\customSettings.json";
            var file = System.IO.File.ReadAllText(filepath);           
            settings = JsonConvert.DeserializeObject<Settings>(file);
         
        }
        private static Settings settings;
        public static StorageSettings Storage { get { return settings.Storage; }  }
        public static SendGridInit SendInit { get { return settings.SendInit; }  }
        public static Forum[] ForumName { get { return settings.ForumName; }  }
    }
}

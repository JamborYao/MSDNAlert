using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    public class LogHelper
    {
        public static void LogMessage(string message)
        {
            var ass = Assembly.GetExecutingAssembly();
            string path = System.IO.Path.GetDirectoryName(ass.Location);
            string filepath = path + "\\log.txt";
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }
            using (var stream = File.Open(filepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message + "/r/n");
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}

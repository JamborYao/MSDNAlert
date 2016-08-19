using Crawl;
using Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace TestAlert
{
    class Program
    {
        static void Main(string[] args)
        {

            InitSettings initSettings = new InitSettings();

            try
            {
                foreach (var forum in InitSettings.ForumName)
                {
                    var history = forum.Blob.History;
                    var current = forum.Blob.Current;
                    var html = Crawl.GetData.GetHtml(forum.Link);
                    List<Crawl.Thread> threads = Crawl.AnalyzeDate.Analyze(html);
                    Crawl.StorageSave handle = new Crawl.StorageSave(forum.Container);
                    handle.SaveToCurrentFile(threads,current);

                    var result = handle.CompareFiles(history, current);
                    handle.CopyToHistory(history,current);
                    if (result != "no history result"&&!string.IsNullOrEmpty(result))
                    {
                        handle.InsertToQueue(result,forum.Queue);
                    }
                }          
               
            }
            catch (Exception e)
            {
                LogHelper.LogMessage(e.Message);
                throw e;
            }

        }
    }
}

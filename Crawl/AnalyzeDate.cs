using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawl
{
    public class AnalyzeDate
    {
        public static List<Thread> Analyze(string html)
        {
            List<Thread> threads = new List<Thread>();
            HtmlDocument hdoc = new HtmlDocument();
            hdoc.LoadHtml(html);
            var resultMSDN = (from h3 in hdoc.DocumentNode.Descendants("h3") orderby h3.Line ascending select h3).Take(10);
            foreach (var item in resultMSDN)
            {
                Thread thread = new Thread();
                thread.Link = item.InnerHtml;
                thread.Title = item.InnerText;
                thread.Line = item.Line;
                threads.Add(thread);
            }
            return threads;
        }
    }
}

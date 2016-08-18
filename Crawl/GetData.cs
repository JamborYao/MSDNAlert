using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Log;


namespace Crawl
{
    public class GetData
    {
        public static string GetHtml(string link)
        {
            string html = string.Empty;
            Assembly ass = Assembly.GetExecutingAssembly();
            string path = System.IO.Path.GetDirectoryName(ass.Location);
            LogHelper.LogMessage(path);
            IWebDriver driver = new InternetExplorerDriver(path + "\\");
            driver.CurrentWindowHandle.Min();
            driver.Navigate().GoToUrl(link);
            html = driver.FindElement(By.Id("threadList")).GetAttribute("innerHTML");

            driver.Quit();
            return html;
        }

    }
}

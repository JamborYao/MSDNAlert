using Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSend
{
    class Program
    {
        static void Main(string[] args)
        {
            InitSettings initSettings = new InitSettings();
            foreach (var forum in InitSettings.ForumName)
            {
                var emailSettings = forum.EmailSet;
                EmailHelper.Send(emailSettings.From, emailSettings.To, emailSettings.CC, InitSettings.SendInit.Account, InitSettings.SendInit.Key, "test1", "this is test email");
            }
        }
    }
}

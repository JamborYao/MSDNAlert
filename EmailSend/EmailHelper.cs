using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using System.Net.Mail;
using System.Net;

namespace EmailSend
{
    public class EmailHelper
    {
        public static void Send(string[] from, string[] to, string[] cc, string sendGridAccount,string sendGridKey,string subject, string content)
        {
            try
            {
                var myMessage = new SendGridMessage();
                // Add the message properties.
                myMessage.From = new MailAddress(from.First());

                // Add multiple addresses to the To field.
                List<String> recipients = new List<String>();
                foreach (var item in to)
                {
                    recipients.Add(item);
                  
                }
               
                myMessage.AddTo(recipients);

                //List<String> cclists = new List<String>();
                //foreach (var item in cc)
                //{
                //    myMessage.AddCc(item);
                //}


                myMessage.Subject = subject;
                //Add the HTML and Text bodies
                myMessage.Html = content;
                //myMessage.Text = "Hello World plain text!";

                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential(sendGridAccount, sendGridKey);

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email, which returns an awaitable task.
                transportWeb.DeliverAsync(myMessage).Wait();
            }
            catch (Exception e)
            {
                throw;
            }
         

        }
    }
}

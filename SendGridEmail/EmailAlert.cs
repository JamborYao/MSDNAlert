using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using SendGrid;

namespace SendGridEmail
{
    public class EmailAlert
    {
        public static void Send()
        {
            try
            {
                var myMessage = new SendGridMessage();

                // Add the message properties.
                myMessage.From = new MailAddress("support-v-jayao@hotmail.com");

                // Add multiple addresses to the To field.
                List<String> recipients = new List<String>
                {
                    @"Jambor Yao (Pactera Technologies Inc) <v-jayao@microsoft.com>",
                };
                 

                myMessage.AddTo(recipients);

                myMessage.Subject = "Testing the SendGrid Library";

                //Add the HTML and Text bodies
                myMessage.Html = "<p>Hello World!</p>";
                myMessage.Text = "Hello World plain text!";

                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential("azure_1db5655b318478c989eba8beed80d69a@azure.com", "qweasdzxc123");

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email, which returns an awaitable task.
                transportWeb.DeliverAsync(myMessage).Wait();
            }
            catch (Exception e)
            {
                throw;
            }
            // Create the email object first, then add the properties.
           
        }
    }
}

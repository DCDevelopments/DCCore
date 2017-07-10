using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace DCCore.WebMvc.Utils
{
    public class ExchangeEmailService : IIdentityMessageService
    {
        //sendGrid
        String APIKey = "apikey";
        String SecretKey ="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";
        //Host=smtp.sendgrid.net


        //MailJet
        //String APIKey = "2272066c6f5b15339ca799e8037a5b6c";
        //String SecretKey = "3583d8d4e0fbcb5770ca4734d5d0d9f8";
        //Host=in.mailjet.com


        public enum Mailboxes
        {
            Noreply = 0,
            Service = 1,
            Contact = 2
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            using (var client = new SmtpClient())
            {
                client.Host = "smtp.sendgrid.net";
                client.Port = 25;
                client.EnableSsl = true;
                //client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(APIKey, SecretKey);

                var from = new MailAddress("no-reply@example.com");
                var to = new MailAddress(email);

                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true                    
                };
                await client.SendMailAsync(mailMessage);
            }
        }

        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
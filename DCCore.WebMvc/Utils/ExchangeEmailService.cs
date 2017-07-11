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
                client.Host = "in.mailjet.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("2272066c6f5b15339ca799e8037a5b6c", "3583d8d4e0fbcb5770ca4734d5d0d9f8");

                var from = new MailAddress("no-reply@example.com");
                var to = new MailAddress(email);

                var mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false                    
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
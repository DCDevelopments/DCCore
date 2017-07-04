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
                client.Host = "smtp.gmail.com";
                client.Port = 465;
                client.EnableSsl = true;
                //client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("cristianramos83@gmail.com", "alberto99");
                
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
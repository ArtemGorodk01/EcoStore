using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace EcoStore.CommonServices.MailSender
{
    public static class SmtpWorker
    {
        public static async Task SendEmailAsync(string toMail, string subject, string message)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("gorodkoartem00@gmail.com", "d0n1pm00")
            };
            MailAddress from = new MailAddress("gorodkoartem00@gmail.com", "Ecoteria");
            MailAddress to = new MailAddress(toMail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = message;
            await smtp.SendMailAsync(m);
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MatchingService.Services
{
    public class NotifyUserService
    {

        SmtpClient smtpClient = new SmtpClient("smtp.strato.de", 587); //Gmail smtp

        System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential("team@industriesymbiose.de", "9RDZb8nOZTZYepHXQDjt");
        MailMessage message;
        string mailbody;

        public NotifyUserService(string from, string to, string mailbody, string subject) // from to are email adresses
        {
            this.message = new MailMessage(from, to);
            this.mailbody = mailbody;
            message.Subject = subject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;  
        }

        public int Send()
        {
            int result = -1;
            try
            {
                smtpClient.Send(message);
                result = 1;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
            






    }
}

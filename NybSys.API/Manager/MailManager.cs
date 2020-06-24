using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace NybSys.API.Manager
{
    public class MailManager : IMailManager
    {
        private readonly IOptions<Models.ViewModels.SmtpMailServer> config;
        public MailManager(IOptions<Models.ViewModels.SmtpMailServer> _config)
        {
            config = _config;
        }


        public virtual async Task SendEmail(string subject, string messages, bool isBcc)
        {

            string fromAddress = this.config.Value.MailFrom;
            int port = this.config.Value.Port;
            string host = this.config.Value.Host;
            string password = this.config.Value.Password;
            string bcc = "salimullah.iu@gmail.com";
            string userName = this.config.Value.UserName;
            string toAddress = this.config.Value.HODMail;
            SmtpClient emailClient = new SmtpClient(host, port);
            emailClient.EnableSsl = true;
            emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailClient.UseDefaultCredentials = false;

            emailClient.Credentials = new System.Net.NetworkCredential(userName, password);

            //Add this line to bypass the certificate validation
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            MailMessage message = new MailMessage(fromAddress, toAddress, subject, messages);
            if (isBcc)
            {
                message.Bcc.Add(bcc);
            }
            message.IsBodyHtml = true;
            await emailClient.SendMailAsync(message);

        }

        public virtual async Task SendEmail(string subject, string messages)
        {

            string fromAddress = this.config.Value.MailFrom;
            int port = this.config.Value.Port;
            string host = this.config.Value.Host;
            string password = this.config.Value.Password;
            string userName = this.config.Value.UserName;
            string toAddress = this.config.Value.HODMail;

            SmtpClient emailClient = new SmtpClient(host);
            emailClient.Port = port;
            emailClient.EnableSsl = true;
            emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailClient.UseDefaultCredentials = true;
            emailClient.Credentials = new System.Net.NetworkCredential(userName, password);

            //Add this line to bypass the certificate validation
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };



            try
            {
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, messages);
                //message.CC.Add("kazisalimullah@nybsys.com.bd");
                message.IsBodyHtml = true;
               await  emailClient.SendMailAsync(message);

            }
            catch (Exception e)
            {
                throw;
            }

        }

        public virtual async Task SendEmail(string subject, string messages, string ccAddress, string fileLocation)
        {


            string fromAddress = this.config.Value.MailFrom;
            int port = this.config.Value.Port;
            string host = this.config.Value.Host;
            string password = this.config.Value.Password;
            string userName = this.config.Value.UserName;
            string toAddress = this.config.Value.HODMail;

            SmtpClient emailClient = new SmtpClient(host);
            emailClient.Port = port;
            emailClient.EnableSsl = true;
            emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailClient.UseDefaultCredentials = true;
            emailClient.Credentials = new System.Net.NetworkCredential(userName, password);

            //Add this line to bypass the certificate validation
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };



            try
            {
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, messages);
                message.Attachments.Add(new Attachment(fileLocation));
                if (!string.IsNullOrEmpty(ccAddress))
                {
                    message.CC.Add(ccAddress);
                }
                message.IsBodyHtml = true;
                await emailClient.SendMailAsync(message);

            }
            catch (Exception e)
            {
                throw;
            }

        }

        public virtual async Task SendEmail(string subject, string messages,  string ccAddress)
        {

            string fromAddress = this.config.Value.MailFrom;
            int port = this.config.Value.Port;
            string host = this.config.Value.Host;
            string password = this.config.Value.Password;
            string userName = this.config.Value.UserName;
            string toAddress = this.config.Value.HODMail;

            SmtpClient emailClient = new SmtpClient(host, port);
            emailClient.EnableSsl = true;
            emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailClient.UseDefaultCredentials = true;
            emailClient.Credentials = new System.Net.NetworkCredential(userName, password);

            //Add this line to bypass the certificate validation
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            MailMessage message = new MailMessage(fromAddress, toAddress, subject, messages);
            if (!string.IsNullOrEmpty(ccAddress))
            {
                message.CC.Add(ccAddress);
            }
            message.IsBodyHtml = true;
            await emailClient.SendMailAsync(message);

        }
    }
}
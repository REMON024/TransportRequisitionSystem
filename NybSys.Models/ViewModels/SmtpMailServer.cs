using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.ViewModels
{
  public class SmtpMailServer
    {
        public string MailFrom { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string HOD { get; set; }
        public string HODMail { get; set; } 


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NybSys.Models.DTO
{
   public class LogWrite
    {
        private int logWriteID = 0;

        public int LogWriteID
        {
            get { return logWriteID; }
            set { logWriteID = value; }
        }

        private string exceptionString = string.Empty;
       
        public string ExceptionString
        {
            get { return exceptionString; }
            set { exceptionString = value; }
        }

        private string exceptionMessage = string.Empty;
       
        public string ExceptionMessage
        {
            get { return exceptionMessage; }
            set { exceptionMessage = value; }
        }

        private DateTime exceptionDate = DateTime.Now;
  
        public DateTime ExceptionDate
        {
            get { return exceptionDate; }
            set { exceptionDate = value; }
        }

        private string formName = string.Empty;
       
        public string FormName
        {
            get { return formName; }
            set { formName = value; }
        }

        private string inputObject = string.Empty;
       
        public string InputObject
        {
            get { return inputObject; }
            set { inputObject = value; }
        }

        private string userID = string.Empty;
       
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private string macAddress = string.Empty;
       
        public string MacAddress
        {
            get { return macAddress; }
            set { macAddress = value; }
        }

        private int logPriority = 0;
       
        public int LogPriority
        {
            get { return logPriority; }
            set { logPriority = value; }
        }

        public string MethodName { get; set; }

        public string SessionID { get; set; }
    }
}

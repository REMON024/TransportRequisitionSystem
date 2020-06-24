using NybSys.Common.Utility;
using System;
using System.Collections.Generic;

namespace NybSys.Models.ViewModels
{
    public class ApiCommonMessage
    {
        public ApiCommonMessage()
        {
            SessionId = Guid.Empty;
        }

        public string UserName { get; set; }
        public Guid SessionId { get; set; }
        public string FormName { get; set; }
        public string ModuleName { get; set; }
        public string Content { get; set; }


        public TEntity GetRequestObject<TEntity>() where TEntity : class
        {
            if(string.IsNullOrEmpty(this.Content))
            {
                throw new ArgumentNullException(Common.Constants.ErrorMessages.CONTENT_IS_EMPTY_OR_NULL);
            }

            return JSONConvert.Convert<TEntity>(this.Content);
        }
    }
}

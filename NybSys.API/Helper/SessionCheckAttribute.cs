using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NybSys.Common.Constants;
using NybSys.Common.Utility;
using System.IdentityModel.Tokens.Jwt;
using NybSys.API.Manager;

namespace NybSys.API.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SessionCheckAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.Filters.Contains(new SessionCheckByPassAttribute()) && context.Filters.Contains(new NybSysAuthorizeWithDBAttribute()))
            {
                try
                {
                    ISecurityManager securityManager = context.HttpContext.RequestServices.GetService<ISecurityManager>();

                    string token = GetTokenFromRequest(context);

                    var result = securityManager.VerifyTokenAsync(GetTokenIdentifier(token)).Result;

                    if (!result)
                    {
                        context.Result = new ObjectResult(ErrorMessages.SESSION_IS_EXPIRED) { StatusCode = (int)HttpStatusCode.Unauthorized };
                    }
                }
                catch(Exception)
                {
                    context.Result = new ObjectResult(ErrorMessages.SESSION_EXPIRED_OR_INACTIVE) { StatusCode = (int)HttpStatusCode.Unauthorized };
                }
            }
        }

        private string GetTokenFromRequest(ActionExecutingContext context)
        {
            string fullToken = context.HttpContext.Request.Headers[Common.Constants.HttpHeaders.Token];
            return fullToken.Replace(Common.Constants.HttpHeaders.AuthenticationSchema, "");
        }

        private Guid GetTokenIdentifier(string token)
        {
            var jtiString = JWTTokenReader.GetTokenValue(JwtRegisteredClaimNames.Jti, token);
            return new Guid(jtiString);
        }
    }
}

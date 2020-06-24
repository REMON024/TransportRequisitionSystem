using Microsoft.AspNetCore.Mvc.Filters;
using NybSys.API.Manager;
using NybSys.Common.Utility;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using NybSys.Common.Constants;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace NybSys.API.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NybSysAuthorizeWithDBAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!context.Filters.Contains(new NybSysAllowAnonymousAttribute()))
            {
                if (!user.Identity.IsAuthenticated)
                {
                    context.Result = new ObjectResult(ErrorMessages.ACCESS_DENIED) { StatusCode = (int)HttpStatusCode.Unauthorized };
                    return;
                }
                else
                {
                    if (!IsAuthorized(context))
                    {
                        context.Result = new ObjectResult(ErrorMessages.FORBIDDEN) { StatusCode = (int)HttpStatusCode.Forbidden };
                        return;
                    }

                    return;
                }
            }
        }

        private bool IsAuthorized(AuthorizationFilterContext context)
        {
            var actionName = context.ActionDescriptor.RouteValues.Where(p => p.Key == "action").Select(p => p.Value).FirstOrDefault();
            var controllerName = context.ActionDescriptor.RouteValues.Where(p => p.Key == "controller").Select(p => p.Value).FirstOrDefault();

            string token = GetTokenFromRequest(context);
            string roleName = GetAccessRight(token);

            IAccessManager accessManager = context.HttpContext.RequestServices.GetService<IAccessManager>();

            return accessManager.VerifyAccess(roleName, controllerName, actionName).Result;
        }

        private string GetTokenFromRequest(AuthorizationFilterContext context)
        {
            string fullToken = context.HttpContext.Request.Headers[Common.Constants.HttpHeaders.Token];
            return fullToken.Replace(Common.Constants.HttpHeaders.AuthenticationSchema, "");
        }

        private string GetAccessRight(string token)
        {
            var jtiString = JWTTokenReader.GetTokenValue(ClaimTypes.Role, token);
            return jtiString;
        }
    }
}

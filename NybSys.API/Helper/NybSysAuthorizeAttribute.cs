using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NybSys.Common.Constants;
using System;
using System.Net;

namespace NybSys.API.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class NybSysAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string[] AllowedPermissions { get; set; }

        public NybSysAuthorizeAttribute()
        {
            AllowedPermissions = null;
        }

        public NybSysAuthorizeAttribute(string someFilterParameter)
        {
            AllowedPermissions = someFilterParameter.Split(',');
        }

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
                    if (AllowedPermissions != null)
                    {
                        if (!isAuthorized(context))
                        {
                            context.Result = new ObjectResult(ErrorMessages.FORBIDDEN) { StatusCode = (int)HttpStatusCode.Forbidden };
                            return;
                        }
                    }

                    return;
                }
            }
        }

        private bool isAuthorized(AuthorizationFilterContext context)
        {
            foreach (var perm in AllowedPermissions)
            {
                if (HasPermission(perm, context.HttpContext))
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasPermission(string permission, HttpContext context)
        {
            var result = context.User.IsInRole(permission);
            return result;
        }
    }
}

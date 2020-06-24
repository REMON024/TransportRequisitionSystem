using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;

namespace NybSys.API.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NybSysAllowAnonymousAttribute : Attribute, IAllowAnonymous, IAllowAnonymousFilter
    {
    }
}

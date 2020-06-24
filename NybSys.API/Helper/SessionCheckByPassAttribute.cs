using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace NybSys.API.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SessionCheckByPassAttribute : Attribute, IFilterMetadata
    {
    }
}

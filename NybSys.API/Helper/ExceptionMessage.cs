using Microsoft.AspNetCore.Mvc;
using NybSys.Common.ExceptionHandle;
using System;
using System.Net;

namespace NybSys.API.Helper
{
    public static class Build
    {
        public static IActionResult ExceptionMessage(Exception ex)
        {
            if(ex is BadRequestException)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            if (ex is NotFoundException)
            {
                return new NotFoundObjectResult(ex.Message);
            }

            return new ObjectResult(Common.Constants.ErrorMessages.SERVER_PROBLEM) { StatusCode = (int)HttpStatusCode.InternalServerError };
        }
        public static IActionResult ExceptionMessage(Exception ex,string message)
        {
            if (ex is BadRequestException)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            if (ex is NotFoundException)
            {
                return new NotFoundObjectResult(ex.Message);
            }

            return new ObjectResult(message) { StatusCode = (int)HttpStatusCode.InternalServerError };
        }

        public static IActionResult SuccessMessage(object data)
        {
            return new OkObjectResult(data);
        }
    }
}

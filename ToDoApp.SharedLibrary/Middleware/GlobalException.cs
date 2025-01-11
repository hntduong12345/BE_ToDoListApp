using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoApp.SharedLibrary.LogEvent;

namespace ToDoApp.SharedLibrary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            //Declare exception variables
            string message = "Sorry, internal server error occured. Kindly try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string title = "Error!";

            try
            {
                await next(context);

                //Check if the Response is 'Too Many Request - 429 status code'
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning!";
                    message = "Too many request made";
                    statusCode = (int)HttpStatusCode.TooManyRequests;
                    await ModifyHeader(context, title, message, statusCode);
                }

                //Check if the Response is 'Unauthorized - 401 status code'
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert!";
                    message = "You are not authorized to access";
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    await ModifyHeader(context, title, message, statusCode);
                }

                //Check if the Response is 'Forbidden - 403 status code'
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Out of Access!";
                    message = "You are not allowed/required to access";
                    statusCode = (int)HttpStatusCode.Forbidden;
                    await ModifyHeader(context, title, message, statusCode);
                }
            }
            catch (Exception ex)
            {
                //Log the Original Exception to Files, Console, Debugger.
                LogEventException.LogExceptions(ex);

                //Check if the Exception is Time Out.
                if (ex is TaskCanceledException || ex is TimeoutException)
                {
                    title = "Out of Time!";
                    message = "Request timeout... try again!";
                    statusCode = (int)StatusCodes.Status408RequestTimeout;
                    await ModifyHeader(context, title, message, statusCode);
                }

                //If there is non of the Above-Specified Error catched, do the default (Internal Error - 500)
                await ModifyHeader(context, title, message, statusCode);
            }
        }

        public static async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
            //Display message to client
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(
                new ProblemDetails()
                {
                    Detail = message,
                    Title = title,
                    Status = statusCode
                }), CancellationToken.None);
            return;
        }
    }
}

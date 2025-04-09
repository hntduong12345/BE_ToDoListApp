using BE_ToDoListApp.SharedLibrary.Logs;
using BE_ToDoListApp.SharedLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace BE_ToDoListApp.SharedLibrary.Middlewares
{
    public class GlobalException
    {
        private readonly RequestDelegate _next;
        public GlobalException(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            int statusCode = 500;

            switch (exception)
            {
                //add more custom exception
                //For example case AppException: do something
                case BadHttpRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    //unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var errorResponse = new ErrorResponse(statusCode, exception.Message, DateTime.UtcNow);
            var result = errorResponse.ToString();
            await context.Response.WriteAsync(result);
        }
    }
}

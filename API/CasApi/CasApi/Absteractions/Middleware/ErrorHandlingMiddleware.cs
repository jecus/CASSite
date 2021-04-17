using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasApi.Absteractions.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CasApi.Absteractions.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private ILogger<ErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate next;
        
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex,
            int code = StatusCodes.Status500InternalServerError)
        {
            // сформируем и передадим ответ наружу
            var result = code == StatusCodes.Status500InternalServerError
                ? JsonConvert.SerializeObject(new {Error = "Кажется, что-то пошло не так... Мы будем благодарны, если вы напишете нам об этом на адрес support@expertnoemnenie.ru"})
                : "";
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(result);
        }
    }
}
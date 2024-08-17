using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;

namespace TestProject1
{
    /// <summary>
    /// 全局异常处理中间件
    /// </summary>
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            this.next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }

            catch (Exception ex)
            {
                _logger.Log(ex is ServicesException ? LogLevel.Warning : LogLevel.Error, exception: ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var data = new ApiResult<object>();
            if (ex is ServicesException)
            {
                data = new ApiResult<object>
                {
                    Code = (ex as ServicesException)?.Code ?? 500,
                    Message = ex.Message,
                    RequestId = context.TraceIdentifier
                };
            }
            else if (ex is UnauthorizedAccessException)
            {
                context.Response.StatusCode = 403;

                data = new ApiResult<object>
                {
                    Code = 401,
                    Message = ex.Message,
                    RequestId = context.TraceIdentifier
                };
            }
            else
            {
                data = new ApiResult<object>
                {
                    Code = 500,
                    Message = "服务器发生故障",
                    RequestId = context.TraceIdentifier
                };
            }
            var result = JsonConvert.SerializeObject(data);
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }
}

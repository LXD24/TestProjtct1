using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.Json.Serialization;

namespace TestProject1.Filters
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = new ApiResult<IActionResult>
            {
                Code = 0,
                Message = string.Empty,
                Data = context.Result is EmptyResult ? null : context.Result,
                RequestId = context.HttpContext.TraceIdentifier
            };
            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = "application/json;charset=utf-8",
                Content = JsonConvert.SerializeObject(result)
            };
        }
    }
}

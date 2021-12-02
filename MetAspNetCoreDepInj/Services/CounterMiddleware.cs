using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetAspNetCoreDepInj.Services
{
    public class CounterMiddleware
    {
        private readonly RequestDelegate _next;
        private int i = 0; // счетчик запросов
        public CounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ICounter counter, CounterService counterService)
        {
            i++;
            httpContext.Response.ContentType = "text/html;charset=utf-8";
            await httpContext.Response.WriteAsync($"Request # {i};<br/>CounterValue from constructor: {counter.Value};<br/>CounterValue from Service: {counterService.Counter.Value}");
        }
    }
}

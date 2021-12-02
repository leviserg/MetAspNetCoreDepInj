using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetAspNetCoreDepInj.Services
{
    public class MessageMiddleware
    {
        private readonly RequestDelegate _next;
        public MessageMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMessageSender messageSender, TimeService timeService, DateService dateService)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(messageSender.Send() + " " + dateService.GetDate + " " + timeService.GetTime);
        }
    }
}

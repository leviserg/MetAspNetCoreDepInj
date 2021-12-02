using MetAspNetCoreDepInj.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetAspNetCoreDepInj
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //private IServiceCollection _services;
        public void ConfigureServices(IServiceCollection services)
        {
            // var servs = services.ToList(); // up to 78+ services
            // e.g. services.AddMvc();
            //_services = services;


            //services.AddTransient<IMessageSender, EmailSender>(); // sending dependency injection as parameter of Configure() method below
            //services.AddTransient<MessageService>();

            services.AddTransient<TimeService>();
            // #### USING OWN Dependency Injection (Static Class Services/ServiceProviderExtensions.cs : DateService) #### 
            services.AddDateService();

            // #### Using Middleware component dependency : Counter Example
            /*
            services.AddTransient<ICounter, RandomCounter>();   // create 2 different 
            services.AddTransient<CounterService>();            // objects of RandomCounter for every request
            
            services.AddScoped<ICounter, RandomCounter>();   // create 1 new object for 
            services.AddScoped<CounterService>();            // whole request : value changed with every request (update page)
            */
            services.AddSingleton<ICounter, RandomCounter>();   // create 1 object for 
            services.AddSingleton<CounterService>();            // whole lifecycle (doesn't depend on requests)


            // #### USING Service Factory E.G for conditional service injection ####
            services.AddTransient<IMessageSender>(provider =>
            {
                if (DateTime.Now.Hour >= 12) return new EmailSender();
                return new SmsSender();
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //                                                                    OR IMessageSender messageSender
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageSender /*MessageService*/ messageSender, DateService dateService, TimeService timeService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            // 1. ##### Using dependency injection from Configure parameters and ConfigureService #####
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(messageSender.Send() + " " + dateService.GetDate + " " + timeService.GetTime);
            });
            

            /*
            // 2. ##### Using dependency injection from RequestServices of HttpContext #####
            // service locator pattern relization
            app.Run(async (context) =>
            {
                IMessageSender requestMessageSender = context.RequestServices.GetService<IMessageSender>();
                TimeService requestTimeService = context.RequestServices.GetService<TimeService>();
                DateService requestDateService = context.RequestServices.GetService<DateService>();
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(requestMessageSender.Send() + " " + requestDateService.GetDate + " " + requestTimeService.GetTime);
            });
            */

            /*
            // 3. ##### Using dependency injection from ApplicationServices of IApplicationBuilder #####
            app.Run(async (context) =>
            {
                IMessageSender appMessageSender = app.ApplicationServices.GetService<IMessageSender>();
                TimeService appTimeService = app.ApplicationServices.GetService<TimeService>();
                DateService appDateService = app.ApplicationServices.GetService<DateService>();
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(appMessageSender.Send() + " " + appDateService.GetDate + " " + appTimeService.GetTime);
            });
            */

            /*
            // 4. ##### Using dependency injection from Invoke Task from middleware component  #####
            app.UseMiddleware<MessageMiddleware>();
            */

            // 5. ##### Using dependency injection from Invoke Task from middleware - dependency lifecycle Counter Example  #####

            app.UseMiddleware<CounterMiddleware>();

            /*
            app.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>All Services</h1>");
                sb.Append("<table>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Realization</th></tr>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync(sb.ToString());
            });
            */
        }
    }
}

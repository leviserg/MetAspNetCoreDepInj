using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MetAspNetCoreDepInj.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddDateService(this IServiceCollection services)
        {
            services.AddTransient<DateService>();
        }
    }
}

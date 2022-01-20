using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Xero.NetStandard.OAuth2.Config;
using XeroConnection.Core.Persistence.Repositories;
using XeroConnection.Infrastructure.Repositories;

namespace XeroConnection.Infrastructure
{
    public static class XeroConnectionINFServiceExtention
    {
        public static IServiceCollection AddXeroConnectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<XeroConfiguration>(configuration.GetSection("XeroConfiguration"));
            services.AddTransient<IXeroConnectionRepository, XeroConnectionRepository>();
     
            return services;
        }

    }
}

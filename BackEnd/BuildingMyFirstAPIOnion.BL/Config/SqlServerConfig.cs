using BuildingMyFirstAPIOnion.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BuildingMyFirstAPIOnion.BL.Config
{
    public static class SqlServerConfig
    {
        public static IServiceCollection ConfigSqlServer(this IServiceCollection services, string connection)
        {
            services.AddDbContext<BaseContext>(op => {
                op.UseSqlServer(connection);
           
                });
            return services;

        }
    }
}

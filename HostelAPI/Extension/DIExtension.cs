using HostelAPI.Core.Repository.Abstraction;
using HostelAPI.Core.Repository.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelAPI.Extension
{
    public static class DIExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}

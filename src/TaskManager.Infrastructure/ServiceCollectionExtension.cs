using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ITaskRepository, Repositories.Mock.InMemoryTaskRepository>();
            services.AddSingleton<IProjectRepository, Repositories.Mock.InMemoryProjectRepository>();
            services.AddSingleton<IUserRepository, Repositories.Mock.InMemoryUserRepository>();
        }
    }
}

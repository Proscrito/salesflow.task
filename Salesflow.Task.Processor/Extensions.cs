using Microsoft.Extensions.DependencyInjection;
using Salesflow.Task.Three.DataAccess;

namespace Salesflow.Task.Processor
{
    internal static class Extensions
    {
        public static IServiceCollection ValidateDomain(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<TasksDbContext>();
            context.Database.EnsureCreated();

            return services;
        }
    }
}

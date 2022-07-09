using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Salesflow.Task.Processor;
using Salesflow.Task.Three;
using Salesflow.Task.Three.DataAccess;

CreateHostBuilder(args).Build().Run();

IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices(collection =>
        {
            collection.AddDbContext<TasksDbContext>(builder => builder
                .UseSqlite($"Data Source={nameof(TasksDbContext)}.db"), ServiceLifetime.Transient);
            collection.AddMediatR(typeof(TasksDbContext));

            collection.AddScoped<IWorker, Worker>();
            collection.AddSingleton<ITaskQueue, TaskQueue>();

            collection.AddHostedService<Publisher>();
            collection.AddHostedService<Subscriber>();

            collection.ValidateDomain();
        });


using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Salesflow.Task.Three;

namespace Salesflow.Task.Processor
{
    internal class Subscriber : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Subscriber(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken)
        {
            var task = Execute(cancellationToken);

            return task.IsCanceled
                ? task
                : System.Threading.Tasks.Task.CompletedTask;
        }

        private async System.Threading.Tasks.Task Execute(CancellationToken cancellationToken)
        {
            await System.Threading.Tasks.Task.WhenAll(Enumerable.Repeat(1, 10)
                .Select(_ => GetWorkerTask(cancellationToken)));

        }

        private async System.Threading.Tasks.Task GetWorkerTask(CancellationToken cancellationToken)
        {
            try
            {
                await using var scope = _scopeFactory.CreateAsyncScope();
                var worker = scope.ServiceProvider.GetRequiredService<IWorker>();
                await worker.RunWorkerAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public System.Threading.Tasks.Task StopAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}

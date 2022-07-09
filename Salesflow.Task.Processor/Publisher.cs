using Microsoft.Extensions.Hosting;
using Salesflow.Task.Three.DataAccess;
using Salesflow.Task.Three.DataAccess.Entity;
using TaskStatus = Salesflow.Task.Three.DataAccess.Entity.TaskStatus;

namespace Salesflow.Task.Processor
{
    internal class Publisher : IHostedService
    {
        private readonly TasksDbContext _context;

        public Publisher(TasksDbContext context)
        {
            _context = context;
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
            while (!cancellationToken.IsCancellationRequested)
            {
                var rnd = new Random();
                var task = new WorkerTask
                {
                    TaskStatus = TaskStatus.New,
                    CommandSql = Convert.ToBase64String(Guid.NewGuid().ToByteArray())[..rnd.Next(10, 21)], //some dummy string
                    WaitingTime = rnd.Next(3000, 20000), //3-20 sec to wait
                    ClientId = rnd.Next(1000, 1025) //25 clients
                };

                _context.Tasks.Add(task);
                await _context.SaveChangesAsync(cancellationToken);

                await System.Threading.Tasks.Task.Delay(rnd.Next(750, 2250), cancellationToken);
            }
        }

        public System.Threading.Tasks.Task StopAsync(CancellationToken cancellationToken)
        {
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}

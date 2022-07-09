using MediatR;
using Microsoft.EntityFrameworkCore;
using Salesflow.Task.Three.DataAccess.Entity;
using TaskStatus = Salesflow.Task.Three.DataAccess.Entity.TaskStatus;

namespace Salesflow.Task.Three.DataAccess.Query
{
    public class GetWorkerTaskHandler : IRequestHandler<GetWorkerTaskQuery, Tuple<WorkerTask?, DateTime?>>
    {
        private readonly TasksDbContext _dbContext;

        public GetWorkerTaskHandler(TasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tuple<WorkerTask?, DateTime?>> Handle(GetWorkerTaskQuery request, CancellationToken cancellationToken)
        {
            var task = request.WorkerTask;

            if (task is null)
            {
                return await GetNewClientTask(cancellationToken);
            }

            var newTask = await _dbContext.Tasks
                .AsNoTracking()
                .Where(x => x.ClientId == task.ClientId)
                .OrderBy(x => x.TaskId)
                .FirstOrDefaultAsync(x => x.TaskStatus == TaskStatus.New, cancellationToken: cancellationToken);

            if (newTask is null)
            {
                return await GetNewClientTask(cancellationToken);
            }

            return new Tuple<WorkerTask?, DateTime?>(newTask, task.LastUpdated?.AddMilliseconds(task.WaitingTime));
        }

        private async Task<Tuple<WorkerTask?, DateTime?>> GetNewClientTask(CancellationToken cancellationToken)
        {
            var q = _dbContext.Tasks
                .GroupBy(x => x.ClientId)
                .Where(x => x.All(y => y.TaskStatus != TaskStatus.InProgress))
                .Where(x => x.Any(y => y.TaskStatus == TaskStatus.New))
                .Select(x => x.Key)
                .ToQueryString();

            var clientId = await _dbContext.Tasks
                .GroupBy(x => x.ClientId)
                .Where(x => x.All(y => y.TaskStatus != TaskStatus.InProgress))
                .Where(x => x.Any(y => y.TaskStatus == TaskStatus.New))
                .Select(x => x.Key)
                .FirstOrDefaultAsync(cancellationToken);

            if (clientId == 0)
            {
                return new Tuple<WorkerTask?, DateTime?>(null, null);
            }

            var lastFinishedTaskFromNewClient = await _dbContext.Tasks
                .AsNoTracking()
                .Where(x => x.ClientId == clientId)
                .Where(x => x.TaskStatus == TaskStatus.Success || x.TaskStatus == TaskStatus.Failed)
                .OrderByDescending(x => x.TaskId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var newTask = await _dbContext.Tasks
                .AsNoTracking()
                .Where(x => x.ClientId == clientId)
                .OrderBy(x => x.TaskId)
                .FirstOrDefaultAsync(x => x.TaskStatus == TaskStatus.New, cancellationToken: cancellationToken);

            return new Tuple<WorkerTask?, DateTime?>(newTask, lastFinishedTaskFromNewClient?.LastUpdated?.AddMilliseconds(lastFinishedTaskFromNewClient.WaitingTime));
        }
    }
}

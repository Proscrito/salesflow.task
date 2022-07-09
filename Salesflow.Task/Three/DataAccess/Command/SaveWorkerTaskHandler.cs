using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Salesflow.Task.Three.DataAccess.Command
{
    public class SaveWorkerTaskHandler : IRequestHandler<SaveWorkerTaskCommand>
    {
        private readonly TasksDbContext _dbContext;

        public SaveWorkerTaskHandler(TasksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(SaveWorkerTaskCommand request, CancellationToken cancellationToken)
        {
            var task = request.WorkerTask;
            task.LastUpdated = DateTime.Now;
            _dbContext.Entry(task).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

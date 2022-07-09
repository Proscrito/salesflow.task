using Salesflow.Task.Three.DataAccess.Entity;

namespace Salesflow.Task.Three
{
    public interface ITaskQueue
    {
        Task<Tuple<WorkerTask?, DateTime?>> GetNext(WorkerTask? workerTask, CancellationToken cancellationToken);
    }
}

using MediatR;
using Salesflow.Task.Three.DataAccess.Entity;

namespace Salesflow.Task.Three.DataAccess.Command
{
    public class SaveWorkerTaskCommand : IRequest
    {
        public SaveWorkerTaskCommand(WorkerTask workerTask)
        {
            WorkerTask = workerTask;
        }

        public WorkerTask WorkerTask { get; set; }
    }
}

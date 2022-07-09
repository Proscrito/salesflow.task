using MediatR;
using Salesflow.Task.Three.DataAccess.Entity;

namespace Salesflow.Task.Three.DataAccess.Query
{
    public class GetWorkerTaskQuery : IRequest<Tuple<WorkerTask?, DateTime?>>
    {
        public WorkerTask? WorkerTask { get; set; }
    }
}

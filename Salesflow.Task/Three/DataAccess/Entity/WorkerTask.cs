using MediatR;

namespace Salesflow.Task.Three.DataAccess.Entity
{
    public class WorkerTask : IRequest<Unit>
    {
        public const int MaxRetryCount = 3;

        public int TaskId { get; init; }
        public int ClientId { get; init; }
        public string CommandSql { get; init; }
        public int WaitingTime { get; init; }
        public TaskStatus TaskStatus { get; set; }
        public int ErrorCounter { get; set; }
        public DateTime? LastUpdated { get; set; }
        //some additional field to track workers
        public TimeSpan? WorkerExecutionTime { get; set; }
        public int? WorkerInstanceId { get; set; }
    }
}

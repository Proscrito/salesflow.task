using Salesflow.Task.Three.DataAccess.Entity;
using TaskStatus = Salesflow.Task.Three.DataAccess.Entity.TaskStatus;

namespace Salesflow.Task.Three;

public class Worker : IWorker
{
    private static int _workersCount;
    private readonly int _workerId;
    private const int DefaultDelayMs = 1000;

    private readonly ITaskQueue _taskQueue;
    private WorkerTask? _task;

    public Worker(ITaskQueue taskQueue)
    {
        _taskQueue = taskQueue;
        _workerId = Interlocked.Increment(ref _workersCount);
    }

    public async System.Threading.Tasks.Task RunWorkerAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var (task, date) = await _taskQueue.GetNext(_task, cancellationToken);

            if (task is null)
            {
                await System.Threading.Tasks.Task.Delay(DefaultDelayMs, cancellationToken);
                continue;
            }

            _task = task;
            var delay = (date - DateTime.Now) ?? TimeSpan.FromMilliseconds(DefaultDelayMs);

            if (delay.TotalMilliseconds > 0)
            {
                await System.Threading.Tasks.Task.Delay(delay, cancellationToken);
            }

            await ProcessTask(cancellationToken);
        }
    }

    private async System.Threading.Tasks.Task ProcessTask(CancellationToken cancellationToken)
    {
        var rnd = new Random();

        try
        {
            //some long work here
            var workDuration = rnd.Next(1000, 3000);
            await System.Threading.Tasks.Task.Delay(workDuration, cancellationToken);

            _task!.WorkerExecutionTime = TimeSpan.FromMilliseconds(workDuration);
            _task!.WorkerInstanceId = _workerId;
            
            //succeed or not
            if (rnd.Next(100) > 20)
            {
                _task!.TaskStatus = TaskStatus.Success;
            }
            else
            {
                throw new InvalidOperationException("Boomer");
            }
        }
        catch
        {
            //some nuclear f@cup detected, no sense to try this task again just fail it forcible
            if (rnd.Next(100) < 20)
            {
                _task!.TaskStatus = TaskStatus.Failed;
            }
        }
    }
}
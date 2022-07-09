using MediatR;
using Salesflow.Task.Three.DataAccess.Command;
using Salesflow.Task.Three.DataAccess.Entity;
using Salesflow.Task.Three.DataAccess.Query;
using TaskStatus = Salesflow.Task.Three.DataAccess.Entity.TaskStatus;

namespace Salesflow.Task.Three;

public class TaskQueue : ITaskQueue
{
    private readonly IMediator _mediator;
    private readonly SemaphoreSlim _semaphore = new(1);

    public TaskQueue(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Tuple<WorkerTask?, DateTime?>> GetNext(WorkerTask? workerTask, CancellationToken cancellationToken)
    {
        try
        {
            await _semaphore.WaitAsync(cancellationToken);

            if (workerTask is not null)
            {
                if (workerTask.TaskStatus == TaskStatus.InProgress)
                {
                    workerTask.ErrorCounter++;

                    workerTask.TaskStatus = workerTask.ErrorCounter >= WorkerTask.MaxRetryCount
                        ? TaskStatus.Failed
                        : TaskStatus.New;
                }

                _ = await _mediator.Send(new SaveWorkerTaskCommand(workerTask), cancellationToken);
            }

            var (task, startTime) = await _mediator.Send(new GetWorkerTaskQuery { WorkerTask = workerTask }, cancellationToken);

            if (task is not null)
            {
                task.TaskStatus = TaskStatus.InProgress;
                _ = await _mediator.Send(new SaveWorkerTaskCommand(task), cancellationToken);
            }

            return new Tuple<WorkerTask?, DateTime?>(task, startTime);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
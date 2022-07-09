namespace Salesflow.Task.Three
{
    public interface IWorker
    {
        System.Threading.Tasks.Task RunWorkerAsync(CancellationToken cancellationToken);
    }
}

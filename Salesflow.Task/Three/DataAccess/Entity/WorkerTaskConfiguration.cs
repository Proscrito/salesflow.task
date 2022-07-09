using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Salesflow.Task.Three.DataAccess.Entity;

public class WorkerTaskConfiguration : IEntityTypeConfiguration<WorkerTask>
{
    public void Configure(EntityTypeBuilder<WorkerTask> builder)
    {
        builder.HasKey(x => x.TaskId);
        builder.Property(x => x.TaskId).ValueGeneratedOnAdd();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Salesflow.Task.One;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        //moved from attributes mapping for consistency
        builder.HasKey(x => x.AccountId);
        builder.Property(x => x.AccountId).ValueGeneratedOnAdd();
        builder.Ignore(x => x.Password);

        builder
            .Property<string>("PasswordEncrypted")
            .HasField("_passwordEncrypted")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
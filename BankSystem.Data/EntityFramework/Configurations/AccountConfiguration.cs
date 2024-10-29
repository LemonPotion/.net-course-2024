using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BankSystem.Data.EntityFramework.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("accounts");

        builder.HasKey(a => a.Id)
            .HasName("account_id");

        builder.Property(a => a.Amount)
            .HasColumnName("amount")
            .IsRequired();

        builder.Property(a => a.UpdatedOn)
            .ValueGeneratedOnUpdate()
            .HasColumnName("updated_on");

        builder.HasOne<Client>(a => a.Client)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.ClientId);

        builder.HasOne<Currency>(a => a.Currency)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.CurrencyId);
    }
}
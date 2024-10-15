using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityFramework.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currencies");

        builder.HasKey(c => c.Id)
            .HasName("currency_id");

        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(3)
            .HasColumnName("code");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("name");

        builder.Property(c => c.Symbol)
            .HasMaxLength(5)
            .HasColumnName("symbol");

        builder.HasMany(c => c.Accounts)
            .WithOne(a => a.Currency)
            .HasForeignKey(a => a.CurrencyId);
    }
}
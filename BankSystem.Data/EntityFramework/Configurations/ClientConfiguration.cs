using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityFramework.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(c => c.Id)
            .HasName("client_id");
        
        builder.HasIndex(c => c.PassportNumber)
            .IsUnique();
        
        builder.Property(с => с.FirstName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("first_name")
            .IsRequired();
        
        builder.Property(с => с.LastName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("last_name")
            .IsRequired();
        
        builder.Property(с => с.BirthDay)
            .IsRequired()
            .HasColumnName("birth_date");
        
        builder.Property(c => c.PhoneNumber)
            .HasColumnName("phone_number");
        
        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(254)
            .HasColumnName("email");
        
        builder.Property(c => c.PassportNumber)
            .HasColumnName("passport_number")
            .IsRequired();

        builder.HasMany(c => c.Accounts)
            .WithOne(a => a.Client)
            .HasForeignKey(a => a.ClientId);
    }
}
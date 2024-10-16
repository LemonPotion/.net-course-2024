using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Data.EntityFramework.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");

        builder.HasKey(e => e.Id)
            .HasName("employee_id");

        builder.HasIndex(e => e.PassportNumber)
            .IsUnique();

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("first_name")
            .IsRequired();

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("last_name")
            .IsRequired();

        builder.Property(e => e.BirthDay)
            .IsRequired()
            .HasColumnName("birth_date");

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("phone_number");

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(254)
            .HasColumnName("email");

        builder.Property(e => e.PassportNumber)
            .HasColumnName("passport_number")
            .IsRequired();

        builder.Property(e => e.Contract)
            .IsRequired()
            .HasColumnName("contract");

        builder.Property(e => e.Salary)
            .IsRequired()
            .HasColumnName("salary");
    }
}
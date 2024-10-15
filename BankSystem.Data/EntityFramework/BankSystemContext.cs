using System.Reflection;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.EntityFramework;

public class BankSystemContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    
    public DbSet<Account> Accounts => Set<Account>();
    
    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Currency> Currencies => Set<Currency>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword; Database=postgres;");
    }
}
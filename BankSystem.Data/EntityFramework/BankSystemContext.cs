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

    public BankSystemContext()
    {
        
    }
    
    public BankSystemContext(DbContextOptions<BankSystemContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
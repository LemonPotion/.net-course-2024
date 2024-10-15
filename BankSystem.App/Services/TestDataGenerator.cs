using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.App.Services;

public class TestDataGenerator
{
    private readonly Faker _faker = new Faker();
    
    private readonly Faker<Employee> _employeeFaker = new Faker<Employee>()
        .RuleFor(e => e.BirthDay, f => f.Date.Past(60, DateTime.Now.AddYears(-18)).ToUniversalTime())  
        .RuleFor(e => e.Contract, f => f.Lorem.Sentence())
        .RuleFor(e => e.Email, f => f.Internet.Email())
        .RuleFor(e => e.FirstName, f => f.Name.FirstName())
        .RuleFor(e => e.LastName, f => f.Name.LastName())
        .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber())
        .RuleFor(e => e.PassportNumber, f => f.Random.Guid().ToString())
        .RuleFor(e => e.Salary, f => f.Random.Decimal())
        .RuleFor(e => e.Contract, f => f.Lorem.Text());
    
    private readonly Faker<Account> _accountFaker = new Faker<Account>()
        .RuleFor(a => a.Currency, f => 
            new Currency(
                f.Finance.Currency().Code, 
                f.Finance.Currency().Description, 
                f.Finance.Currency().Symbol))
        .RuleFor(a => a.Amount, f => f.Random.Int());

    private readonly Faker<Client> _clientFaker = new Faker<Client>()
        .RuleFor(c => c.BirthDay, f => f.Date.Past(60, DateTime.Now.AddYears(-18)).ToUniversalTime())
        .RuleFor(c => c.BankAccountNumber, f => f.Finance.Account())
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.FirstName, f => f.Name.FirstName())
        .RuleFor(c => c.LastName, f => f.Name.LastName())
        .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
        .RuleFor(c => c.PassportNumber, f => f.Random.Guid().ToString());

    public List<Client> GenerateClients(int count = 1000)
    {
        var clients = new List<Client>();
        for (var i = 0; i < count; i++)
        {
            var client = _clientFaker.Generate();
            clients.Add(client);
        }

        return clients;
    }

    public Dictionary<string, Client> GenerateClientsDictionary(int count = 1000)
    {
        var clients = new Dictionary<string, Client>();
        for (var i = 0; i < count; i++)
        {
            var client = _clientFaker.Generate();
            clients.Add(client.PhoneNumber, client);
        }

        return clients;
    }
    
    public List<Employee> GenerateEmployees(int count = 1000)
    {
        var employees = new List<Employee>();
        for (var i = 0; i < count; i++)
        {
            var employee = _employeeFaker.Generate();
            employees.Add(employee);
        }

        return employees;
    }

    public Dictionary<Client, Account> GenerateClientAccount(int count = 1000)
    {
        var dictionary = new Dictionary<Client, Account>();
        for (var i = 0; i < count; i++)
        {
            var client = _clientFaker.Generate();

            var account = new Account()
            {
                Amount = _faker.Random.Int(),
                Currency = new Currency(
                    _faker.Finance.Currency().Code, 
                    _faker.Finance.Currency().Description, 
                    _faker.Finance.Currency().Symbol)
            };
            dictionary.Add(client, account);
        }

        return dictionary;
    }

    public Dictionary<Client, List<Account>> GenerateClientAccounts(int count = 1000)
    {
        var dictionary = new Dictionary<Client, List<Account>>();
        for (var i = 0; i < count; i++)
        {
            var client = _clientFaker.Generate();
            
            var accounts = new List<Account>();
            for (var j = 0; j < 10; j++)
            {
                var account = new Account()
                {
                    Amount = _faker.Random.Int(),
                    Currency = new Currency(
                        _faker.Finance.Currency().Code, 
                        _faker.Finance.Currency().Description, 
                        _faker.Finance.Currency().Symbol)
                };
                accounts.Add(account);
            }

            dictionary.Add(client, accounts);
        }

        return dictionary;
    }
    
    public List<Account> GenerateAccounts(int count = 1000)
    { 
        return _accountFaker.Generate(count);
    }
}
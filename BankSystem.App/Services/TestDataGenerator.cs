using BankSystem.Domain.Models;
using Bogus;

namespace BankSystem.App.Services;

public class TestDataGenerator
{
    private readonly Faker _faker = new Faker();
    public List<Client> GenerateClients()
    {
        var clients = new List<Client>();
        for (var i = 0; i < 1000; i++)
        {
            var client = new Client()
            {
                BirthDay = _faker.Date.Past(60, DateTime.Now.AddYears(-18)),
                BankAccountNumber = _faker.Random.Int().ToString(),
                Email = _faker.Internet.Email(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
                PassportNumber = _faker.Random.Int().ToString()
            };
            clients.Add(client);
        }

        return clients;
    }

    public Dictionary<string, Client> GenerateClientsDictionary()
    {
        var clients = new Dictionary<string, Client>();
        for (var i = 0; i < 1000; i++)
        {
            var client = new Client()
            {
                BirthDay = _faker.Date.Past(60, DateTime.Now.AddYears(-18)),
                BankAccountNumber = _faker.Random.Int().ToString(),
                Email = _faker.Internet.Email(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
                PassportNumber = _faker.Random.Int().ToString()
            };
            clients.Add(client.PhoneNumber, client);
        }

        return clients;
    }
    
    public List<Employee> GenerateEmployees()
    {
        var employees = new List<Employee>();
        for (var i = 0; i < 1000; i++)
        {
            var employee = new Employee()
            {
                BirthDay = _faker.Date.Past(60, DateTime.Now.AddYears(-18)),
                Contract = _faker.Lorem.Text(),
                Email = _faker.Internet.Email(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
                Salary = _faker.Random.Decimal(),
                PassportNumber = _faker.Random.Int().ToString()
            };
            employees.Add(employee);
        }

        return employees;
    }

    public Dictionary<Client, Account> GenerateClientAccount()
    {
        var dictionary = new Dictionary<Client, Account>();
        for (var i = 0; i < 1000; i++)
        {
            var client = new Client()
            {
                BirthDay = _faker.Date.Past(60, DateTime.Now.AddYears(-18)),
                BankAccountNumber = _faker.Random.Int().ToString(),
                Email = _faker.Internet.Email(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
                PassportNumber = _faker.Random.Int().ToString()
            };

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

    public Dictionary<Client, List<Account>> GenerateClientAccounts()
    {
        var dictionary = new Dictionary<Client, List<Account>>();
        for (var i = 0; i < 1000; i++)
        {
            var client = new Client()
            {
                BirthDay = _faker.Date.Past(60, DateTime.Now.AddYears(-18)),
                BankAccountNumber = _faker.Random.Int().ToString(),
                Email = _faker.Internet.Email(),
                FirstName = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
                PassportNumber = _faker.Random.Int().ToString()
            };
            
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
    
    public List<Account> GenerateAccounts()
    {
        var accountFaker = new Faker<Account>()
            .RuleFor(a => a.Currency, f => new Currency(f.Finance.Currency().Code, f.Finance.Currency().Symbol, f.Finance.Currency().Description))
            .RuleFor(a => a.Amount, f => f.Random.Int());
        return accountFaker.Generate(1000);
    }
}
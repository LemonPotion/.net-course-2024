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
                BirthDay = _faker.Date.Past(),
                BankAccountNumber = _faker.Random.Int().ToString(),
                Email = _faker.Internet.Email(),
                Firstname = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
            };
            clients.Add(client);
        }
        return clients;
    }
    
    public Dictionary<string,Client> GenerateClientsDictionary()
    {
        var clients = new Dictionary<string, Client>();
        for (var i = 0; i < 1000; i++)
        {
            var client = new Client()
            {
                BirthDay = _faker.Date.Past(),
                BankAccountNumber = _faker.Random.Int().ToString(),
                Email = _faker.Internet.Email(),
                Firstname = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
            };
            clients.Add(client.PhoneNumber,client);
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
                BirthDay = _faker.Date.Past(),
                Contract = _faker.Lorem.Text(),
                Email = _faker.Internet.Email(),
                Firstname = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
                Salary = _faker.Random.Decimal()
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
                BirthDay = _faker.Date.Past(),
                BankAccountNumber = _faker.Random.Int().ToString(),
                Email = _faker.Internet.Email(),
                Firstname = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
            };

            var account = new Account()
            {
                Amount = _faker.Random.Int(),
                Currency = new Currency(
                    _faker.Finance.Currency().Code, 
                    _faker.Finance.Currency().Description, 
                    _faker.Finance.Currency().Symbol)
            };
            dictionary.Add(client,account);
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
                BirthDay = _faker.Date.Past(),
                BankAccountNumber = _faker.Random.Int().ToString(),
                Email = _faker.Internet.Email(),
                Firstname = _faker.Name.FirstName(),
                LastName = _faker.Name.LastName(),
                PhoneNumber = _faker.Phone.PhoneNumber(),
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
            dictionary.Add(client,accounts);
        }
        return dictionary;
    }
}
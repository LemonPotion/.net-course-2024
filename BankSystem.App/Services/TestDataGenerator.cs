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
                BankAccountNumber = _faker.Random.Int(),
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
                BankAccountNumber = _faker.Random.Int(),
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
}

namespace BankSystem.Domain.Models;

public class Person
{
    public string Firstname { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime BirthDay { get; set; }
    
    public int Age => DateTime.Now.Year - BirthDay.Year;
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
}

namespace BankSystem.Domain.Models;

public class Person
{
    public string Firstname { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime BirthDay { get; set; }
    
    public int Age => DateTime.Now.Year - BirthDay.Year;
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        else if (obj is not Person entity)
            return false;
        else if (entity.Firstname != Firstname)
            return false;
        else if (entity.LastName != LastName)
            return false;
        else if (entity.BirthDay != BirthDay)
            return false;
        return true;
    }

    public override int GetHashCode()
    {
        return Firstname.GetHashCode() + LastName.GetHashCode() + BirthDay.GetHashCode();
    }
    
}
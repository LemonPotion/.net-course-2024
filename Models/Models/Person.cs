﻿namespace BankSystem.Domain.Models;

public class Person
{
    public Guid Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime BirthDay { get; set; }
    
    public int Age => DateTime.Now.Year - BirthDay.Year;
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    public string PassportNumber { get; set; }
    
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        else if (obj is not Person entity)
            return false;
        else if (entity.Id != Id)
            return false;
        return true;
    }

    public override int GetHashCode()
    {
        return PassportNumber.GetHashCode();
    }
}
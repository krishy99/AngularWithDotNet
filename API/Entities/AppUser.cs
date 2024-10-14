using System.ComponentModel;

namespace API.Entities;

public class AppUser
{
    public int Id{get;set;}

    public required string FirstName{get;set;}
    public string LastName{get;set;}
    public required byte[] PasswordHash{get;set;}
    public required byte[] PasswordSalt{get;set;}

}

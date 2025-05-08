namespace MakersBnB.Models;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int ID {get; set;}
    public required string Username {get; set;}
    public required string Email {get; set;}
    public required string Password {get; set;}

    public User(string username, string email, string password)
    {
        this.Username = username;
        this.Email = email;
        this.Password = password;
    }

    public User(){}
   
}
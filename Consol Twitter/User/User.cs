using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consol_Twitter.User;

public class User
{
    public string userName {  get; set; }
    public string Password { get; set; }
    public string Email {  get; set; }

    public User() { }
    public User(string userName, string password, string email)
    {
        this.userName = userName;
        this.Password = password;
        this.Email = email; 
    }
    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(Email);  
    }
    public void ShowUserProfile()
    {
        Console.WriteLine("User Name: " + userName);
        Console.WriteLine("Email: " + Email);
        Console.WriteLine("-----------------------------");
    }
}

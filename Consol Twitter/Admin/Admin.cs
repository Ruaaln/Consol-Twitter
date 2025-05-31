namespace Consol_Twitter.Admin;

using System.Drawing;
using System.Text.Json.Serialization;
using Consol_Twitter.Consol_Help;
using Consol_Twitter.Post;
using static System.Net.Mime.MediaTypeNames;


internal class Admin 
{

    public string AdminName { get; set; }
    public string AdminPassword { get; set; }
    public string AdminEmail { get; set; }
    public List<Post> Posts { get; set; } = new();


    public Admin() { }
    public Admin(string name, string password, string email)
    {
        this.AdminName = name;
        this.AdminPassword = password;
        this.AdminEmail = email;
    }
    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(AdminEmail); 
    }
    public void ShowAdminProfil()
    {
        
        Console.WriteLine("Admin Name: " + AdminName);
        Console.WriteLine("Admin Email: " + AdminEmail);
        Console.WriteLine("Posts Count: " + Posts.Count);
        Console.WriteLine("-----------------------------");
    }

    public void AddPost(Post post)
    {
        if (post != null)
        {
            Posts.Add(post);
            Console.WriteLine("Post added successfully.");
        }
        else
        {
            Console.WriteLine("Post cannot be null.");
        }
    }
    public void RemovePost(string postId)
    {
        var post = Posts.FirstOrDefault(p => p.id == postId);
        if (post != null)
        {
            Posts.Remove(post);
            Console.WriteLine("Post removed successfully.");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
    }
    public void UpdatePost(string postId, string newContent)
    {
        var post = Posts.FirstOrDefault(p => p.id == postId);
        if (post != null)
        {
            post.Content = newContent;
            Console.WriteLine("Post updated successfully.");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
    }
}

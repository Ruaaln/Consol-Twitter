using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consol_Twitter.Post;
using Consol_Twitter.Comeand;

internal class Post
{
    public string id = Guid.NewGuid().ToString();
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; } = 0;
    public int Shares { get; set; } = 0;

    public List<Comment> Comments { get; set; } = new();

    public Post(string content)
    {
        Content = content;
        CreatedAt = DateTime.Now;
    }

}

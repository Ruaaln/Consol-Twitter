using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Consol_Twitter.Comeand;
using Consol_Twitter.User;

internal class Comment
{
    //16 karakterli id olmalidir, 0dan baslamamalidir
    public string Id = Guid.NewGuid().ToString();
    public string Text { get; set; }
    public User Author { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Comment(string text, User author)
    {
        Text = text;
        Author = author;
    }
}

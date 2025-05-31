using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Consol_Twitter.Admin;
using Consol_Twitter.Comeand;
using Consol_Twitter.Consol_Help;
using Consol_Twitter.NetworkNamespace;
using Consol_Twitter.Post;
using Consol_Twitter.User;

//📄 Program.cs                                                            
//│                                                                        
//├───── Classlar
//│      │
//│      ├─── Admin.cs
//│      │   │
//│      │   ├── IsEmpty() - Admin emailinin boş olub olmadığını yoxlayır
//│      │   │
//│      │   ├── ShowAdminProfil() - Admin profilini göstərir
//│      │   │
//│      │   ├── ShowAdminPosts() - Adminin postlarını göstərir
//│      │   │
//│      │   ├── AddPost(Post post) - Yeni post əlavə edir
//│      │   │
//│      │   ├── RemovePost(string postId) - Postu silir
//│      │   │
//│      │   └── UpdatePost(string postId, string newContent) - Postu yeniləyir
//│      │
//│      ├─── Commend.cs
//│      │   │
//│      │   └── Comment(string text, User author) - Yeni şərh yaradır
//│      │
//│      ├─── Consol_Help.cs
//│      │   │
//│      │   └── PrintColored(string message, ConsoleColor color) - Konsolda rəngli mesaj çap edir
//│      │
//│      ├─── Network.cs
//│      │   │
//│      │   ├── SendEmail(string toEmail, string subject, string body) - Email göndərir
//│      │   │
//│      │   ├── GenerateConfirmationCode(string email) - Email təsdiq kodu yaradır və göndərir
//│      │   │
//│      │   ├── NewLike(string adminEmail, string postContent, string userName) - Yeni bəyənmə bildirişi göndərir
//│      │   │
//│      │   └── NewComment(string adminEmail, string postContent, string userName, string commentText) - Yeni şərh bildirişi göndərir
//│      │
//│      ├─── Post.cs
//│      │   │
//│      │   └── Post(string content) - Yeni post yaradır
//│      │
//│      └─── User.cs
//│          │
//│          └── User(string userName, string password, string email) - Yeni istifadəçi yaradır
//│
//├───── Methodlar
//│      │
//│      ├── AdminPanel(Admin admin) - Admin panelini göstərir
//│      │
//│      ├── UserPanel(User user) - İstifadəçi panelini göstərir
//│      │
//│      ├── Main(string[] args) - Proqramın giriş nöqtəsi
//│      │
//│      ├── ReadAdmins() - Adminləri oxuyur
//│      │
//│      ├── WriteAdmins(List<Admin> admins) - Adminləri yazır
//│      │
//│      ├── ReadUsers() - İstifadəçiləri oxuyur
//│      │
//│      ├── WriteUsers(List<User> users) - İstifadəçiləri yazır
//│      │
//│      └── IsEmail(string? email) - Email ünvanının düzgünlüyünü yoxlayır
//│
//│

class Program
{
    static void AdminPanel(Admin admin)
    {
        Console.Clear();
        string indexer = "1";
        do
        {
            Console.Clear();
            Console.WriteLine($"===================== Admin: {admin.AdminName} =====================\n\n\n\n");
            var admins = ReadAdmins();
            admin.ShowAdminProfil();

            int I = 0; //adminin indexini tapmaq üçün
            foreach (var e in admins)
            {
                if (e.AdminEmail == admin.AdminEmail)
                {
                    break;
                }
                I++;
            }

            //menyu 
            Console.WriteLine("[1] - Show All post");
            Console.WriteLine("[2] - Add post");
            Console.WriteLine("[3] - Delet post");
            Console.WriteLine("[4] - Edit post");
            Console.WriteLine("[5] - Exit");

            Console.Write("Secim daxil et: ");
            char index = Console.ReadKey(true).KeyChar;

            Console.Clear();
            switch (index)
            {
                case '1':
                    foreach (var p in admins[I].Posts)
                    {
                        Console.WriteLine($"\n├───ID─>> {p.id} ├─────────────────────────────────────>>>");
                        ConsoleHelper.PrintColored($"    │", ConsoleColor.Yellow);
                        ConsoleHelper.PrintColored($"    └─|Comment>─>> {p.Content}\n", ConsoleColor.Yellow);
                    }
                    Console.WriteLine("├------------------------------------------------------------");
                    break;
                case '2':
                    Console.Clear();
                    Console.WriteLine("Post Conted: ");
                    string postConted = Console.ReadLine() ?? string.Empty;
                    if (postConted.Length > 2)
                    {
                        var post = new Post(postConted);
                        admins[I].AddPost(post);
                        WriteAdmins(admins);
                        Console.WriteLine("Elave olundu!");
                        Console.WriteLine();
                    }
                    else
                    {
                        ConsoleHelper.PrintColored("Post metni 2 simvoldan cox olmalidir!", ConsoleColor.Red);
                    }
                    Console.WriteLine("Davam etmey ucun ENTER..");
                    Console.ReadKey();
                    break;
                case '3':
                    Console.Clear();
                    Console.WriteLine("======================== DELET POST ========================");
                    foreach (var p in admins[I].Posts)
                    {
                        Console.WriteLine("------------------------------------------------------------");
                        Console.WriteLine($"{p.id}: {p.Content}");
                    }
                    Console.Write("Enter post Id: ");
                    string? postId = Console.ReadLine();
                    if (string.IsNullOrEmpty(postId))
                    {
                        ConsoleHelper.PrintColored("Post ID boş ola bilməz!", ConsoleColor.Red);
                        continue;
                    }
                    if (!admins[I].Posts.Any(p => p.id == postId))
                    {
                        ConsoleHelper.PrintColored("Bu ID-yə aid post tapılmadı!", ConsoleColor.Red);
                        continue;
                    }
                    foreach (var p in admins[I].Posts)
                    {
                        if (p.id == postId)
                        {
                            admins[I].RemovePost(postId);
                            WriteAdmins(admins);
                            ConsoleHelper.PrintColored("Post silindi!", ConsoleColor.Green);
                            break;
                        }

                    }
                    break;
                case '4':
                    Console.Clear();
                    Console.WriteLine("======================== update POST ========================");
                    foreach (var p in admins[I].Posts)
                    {
                        Console.WriteLine("------------------------------------------------------------");
                        Console.WriteLine($"{p.id}: {p.Content}");
                    }
                    Console.WriteLine("Enter post Id: ");
                    string? deletPostId = Console.ReadLine();
                    if (string.IsNullOrEmpty(deletPostId))
                    {
                        ConsoleHelper.PrintColored("Post ID boş ola bilməz!", ConsoleColor.Red);
                        continue;
                    }
                    if (!admins[I].Posts.Any(p => p.id == deletPostId))
                    {
                        ConsoleHelper.PrintColored("Bu ID-yə aid post tapılmadı!", ConsoleColor.Red);
                        continue;
                    }

                    foreach (var p in admins[I].Posts)
                    {
                        if (p.id == deletPostId)
                        {
                            Console.Clear();
                            Console.WriteLine(p.Content);
                            Console.WriteLine("\nYeni metin");
                            string newContend = Console.ReadLine() ?? string.Empty;

                            admins[I].UpdatePost(deletPostId, newContend);
                            ConsoleHelper.PrintColored("Post update edildi!", ConsoleColor.Green);
                            WriteAdmins(admins);
                            break;
                        }

                    }
                    break;
                case '5':
                    indexer = "0";
                    break;
                default:
                    continue;
            }

            Console.WriteLine("Davam etmey ucun ENTER..");
            Console.ReadLine();




        } while (indexer != "0");


    }
    static void UserPanel(User user)
    {
        Console.Clear();
        string indexer = "1";
        var users = ReadUsers();
        var admins = ReadAdmins();

        var msgNetwork = new Network();
        bool x = true;
        while (x)
        {
            foreach (var A in admins)
            {
                if (!x)
                    break;
                foreach (var post in A.Posts)
                {
                    if (!x)
                        break;
                    Console.Clear();
                    post.Shares++;
                    ConsoleHelper.PrintColored($"=<< {A.AdminName} >>===============================================================", ConsoleColor.Magenta);
                    ConsoleHelper.PrintColored($"\n{post.Content}", ConsoleColor.White);
                    ConsoleHelper.PrintColored($"\n\n=< {post.CreatedAt} >===< Comment: {post.Comments.Count} >==< Like: {post.Likes} >==< Reyting: {post.Shares} >========", ConsoleColor.Magenta);
                    ConsoleHelper.PrintColored("[c] - Comment | [n] - Newxt | [l] - Like | [0] Exit\n", ConsoleColor.DarkBlue);


                    var key = Console.ReadKey(true).KeyChar;

                    switch (key)
                    {
                        case 'c':
                            Console.Clear();
                            ConsoleHelper.PrintColored("\n=<< Comments >>=================================================================\n", ConsoleColor.Blue);
                            foreach (var comment in post.Comments)
                            {
                                ConsoleHelper.PrintColored($"@{comment.Author.userName}", ConsoleColor.Cyan);
                                ConsoleHelper.PrintColored($"\b  │", ConsoleColor.Cyan);
                                ConsoleHelper.PrintColored($"  └─>> {comment.Text}", ConsoleColor.Cyan);
                                ConsoleHelper.PrintColored($"----------------------------------------------------<< {comment.CreatedAt} >>-\n", ConsoleColor.DarkGray);
                            }
                            Console.WriteLine("\n[1]Add Command\n[0] back");
                            char n = Console.ReadKey(true).KeyChar ;
                            if (n == '1')
                            {
                                Console.WriteLine("Commend yaz: ");
                                string commant = Console.ReadLine() ?? string.Empty;
                                var com = new Comment(commant, user);
                                post.Comments.Add(com);
                                msgNetwork.NewComment(A.AdminEmail, post.Content, user.userName, com.Text);
                            }
                            break;
                        case 'n':
                            break;
                        case 'l':
                            post.Likes++;
                            ConsoleHelper.PrintColored("Like gonderilir..", ConsoleColor.Green);
                            msgNetwork.NewLike(A.AdminEmail, post.Content, user.userName);
                            break;
                        case '0':
                            x = false;
                            break;
                        default:
                            break;

                    }
                }
            }
            ConsoleHelper.PrintColored("Butun pustlari gordun cixmag isdeyirsen? [y/n]:", ConsoleColor.Red);
            if (Console.ReadKey(true).KeyChar == 'y')
            {
                x = false;
            }
            else
            {
                x = true;
                Console.Clear();
            }
            //write e;le butun datalari
            WriteAdmins(admins);
            WriteUsers(users);

        }



    }
    static void Main(string[] args)
    {

        while (true)
        {
            Console.Clear();
            ///////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("========================== Consol Twitter ==========================\n\n\n\n");
            ConsoleHelper.PrintColored("Position sec\n\n[1] Admin \n[2] User\n", ConsoleColor.Cyan);
            char index = Console.ReadKey(true).KeyChar;
            Console.Clear();

            if (index == '1' || index == '2')
            {
                if (index == '1')
                    Console.WriteLine("====================== Login [Admin] =======================\n\n\n\n");
                else
                    Console.WriteLine("====================== Login [User] =======================\n\n\n\n");

                Console.Write("\n\nEmail adres: ");
                string? email = Console.ReadLine();


                if (!IsEmail(email))
                {
                    ConsoleHelper.PrintColored("Email düzgün deyil!", ConsoleColor.Red);
                    continue;
                }
                else
                {
                    String GetCode = "1";
                    if (index == '1')
                    {
                        var admins = ReadAdmins();
                        foreach (var admin in admins)
                        {
                            if (admin.AdminEmail == email)
                            {
                                GetCode = "0";
                            }
                        }

                    }
                    else
                    {
                        var users = ReadUsers();
                        foreach (var user in users)
                        {
                            if (user.Email == email)
                            {
                                GetCode = "0";
                            }
                        }
                    }



                    if (GetCode == "1")
                    {
                        var network = new Network();
                        ConsoleHelper.PrintColored($"Email: {email} ", ConsoleColor.Yellow);
                        ConsoleHelper.PrintColored(" emaili təsdiqləmək üçün 6 rəqəmli kod göndərildi!", ConsoleColor.Green);
                        string code = network.GenerateConfirmationCode(email);
                        Console.Write($"{email} kodu daxil edin \nKod: ");
                        string inputCode = Console.ReadLine() ?? string.Empty;
                        if (inputCode != code)
                        {
                            ConsoleHelper.PrintColored("Kod yanlisdir!", ConsoleColor.Red);
                            Console.WriteLine("Davam etmey ucun enter");
                            Console.ReadKey();
                            continue;
                        }
                    }




                    if (index == '1')
                    {

                        var admins = ReadAdmins();

                        var adminData = new Admin();
                        foreach (var admin in admins)
                        {
                            if (admin.AdminEmail == email)
                            {
                                adminData = admin;
                                continue;
                            }
                        }
                        if (!adminData.IsEmpty())
                        {

                            Console.Write("Password: ");
                            string? inputPassword = Console.ReadLine();
                            if (inputPassword == adminData.AdminPassword)
                            {
                                AdminPanel(adminData);
                            }
                            else
                            {
                                ConsoleHelper.PrintColored("Şifrə yanlışdır!", ConsoleColor.Red);
                                Console.WriteLine("Davam etmey ucun enter");
                                Console.ReadKey();
                                continue;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(@$"Email: {email} ");
                            Console.Write("Name: ");
                            string? name = Console.ReadLine();
                            Console.Write("Password: ");
                            string? password = Console.ReadLine();
                            var newAdmin = new Admin(name, password, email);
                            var adminss = ReadAdmins();
                            adminss.Add(newAdmin);
                            WriteAdmins(adminss);
                            ConsoleHelper.PrintColored("Admin yaradıldı!\n Davam etmey ucun ENTER duymesini basin!", ConsoleColor.Green);
                            Console.ReadKey();
                            AdminPanel(newAdmin);
                        }
                    }
                    else
                    {
                        var users = ReadUsers();
                        var userData = new User();
                        foreach (var user in users)
                        {
                            if (user.Email == email)
                            {
                                userData = user;
                                continue;
                            }
                        }
                        if (!userData.IsEmpty())
                        {

                            Console.Write("Password: ");
                            string? inputPassword = Console.ReadLine();
                            if (inputPassword == userData.Password)
                            {
                                UserPanel(userData);
                            }
                            else
                            {
                                ConsoleHelper.PrintColored("Şifrə yanlışdır!", ConsoleColor.Red);
                                Console.Write("Davam etmey ucun enter");
                                Console.ReadKey();
                                continue;
                            }
                        }
                        else
                        {

                            Console.Clear();
                            Console.WriteLine($"Email: {email} ");
                            Console.Write("Name: ");
                            string name = Console.ReadLine() ?? string.Empty;
                            Console.Write("Password: ");
                            string password = Console.ReadLine() ?? string.Empty;
                            var newUser = new User(name, password, email);
                            var userss = ReadUsers();
                            userss.Add(newUser);
                            WriteUsers(userss);
                            ConsoleHelper.PrintColored("Admin yaradıldı!\n Davam etmey ucun ENTER duymesini basin!", ConsoleColor.Green);
                            Console.ReadKey();
                            UserPanel(newUser);
                        }
                    }
                    break;
                }
            }
            else if (index == '0')
            {
                ConsoleHelper.PrintColored("Cixis etdiniz.", ConsoleColor.Red);
                break;
            }
            else
            {
                ConsoleHelper.PrintColored("Duzgun secim edin!", ConsoleColor.Red);
            }

        }
    }
    static List<Admin> ReadAdmins()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var readData = File.ReadAllText(@"C:\Users\USER\Documents\VSC & VSC 2022\VSC 2022\Step\Consol Twitter\Consol Twitter\Mydata\Admins.json");
        var admins = JsonSerializer.Deserialize<List<Admin>>(readData, options);

        return admins ?? new List<Admin>();
    }
    static void WriteAdmins(List<Admin> admins)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var jsonData = JsonSerializer.Serialize(admins, options);
        File.WriteAllText(@$"C:\Users\USER\Documents\VSC & VSC 2022\VSC 2022\Step\Consol Twitter\Consol Twitter\Mydata\Admins.json", jsonData);
    }

    static List<User> ReadUsers()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var readData = File.ReadAllText(@"C:\Users\USER\Documents\VSC & VSC 2022\VSC 2022\Step\Consol Twitter\Consol Twitter\Mydata\Users.json");
        var users = JsonSerializer.Deserialize<List<User>>(readData, options);

        return users ?? new List<User>();
    }
    static void WriteUsers(List<User> users)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var jsonData = JsonSerializer.Serialize(users, options);
        File.WriteAllText(@"C:\Users\USER\Documents\VSC & VSC 2022\VSC 2022\Step\Consol Twitter\Consol Twitter\Mydata\Users.json", jsonData);
    }


    private static bool IsEmail(string? email)
    {
        if (email.EndsWith(".com"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

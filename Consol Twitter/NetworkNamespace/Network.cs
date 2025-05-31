using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Consol_Twitter.NetworkNamespace;

internal class Network
{
    public void SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("ruslanserifov70@gmail.com");
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("ruslanserifov70@gmail.com", "ftrt vxbo pzdr mkjq");
            smtp.EnableSsl = true;

            smtp.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Mail göndərilərkən xəta baş verdi: " + ex.Message);
        }
    }

    //gmail dorulamak icn gonderilen onay kodu 6 rakamli kod

    public string GenerateConfirmationCode(string email)
    {
        Random random = new Random();
        int code = random.Next(100000, 999999);
        SendEmail(email, "confirmation code", $@"Sizin email testiy kodunuz {code}.");
        return code.ToString();
    }

    //admine yeni beyeni olduguni bildiren mesaj

    public void NewLike(string adminEmail, string postContent, string userName)
    {
        string subject = "Yeni Bəyənmə Bildirişi";
        string body = $"{userName} adlı istifadəçi aşağıdakı postu bəyəndi:\n\n{postContent}";
        SendEmail(adminEmail, subject, body);
    }

    public void NewComment(string adminEmail, string postContent, string userName, string commentText)
    {

        string subject = "Yeni Yorum Bildirişi";
        string body = $"{userName} adlı istifadəçi aşağıdakı posta yorum yazdı:\n\nPost: {postContent}\n\nYorum: {commentText}";
        SendEmail(adminEmail, subject, body);
    }

}

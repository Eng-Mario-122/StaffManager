using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.HelperClass
{
    static class EmailSetting
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl=true;
            Client.Credentials=new NetworkCredential("marionady12004@gmail.com", "5051 6058");
            Client.Send("marionady12004@gmail.com", email.Recipients,email.Subject,email.Body);
        }
    }
}

using Store.Domain.Interfaces;
using System.Net;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace Store.Domain.Tools
{
    public class EmailSender
    {
        public static void SendRegistationConfirmingEmail(string email, string url)
        {
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress("nikolashinpaul@yandex.ru", "PetPRO");
            message.To.Add(email);
            message.Subject = "Подтверждение регистрации";
            message.Body = "Для того чтобы закончить регистрацию перейдите по ссылке " +
                           "<a href =\"" + url + "\">" + url + "</a>";
            using (SmtpClient client = new SmtpClient("smtp.yandex.ru"))
            {
                client.Credentials = new NetworkCredential("nikolashinpaul@yandex.ru", "aeaeaafgzvwcwckz");
                client.EnableSsl = true;
                client.Port = 587;
                client.Send(message);
            }
        }
    }
}

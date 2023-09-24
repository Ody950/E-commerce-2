using E_commerce_2.Models.Interface;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Net.Mail;
using System.Net;

namespace E_commerce_2.Models.Services
{
    public class EmailServices : IEmail
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "test_ltuc950@outlook.com";
            var pw = "Odai123456+++";

            using (var client = new SmtpClient("smtp.office365.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(mail, pw);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(mail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = false 
                };

                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
    }




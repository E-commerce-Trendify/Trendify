using SendGrid.Helpers.Mail;
using SendGrid;
using Trendify.Interface;
using System.Configuration;

namespace Trendify.Services
{
    public class EmailService : IEmail
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public    async Task SendEmail(string email, string subject, string htmlMessage)
        {
          
            string apiKey =  _config["SendGrid:Key"];    
            string fromEmail = _config["SendGrid:DefaultFromEmail"];
            string fromName = _config["SendGrid:DefaultFromName"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var subjects = subject;
            var to = new EmailAddress(email);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var msg = MailHelper.CreateSingleEmail(from, to, subjects, plainTextContent, htmlMessage);
            var response = await client.SendEmailAsync(msg);
            //var clinet =new SendGridClient(apiKey);
            //SendGridMessage ms = new SendGridMessage()
            //ms.SetFrom(fromEmail,fromName);
            //ms.AddTo(email);
            //ms.SetSubject(subject);
            //ms.AddContent(MimeType.Html, htmlMessage);

            //await clinet.SendEmailAsync(ms);
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("test@example.com", "Example User");
            //var subject = "Sending with SendGrid is Fun";
            //var to = new EmailAddress("test@example.com", "Example User");
            //var plainTextContent = "and easy to do anywhere with C#.";
            //var htmlContent = "<strong>and easy to do anywhere with C#.</strong>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);
        }
    }
}

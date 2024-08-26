using System.Net;
using System.Net.Mail;
using CleanArchitecture.Application.Abstractions.Email;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Email;

internal sealed class EmailService : IEmailService
{
  private GmailSettings _gmailSettings;
  public EmailService(IOptions<GmailSettings> settings)
  {
    _gmailSettings = settings.Value;
  }
  public void Send(string recipient, string subject, string body)
  {
    try
    {
      var fromEmail = _gmailSettings.Username;
      var fromPW = _gmailSettings.Password;

      var message = new MailMessage();
      message.From = new MailAddress(fromEmail!);
      message.Subject = subject;
      message.To.Add(new MailAddress(recipient));
      message.Body = body;
      message.IsBodyHtml = true;

      var smtp = new SmtpClient("smtp.gmail.com")
      {
        Port = _gmailSettings.Port,
        Credentials = new NetworkCredential(fromEmail, fromPW),
        EnableSsl = true
      };

      smtp.Send(message);
    }
    catch (Exception ex)
    {
      throw new Exception("Error sending email", ex);
    }
  }
}
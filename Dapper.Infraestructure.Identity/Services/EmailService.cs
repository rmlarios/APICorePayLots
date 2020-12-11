using System;
//using System.Net.Mail;
using System.Threading.Tasks;
using Dapper.Application.DTOs.Email;
using Dapper.Application.Exceptions;
using Dapper.Application.Interfaces.Account;
using Dapper.Core.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Dapper.Infraestructure.Identity.Services
{
  public class EmailService : IEmailService
  {
    public MailSettings _mailSettings { get; }

    public EmailService(IOptions<MailSettings> mailSettings)
    {
      _mailSettings = mailSettings.Value;
    }

    public async Task SendAsync(EmailRequest request)
    {
      try
      {
        // create message
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        
        var builder = new BodyBuilder();
        builder.HtmlBody = request.Body;
        email.Body = builder.ToMessageBody();
        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);

      }
      catch (System.Exception ex)
      {
        //_logger.LogError(ex.Message, ex);
        throw new ApiException(ex.Message);
      }
    }
  }
}

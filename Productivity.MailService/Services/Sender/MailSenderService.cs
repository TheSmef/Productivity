using DocumentFormat.OpenXml.Vml;
using Productivity.MailService.Services.Sender.Interfaces;
using Productivity.Shared.Models.Entity;
using System;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using Productivity.Shared.Models.Utility;


namespace Productivity.MailService.Services.Sender
{
    public class MailSenderService : IMailSenderService
    {

        private readonly SMTPConfiguration _configuration;
        public MailSenderService(IOptions<SMTPConfiguration> options)
        {
            _configuration = options.Value;
        }
        public async Task Send(Mail record)
        {
            using var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.Name, _configuration.Email));
            message.To.Add(new MailboxAddress(string.Empty, record.To));
            message.Subject = record.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = record.Body,
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration.Server, _configuration.Port, true);
                await client.AuthenticateAsync(_configuration.Email, _configuration.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}

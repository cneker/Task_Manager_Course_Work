using System;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Hosting;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StraightEdgeServer.Models;
using Task = System.Threading.Tasks.Task;

namespace StraightEdgeServer.Services
{
    public class EmailService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private string _connection = "Server=(localdb)\\mssqllocaldb;Database=TaskManagerCW;Trusted_Connection=True;";

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckingForNotifications, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));
            _logger.LogInformation("Email service started");
            return Task.CompletedTask;
        }

        private async void SendEmail(string email, string taskName)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage()
                {
                    Subject = "Deadline is coming",
                    Body = new BodyBuilder()
                    {
                        HtmlBody =
                            $"<div style=\"font-size: 20px;\">One day left until the end of the task \"{taskName}\"</div>"
                    }.ToMessageBody()
                };
                emailMessage.From.Add(new MailboxAddress("TaskManager", "LookToHelper@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", email));

                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("LookToHelper@gmail.com", "vzvitychlfbqgruc");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);

                _logger.LogInformation($"Message sent to {email}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
            }
        }

        private void CheckingForNotifications(object state)
        {
            _logger.LogInformation("Searching for receivers");
            if (DateTime.Now.Minute % 2 == 0)
            {
                var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(_connection).Options;
                using var db = new ApplicationContext(options);
                var tasks = db.Tasks
                    .Where(t => t.DeadLine.Date == DateTime.Now.Date.AddDays(1) && t.NotificationEnabled)
                    .ToList();
                foreach (var task in tasks)
                {
                    SendEmail(task.UserEmail, task.Name);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Email service terminated");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

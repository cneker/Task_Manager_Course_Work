using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Hosting;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using StraightEdgeServer.Models;
using Task = System.Threading.Tasks.Task;

namespace StraightEdgeServer
{
    public class EmailService : IHostedService, IDisposable
    {
        private Timer _timer;
        private string _connection = "Server=(localdb)\\mssqllocaldb;Database=TaskManagerCW;Trusted_Connection=True;";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CheckingForNotifications, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        private async void SendEmail(string email)
        {
            try
            {
                MimeMessage emailMessage = new MimeMessage()
                {
                    Subject = "Deadline is coming",
                    Body = new BodyBuilder() { HtmlBody = $"<div style=\"font-size: 20px;\">DeadLine</div>" }.ToMessageBody()
                };
                emailMessage.From.Add(new MailboxAddress("TaskManager", "LookToHelper@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("LookToHelper@gmail.com", "vzvitychlfbqgruc");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
                Debug.WriteLine($"Сообщение на почту {email} отправлено успешно!");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.GetBaseException().Message);
            }
        }

        private void CheckingForNotifications(object state)
        {
            if (DateTime.Now.Minute % 2 == 0)
            {
                var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(_connection).Options;
                using var db = new ApplicationContext(options);
                var tasks = db.Tasks.Where(t => t.DeadLine.Date == DateTime.Now.Date.AddDays(1) && t.NotificationEnabled).ToList();
                foreach (var task in tasks)
                {
                    SendEmail(task.UserEmail);
                }
            }
            else
            {
                Debug.WriteLine($"Wait");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StraightEdgeServer.Models;
using Task = System.Threading.Tasks.Task;

namespace StraightEdgeServer.Services
{
    public class DeadlineCheckService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILogger _logger;
        private string _connection = "Server=(localdb)\\mssqllocaldb;Database=TaskManagerCW;Trusted_Connection=True;";

        public DeadlineCheckService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Check, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));
            _logger.LogInformation("Deadline check service started");
            return Task.CompletedTask;
        }

        private void Check(object state)
        {
            _logger.LogInformation("Deadline check");
            try
            {
                if (DateTime.Now.Minute % 2 == 0)
                {
                    var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(_connection).Options;
                    using var db = new ApplicationContext(options);
                    var tasks = db.Tasks.ToList();
                    foreach (var task in tasks)
                    {
                        if (DateTime.Compare(task.DeadLine.Date, DateTime.Now.Date) < 0 && task.IsCompleted == false)
                        {
                            task.IsCompleted = true;
                            db.Tasks.Update(task);
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deadline check service terminated");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

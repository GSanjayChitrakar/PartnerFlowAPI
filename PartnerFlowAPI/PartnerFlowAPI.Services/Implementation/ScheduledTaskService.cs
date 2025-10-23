using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PartnerFlowAPI.Database.Context;
using PartnerFlowAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace PartnerFlowAPI.Services.Implementation
{

    public class ScheduledTaskService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScheduledTaskService> _logger;

        public ScheduledTaskService(IServiceProvider serviceProvider, ILogger<ScheduledTaskService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime now = DateTime.Now;
                DateTime scheduledTime = now.Date.AddHours(10); // Today at 10 AM
                //DateTime scheduledTime = now.AddMinutes(1); // for testing

                if (now > scheduledTime) // If time already passed, schedule for next day
                {
                    scheduledTime = scheduledTime.AddDays(1);
                    //scheduledTime = scheduledTime.AddMinutes(1);
                }

                TimeSpan delay = scheduledTime - now;

                _logger.LogInformation("Scheduled task will run at {ScheduledTime}", scheduledTime);
                await Task.Delay(delay, stoppingToken); // Wait until 10 AM

                try
                {
                    _logger.LogInformation("Executing scheduled task at {ExecutionTime}", DateTime.Now);
                    await RunTask(stoppingToken); // Execute the task
                    _logger.LogInformation("Scheduled task executed successfully at {ExecutionTime}", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error executing scheduled task at {ExecutionTime}", DateTime.Now);
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Wait for next day
               //await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Wait for next day
            }
        }

        private async Task RunTask(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.ExecuteSqlRawAsync("EXEC ApplyApprovedChangeRequest");
            }
        }
    }
}

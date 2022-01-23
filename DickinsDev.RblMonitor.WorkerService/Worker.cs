using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DickinsDev.RblMonitor.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Polling: {time}", DateTimeOffset.Now);
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        byte[] dl = wc.DownloadData("https://rbl.dickins.dev/api/checkall");
                        _logger.LogInformation("Downloaded.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex.Message);
                }
                await Task.Delay(Convert.ToInt32(TimeSpan.FromMinutes(30).TotalMilliseconds), stoppingToken);
            }
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using NCrontab;


namespace ArchiveService
{
    public class ArchiveWorker : BackgroundService
    {
        private readonly ApiClient _client;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<ArchiveWorker> _logger;
        private  string Schedule => "* * */1 * * *";
        

        public ArchiveWorker(ILogger<ArchiveWorker> logger, ApiClient client)
        {
            _logger = logger;
            _client = client;
            _schedule = CrontabSchedule.Parse(Schedule,new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await Task.Delay(30000, stoppingToken);
                    Console.WriteLine( DateTime.Now.ToString("F"));
                    await _client.EvaluateEvents();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
            } while (!stoppingToken.IsCancellationRequested);
        }
    }
}
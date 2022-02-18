using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Jobs
{
    [DisallowConcurrentExecution]
    public class SchedulerJob : IJob
    {
        private readonly ILogger<SchedulerJob> _logger;

        public SchedulerJob(ILogger<SchedulerJob> logger)
        {
            _logger = logger;
        }
        
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Inserindo Artigos no banco");
            string[] start = new string[1] {"0"};
            HttpClientDesafioCoodesh.Program.Main(start);
            return Task.CompletedTask;
        }       
    }
}

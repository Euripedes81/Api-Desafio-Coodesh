using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh.Jobs
{
    [DisallowConcurrentExecution]
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;

        public HelloWorldJob(ILogger<HelloWorldJob> logger)
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

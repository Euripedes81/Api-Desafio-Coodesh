using ApiDesafioCoodesh.Jobs;
using ApiDesafioCoodesh.Repositories;
using ApiDesafioCoodesh.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDesafioCoodesh
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiDesafioCoodesh", Version = "v1" });
            });
            
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
               
                var jobKey = new JobKey("Inserindo Artigos no banco");
                
                q.AddJob<SchedulerJob>(opts => opts.WithIdentity(jobKey));
               
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) 
                    .WithIdentity("Inserindo Artigos no banco") 
                    //.WithCronSchedule("0 0 9 * * ?")); 
                    .WithCronSchedule("0/15 * * * * ?")); 
        });
           
            services.AddQuartzHostedService(q =>
                q.WaitForJobsToComplete = true
            ); ;
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiDesafioCoodesh v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();               
            });          

           
        }
    }
}

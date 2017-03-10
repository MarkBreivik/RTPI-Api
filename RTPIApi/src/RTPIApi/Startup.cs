// Startup.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RTPIAPI.Models;
using RTPIAPI.Services;

namespace RTPIAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRTPIServiceFactory, RTPIServiceFactory>();
            services.AddSingleton<IHttpRequestService, HttpRequestService>();
            services.AddLogging();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IRTPIServiceFactory rtpiFactory,
            IHttpRequestService httpRequestService
            )
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseMvc();

            rtpiFactory.RegisterRTPIService("LUAS", new LUASRTPIService(httpRequestService));
            rtpiFactory.RegisterRTPIService("DublinBus", new DublinBusRTPIService(httpRequestService));
        }
    }
}

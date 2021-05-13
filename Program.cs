using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fluxor;
using Sudoku.Store.Middlewares;
using Blazor.Extensions.Logging;

namespace Sudoku
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            AddLoggingService(builder);

            AddFluxorService(builder);

            await builder.Build().RunAsync();

            static void AddFluxorService(WebAssemblyHostBuilder builder)
            {
                var currentAssembly = typeof(Program).Assembly;
                builder.Services.AddFluxor(
                    options => options
                        .ScanAssemblies(currentAssembly)
                        .UseRouting()
                        .AddMiddleware<LoggingMiddleware>()
                        .UseReduxDevTools()
                   ); ;
            }

            static void AddLoggingService(WebAssemblyHostBuilder builder)
            {
                builder.Services.AddLogging(builder => builder
                    .AddBrowserConsole()
                    .SetMinimumLevel(LogLevel.Trace)
                );
            }
        }
    }
}

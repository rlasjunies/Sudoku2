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

namespace Sudoku
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            loadFluxor(builder);

            await builder.Build().RunAsync();

            static void loadFluxor(WebAssemblyHostBuilder builder)
            {
                var currentAssembly = typeof(Program).Assembly;
                builder.Services.AddFluxor(options => options.ScanAssemblies(currentAssembly));
            }
        }
    }
}

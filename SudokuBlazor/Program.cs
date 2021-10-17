using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Fluxor;
using Sudoku.Store.Middlewares;
using Blazor.Extensions.Logging;
using Blazored.LocalStorage;

using Fluxor.Persist.Middleware;
using Fluxor.Persist.Storage;
using Sudoku.Shared.Storage;
using Sudoku;

namespace Sudoku
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<Sudoku.App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<IStringStateStorage, LocalStateStorageSudoku>();
            builder.Services.AddScoped<IObjectStateStorage, InMemoryStateStorageSudoku>();
            builder.Services.AddScoped<IStoreHandler, JsonStoreHandlerSudoku>();
            // builder.Services.AddScoped<IStoreHandler, JsonStoreHandler>();

            AddLoggingService(builder);
            AddFluxorService(builder);
            AddLocalStorageService(builder);


            await builder.Build().RunAsync();

            static void AddFluxorService(WebAssemblyHostBuilder builder)
            {
                var currentAssembly = typeof(Program).Assembly;
                builder.Services.AddFluxor(
                    options => options
                        .ScanAssemblies(currentAssembly)
                        .UseRouting()
                        .AddMiddleware<LoggingMiddleware>()
                        // .UsePersist()
                        // .UsePersist(options => options.UseInclusionApproach())
                        // .UsePersist(x => x.SetWhiteList(new string[] { "StateCounter", "StateGame" }))
                        .UseReduxDevTools()
                   );
            }

            static void AddLoggingService(WebAssemblyHostBuilder builder)
            {
                builder.Services.AddLogging(builder => builder
                    .AddBrowserConsole()
                    .SetMinimumLevel(LogLevel.Information)
                );
            }

            static void AddLocalStorageService(WebAssemblyHostBuilder builder)
            {
                // builder.Services.AddBlazoredLocalStorage(config => {config.JsonSerializerOptions.WriteIndented = true;});
                builder.Services.AddBlazoredLocalStorage();
            }
        }
    }
}

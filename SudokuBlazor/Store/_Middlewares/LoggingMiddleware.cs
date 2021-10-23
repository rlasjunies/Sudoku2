using Fluxor;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sudoku.Store.Middlewares
{
	public class LoggingMiddleware : Middleware
	{
       private IStore Store;

        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware (ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public override Task InitializeAsync(IStore store)
        {
            Store = store;
            _logger.LogDebug($"Initialize @{nameof(LoggingMiddleware)} Middleware");
            return Task.CompletedTask;
        }

        public override void AfterInitializeAllMiddlewares()
        {
            _logger.LogDebug(nameof(AfterInitializeAllMiddlewares));
        }

        public override bool MayDispatchAction(object action)
        {
            _logger.LogDebug(nameof(MayDispatchAction) + ObjectInfo(action));
            return true;
        }

        public override void BeforeDispatch(object action)
        {
            // _logger.LogDebug(nameof(BeforeDispatch) + ObjectInfo(action));
        }

        public override void AfterDispatch(object action)
        {
            //_logger.LogDebug(nameof(AfterDispatch) + ObjectInfo(action));
        }

        private string ObjectInfo(object obj)
            => ": " + obj.GetType().Name + " " + JsonConvert.SerializeObject(obj);
	}
}
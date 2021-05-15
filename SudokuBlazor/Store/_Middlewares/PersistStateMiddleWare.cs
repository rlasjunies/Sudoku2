
using Fluxor;
using Newtonsoft.Json;
using System.Collections.Generic;
using Blazored.LocalStorage;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Sudoku.Store.Middlewares
{
    public class PersistStateMiddleware : Middleware
    {
        private IStore Store;

        private readonly ISyncLocalStorageService _localStorage;
        private readonly ILogger<PersistStateMiddleware> _logger;

        public PersistStateMiddleware(ISyncLocalStorageService localStorage, ILogger<PersistStateMiddleware> logger)
        {
            _localStorage = localStorage;
            _logger = logger;
        }

        public override Task InitializeAsync(IStore store)
        {
            Store = store;
            _logger.LogDebug($"Initialize @{nameof(PersistStateMiddleware)} Middleware");
            return Task.CompletedTask;
        }

        public override void AfterDispatch(object action)
        {
            _logger.LogDebug(nameof(AfterDispatch) + ObjectInfo(action));

            // if (action is LoadTodosSuccessAction)
            // {
            // var state = Store!.Features["Todos"].GetState();

            // if (state is TodosState todosState)
            // {
            // _localStorage.SetItem<string>("todos", todosState.CurrentTodos ?? new List<TodoDTO>());
            // }
            // }
        }

        private string ObjectInfo(object obj)
            => ": " + obj.GetType().Name + " " + JsonConvert.SerializeObject(obj);
    }
}
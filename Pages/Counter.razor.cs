using Fluxor;
using Microsoft.AspNetCore.Components;
using Fluxor.Blazor.Web.Components;
using CounterStore = Sudoku.Store.Counter;
using Microsoft.Extensions.Logging;

namespace Sudoku.Pages
{
    public partial class Counter
    {
        [Inject]
        protected ILogger<Counter> Logger { get; set; }

        [Inject]
        private IState<CounterStore::StateCounter> CounterState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private void IncrementCount()
        {
            var action = new CounterStore::Actions.IncrementCounter();
            Dispatcher.Dispatch(action);
            Logger.LogInformation($"increment the current value: @{CounterState.Value.ClickCount}");
        }
    }
}
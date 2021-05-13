using Fluxor;
using Microsoft.AspNetCore.Components;
using Fluxor.Blazor.Web.Components;
using CounterStore = Sudoku.Store.Counter;

namespace Sudoku.Pages
{
    public partial class Counter
    {
        [Inject]
        private IState<CounterStore::StateCounter> CounterState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private void IncrementCount()
        {
            var action = new CounterStore::Actions.IncrementCounter();
            Dispatcher.Dispatch(action);
        }
    }
}
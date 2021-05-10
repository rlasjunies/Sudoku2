using Fluxor;
using Microsoft.AspNetCore.Components;
using Sudoku.Store.CounterState;
using Fluxor.Blazor.Web.Components;

namespace Sudoku.Pages
{
    public partial class Counter
    {
        [Inject]
        private IState<CounterState> CounterState { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private void IncrementCount()
        {
            var action = new IncrementCounterAction();
            Dispatcher.Dispatch(action);
        }
    }
}
using Fluxor;
using Microsoft.AspNetCore.Components;
using Fluxor.Blazor.Web.Components;
using Store = Sudoku.Store.Counter;
using Microsoft.Extensions.Logging;
using System;

namespace Sudoku.Pages
{
    public partial class CounterBase : ComponentBase, IDisposable
    {
        [Inject]
        protected ILogger<CounterBase> Logger { get; set; }

        [Inject]
        protected IState<Store::StateCounter> _State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        protected void IncrementCount()
        {
            var action = new Store::Actions.IncrementCounter();
            Dispatcher.Dispatch(action);
            Logger.LogInformation($"increment the current value: @{_State.Value.ClickCount}");
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _State.StateChanged += StateChanged;
        }
        private void StateChanged(Object sender, Store::StateCounter state)
        {
            InvokeAsync(StateHasChanged);
        }

        void IDisposable.Dispose()
        {
            _State.StateChanged -= StateChanged;
        }

    }
}
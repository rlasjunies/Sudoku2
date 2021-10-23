using System;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Sudoku.Pages
{
    public class PageBase<TState, T> : ComponentBase, IDisposable
    {

        [Inject]
        protected ILogger<T> Logger { get; set; }

        [Inject]
        protected IState<TState> State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        protected override void OnInitialized()
        {
            Logger?.LogDebug($"{this.GetType().FullName} initialization");
            State.StateChanged += StateChanged;
            base.OnInitialized();
        }

        private void StateChanged(Object sender, TState state)
        {
            InvokeAsync(StateHasChanged);
        }

        void IDisposable.Dispose()
        {
            State.StateChanged -= StateChanged;
        }

    }
}

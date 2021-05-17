using System;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Sudoku.Pages
{
    public class PageBase<T> : ComponentBase, IDisposable
    {

        [Inject]
        protected ILogger<T> Logger { get; set; }

        [Inject]
        protected IState<T> State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        protected override void OnInitialized()
        {
            // Logger?.LogDebug($"{typeof(T)} initialization");
            Logger?.LogDebug($"{this.GetType().FullName} initialization");
            State.StateChanged += StateChanged;
            base.OnInitialized();
        }

        private void StateChanged(Object sender, T state)
        {
            InvokeAsync(StateHasChanged);
        }

        void IDisposable.Dispose()
        {
            State.StateChanged -= StateChanged;
        }

    }
}

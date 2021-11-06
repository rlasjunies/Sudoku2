using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using Store = Sudoku.Store.App;

namespace Sudoku
{
    public partial class App
    {
        [Inject]
        protected ILogger<App> Logger { get; set; }

        [Inject]
        protected IState<Store::StateApp> State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        //public override void task OnInitializedAsync()
        protected override void OnInitialized()
        {
            Logger?.LogDebug($"{this.GetType().FullName} App initialization");
            Dispatcher.Dispatch(new Store::Actions.RetrieveAppInformation());
            base.OnInitialized();
        }
    }
}
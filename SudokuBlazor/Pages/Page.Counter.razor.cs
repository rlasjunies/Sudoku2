using Fluxor;
using Microsoft.AspNetCore.Components;
using Fluxor.Blazor.Web.Components;
using Store = Sudoku.Store.Counter;
using Microsoft.Extensions.Logging;
using System;

namespace Sudoku.Pages
{
    public partial class CounterBase : PageBase<Store::StateCounter>, IDisposable
    {

        protected void IncrementCount()
        {
            var action = new Store::Actions.IncrementCounter();
            Dispatcher.Dispatch(action);
            Logger?.LogInformation($"increment the current value: @{State.Value.ClickCount}");
        }

    }
}
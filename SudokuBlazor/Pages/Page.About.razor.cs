using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using System;
using Store = Sudoku.Store.App;

namespace Sudoku.Pages
{
    public partial class AboutPage : PageBase<Store::StateApp, AboutPage> , IDisposable
    {

        protected override void OnInitialized()
        {
            if (!State.Value.Initialized)
            {
                Dispatcher.Dispatch(new Store::Actions.RetrieveAppInformation());
            }
            base.OnInitialized();
        }

        protected void NavigateToHome()
        {
            Dispatcher.Dispatch(new GoAction(Pages.Home));
        }
    }
}
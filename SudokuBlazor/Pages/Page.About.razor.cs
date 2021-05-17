using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using System;
using Store = Sudoku.Store.About;

namespace Sudoku.Pages
{
    public partial class AboutBase : PageBase<Store::StateAbout> , IDisposable
    {

        protected override void OnInitialized()
        {
            if (!State.Value.Initialized)
            {
                Dispatcher.Dispatch(new Store::Actions.RetrieveAboutInformation());
            }
            base.OnInitialized();
        }

        protected void NavigateToHome()
        {
            Dispatcher.Dispatch(new GoAction(Pages.Home));
        }
    }
}
using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using System;
using Store = Sudoku.Store.Game;

namespace Sudoku.Pages
{
    public partial class SudokuBase : PageBase<Store::StateGame>, IDisposable
    {

        protected void NavigateToHome()
        {
            Dispatcher.Dispatch(new GoAction(Pages.Home));
        }

    }
}
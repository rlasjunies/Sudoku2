using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using System;
using Store = Sudoku.Store.Game;

namespace Sudoku.Pages
{
    public partial class NewBoardBase : PageBase<Store::StateGame>, IDisposable
    {
        protected void NavigateToSudokuPage()
        {
            Dispatcher.Dispatch(new GoAction(Pages.Sudoku));
        }

        protected void NavigateToHome()
        {
            Dispatcher.Dispatch(new GoAction(Pages.Home));
        }

    }
}
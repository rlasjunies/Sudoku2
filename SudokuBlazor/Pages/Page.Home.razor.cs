using Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using Store= Sudoku.Store.Game;
using Fluxor.Blazor.Web.Middlewares.Routing;
namespace Sudoku.Pages
{
    public partial class HomeBase : PageBase<Store::StateGame>
    {
        protected bool GameOnGoing => base.State.Value.gameOnGoing;
        protected string styling => GameOnGoing ? "" : "success";

        protected void NavigateToSudokuPage()
        {
            base.Dispatcher.Dispatch(new GoAction(Pages.Sudoku));
        }

        protected void NavigateToCreateNewBoardPage()
        {
            base.Dispatcher.Dispatch(new GoAction(Pages.NewBoard));
        }
        protected void NavigateToAboutPage()
        {
            base.Dispatcher.Dispatch(new GoAction(Pages.About));
        }

    }
}
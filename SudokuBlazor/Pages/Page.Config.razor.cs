using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using System;
using Store = Sudoku.Store.Game;

namespace Sudoku.Pages
{
    public partial class ConfigPage : PageBase<Store::StateGame, ConfigPage>, IDisposable
    {

        public bool ShowIdenticalNumber => State.Value.wizardConfiguration.showIdenticalNumber;
        public bool ShowErrornousCells => State.Value.wizardConfiguration.showErrornousCells;
        public bool HighlightCellWithUniqueCandidate => State.Value.wizardConfiguration.showUniquePossibleValueInRowOrColumn;
        public bool DevMode => State.Value.devMode;


        protected void ToggleShowIdenticalNumber(){
            Dispatcher.Dispatch(new Store::Actions.ToggleShowIdenticalNumber());
        }
        protected void ToggleShowErrornousCells(){
            Dispatcher.Dispatch(new Store::Actions.ToggleShowErrornousCells());
        }
        protected void ToggleHighlightCellWithUniqueCandidate(){
            Dispatcher.Dispatch(new Store::Actions.ToggleHighlightCellWithUniqueCandidate());
        }
        protected void ToggleDevMode()
        {
            Dispatcher.Dispatch(new Store::Actions.ToggleDevMode());
        }

        protected void NavigateToGame()
        {
            Dispatcher.Dispatch(new GoAction(Pages.Sudoku));
        }

    }
}
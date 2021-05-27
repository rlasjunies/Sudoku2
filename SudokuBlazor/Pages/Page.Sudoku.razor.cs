using System;
using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Sudoku.Board;
using Store = Sudoku.Store.Game;

namespace Sudoku.Pages
{
    public partial class SudokuBase : PageBase<Store::StateGame>, IDisposable
    {

        //public bool ShowIdenticalNumber => State.Value.wizardConfiguration.showIdenticalNumber;
        public Sudoku.Board.SudokuBoard Board => State.Value.board;
        public int[] IncorrectCells => State.Value.board.incorrectCells;

        public int CellSelected => State.Value.cellSelected;
        public int LastCellOfTheGame => State.Value.lastCellOfTheGame;
        public bool DraftMode => State.Value.draftMode;

        public int ColSolved => State.Value.colSolved;
        public int RowSolved => State.Value.rowSolved;
        public int BlockSolved => State.Value.blockSolved;
        public bool BoardSolved => State.Value.boardSolved;

        public bool GameOnGoing => State.Value.gameOnGoing;
        public int Timer => State.Value.timer;
        public bool GameInPause => State.Value.gameInPause;
        public SolutionByRules SolutionsByRules => State.Value.solutionsByRules;
        public SudokuWizardConfiguration WizardConfiguration => State.Value.wizardConfiguration;


        protected void dispatchCellSelection(int cell)
        {
            Dispatcher.Dispatch(new Store::Actions.SelectCell() { SelectedCell = cell });
        }

        protected void dispatchPauseGame()
        {
                Dispatcher.Dispatch(new Store::Actions.PauseGame());
        }
        protected void dispatchResumeGame()
        {
                Dispatcher.Dispatch(new Store::Actions.ResumeGame());
        }
        protected void dispatchNumberTyped(int keyTyped)
        {
            Dispatcher.Dispatch(new Store::Actions.ValueTyped() { valueTyped = keyTyped });
        }
        protected void dispatchDraftNumberTyped(int keyTyped)
        {
            //   dispatchDraftNumberTyped({ detail: keyTyped }) {
            Dispatcher.Dispatch(new Store::Actions.DraftValueTyped() { ValueTyped = keyTyped });
        }
        protected void dispatchClearTyped()
        {
            // store.dispatch(sudokuClearTyped.action());
            Dispatcher.Dispatch(new Store::Actions.ClearTyped());
        }

        //protected void dispatchUndoTyped()
        //{
        //    Dispatcher.Dispatch(new Store::Actions.Undo());
        //}

        //protected void dispatchNavigateToWizardPage()
        //{
        //    // store.dispatch(navigateToWizard.action());
        //}

        //protected void onBackClickHandler()
        //{
        //    // store.dispatch(navigateToSplashScreen_PauseTimer.action());
        //}

        protected void onUndoClickHandler()
        {
            Dispatcher.Dispatch(new Store::Actions.Undo());
        }

        protected void onTimerSwitch()
        {
            if (GameInPause)
            {
                //   store.dispatch(resumeGame_ResumeTimer.action());
            }
            else
            {
                //   store.dispatch(pauseGame_PauseTimer.action());
            }
        }

        protected void onNewGameClicked()
        {
            // store.dispatch(navigateTo.action(navigateTo.pages.newGame));
        }

        protected void onWizardClickHandler()
        {
            // store.dispatch(navigateToWizard.action());
        }

        protected void onCalculatePossibleValuesClickHandler()
        {
            // store.dispatch(autoCalculatePossibleValuesAction.action());
        }


        protected void NavigateToHome()
        {
            Dispatcher.Dispatch(new GoAction(Pages.Home));
        }
        protected void NavigateToConfiguration()
        {
            Dispatcher.Dispatch(new GoAction(Pages.Config));
        }

    }
}
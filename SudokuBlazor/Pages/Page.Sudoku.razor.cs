using System;
using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Sudoku.Board;
using Store = Sudoku.Store.Game;

namespace Sudoku.Pages
{
    public partial class SudokuPageBase : PageBase<Store::StateGame>, IDisposable
    {
        // TODO important test case, when the board is not loaded

        //public bool ShowIdenticalNumber => State.Value.wizardConfiguration.showIdenticalNumber;
        public Sudoku.Board.SudokuBoard Board => State.Value.board;

        // TODO trial to avoid control explosion when no Board is loaded
        // TODO replace to have centralized initialized or be sure there is something in the control at loading time
        // or it accept not initialized board
        public int[] RemainingNumbers => new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };

        public bool[] IncorrectCells => State.Value.board.incorrectCells;

        // TODO remove seed control to check if that the reason of the crash
        //public bool ShouldHideClearKey => CellSelected == Sudoku.Store.Game.Const.NoCellSelected || !GameOnGoing || Board.cells[CellSelected].seed;
        public bool ShouldHideClearKey() {
            if (CellSelected == Sudoku.Store.Game.Const.NoCellSelected) return true;
            if (Board.cells[CellSelected].seed) return true;
            if (Board.cells[CellSelected].value == 0) return true;
            return false;
        }

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

        protected void PauseGame()
        {
            Dispatcher.Dispatch(new Store::Actions.PauseGame());
        }
        protected void ResumeGame()
        {
            Dispatcher.Dispatch(new Store::Actions.ResumeGame());
        }
        protected void NumberTyped(int keyTyped)
        {
            Dispatcher.Dispatch(new Store::Actions.ValueTyped() { valueTyped = keyTyped });
        }
        protected void DraftNumberTyped(int keyTyped)
        {
            //   dispatchDraftNumberTyped({ detail: keyTyped }) {
            Dispatcher.Dispatch(new Store::Actions.DraftValueTyped() { ValueTyped = keyTyped });
        }
        protected void ClearCellClicked()
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

        protected void UndoClicked()
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

        protected void NavigateToNewboard()
        {
            Dispatcher.Dispatch(new GoAction(Pages.NewBoard));
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
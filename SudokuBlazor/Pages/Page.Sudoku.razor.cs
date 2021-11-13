using System;
using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sudoku.Board;
using Sudoku.Shared;
using Sudoku.Store.Game;
using Store = Sudoku.Store.Game;

namespace Sudoku.Pages
{
    public partial class SudokuPage : PageBase<Store::StateGame, SudokuPage>, IDisposable
    {

        [Inject]
        protected ITimer Timer { get; set; }

        public Sudoku.Board.SudokuBoard Board => State.Value.board;

        // TODO trial to avoid control explosion when no Board is loaded
        // TODO replace to have centralized initialized or be sure there is something in the control at loading time
        // or it accept not initialized board
        public int[] RemainingNumbers => new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };

        public bool ShouldHideClearKey()
        {
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
        //public bool BoardSolved => State.Value.boardSolved;

        public bool _BoardSolved = false;
        public bool BoardSolved
        {
            get => _BoardSolved;
            set
            {
                Logger?.LogDebug($"BoardSolved 6 property:_BoardSolved={_BoardSolved} StateValue={value}");

                if (!_BoardSolved && value)
                {
                    _BoardSolved = true;
                    Dispatcher.Dispatch(new Store::Actions.EndGame());
                    //Logger?.LogDebug($"Board is solved, EndGame action triggered ");
                }
            }
        }

        public bool GameOnGoing => State.Value.gameOnGoing;
        // public int TimerValue => State.Value.timer;
        public int TimerValue { get; set; }
        public string TimerValueString { get; set; }
        public bool GameInPause => State.Value.gameInPause;
        public SolutionByRules SolutionsByRules => State.Value.solutionsByRules;
        public SudokuWizardConfiguration WizardConfiguration => State.Value.wizardConfiguration;

        protected override void OnInitialized()
        {
            State.StateChanged += StateChanged;
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (State is not null)
                {
                    DoWeNeedToStartTheTimer((StateGame)State.Value);
                    InvokeAsync(StateHasChanged);
                }
                else
                {
                    Logger?.LogWarning($"OnAfterRender State is null");
                }
            }
            base.OnAfterRender(firstRender);
        }

        private bool isTimerOn = false;
        private void timerTick(object state)
        {
            //Dispatcher.Dispatch(new Store::Actions.TimerTick());
            TimerValue += 1;
            TimeSpan time = TimeSpan.FromSeconds(TimerValue);

            //here backslash is must to tell that colon is
            //not the part of format, it just a character that we want in output
            TimerValueString = time.ToString(@"hh\:mm\:ss");
            InvokeAsync(StateHasChanged);
        }

        private void StateChanged(Object sender, Store::StateGame state)
        {
            //Logger?.LogDebug($"{ this.GetType().FullName}State Is changing");
            this.BoardSolved = state.boardSolved;
            DoWeNeedToStartTheTimer(state);
            DoWeNeedToStopTheTimer(state);

            InvokeAsync(StateHasChanged);

            void DoWeNeedToStopTheTimer(Store.Game.StateGame state)
            {
                if ((!state.gameOnGoing || state.gameInPause) && this.isTimerOn)
                {
                    Timer.StopTimer();
                    Dispatcher.Dispatch( new Store::Actions.TimerUpdateValue { TimerValue = this.TimerValue });
                    isTimerOn = false;
                }
            }
        }

        private void DoWeNeedToStartTheTimer(Store.Game.StateGame state)
        {
            if (state.gameOnGoing && !state.gameInPause && !this.isTimerOn)
            {
                TimerValue = state.timer;
                Timer.StartTimer(timerTick, 1000);
                this.isTimerOn = true;
            }
        }

        void IDisposable.Dispose()
        {
            Logger?.LogDebug($"{this.GetType().FullName} dispose handler");
            Dispatcher.Dispatch(new Store::Actions.TimerUpdateValue { TimerValue = this.TimerValue });
            State.StateChanged -= StateChanged;
        }

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
            Dispatcher.Dispatch(new Store::Actions.DraftValueTyped() { ValueTyped = keyTyped });
        }
        protected void ClearCellClicked()
        {
            Dispatcher.Dispatch(new Store::Actions.ClearTyped());
        }

        protected void UndoClicked()
        {
            Dispatcher.Dispatch(new Store::Actions.Undo());
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
using Fluxor;

namespace Sudoku.Store.Game.Actions
{
    public record TimerTick { }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducersTimerTick
    {
        [ReducerMethod]
        public static StateGame OnTimerTick(StateGame state, Actions.TimerTick action) =>
        state with
        {
            timer = state.timer + 1,
        };
    }
}
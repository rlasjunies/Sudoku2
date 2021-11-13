using Fluxor;

namespace Sudoku.Store.Game.Actions
{
    public record TimerUpdateValue {
        public int TimerValue;
    }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducersTimerUpdateValue
    {
        [ReducerMethod]
        public static StateGame OnTimerUpdateValue(StateGame state, Actions.TimerUpdateValue action) =>
        state with
        {
            timer = action.TimerValue,
        };
    }
}

using Fluxor;

namespace Sudoku.Store.Counter
{
    public static class Reducers
    {
        [ReducerMethod]
        public static StateCounter OnIncrementCounterAction(StateCounter state, Actions.IncrementCounter action) =>
            state with { ClickCount = state.ClickCount + 1 };
    }

}
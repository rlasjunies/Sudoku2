
using Fluxor;

namespace Sudoku.Store.CounterState
{
    public static class Reducers
    {
        [ReducerMethod]
        public static CounterState OnIncrementCounterAction(CounterState state, IncrementCounterAction action) =>
            state with { ClickCount = state.ClickCount + 1 };
    }

}
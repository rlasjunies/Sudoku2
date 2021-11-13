using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record ToggleDevMode { }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducerToggleDevMode
    {
        [ReducerMethod]
        public static StateGame OnToggleDevMode(StateGame state, Actions.ToggleDevMode Action) =>
            state with
            {
                devMode = !state.devMode
            };
    }
}
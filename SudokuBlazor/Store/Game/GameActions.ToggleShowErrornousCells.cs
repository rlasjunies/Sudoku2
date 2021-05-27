using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record ToggleShowErrornousCells { }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducerToggleShowErrornousCells
    {
        [ReducerMethod]
        public static StateGame OnToggleShowErrornousCells(StateGame state, Actions.ToggleShowErrornousCells Action) =>
            state with
            {
                wizardConfiguration = state.wizardConfiguration with
                {
                    showErrornousCells = !state.wizardConfiguration.showErrornousCells
                }
            };
    }
}
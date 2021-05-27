using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record ToggleHighlightCellWithUniqueCandidate { }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducerToggleHighlightCellWithUniqueCandidate
    {
        [ReducerMethod]
        public static StateGame OnToggleHighlightCellWithUniqueCandidate(StateGame state, Actions.ToggleHighlightCellWithUniqueCandidate Action) =>
    state with
    {
        wizardConfiguration = state.wizardConfiguration with
        {
            showUniquePossibleValueInRowOrColumn = !state.wizardConfiguration.showUniquePossibleValueInRowOrColumn,
            showUniquePossibleValueInZones = !state.wizardConfiguration.showUniquePossibleValueInZones
        }
    };
    }
}
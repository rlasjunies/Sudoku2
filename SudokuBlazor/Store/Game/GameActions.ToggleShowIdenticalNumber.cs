using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record ToggleShowIdenticalNumber { }


}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducerToggleShowIndenticalNumber
    {
        [ReducerMethod]
        public static StateGame OnToggleShowIdenticalNumber(StateGame state, Actions.ToggleShowIdenticalNumber Action) =>
            state with
            {
                wizardConfiguration = state.wizardConfiguration with
                {
                    showIdenticalNumber = !state.wizardConfiguration.showIdenticalNumber
                }
            };
    }
}
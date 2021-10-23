using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record EndGame
    {
    }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducerEndGame
    {
        [ReducerMethod]
        public static StateGame OnEndGame(StateGame state, Actions.EndGame action) =>
        state with
        {
            cellSelected = Sudoku.Store.Game.Const.NoCellSelected,
            lastCellOfTheGame = state.cellSelected,
            gameOnGoing = false,
            boardSolved = false,
        };

    }
}


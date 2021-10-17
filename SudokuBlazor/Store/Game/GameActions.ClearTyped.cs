using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record ClearTyped { }

}

namespace Sudoku.Store.Game.Reducers
{

   public static class ReducerClearTyped
    {
        [ReducerMethod]
        public static StateGame OnClearTyped(StateGame state, Actions.ClearTyped Action)
        {

            var currentCell = state.cellSelected;
            var oldBoard = state.board;
            var newBoard = hlpr.sudokuBoardClone(state.board);

            // TODO: algo a revoir quand fonctionnel avancé, il faut mettre dans des sous fonctions l'ensemeble des cas

            if (currentCell == Sudoku.Store.Game.Const.NoCellSelected)
            {
                // noting done
            }
            else if (newBoard.cells[currentCell].seed) 
            {
                // no modification allowed
            }
            else
            {

                newBoard.cells[currentCell].drafted = new bool[9] { false, false, false, false, false, false, false, false, false };
                newBoard.cells[currentCell].value = 0;
                // remove the cell of the incorrect cells
                newBoard.incorrectCells[currentCell] = false;

                // check if zones are solved in order to provide animation for the player
                newBoard.remainingNumbers = hlpr.remainingNumbers(newBoard.cells);

                state = state with
                {
                    board = newBoard,
                    boardHistory = hlpr.Push(state.boardHistory,oldBoard),
                    rowSolved = Sudoku.Store.Game.Const.NothingSolved,
                    colSolved = Sudoku.Store.Game.Const.NothingSolved,
                    blockSolved = Sudoku.Store.Game.Const.NothingSolved,
                    boardSolved = false,
                };
            }
            return state;
        }
    }
}
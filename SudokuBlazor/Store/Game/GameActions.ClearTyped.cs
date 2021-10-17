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
            var row = hlpr.rowOfCellNumber(currentCell);
            var col = hlpr.colOfCellNumber(currentCell);
            var block = hlpr.blockOfCellNumber(currentCell);
            var rowSolved = -1;
            var colSolved = -1;
            var blockSolved =-1;
            var boardSolved = false;

            var newBoard = hlpr.sudokuBoardClone(state.board);

            // TODO: algo a revoir quand fonctionnel avancé, il faut mettre dans des sous fonctions l'ensemeble des cas

            if (currentCell == -1)
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
                newBoard.cells[currentCell].value = -1;
                // remove the cell of the incorrect cells
                newBoard.incorrectCells[currentCell] = false;

                // check if zones are solved in order to provide animation for the player
                rowSolved = hlpr.isRowSolvedx(row, newBoard) ? row : -1;
                colSolved = hlpr.isColSolvedx(col, newBoard) ? col : -1;
                blockSolved = hlpr.isBlockSolvedx(block, newBoard) ? block : -1;
                boardSolved = hlpr.isBoardSolvedx(newBoard);
                newBoard.remainingNumbers = hlpr.remainingNumbers(newBoard.cells);

                state = state with
                {
                    board = newBoard,
                    boardHistory = hlpr.Push(state.boardHistory,oldBoard),
                    rowSolved = rowSolved,
                    colSolved = colSolved,
                    blockSolved = blockSolved,
                    boardSolved = boardSolved,
                };
            }
            return state;
        }
    }
}
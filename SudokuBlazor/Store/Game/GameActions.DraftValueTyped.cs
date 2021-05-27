using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record DraftValueTyped
    {
        public int ValueTyped { get; init; }
    }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducerDraftValueTyped
    {
        [ReducerMethod]
        public static StateGame OnDraftValueTyped(StateGame state, Actions.DraftValueTyped Action)
        {

            //const payload = action.payload;
            var currentCell = state.cellSelected;
            var oldBoard = state.board;

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
                var value = Action.ValueTyped;

                newBoard.cells[currentCell].drafted[value - 1] = newBoard.cells[currentCell].drafted[value - 1] ? false : true;
                state = state with
                {
                    board = newBoard,
                    boardHistory = hlpr.Push(state.boardHistory, oldBoard),
                };
            }
            return state;
        }
    }
}
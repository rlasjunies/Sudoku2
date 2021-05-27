using Fluxor;
using Sudoku.Board;
using System;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record Undo {}

}

namespace Sudoku.Store.Game.Reducers
{

   public static class ReducerUndo
    {
        [ReducerMethod]
        public static StateGame OnUndo(StateGame state, Actions.Undo Action)
        {

            return hlpr.PopBoard(state.boardHistory) switch {
                (null, _) => state,
                (var oldBoard, var oldHistory) => mergeState(oldBoard, oldHistory),
            };

            StateGame mergeState(SudokuBoard board, SudokuBoard[] boards) =>
                state with
                {
                    board = board,
                    boardHistory = boards,
                };


            //if (state.boardHistory.Length > 1)
            //{ // 1st element cannot be undone
            //    var (_, newHistory) = hlpr.PopBoard(state.boardHistory);
            //    state = state with
            //    {
            //        board = state.boardHistory[state.boardHistory.Length-1],
            //        boardHistory = newHistory,
            //    };
            //}
            //else
            //{
            //    // 1st element cannot be undone
            //}
            //return state;
        }
    }
}
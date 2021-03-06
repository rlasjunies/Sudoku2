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

            // TODO should not undo and come back to previous game
            // bug: start a game, do a new game, and undo ... I came back to the new game page
            // ... history must be reinitialized when there is a new game and ???

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
        }
    }
}
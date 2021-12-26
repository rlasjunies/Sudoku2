using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.Board;
using Fluxor;
using hlpr = Sudoku.Board.Helpers;

namespace Sudoku.Store.Game.Actions
{

    public record BoardGenerated
    {
        public Board.SudokuBoard board { get; init; }
        public Board.SudokuLevelType level { get; init; }
    }
}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducerBoardGenerated
    {

        [ReducerMethod]
        public static StateGame OnGeneratedBoard(StateGame state, Actions.BoardGenerated Action)
        {
            Console.WriteLine($"{state.boardHistory.Length} - {Action.board}");
            var newHistory = hlpr.Push(state.boardHistory, Action.board);
            return state with
            {
                board = Action.board,
                boardLevel = Action.level,
                boardHistory = newHistory,
                cellSelected = Sudoku.Store.Game.Const.NoCellSelected,
                gameOnGoing = true,
                gameInPause = false, // TODO check if gameOnGoing and GameInPause are not equivalent
                timer = 0
            };
        }
    }
}

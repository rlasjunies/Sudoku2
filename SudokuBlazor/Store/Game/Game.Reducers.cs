using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;

namespace Sudoku.Store.Game
{
    public static class Reducers
    {

        [ReducerMethod]
        public static StateGame OnGenerateBoard(StateGame state, Actions.GenerateBoard action) =>
            state with { };

        [ReducerMethod]
        public static StateGame OnGeneratedBoard(StateGame state, Actions.BoardGenerated action)
        {
            state.boardHistory.Push(action.board);

            return state with
            {
                board = action.board,
                boardLevel = action.level,
                boardHistory = state.boardHistory,
                cellSelected = -1,
                gameOnGoing = true,
                gameInPause = false, // TODO check if gameOnGoing and GameInPause are not equivalent
            };
        }
    }
}
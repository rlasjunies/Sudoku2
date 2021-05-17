using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.Game
{
    public static class Reducers
    {

        [ReducerMethod]
        public static StateGame OnGenerateBoard(StateGame state, Actions.GenerateBoard action) =>
            state with { };

        [ReducerMethod]
        public static StateGame OnGeneratedBoard(StateGame state, Actions.BoardGenerated action) =>
            state with { 
                board = action.board,
                // boardHistory = state.boardHistory.Add(action.board)
                // board: board,
                // boardHistory: [board], // initialize the history with the new board
                // boardLevel: level,
                // cellSelected: -1,
                gameOnGoing = true,
                gameInPause = false, // TODO check if gameOnGoing and GameInPause are not equivalent
                // solutionsByRules: solutionsByRules
    };

        // [ReducerMethod]
        // public static StateAbout OnRetrieveAboutInformation(StateAbout state, Actions.RetrieveAboutInformation action) =>
        //     state;

    }
}
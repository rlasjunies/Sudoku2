using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Sudoku.Store.Game.Effects
{
    public class GenerateBoard
    {

        [EffectMethod]
        public async Task GenerateBoardEffect(Actions.GenerateBoard action, IDispatcher dispatcher)
        {
            var board = Sudoku.Board.Board.generateSudokuBoard(action.level);
            dispatcher.Dispatch(new Actions.BoardGenerated
            {
                board = board,
                level = action.level,
            });
            await Task.CompletedTask;
        }
    }
}

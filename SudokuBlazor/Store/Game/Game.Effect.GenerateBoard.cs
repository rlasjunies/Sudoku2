using Fluxor;
using System.Threading.Tasks;

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

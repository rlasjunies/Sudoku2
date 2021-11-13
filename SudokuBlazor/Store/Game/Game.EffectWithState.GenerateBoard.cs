using Fluxor;
using System.Threading.Tasks;

namespace Sudoku.Store.Game.Effects
{
    public class GenerateBoard
    {

        [EffectWithStateMethod]
        public async Task GenerateBoardEffect(Actions.GenerateBoard action, StateGame state, IDispatcher dispatcher)
        {
            var board = Sudoku.Board.Board.generateSudokuBoard(action.level,state.devMode) ;

            dispatcher.Dispatch(new Actions.BoardGenerated
            {
                board = board,
                level = action.level,
            });
            await Task.CompletedTask;
        }
    }
}

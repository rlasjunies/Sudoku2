using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record GenerateBoard
    {
        public SudokuLevelType level { get; init; }
    }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducerGenerateBoard
    {
        [ReducerMethod]
        public static StateGame OnGenerateBoard(StateGame state, Actions.GenerateBoard action) =>
        state with { };
    }
}
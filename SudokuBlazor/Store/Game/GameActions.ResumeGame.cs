using Fluxor;

namespace Sudoku.Store.Game.Actions
{
    public record ResumeGame
    {
    }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducersResumeGame
    {
        [ReducerMethod]
        public static StateGame OnResumeGame(StateGame state, Actions.ResumeGame action) =>
        state with
        {
            gameInPause = false,
        };
    }
}
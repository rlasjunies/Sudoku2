using Fluxor;

namespace Sudoku.Store.Game.Actions
{
    public record PauseGame
    {
    }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducersPauseGame
    {
        [ReducerMethod]
        public static StateGame OnPauseGame(StateGame state, Actions.PauseGame action) =>
        state with
        {
            gameInPause = true,
        };
    }
}
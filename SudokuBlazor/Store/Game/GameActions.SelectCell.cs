using Fluxor;

namespace Sudoku.Store.Game.Actions
{
    public record SelectCell
    {
        public int SelectedCell { get; init; }
    }

}

namespace Sudoku.Store.Game.Reducers
{

    public static class ReducersSelectCell
    {
        [ReducerMethod]
        public static StateGame OnSelectCell(StateGame state, Actions.SelectCell action) =>
        state with
        {
            cellSelected = action.SelectedCell,
        };
    }
}
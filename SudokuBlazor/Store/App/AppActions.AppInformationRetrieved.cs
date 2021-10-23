using Fluxor;

namespace Sudoku.Store.App.Actions
{

    public record AppInformationRetrieved
    {
        public string Version { get; init; }
    }
}

namespace Sudoku.Store.App.Reducers
{

    public static class ReducerRetrieveAppInformation
    {

        [ReducerMethod]
        public static StateApp OnAppInformationRetrieved(StateApp state, Actions.AppInformationRetrieved action) =>
            state with
            {
                Version = action.Version,
                Initialized = true
            };
    }
}

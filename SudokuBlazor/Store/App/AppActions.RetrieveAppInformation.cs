using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.Board;
using Fluxor;
using hlpr = Sudoku.Board.Helpers;

namespace Sudoku.Store.App.Actions
{

    public record RetrieveAppInformation { }

}

namespace Sudoku.Store.App.Reducers
{

    public static class ReducerRetrieveAboutInformation
    {

        [ReducerMethod]
        public static StateApp OnRetrieveAboutInformation(StateApp state, Actions.RetrieveAppInformation action) =>
            state;
    }
}

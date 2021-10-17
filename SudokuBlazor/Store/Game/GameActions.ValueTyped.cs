using Fluxor;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;
namespace Sudoku.Store.Game.Actions
{
    public record ValueTyped
    {
        public int valueTyped { get; init; }
    }

}

namespace Sudoku.Store.Game.Reducers
{

   public static class ReducerValueTyped
    {
        [ReducerMethod]
        public static StateGame OnValueTyped(StateGame state, Actions.ValueTyped Action)
        {

            // const payload = action.payload;
            var currentCell = state.cellSelected;
            var oldBoard = state.board;
            var row = hlpr.rowOfCellNumber(currentCell);
            var col = hlpr.colOfCellNumber(currentCell);
            var block = hlpr.blockOfCellNumber(currentCell);

            var solutionsByRules = new SolutionByRules();

            var value = Action.valueTyped;
            // TODO: algo a revoir quand fonctionnel avancé, il faut mettre dans des sous fonctions l'ensemeble des cas

            if ((currentCell == Const.NoCellSelected) ||
            (state.board.cells[currentCell].seed) ||
            (state.board.cells[currentCell].value == value))
            {
                // nothing done
                // testEnvironment && console.debug(...DEV_MODE,`skip type number BL`);

                return state;
            }
            else
            {
                var newBoard = hlpr.sudokuBoardClone(state.board);


                // remove the value of the cell of the current board. because PossibleNumber check the value already in the board
                // oldBoard.cells[currentCell].value = null;

                // managed incorrect cell
                // const isValueCorrect = isPossibleNumberx(currentCell, value, oldBoard);
                // the value is not correct when the value is not the same as the expected One
                // console.log("check is value is correct", value, newBoard.cells[currentCell].expectedValue, (value == newBoard.cells[currentCell].expectedValue))
                // const isValueCorrect = (value == newBoard.cells[currentCell].expectedValue) ? true : false;

                var isValuePossible = hlpr.isPossibleNumberx(currentCell, value, oldBoard);
                
                // TODO: check if incorrect should be done in the library if, the state is there
                var isValueCorrect = false;
                if (isValuePossible)
                {
                    newBoard.cells[currentCell].value = value;
                    (_, isValueCorrect, _) = Sudoku.Board.Solver.resolverWorkForce(currentCell, newBoard);
                    newBoard.incorrectCells[currentCell] = !isValueCorrect;
                }
                else
                {
                    newBoard.incorrectCells[currentCell] = true;
                }

                // remove the value from the list if already exists
                // TODO: create an array library - retrieve the one from uacommander
                //testEnvironment && console.debug(...DEV_MODE,`isValuePossible:${ isValuePossible} -isValueCorrect:${ isValueCorrect} incorrect cells before:`,newBoard.incorrectCells);

                //testEnvironment && console.debug(...DEV_MODE,`isValuePossible:${ isValuePossible}-isValueCorrect:${ isValueCorrect} incorrect cells AFTER:`,newBoard.incorrectCells);


                // insert the value in the board even if incorrect, 
                // if incorrect the value will be highlighted to the player
                newBoard.cells[currentCell].value = value;

                // check if zones are solved in order to provide animation for the player
                var rowSolved = hlpr.isRowSolvedx(row, newBoard) ? row : Sudoku.Store.Game.Const.NothingSolved;
                var colSolved = hlpr.isColSolvedx(col, newBoard) ? col : Sudoku.Store.Game.Const.NothingSolved;
                var blockSolved = hlpr.isBlockSolvedx(block, newBoard) ? block : Sudoku.Store.Game.Const.NothingSolved;
                var boardSolved = hlpr.isBoardSolvedx(newBoard) ? true : false;

                // remove equal drafted in the related zones
                // only when the value is correct
                // TODO: this should be done in the service, 
                if (isValueCorrect)
                {
                    newBoard = hlpr.removeDraftedValueInZone(newBoard, value, currentCell);
                }

                // TODO : this should be done in the service, 
                // newBoard = possibleValues(newBoard);
                solutionsByRules = Sudoku.Board.Board.resolveByRules(newBoard);

                // console.log(` [${col}-${row}-${block}] rowSolved:${rowSolved} - colSolved:${colSolved} - blockSolved:${blockSolved} - boardSolved:${boardSolved}`);
                newBoard.remainingNumbers = hlpr.remainingNumbers(newBoard.cells);


                return state with
                {
                    board = newBoard,
                    boardHistory = hlpr.Push(state.boardHistory, oldBoard),
                    rowSolved = rowSolved,
                    colSolved = colSolved,
                    blockSolved = blockSolved,
                    boardSolved = boardSolved,
                    solutionsByRules = solutionsByRules,
                };
            }
        }
    }
}
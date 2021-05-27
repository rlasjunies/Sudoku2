namespace Sudoku.Board
{
    public static class Solver{
        public static (SudokuBoard board, bool finish, bool resolved) resolverWorkForce(int cellNumber, SudokuBoard board)
        {

            // console.log(`resolverWorkForce: cell:${cellNumber}`);
            // sortir de la recurrence
            if (cellNumber >= board.cells.Length)
            {
                return (board: board, finish: true, resolved: true);
            }

            // if (board.cells[cellNumber].value != null)
            // TODO checkf this modification does not create problem
            // TODO replace null by 0
            // TODO 
            // TODO 
            // TODO 
            // TODO 
            // TODO 
            // TODO 
            // TODO 
            if (board.cells[cellNumber].value != 0)
            {
                // the cell is already filled we continue recurrence
                // console.log(`resolverWorkForce: ALREADY FILLED - cell:${cellNumber} - value:${board.cells[cellNumber].value}`);
                return resolverWorkForce(cellNumber + 1, board);
            }
            else
            {
                // console.log(`resolverWorkForce: NO VALUE DEFINED cell:${cellNumber} - value:${board.cells[cellNumber].value}`);

                // liste of potential values for the cell
                var possibleValuesShuffled = Helpers.determinePossibleValuesx(cellNumber, board).Shuffle_knuthfisheryates2();

                // parse all the potential values
                // for (var index = 0; index < possibleValuesShuffled.Length; index++)
                foreach (var possibleValue in possibleValuesShuffled)
                {
                    var nextBoard = Helpers.sudokuBoardClone(board);

                    // nextBoard.cells[cellNumber].value = possibleValuesShuffled[index];
                    nextBoard.cells[cellNumber].value = possibleValue;

                    // console.log(`resolverWorkForce: PROPOSE VALUE cell:${cellNumber} - value:${nextBoard.cells[cellNumber].value}`);

                    var (finishedBoard, finish, resolved) = resolverWorkForce(cellNumber + 1, nextBoard);
                    if (finish)
                    {
                        return (board: finishedBoard, finish: finish, resolved: resolved);
                    }
                }

                // console.log(`resolverWorkForce: xxxxxxxxxxxxxxxxxxxxxxxxx !!! no possible value dead end: cell:${cellNumber}`, board)
                // all cell values are wrong
                return (board: null, finish: false, resolved: false);
            }
        }

    }
}
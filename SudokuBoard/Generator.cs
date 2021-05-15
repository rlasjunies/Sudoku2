
namespace Sudoku.Board
{
    public static class Generator
    {

        public static SudokuBoard generateBoard()
        {
            var emptyBoard = Helpers.initializeSudokuBoard();

            var (board, _, _) = generateBoardFromCell(0, emptyBoard);
            // visualize(board);

            return board;
        }
        private static (SudokuBoard board, bool finish, bool deadEnd) generateBoardFromCell(int cellNumber, SudokuBoard board)
        {
            // var possibleValuesShuffled = Helpers.determinePossibleValuesx(cellNumber, board).Shuffle_knuthfisheryates2();
            var possibleValuesShuffled = Helpers.determinePossibleValuesx(cellNumber, board).Shuffle_knuthfisheryates2();

            // for (var index = 0; index < possibleValuesShuffled.Length; index++)
            foreach (var possibleValue in possibleValuesShuffled)
            {
                var nextBoard = Helpers.sudokuBoardClone(board);

                // nextBoard.cells[cellNumber].value = possibleValuesShuffled[index];
                nextBoard.cells[cellNumber].value = possibleValue;
                nextBoard.cells[cellNumber].expectedValue = nextBoard.cells[cellNumber].value;
                nextBoard.cells[cellNumber].seed = true;

                var nextCellNumber = cellNumber + 1;
                if (nextCellNumber == board.cells.Length)
                {
                    return (board: nextBoard, finish: true, deadEnd: false);
                }

                var (finishedBoard, finish, deadEnd) = generateBoardFromCell(nextCellNumber, nextBoard);
                if (finish)
                {
                    return (board: finishedBoard, finish: true, deadEnd: deadEnd);
                }
            }

            // all cell values are wrong
            return (board: null, finish: false, deadEnd: true);
        }
    }
}
using System;
using Xunit;

namespace Sudoku.Board
{
    public class SudokuTests
    {
        [Fact]
        public void should_create_new_board()
        {
            // 1
            var board = Sudoku.Board.Board.generateSudokuBoard(Sudoku.Board.SudokuLevelType.easy);

            // 2 

            // 3
            var cell = board.cells[0];
            Assert.Equal("SudokuBoardCell", cell.GetType().Name);
        }
    }
}

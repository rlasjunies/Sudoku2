using System;
using Xunit;

namespace Sudoku.Board
{
    public class GeneratorTests
    {
        [Fact]
        public void should_generate_a_board()
        {
            // 1
            var board = Sudoku.Board.Generator.generateBoard();

            // 2 

            // 3
            var cell = board.cells[0];
            Assert.Equal(cell.GetType().Name,"SudokuBoardCell");
        }
    }
}
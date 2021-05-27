using System;
using Xunit;

namespace Sudoku.Board
{
    public class HelpersTests
    {
        [Fact]
        public void rowOfCellNumber_should_be_correct()
        {

            // Arrange

            // Act

            // Assert
            Assert.Equal(0, Helpers.rowOfCellNumber(5));
            Assert.Equal(0, Helpers.rowOfCellNumber(8));
            Assert.Equal(1, Helpers.rowOfCellNumber(9));
            Assert.Equal(1, Helpers.rowOfCellNumber(15));
            Assert.Equal(2, Helpers.rowOfCellNumber(18));
            Assert.Equal(2, Helpers.rowOfCellNumber(20));
        }

        [Fact]
        public void Push_should_work()
        {

            var board1 = Helpers.initializeSudokuBoard();
            board1.cells[0].value = 1;
            var board2 = Helpers.initializeSudokuBoard();
            board2.cells[0].value = 2;
            var board3 = Helpers.initializeSudokuBoard();
            board3.cells[0].value = 3;

            var boards = new SudokuBoard[] { board1, board2 };
            var bPush = Helpers.Push(boards, board3);

            // Test if "Empty board"
            var boards2 = Array.Empty<SudokuBoard>();
            var bPush2 = Helpers.Push(boards2, board3);

        }

    }
}

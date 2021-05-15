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


    }
}

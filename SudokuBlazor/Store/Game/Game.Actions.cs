using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.Board;

namespace Sudoku.Store.Game.Actions
{

    public record GenerateBoard
    {
        public SudokuLevelType level { get; init; }
    }
    public record BoardGenerated
    {
        public Board.SudokuBoard board { get; init; }
        public Board.SudokuLevelType level {get;init;}
}

}

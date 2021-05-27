using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sudoku.Board
{

    public static class Helpers
    {
        public static int rowOfCellNumber(int cellNumber) => (int)Math.Floor((double)(cellNumber / 9));
        public static int colOfCellNumber(int cellNumber) => cellNumber % 9;
        public static int blockOfCellNumber(int cellNumber) => (int)(Math.Floor((double)(rowOfCellNumber(cellNumber) / 3)) * 3 + Math.Floor((double)(colOfCellNumber(cellNumber) / 3)));
        // static int cellNumberOfColRowOfBlock(int col, int row, int block) => (col + (block % 3) * 3) + ((row * 9) + (27 * (int)Math.Floor((double)(block / 3))));

        public static int cellIndexFromBlock(int block, int i) => (int)(Math.Floor((double)(block / 3)) * 27 + i % 3 + 9 * Math.Floor((double)(i / 3)) + 3 * (block % 3));
        // public static int blockOfColRow(int col, int row) => (int)(Math.Floor((double)(row / 3)) * 3 + Math.Floor((double)(col / 3)));

        public static List<int> cellsNumberOfTheBlock(int block)
        {
            return new List<int> {
                cellIndexFromBlock(block,0),
                cellIndexFromBlock(block,1),
                cellIndexFromBlock(block,2),
                cellIndexFromBlock(block,3),
                cellIndexFromBlock(block,4),
                cellIndexFromBlock(block,5),
                cellIndexFromBlock(block,6),
                cellIndexFromBlock(block,7),
                cellIndexFromBlock(block,8),
                // cellNumberOfColRowOfBlock(0, 0, block),
                // cellNumberOfColRowOfBlock(1, 0, block),
                // cellNumberOfColRowOfBlock(2, 0, block),
                // cellNumberOfColRowOfBlock(0, 1, block),
                // cellNumberOfColRowOfBlock(1, 1, block),
                // cellNumberOfColRowOfBlock(2, 1, block),
                // cellNumberOfColRowOfBlock(0, 2, block),
                // cellNumberOfColRowOfBlock(1, 2, block),
                // cellNumberOfColRowOfBlock(2, 2, block)
            };
        }
        public static SudokuBoard sudokuBoardClone(SudokuBoard src)
        {
            // return JSON.parse(JSON.stringify(src));
            // TODO may not need clone function in dotnet

            string jsonString = JsonSerializer.Serialize(src);
            var ret = JsonSerializer.Deserialize<SudokuBoard>(jsonString);
            return ret;
        }

        static SudokuBoardCell EMPTYCELL => new SudokuBoardCell
        {
            value = 0,
            drafted = new bool[9],
            calculatedPossibleValues = new List<int>(),
            seed = false,
            expectedValue = 0,
        };
        public static SudokuBoard initializeSudokuBoard()
        {
            return new SudokuBoard
            {
                cells = new SudokuBoardCell[] {
                    EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,
                    EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,
                    EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,
                    EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,
                    EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,
                    EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,
                    EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,
                    EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,EMPTYCELL,
                    EMPTYCELL},
                incorrectCells = new int[81],
                remainingNumbers = new int[9]
            };
        }

        public static List<int> determinePossibleValuesx(int cellNumber, SudokuBoard board)
        {
            var possible = new List<int>();
            for (var i = 1; i <= 9; i++)
            {
                if (isPossibleNumberx(cellNumber, i, board))
                {
                    // possible.SetValue(i);
                    possible.Add(i);
                }
            }
            return possible;
        }

        public static bool isPossibleRowx(int cellValue, int row, SudokuBoard board)
        {
            for (var i = 0; i <= 8; i++)
            {
                if (board.cells[row * 9 + i].value == cellValue)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isPossibleColx(int cellValue, int col, SudokuBoard board)
        {
            for (var i = 0; i <= 8; i++)
            {
                if (board.cells[col + 9 * i].value == cellValue)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isPossibleBlockx(int cellValue, int block, SudokuBoard board)
        {
            for (var i = 0; i <= 8; i++)
            {
                if (board.cells[(int)(Math.Floor((double)(block / 3)) * 27 + i % 3 + 9 * Math.Floor((double)(i / 3)) + 3 * (block % 3))].value == cellValue)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isPossibleNumberx(int cellNumber, int cellValue, SudokuBoard board)
        {
            var row = Sudoku.Board.Helpers.rowOfCellNumber(cellNumber);
            var col = Helpers.colOfCellNumber(cellNumber);
            var block = Helpers.blockOfCellNumber(cellNumber);
            return isPossibleRowx(cellValue, row, board) && isPossibleColx(cellValue, col, board) && isPossibleBlockx(cellValue, block, board);
        }

        public static bool isRowSolvedx(int row, SudokuBoard board)
        {
            var rowValues = new int[9];
            for (var i = 0; i <= 8; i++)
            {
                rowValues[i] = board.cells[row * 9 + i].value;
            }

            return isZoneSolved(rowValues);
        }

        static bool isZoneSolved(int[] testSequence)
        {
            var correctSequenceOfValue = "123456789";
            Array.Sort(testSequence);
            var testSequenceStringified = String.Join("", Array.ConvertAll<int, string>(testSequence, Convert.ToString));
            // Console.WriteLine($"Are the sequences @{refSequence} @{testSequenceStringified} equal?: @{refSequence == testSequenceStringified}");
            return correctSequenceOfValue == testSequenceStringified;
        }

        public static bool isColSolvedx(int col, SudokuBoard board)
        {
            // var rightSequence = new Array(1, 2, 3, 4, 5, 6, 7, 8, 9);
            var colTemp = new int[9];
            for (var i = 0; i <= 8; i++)
            {
                colTemp[i] = board.cells[col + i * 9].value;
            }
            // colTemp.sort();
            // return colTemp.join() == rightSequence.join();
            return isZoneSolved(colTemp);
        }

        public static bool isBlockSolvedx(int block, SudokuBoard board)
        {
            // var rightSequence = new Array(1, 2, 3, 4, 5, 6, 7, 8, 9);
            var blockTemp = new int[9];
            for (var i = 0; i <= 8; i++)
            {
                // blockTemp[i] = board.cells[(int)(Math.Floor((double)(block / 3)) * 27 + i % 3 + 9 * Math.Floor((double)(i / 3)) + 3 * (block % 3))].value;
                blockTemp[i] = board.cells[Helpers.cellIndexFromBlock(block, i)].value;
            }
            // blockTemp.sort();
            // return blockTemp.join() == rightSequence.join();
            return isZoneSolved(blockTemp);
        }

        public static bool isBoardSolvedx(SudokuBoard board)
        {
            for (var i = 0; i <= 8; i++)
            {
                if (!isBlockSolvedx(i, board) || !isRowSolvedx(i, board) || !isColSolvedx(i, board))
                {
                    return false;
                }
            }
            return true;
        }

        public static int[] remainingNumbers(SudokuBoardCell[] sudokuBoardCells)
        {

            var remainingNumbers = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
            foreach (var cell in sudokuBoardCells)
            {
                remainingNumbers[cell.value] -= 1;
            }
            return remainingNumbers;
            // return sudokuBoardCells.it reduce<number[]>((tmp, cell, _, __) =>
            // {
            //     tmp[cell.value - 1] -= 1;
            //     return tmp;

            // }, init);
        }

        public static (SudokuBoard board, SudokuBoard[] boards) PopBoard(SudokuBoard[] boards)
        {
            if ( boards.Length > 1)
            {
                SudokuBoard[] newBoardHistory = new SudokuBoard[boards.Length - 1];
                Array.Copy(boards, newBoardHistory, boards.Length - 1);
                return (board: boards[boards.Length - 1], boards: newBoardHistory);
            }
            else
            {
                return (board: null, boards: boards);
            }
        }

        public static SudokuBoard[] Push(SudokuBoard[] boards, SudokuBoard board)
        {
            SudokuBoard[] newBoards = new SudokuBoard[boards.Length + 1];
            Array.Copy(boards, newBoards, boards.Length);
            newBoards[boards.Length] = board;
            return newBoards;
        }


        public static SudokuBoard removeDraftedValueInZone(SudokuBoard board, int value, int cellNumber)
        {
            var newBoard = Helpers.sudokuBoardClone(board);

            var row = Helpers.rowOfCellNumber(cellNumber);
            var col = Helpers.colOfCellNumber(cellNumber);
            var block = Helpers.blockOfCellNumber(cellNumber);

            removeDraftedValueInRow(value, row, newBoard);
            removeDraftedValueInCol(value, col, newBoard);
            removeDraftedValueInBlock(value, block, newBoard);

            return newBoard;

            static void removeDraftedValueInRow(int value, int row, SudokuBoard board)
            {
                for (var i = 0; i <= 8; i++)
                {
                    board.cells[row * 9 + i].drafted = removeDraftedValue(value, board.cells[row * 9 + i].drafted);
                }
            }

            static void removeDraftedValueInCol(int value, int col, SudokuBoard board)
            {
                for (var i = 0; i <= 8; i++)
                {
                    board.cells[col + 9 * i].drafted = removeDraftedValue(value, board.cells[col + 9 * i].drafted);
                }
            }

            static void removeDraftedValueInBlock(int value, int block, SudokuBoard board)
            {
                for (var i = 0; i <= 8; i++)
                {
                    // board.cells[cellIndexFromBlock(block,i)].drafted = removeDraftedValue(value, board.cells[Math.floor(block / 3) * 27 + i % 3 + 9 * Math.floor(i / 3) + 3 * (block % 3)].drafted);
                    board.cells[Helpers.cellIndexFromBlock(block, i)].drafted = removeDraftedValue(value, board.cells[Helpers.cellIndexFromBlock(block, i)].drafted);
                }
            }

            static bool[] removeDraftedValue(int value, bool[] draftedValues)
            {
                if (draftedValues[value - 1])
                {
                    draftedValues[value - 1] = false;
                }
                return draftedValues;
            }
        }


    }

}
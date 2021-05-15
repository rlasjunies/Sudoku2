using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Board
{

    public record Solution(int cell, int value);

    public record SolutionByRules(Solution[] uniquePossibleValue, Solution[] uniqueOccurrenceInZone);

    public record SudokuWizardConfiguration
    {
        bool calculatePossibleValues { get; init; }
        bool showUniquePossibleValueInRowOrColumn { get; init; }
        bool showUniquePossibleValueInZones { get; init; }
        bool showErrornousCells { get; init; }
        bool showIdenticalNumber { get; init; }
    }

    [Serializable]  
    public record SudokuBoard
    {
        public SudokuBoardCell[] cells { get; set; }
        public int[] incorrectCells { get; set; }
        public int[] remainingNumbers { get; set; }
    }

    public record SudokuBoardCell
    {
        public int value { get; set; }
        public bool[] drafted { get; set; }
        public List<int> calculatedPossibleValues { get; set; }
        public bool seed { get; set; }
        public int expectedValue { get; set; }
    }

    public enum SudokuLevelType { easy, medium, complex, veryComplex }

    public static class Board
    {

        // export function visualize(board: SudokuBoard, name?: string)
        // {
        //     let clone = { ...board };
        //     let boardToWrite: string;

        //     boardToWrite = "*******************\n";
        //     boardToWrite += "* " + name + "\n";
        //     boardToWrite += "*******************\n";
        //     for (let lineCounter = 0; lineCounter < 9; lineCounter++)
        //     {
        //         const boardLine = clone.cells.splice(0, 9);
        //         const boardLineWithSpace = boardLine.map(replaceNullBySpace())
        //         if (lineCounter != 0 && lineCounter % 3 == 0)
        //             boardToWrite += "*-----------------*\n";
        //         boardToWrite += "*" +
        //             boardLineWithSpace.splice(0, 3).join(":") + "|" +
        //             boardLineWithSpace.splice(0, 3).join(":") + "|" +
        //             boardLineWithSpace.splice(0, 3).join(":") +
        //             "*\n";
        //     }
        //     boardToWrite += "*******************\n";
        //     console.log(boardToWrite);

        //     function replaceNullBySpace(): (cell: SudokuBoardCell) => number | " " {
        //         return cell =>
        //         {
        //             if (cell.value == null)
        //                 return " ";
        //             return cell.value;
        //         };
        //     }
        // }

        static public void visualize(SudokuBoardCell board)
        {
            throw new NotImplementedException();
        }

        private static SudokuBoard removeDraftedValueInZone(SudokuBoard board, int value, int cellNumber)
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

        public static SudokuBoard generateSudokuBoard(SudokuLevelType level)
        {
            var solvedBoard = Generator.generateBoard();
            // var sudokuBoard = { ...solvedBoard };
            var sudokuBoard = solvedBoard;
            // "easy":         62
            // "medium":       53
            // "hard":         44
            // "very-hard":    35
            // "insane":       26
            // "inhuman":      17

            // TODO
            // TODO
            // TODO
            // TODO
            // TODO
            // TODO     How to define if it's in test or not?
            // TODO
            // TODO
            // TODO
            // TODO
            // TODO
            bool testEnvironment = true;

            var numberOfComplexity = (level.ToString(), testEnvironment) switch
            {
                (_, true) => 81 - 78,
                ("easy", _) => 81 - 62,
                ("medium", _) => (81 - 53),
                ("complex", _) => (81 - 44),
                ("very Complex", _) => (81 - 35),
                (_, _) => throw new NotSupportedException(),
            };

            // let's shuffled the array of number 
            // var cellsToHide = hideCells(numberOfComplexity);
            var cellsToHide = Enumerable.Range(0, 80).ToArray().Shuffle_knuthfisheryates2();

            // for the complexity define, use this array shuffled to "remove" the values of the cells
            for (var index = 0; index < numberOfComplexity; index++)
            {
                var cellToHide = cellsToHide[index] - 1;
                // console.log(`index:${index} - cellsToHide[${index}]: ${cellToHide}`);
                // console.log(`index:${index} - cellsToHide[${index}]: ${cellToHide} - sudokuBoard.cells[index].expectedValue:${sudokuBoard.cells[cellToHide].expectedValue} `);


                // sudokuBoard.cells[cellToHide] = EMPTYCELL with { expectedValue: sudokuBoard.cells[cellToHide].expectedValue };
                // var temp = EMPTYCELL;
                // temp.expectedValue = sudokuBoard.cells[cellToHide].expectedValue;
                sudokuBoard.cells[cellToHide].value = 0;
                sudokuBoard.cells[cellToHide].seed = false;
            }

            // update the remaining number of numbers
            sudokuBoard.remainingNumbers = Helpers.remainingNumbers(sudokuBoard.cells);

            // TODO evaluate the complexity to solve
            return sudokuBoard;
        }

        // int[] hideCells(int numberOfComplexity)
        // {

        //     var cellsToHide = knuthfisheryates2(Enumerable.Range(0, 80).ToArray());

        //     // var rowsRemainingValues = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
        //     // var colsRemainingValues = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9 };
        //     for (var index = 0; index < numberOfComplexity; index++)
        //     {
        //         var cellToHide = cellsToHide[index] - 1;
        //         // rowsRemainingValues[rowOfCellNumber(cellToHide)] -= 1;
        //         // colsRemainingValues[colOfCellNumber(cellToHide)] -= 1;
        //     }
        //     // if (rowsRemainingValues.find(value => value === 0) === 0 ||
        //     //     colsRemainingValues.find(value => value === 0) === 0)
        //     // {
        //     //     // testEnvironment && console.debug(...DEV_MODE, rowsRemainingValues);
        //     //     // testEnvironment && console.debug(...DEV_MODE, colsRemainingValues);
        //     //     // testEnvironment && console.debug(...DEV_MODE, `board WITHOUT rows or cols let's try again`);
        //     //                     // debugger;
        //     //     return hideCells(numberOfComplexity);
        //     // }
        //     // else {
        //     //    testEnvironment && console.debug(...DEV_MODE, `!!!!board ok `);
        //     //     testEnvironment && console.debug(...DEV_MODE, rowsRemainingValues);
        //     //     testEnvironment && console.debug(...DEV_MODE, colsRemainingValues);
        //     //}

        //     return cellsToHide;
        // }

        // calculate the number of each numbers in cell
        // this is used to hide key when the a specific number has been used 9th times


        public static SudokuBoard possibleValues(SudokuBoard board)
        {
            var boardWithPossibleValues = Helpers.sudokuBoardClone(board);

            for (var index = 0; index < boardWithPossibleValues.cells.Length; index++)
            {
                if (boardWithPossibleValues.cells[index].value == 0)
                {
                    var possibleValue = Helpers.determinePossibleValuesx(index, boardWithPossibleValues);
                    boardWithPossibleValues.cells[index].calculatedPossibleValues = possibleValue;
                }
                else
                {
                    boardWithPossibleValues.cells[index].calculatedPossibleValues = new List<int>();
                }
            }

            return boardWithPossibleValues;
        }
        
        /**
         * looks for solution of each empty cell in the board
         * based on rules
         * 
         * @param board 
         */
        private static (List<(int cell, int value)> uniquePossibleValue, List<(int cell, int value)> uniqueOccurenceInZones) resolveByRules(SudokuBoard board)
        {

            // calculate the possibleValues of all the cells
            var boardWithPossibleValues = possibleValues(board);

            // rule#1: unique possible value
            var cellsWithUniquePossibleValue = rule1_cellsWithUniquePossibleValueParser(boardWithPossibleValues);

            // rule#2.1: unique occurence of possible value in the cell row
            // rule#2.2: unique occurence of possible value in the cell column
            // rule#2.3: unique occurence of possible value in the cell block

            var uniqueOcurrenceOfPossibleValueInZones = rule2_cellsWithUniqueOccurenceOfPossibleValueInCellZones(boardWithPossibleValues);

            // rule#3: unique possibility of value in 3 lines or rows

            return (
            uniquePossibleValue: cellsWithUniquePossibleValue,
        uniqueOccurenceInZones: uniqueOcurrenceOfPossibleValueInZones
            );

            static List<(int cell, int value)> rule1_cellsWithUniquePossibleValueParser(SudokuBoard board)
            {
                // the board should already have the uniquePossibleValuesParsed
                // var uniquePossibleValues = new int[81];
                // var uniquePossibleValues = new List<(int cell, int[] value)>();
                var uniquePossibleValues = new List<(int cell, int value)>();
                for (var index = 0; index < board.cells.Length; index++)
                {
                    if (board.cells[index].calculatedPossibleValues.Count == 1)
                    {
                        var possibleValue = board.cells[index].calculatedPossibleValues[0];
                        // console.log(`cell:${index} unique solution:${possibleValue}}`);
                        uniquePossibleValues.Add((cell: index, value: possibleValue));
                    }
                }
                return uniquePossibleValues;
            }

            static List<(int cell, int value)> rule2_cellsWithUniqueOccurenceOfPossibleValueInCellZones(SudokuBoard board)
            {
                var uniqueOcurrenceOfPossibleValue = new List<(int cell, int value)>();
                for (var cellIndex = 0; cellIndex < board.cells.Length; cellIndex++)
                {
                    // for each possible value
                    // only if NOT in the case **unique**PossibleValue
                    if (board.cells[cellIndex].calculatedPossibleValues.Count > 1)
                    {
                        foreach (var possibleValue in board.cells[cellIndex].calculatedPossibleValues)
                        {
                            // is this value in another zone
                            // yes, continue it's not unique
                            // no, add in the rule solution
                            var uniqueOccurenceInARow = (1 == numberOfOccurenceInCalculatedPossibleValuesInRow(board, cellIndex, possibleValue));
                            if (uniqueOccurenceInARow)
                            {
                                // console.log(`21 - Unique occurence in a row ${cellIndex} - value:${possibleValue}`);
                                uniqueOcurrenceOfPossibleValue.Add((cell: cellIndex, value: possibleValue));
                            }

                            var uniqueOccurenceInACol = (1 == numberOfOccurenceInCalculatedPossibleValuesInCol(board, cellIndex, possibleValue));
                            if (uniqueOccurenceInACol)
                            {
                                // console.log(`22 - Unique occurence in a col ${cellIndex} - value:${possibleValue}`);
                                uniqueOcurrenceOfPossibleValue.Add((cell: cellIndex, value: possibleValue));
                            }

                            var uniqueOccurenceInABlock = (1 == numberOfOccurenceInCalculatedPossibleValuesInBlock(board, cellIndex, possibleValue));
                            if (uniqueOccurenceInABlock)
                            {
                                // console.log(`23 - Unique occurence in a block ${blockOfCellNumber(cellIndex)} - value:${possibleValue}`);
                                uniqueOcurrenceOfPossibleValue.Add((cell: cellIndex, value: possibleValue));
                            }
                        }
                    }
                }
                return uniqueOcurrenceOfPossibleValue;
            }
        }


        static int numberOfOccurenceInCalculatedPossibleValuesInRow(SudokuBoard board, int cellNumber, int possibleValue)
        {
            var row = Helpers.rowOfCellNumber(cellNumber);
            var numberOfOccurence = 0;

            // for each column of the row
            // is the the possibleValue in the possibleValues?
            for (var colIndex = 0; colIndex < 9; colIndex++)
            {
                // incrementNumberOfOccurenceIfItsPossibleValue(, possibleValue);
                if (board.cells[row * 9 + colIndex].calculatedPossibleValues.Exists(value => value == possibleValue))
                {
                    numberOfOccurence += 1;
                }

            }
            return numberOfOccurence;
        }

        static int numberOfOccurenceInCalculatedPossibleValuesInCol(SudokuBoard board, int cellNumber, int possibleValue)
        {
            var col = Helpers.colOfCellNumber(cellNumber);
            var numberOfOccurence = 0;

            // for each column of the row
            // is the the possibleValue in the possibleValues?
            for (var rowIndex = 0; rowIndex < 9; rowIndex++)
            {
                if (board.cells[rowIndex * 9 + col].calculatedPossibleValues.Exists(value => value == possibleValue))
                {
                    numberOfOccurence += 1;
                }

            }

            return numberOfOccurence;
        }

        static int numberOfOccurenceInCalculatedPossibleValuesInBlock(SudokuBoard board, int cellIndex, int possibleValue)
        {
            var block = Helpers.blockOfCellNumber(cellIndex);
            var numberOfOccurence = 0;

            // for each cells in the block
            // is the the possibleValue in the possibleValues?
            foreach( var cellIndexInBlock in Helpers.cellsNumberOfTheBlock(block))
            {
                // incrementNumberOfOccurenceIfItsPossibleValue();
                if (board.cells[cellIndexInBlock].calculatedPossibleValues.Exists(value => value == possibleValue))
                {
                    numberOfOccurence += 1;
                }
                // numberOfOccurence += 1;
            };

            return numberOfOccurence;

            // function incrementNumberOfOccurenceIfItsPossibleValue(cell: SudokuBoardCell) {

            // }
        }
    }

}
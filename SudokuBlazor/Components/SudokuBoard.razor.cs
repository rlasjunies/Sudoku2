using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;

namespace Sudoku.Components
{
    public partial class SudokuBoardBase : AccComponentBase
    {
        // @Element() element: HTMLSudokuBoardComponentElement;

        [Parameter]
        public EventCallback<int> OnCellSelection { get; set; }

        [Parameter]
        public int solvedRow { get; set; }

        // TODO manage the setter replacement of Stencil Watch
        // @Watch("solvedRow")
        // solvedRowWatcher(newValue: number, _oldValue: number) {
        //     // console.log(`solvedRowWatcher:${newValue}, ${this.cellSelected}, ${rowOfCellNumber(this.cellSelected)} ${oldValue}`);
        //     if (newValue === rowOfCellNumber(this.cellSelected)) {
        //         this.chenillardRow(this.cellSelected);
        //     }
        // }
        [Parameter]
        public int solvedCol { get; set; }
        // TODO manage the setter replacement of Stencil Watch
        // @Watch("solvedCol")
        // solvedColWatcher(newValue: number, _oldValue: number) {
        //     // console.log(`solvedColWatcher:${newValue}, ${this.cellSelected},${oldValue}`);
        //     if (newValue === colOfCellNumber(this.cellSelected)) {
        //         this.chenillardCol(this.cellSelected);
        //     }
        // }
        [Parameter]
        public int solvedBlock { get; set; }
        // TODO manage the setter replacement of Stencil Watch
        // @Watch("solvedBlock")
        // solvedBlockWatcher(newValue: number, _oldValue: number) {
        //     // console.log(`solvedBlockWatcher:${newValue}, ${this.cellSelected},${oldValue}`);
        //     if (newValue === blockOfCellNumber(this.cellSelected)) {
        //         this.chenillardBlock(this.cellSelected);
        //     }
        // }

        [Parameter]
        public bool gameOnGoing { get; set; }
        // TODO manage the setter replacement of Stencil Watch
        // @Watch("gameOnGoing")
        // endOfGameWatcher(newValue: boolean, oldValue: boolean) {
        //     // console.log(`endOfGameWatcher:${newValue}, ${oldValue} - cellSelected:${this.lastCellOfTheGame}`);
        //     if (!newValue
        //         && oldValue
        //         && (oldValue !== undefined)) {
        //         // this.chenillardBoard(this.lastCellOfTheGame);
        //         this.chenillardBoard(this.lastCellOfTheGame);
        //     }
        // }
        [Parameter]
        // conversion to .NET
        // public SudokuBoard board { get; set; } = initializeSudokuBoard();
        public Sudoku.Board.SudokuBoard board { get; set; } 
        // @Watch("board")
        // boardWatcher(newValue: SudokuBoard) {

        //     // reinitialize the board class
        //     this.classboard = [];

        //     const selectedValue = selectedCellValue;
        //     const selectedValueNumber = parseInt(selectedValue);
        //     const colOfCellSelected = colOfCellNumber(this.cellSelected);
        //     const rowOfCellSelected = rowOfCellNumber(this.cellSelected);
        //     const blockOfCellSelected = blockOfCellNumber(this.cellSelected);


        //     newValue.cells.forEach((cell, index) => {
        //         console.log(`index:${index} - cell:${cell}`)
        //         const colOfCell = colOfCellNumber(index);
        //         const rowOfCell = rowOfCellNumber(index);
        //         const blockOfCell = blockOfCellNumber(index);    

        //         if (selectedValue === "") {
        //             // mode input
        //             console.log("--- mode input");

        //             // highlight area selected
        //             this.classboard[index] += ( (colOfCell === colOfCellSelected) ||
        //                                         (rowOfCell === rowOfCellSelected) ||
        //                                         (blockOfCell === blockOfCellSelected)) ? " area-selected " : "";

        //             // highlight the selected cell
        //             this.classboard[index] += (this.cellSelected === index) ? " selected " : "";

        //         }else {
        //             // mode same number
        //             console.log("--- mode same number");

        //             if ((cell.value) === selectedValueNumber){
        //                 // highlight the value with the same value
        //             }
        //         }
        //     })

        // }

        [Parameter]
        public int cellSelected { get; set; } = -1;

        [Parameter]
        public int lastCellOfTheGame { get; set; } = -1;

        [Parameter]
        public int[] incorrectCells { get; set; } = new int[] { };

        // TODO centralize initializer for SolutionByRules or support null
        [Parameter]
        public SolutionByRules solutionsByRules { get; set; } = new Board.SolutionByRules()
        {
            uniqueOccurrenceInZone = new List<Board.Solution>(),
            uniquePossibleValue = new List<Board.Solution>(),
        };

        // TODO centralize initializer for SudokuWizard or support null
        [Parameter]
        public SudokuWizardConfiguration wizardConfiguration { get; set; } = new Board.SudokuWizardConfiguration
        {
            calculatePossibleValues = false,
            showUniquePossibleValueInZones = false,
            showUniquePossibleValueInRowOrColumn = false,
            showErrornousCells = true,
            showIdenticalNumber = true
        };

        // TODO activate chenillard
        protected void chenillardBoard(int cell)
        {
            var startCell = cell;
            var col = hlpr.colOfCellNumber(startCell);
            var row = hlpr.rowOfCellNumber(startCell);
            var block = hlpr.blockOfCellNumber(startCell);

            var coeff = 6;
            var i = coeff * 0;
            // chenillardNorth(this.element, col, row, block, false, 0);
            // chenillardSouth(this.element, col, row, block, false, 0);
            // chenillardWest(this.element, col, row, block, false, 0);
            // chenillardEast(this.element, col, row, block, false, 0);
            // chenillardNorthEast(this.element, col, row, block, false, 0);
            // chenillardSouthEast(this.element, col, row, block, false, 0);
            // chenillardNorthWest(this.element, col, row, block, false, 0);
            // chenillardSouthWest(this.element, col, row, block, false, 0);
            // i = coeff * 1;
            // chenillardNorth(this.element, col, row, block, false, i);
            // chenillardSouth(this.element, col, row, block, false, i);
            // chenillardWest(this.element, col, row, block, false, i);
            // chenillardEast(this.element, col, row, block, false, i);
            // chenillardNorthEast(this.element, col, row, block, false, i);
            // chenillardSouthEast(this.element, col, row, block, false, i);
            // chenillardNorthWest(this.element, col, row, block, false, i);
            // chenillardSouthWest(this.element, col, row, block, false, i);
            // i = coeff * 2;
            // chenillardNorth(this.element, col, row, block, false, i);
            // chenillardSouth(this.element, col, row, block, false, i);
            // chenillardWest(this.element, col, row, block, false, i);
            // chenillardEast(this.element, col, row, block, false, i);
            // chenillardNorthEast(this.element, col, row, block, false, i);
            // chenillardSouthEast(this.element, col, row, block, false, i);
            // chenillardNorthWest(this.element, col, row, block, false, i);
            // chenillardSouthWest(this.element, col, row, block, false, i);
        }

        protected void chenillardRow(int startCell)
        {
            var col = hlpr.colOfCellNumber(startCell);
            var row = hlpr.rowOfCellNumber(startCell);
            var block = hlpr.blockOfCellNumber(startCell);

            // chenillardWest(this.element, col, row, block, false, 0);
            // chenillardEast(this.element, col, row, block, false, 0);
        }

        protected void chenillardCol(int startCell)
        {
            var col = hlpr.colOfCellNumber(startCell);
            var row = hlpr.rowOfCellNumber(startCell);
            var block = hlpr.blockOfCellNumber(startCell);

            // chenillardNorth(this.element, col, row, block, false, 0);
            // chenillardSouth(this.element, col, row, block, false, 0);
        }

        protected void chenillardBlock(int startCell)
        {
            var col = hlpr.colOfCellNumber(startCell);
            var row = hlpr.rowOfCellNumber(startCell);
            var block = hlpr.blockOfCellNumber(startCell);

            // chenillardNorth(this.element, col, row, block, true, 0);
            // chenillardSouth(this.element, col, row, block, true, 0);
            // chenillardWest(this.element, col, row, block, true, 0);
            // chenillardEast(this.element, col, row, block, true, 0);
            // chenillardNorthEast(this.element, col, row, block, true, 0);
            // chenillardSouthEast(this.element, col, row, block, true, 0);
            // chenillardNorthWest(this.element, col, row, block, true, 0);
            // chenillardSouthWest(this.element, col, row, block, true, 0);
        }

        // chenillardBoard(startCell: number) {
        //     const col = colOfCellNumber(startCell);
        //     const row = rowOfCellNumber(startCell);
        //     const block = blockOfCellNumber(startCell);

        //     chenillardNorth(this.element, col, row, block, true, 0);
        //     chenillardSouth(this.element, col, row, block, true, 0);
        //     chenillardWest(this.element, col, row, block, true, 0);
        //     chenillardEast(this.element, col, row, block, true, 0);
        //     chenillardNorthEast(this.element, col, row, block, true, 0);
        //     chenillardSouthEast(this.element, col, row, block, true, 0);
        //     chenillardNorthWest(this.element, col, row, block, true, 0);
        //     chenillardSouthWest(this.element, col, row, block, true, 0);
        // }

        protected string selectedCellValue()
        {
            return cellSelected != -1 ? board.cells[cellSelected].value + "" : "";
        }

        // classboard:string[] = []


        protected string returnClassForTheCell(int cell)
        {
            // console.log("this.cellSelected",this.cellSelected);
            // conversion to .NET var isCellSelectedCorrect = (cellSelected != -1) && (cellSelected is null);
            var isCellSelectedCorrect = (cellSelected != -1);

            // conversion to .ENT var selectedCellValue = isCellSelectedCorrect ? board.cells[cellSelected].value : null;
            var selectedCellValue = isCellSelectedCorrect ? board.cells[cellSelected].value : -1;
            var cellValue = this.board.cells[cell].value;

            // conversion to .NET var modeEntry = isNullOrEmpty(selectedCellValue) ;
            var modeEntry = selectedCellValue == -1;

            var modeHighlightNumber = !modeEntry;
            // testEnvironment && console.debug(...BOARD_DEV_MODE,`cellSelected:${this.cellSelected} - SelectedValue:${selectedCellValue} - cellValue:${cellValue} - modeEntry:${modeEntry}`);

            var colOfCell = hlpr.colOfCellNumber(cell);
            var rowOfCell = hlpr.rowOfCellNumber(cell);
            var blockOfCell = hlpr.blockOfCellNumber(cell);

            var colOfCellSelected = hlpr.colOfCellNumber(cellSelected);
            var rowOfCellSelected = hlpr.rowOfCellNumber(cellSelected);
            var blockOfCellSelected = hlpr.blockOfCellNumber(cellSelected);

            // Conversion to .NET var isIncorrectCell = wizardConfiguration.showErrornousCells ? incorrectCells.lastIndexOf(cell) !== -1 : false;
            // TODO not so sure about this conversion ... need to be double checked
            var isIncorrectCell = wizardConfiguration.showErrornousCells ? incorrectCells[cell] != -1 : false;
            var incorrectClass = isIncorrectCell ? " incorrect " : "";

            // console.log(`cell:${cell} - selected:${this.cellSelected} - ${((this.cellSelected !== -1) && (this.cellSelected !== null))}`);

            var isNotTheSelectedCell = this.cellSelected != cell;
            var isTheSelectedCell = this.cellSelected == cell;
            var isColRowOrBlockSelected = (colOfCell == colOfCellSelected) || (rowOfCell == rowOfCellSelected) || (blockOfCell == blockOfCellSelected);
        

    var areaSelectedClass = (modeEntry && isCellSelectedCorrect && isNotTheSelectedCell && isColRowOrBlockSelected) ? " area-selected " : "";
            var cellSelectedClass = isTheSelectedCell ? " selected " : "";
            var sameValueAsTheSelectedCellClass = modeHighlightNumber
                                                    && cellValue == selectedCellValue
                                                    && this.wizardConfiguration.showIdenticalNumber ? " sameValueAsTheOneSelected " : "";

            // testEnvironment && console.debug(...BOARD_DEV_MODE,`modeHighlightNumber: ${modeHighlightNumber} - sameValueAsTheSelectedCellClass:${sameValueAsTheSelectedCellClass}`);
            // const sameValueAsTheOneSelected = 
            //     (
            //         (selectedCellValue !== "null") &&
            //         (selectedCellValue == cellValue) &&
            //         (selectedCellValue !== "undefined") &&
            //         (cellValue !== "undefined")
            //     ) ? " selected " : "";
            // TODO: console.log(`selectCellValue:${selectedCellValue} - cellValue:${cellValue} - ${sameValueAsTheSelectedCellClass} - ${typeof (selectedCellValue)} - ${typeof (cellValue)}`);

            var isSolution_UniqueOccurenceInZoneClass = "";
            var isSolution_UniquePossibleValueClass = "";

            if (wizardConfiguration.showUniquePossibleValueInRowOrColumn)
            {
                var isSolution_UniquePossibleValue = solutionsByRules.uniquePossibleValue.FindAll(solution => solution.cell == cell).Count > 0;
                isSolution_UniquePossibleValueClass = isSolution_UniquePossibleValue ? " UniquePossibleValue" : "";
            }
            if (this.wizardConfiguration.showUniquePossibleValueInZones)
            {
                var isSolution_UniquePossibleValueInZone = solutionsByRules.uniqueOccurrenceInZone.FindAll(solution => solution.cell == cell).Count > 0;
                isSolution_UniqueOccurenceInZoneClass = isSolution_UniquePossibleValueInZone ? " UniqueOccurenceInZone" : "";
            }

            return $"cell" +
                $" cell{cell} " +
                $" row{rowOfCell} " +
                $" column{colOfCell} " +
                $" block{blockOfCell}" +
                incorrectClass +
                (isIncorrectCell ? "" : areaSelectedClass) +
                (isIncorrectCell ? "" : cellSelectedClass) +
                (isIncorrectCell ? "" : sameValueAsTheSelectedCellClass) +
                (isIncorrectCell ? "" : isSolution_UniquePossibleValueClass) +
                (isIncorrectCell ? "" : isSolution_UniqueOccurenceInZoneClass);
        }

        protected void cellSelectedHandler(int cell)
        {
            OnCellSelection.InvokeAsync(cell);
        }
    }
}
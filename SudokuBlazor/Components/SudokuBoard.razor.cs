using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Sudoku.Board;
using hlpr = Sudoku.Board.Helpers;

namespace Sudoku.Components
{
    public partial class SudokuBoardBase : AccComponentBase
    {
        [Inject]
        protected ILogger<SudokuBoardBase> Logger { get; set; }

        // @Element() element: HTMLSudokuBoardComponentElement;

        [Parameter]
        public EventCallback<int> OnCellSelection { get; set; }

        private int _solvedRow;
        [Parameter]
        public int solvedRow { 
            get => _solvedRow;
            set {
                // Logger.LogDebug($"SolvedRow:{value} OldSolvedRow:{_SolvedRow}");
                if (_solvedRow != value)
                {
                    _solvedRow = value;
                }; 
        }
}
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
        public int selectedCell { get; set; } = Sudoku.Store.Game.Const.NoCellSelected;

        [Parameter]
        public int lastCellOfTheGame { get; set; } = Sudoku.Store.Game.Const.NoCellSelected;


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
        //protected void chenillardBoard(int cell)
        //{
        //    var startCell = cell;
        //    var col = hlpr.colOfCellNumber(startCell);
        //    var row = hlpr.rowOfCellNumber(startCell);
        //    var block = hlpr.blockOfCellNumber(startCell);

        //    var coeff = 6;
        //    var i = coeff * 0;
        //    // chenillardNorth(this.element, col, row, block, false, 0);
        //    // chenillardSouth(this.element, col, row, block, false, 0);
        //    // chenillardWest(this.element, col, row, block, false, 0);
        //    // chenillardEast(this.element, col, row, block, false, 0);
        //    // chenillardNorthEast(this.element, col, row, block, false, 0);
        //    // chenillardSouthEast(this.element, col, row, block, false, 0);
        //    // chenillardNorthWest(this.element, col, row, block, false, 0);
        //    // chenillardSouthWest(this.element, col, row, block, false, 0);
        //    // i = coeff * 1;
        //    // chenillardNorth(this.element, col, row, block, false, i);
        //    // chenillardSouth(this.element, col, row, block, false, i);
        //    // chenillardWest(this.element, col, row, block, false, i);
        //    // chenillardEast(this.element, col, row, block, false, i);
        //    // chenillardNorthEast(this.element, col, row, block, false, i);
        //    // chenillardSouthEast(this.element, col, row, block, false, i);
        //    // chenillardNorthWest(this.element, col, row, block, false, i);
        //    // chenillardSouthWest(this.element, col, row, block, false, i);
        //    // i = coeff * 2;
        //    // chenillardNorth(this.element, col, row, block, false, i);
        //    // chenillardSouth(this.element, col, row, block, false, i);
        //    // chenillardWest(this.element, col, row, block, false, i);
        //    // chenillardEast(this.element, col, row, block, false, i);
        //    // chenillardNorthEast(this.element, col, row, block, false, i);
        //    // chenillardSouthEast(this.element, col, row, block, false, i);
        //    // chenillardNorthWest(this.element, col, row, block, false, i);
        //    // chenillardSouthWest(this.element, col, row, block, false, i);
        //}

        //protected void chenillardRow(int startCell)
        //{
        //    var col = hlpr.colOfCellNumber(startCell);
        //    var row = hlpr.rowOfCellNumber(startCell);
        //    var block = hlpr.blockOfCellNumber(startCell);

        //    chenillardWest(this.element, col, row, block, false, 0);
        //    chenillardEast(this.element, col, row, block, false, 0);
        //}

        //protected void chenillardCol(int startCell)
        //{
        //    var col = hlpr.colOfCellNumber(startCell);
        //    var row = hlpr.rowOfCellNumber(startCell);
        //    var block = hlpr.blockOfCellNumber(startCell);

        //    // chenillardNorth(this.element, col, row, block, false, 0);
        //    // chenillardSouth(this.element, col, row, block, false, 0);
        //}

        //protected void chenillardBlock(int startCell)
        //{
        //    var col = hlpr.colOfCellNumber(startCell);
        //    var row = hlpr.rowOfCellNumber(startCell);
        //    var block = hlpr.blockOfCellNumber(startCell);

        //    // chenillardNorth(this.element, col, row, block, true, 0);
        //    // chenillardSouth(this.element, col, row, block, true, 0);
        //    // chenillardWest(this.element, col, row, block, true, 0);
        //    // chenillardEast(this.element, col, row, block, true, 0);
        //    // chenillardNorthEast(this.element, col, row, block, true, 0);
        //    // chenillardSouthEast(this.element, col, row, block, true, 0);
        //    // chenillardNorthWest(this.element, col, row, block, true, 0);
        //    // chenillardSouthWest(this.element, col, row, block, true, 0);
        //}

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

        //protected string selectedCellValue()
        //{
        //    return cellSelected != Sudoku.Store.Game.Const.NoCellSelected ? board.cells[cellSelected].value + "" : "";
        //}

        // classboard:string[] = []

        protected string returnClassForTheCell(int cell)
        {
            if (this.board is not null)
            {
                //Logger.LogDebug($".....cell[{cell}] - cellSelected:{selectedCell}");
                //if (selectedCell != Sudoku.Store.Game.Const.NoCellSelected ) Logger.LogDebug($"cell[{cell}] - board.cells[selectedCell].value:{board.cells[selectedCell].value}");
                //Logger.LogDebug($"cell[{cell}] - board.cells[cell].value:{board.cells[cell].value}");

                var isCellSelectedCorrect = (selectedCell != Sudoku.Store.Game.Const.NoCellSelected);

                // conversion to .ENT var selectedCellValue = isCellSelectedCorrect ? board.cells[cellSelected].value : null;
                var selectedCellValue = isCellSelectedCorrect ? board.cells[selectedCell].value : Sudoku.Store.Game.Const.NoValueInTheCell;
                var cellValue = board.cells[cell].value;

                // conversion to .NET var modeEntry = isNullOrEmpty(selectedCellValue) ;
                var modeEntry = selectedCellValue == Sudoku.Store.Game.Const.NoValueInTheCell;

                var modeHighlightNumber = !modeEntry;
                // testEnvironment && console.debug(...BOARD_DEV_MODE,`cellSelected:${this.cellSelected} - SelectedValue:${selectedCellValue} - cellValue:${cellValue} - modeEntry:${modeEntry}`);

                var colOfCell = hlpr.colOfCellNumber(cell);
                var rowOfCell = hlpr.rowOfCellNumber(cell);
                var blockOfCell = hlpr.blockOfCellNumber(cell);

                var colOfCellSelected = hlpr.colOfCellNumber(selectedCell);
                var rowOfCellSelected = hlpr.rowOfCellNumber(selectedCell);
                var blockOfCellSelected = hlpr.blockOfCellNumber(selectedCell);

                // TODO not so sure about this conversion ... need to be double checked
                var isIncorrectCell = wizardConfiguration.showErrornousCells ? this.board.incorrectCells[cell] : false;
                //logger.LogDebug($"cell[{cell}] - isIncorrectCells:{incorrectCells[cell]}");
                var incorrectClass = isIncorrectCell ? " incorrect " : "";

                var isNotTheSelectedCell = this.selectedCell != cell;
                var isTheSelectedCell = this.selectedCell == cell;
                //logger.LogDebug($"cell[{cell}] - isTheCellSelected:{isTheSelectedCell}");

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
            else
            {
                Logger.LogDebug($"!!!!!!!!!! Board is not initialized yet");
                return "";
            }
        }

        protected void cellSelectedHandler(int cell)
        {
            OnCellSelection.InvokeAsync(cell);
        }
    }
}
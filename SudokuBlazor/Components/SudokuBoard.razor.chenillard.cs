////import { testEnvironment } from "../../global/global";
////import { blockOfColRow } from "../../services/sudoku/sudoku";

//namespace Sudoku.Components;

////public void  chenillardNorth(element: HTMLSudokuBoardComponentElement, col: number, row: number, block: number, stayInCurrentBlock: boolean, index: number) {

////    // highlightTheCellForChenillard
////    highlightCell(element, row, col, index);

////    // north increment
////    // leave if reach limit of the board
////    var row -= 1;
////    if (row === -1) return

////  // are we still in the same block?
////  // leave if not in same block and request to stay in same block
////    const inSameBlock = block === blockOfColRow(col, row);
////    if (stayInCurrentBlock && !inSameBlock) return;

////    // go to next cell to highlight
////    // incrementing the index
////    index += 1;
////    chenillardNorth(element, col, row, block, stayInCurrentBlock, index);
////}

////export function chenillardSouth(element: HTMLSudokuBoardComponentElement, col: number, row: number, block: number, stayInCurrentBlock: boolean, index: number) {


////    // highlightTheCellForChenillard
////    highlightCell(element, row, col, index);

////    // north increment
////    // leave if reach limit of the board
////    row += 1;
////    if (row === 9) return

////  // are we still in the same block?
////  // leave if not in same block and request to stay in same block
////    const inSameBlock = block === blockOfColRow(col, row);
////    if (stayInCurrentBlock && !inSameBlock) return;

////    // go to next cell to highlight
////    // incrementing the index
////    index += 1;
////    chenillardSouth(element, col, row, block, stayInCurrentBlock, index);
////}

//public void chenillardWest(element: HTMLSudokuBoardComponentElement, int col, int row, int block, bool stayInCurrentBlock, int index) {

//    // highlightTheCellForChenillard
//    highlightCell(element, row, col, index);

//    // north increment
//    // leave if reach limit of the board
//    col -= 1;
//    if (col == -1) return;

//  // are we still in the same block?
//  // leave if not in same block and request to stay in same block
//    const bool inSameBlock = block === blockOfColRow(col, row);
//    if (stayInCurrentBlock && !inSameBlock) return;

//    // go to next cell to highlight
//    // incrementing the index
//    index += 1;
//    chenillardWest(element, col, row, block, stayInCurrentBlock, index);
//}

////export function chenillardEast(element: HTMLSudokuBoardComponentElement, col: number, row: number, block: number, stayInCurrentBlock: boolean, index: number) {

////    // highlightTheCellForChenillard
////    highlightCell(element, row, col, index);

////    // north increment
////    // leave if reach limit of the board
////    col += 1;
////    if (col === 9) return

////  // are we still in the same block?
////  // leave if not in same block and request to stay in same block
////    const inSameBlock = block === blockOfColRow(col, row);
////    if (stayInCurrentBlock && !inSameBlock) return;

////    // go to next cell to highlight
////    // incrementing the index
////    index += 1;
////    chenillardEast(element, col, row, block, stayInCurrentBlock, index);
////}


////export function chenillardNorthEast(element: HTMLSudokuBoardComponentElement, col: number, row: number, block: number, stayInCurrentBlock: boolean, index: number) {

////    // highlightTheCellForChenillard
////    highlightCell(element, row, col, index);

////    // north increment
////    // leave if reach limit of the board
////    col += 1;
////    row -= 1;
////    if (col === 9) return
////  if (row === -1) return

////  // are we still in the same block?
////  // leave if not in same block and request to stay in same block
////    const inSameBlock = block === blockOfColRow(col, row);
////    if (stayInCurrentBlock && !inSameBlock) return;

////    // go to next cell to highlight
////    // incrementing the index
////    index += 1;
////    chenillardNorth(element, col, row, block, stayInCurrentBlock, index);
////    chenillardEast(element, col, row, block, stayInCurrentBlock, index);
////    chenillardNorthEast(element, col, row, block, stayInCurrentBlock, index);
////}
////export function chenillardSouthEast(element: HTMLSudokuBoardComponentElement, col: number, row: number, block: number, stayInCurrentBlock: boolean, index: number) {

////    // highlightTheCellForChenillard
////    highlightCell(element, row, col, index);

////    // north increment
////    // leave if reach limit of the board
////    col += 1;
////    row += 1;
////    if (col === 9) return
////  if (row === 9) return

////  // are we still in the same block?
////  // leave if not in same block and request to stay in same block
////    const inSameBlock = block === blockOfColRow(col, row);
////    if (stayInCurrentBlock && !inSameBlock) return;

////    // go to next cell to highlight
////    // incrementing the index
////    index += 1;
////    chenillardSouth(element, col, row, block, stayInCurrentBlock, index);
////    chenillardEast(element, col, row, block, stayInCurrentBlock, index);
////    chenillardSouthEast(element, col, row, block, stayInCurrentBlock, index);
////}

////export function chenillardSouthWest(element: HTMLSudokuBoardComponentElement, col: number, row: number, block: number, stayInCurrentBlock: boolean, index: number) {

////    // highlightTheCellForChenillard
////    highlightCell(element, row, col, index);

////    // north increment
////    // leave if reach limit of the board
////    col -= 1;
////    row += 1;
////    if (col === -1) return
////  if (row === 9) return

////  // are we still in the same block?
////  // leave if not in same block and request to stay in same block
////    const inSameBlock = block === blockOfColRow(col, row);
////    if (stayInCurrentBlock && !inSameBlock) return;

////    // go to next cell to highlight
////    // incrementing the index
////    index += 1;
////    chenillardSouth(element, col, row, block, stayInCurrentBlock, index);
////    chenillardWest(element, col, row, block, stayInCurrentBlock, index);
////    chenillardSouthWest(element, col, row, block, stayInCurrentBlock, index);
////}

////export function chenillardNorthWest(element: HTMLSudokuBoardComponentElement, col: number, row: number, block: number, stayInCurrentBlock: boolean, index: number) {

////    // highlightTheCellForChenillard
////    highlightCell(element, row, col, index);

////    // north increment
////    // leave if reach limit of the board
////    col -= 1;
////    row -= 1;
////    if (col === -1) return
////  if (row === -1) return

////  // are we still in the same block?
////  // leave if not in same block and request to stay in same block
////    const inSameBlock = block === blockOfColRow(col, row);
////    if (stayInCurrentBlock && !inSameBlock) return;

////    // go to next cell to highlight
////    // incrementing the index
////    index += 1;
////    chenillardNorth(element, col, row, block, stayInCurrentBlock, index);
////    chenillardWest(element, col, row, block, stayInCurrentBlock, index);
////    chenillardNorthWest(element, col, row, block, stayInCurrentBlock, index);
////}

//public void highlightCell(element: HTMLSudokuBoardComponentElement, int row, int col, int index) {
//    const int[] cell = element.querySelectorAll(`.row${ row}.column${ col}`);
//    if (cell[0] == undefined)
//    {
//        //testEnvironment && console.log(`chenillard - cannot found cell in the row - col:${ row} -${ col}`);
//        // throw `chenillard - cannot found cell in the row-col:${ row} -${ col}`;
//    }
//    else
//    {
//        addRemoveChenillardClassToElement(cell[0], index);
//    }
//}

//public void addRemoveChenillardClassToElement(element: Element, int delayCoeff) {
//    //const int delay = delayCoeff * 100;
//    //setTimeout(() => {
//    //    element.classList.add("chenillardCol");
//    //    setTimeout(() => {
//    //        element.classList.remove("chenillardCol");
//    //    }, 100);
//    //}, delay)
//}
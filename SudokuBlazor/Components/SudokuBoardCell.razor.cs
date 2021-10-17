// using System;
// using System.Runtime.CompilerServices;
// using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
// using Sudoku.Board;

namespace Sudoku.Components
{
    public partial class SudokuBoardCellBase : AccComponentBase
    {
        [Inject]
        protected ILogger<SudokuBoardCellBase> Logger { get; set; }

        [Parameter]
        public bool[] drafted { get; set; }

        [Parameter]
        public Sudoku.Board.SudokuBoardCell cell { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
        //private string TitleCssClass => ContainerTabSet.ActiveTab == this ? "active" : null;

        protected void OnClickRippleEffect(MouseEventArgs mouseEvent)
        {
            // TODO activate the ripple
            // testEnvironment && console.debug(...BOARD_CELL_DEV_MODE, `onClickRippleEffect-event:${mouseEvent}`);
            // const $cell = (mouseEvent.target as HTMLElement).closest("sudoku-board-cell-component");

            // const rect = $cell.getBoundingClientRect();
            // let $ripple = $cell.querySelector('.ripplex') as HTMLElement;
            // if (!$ripple) {
            //     $ripple = document.createElement('span');
            //     $ripple.className = 'ripplex';
            //     $ripple.style.height = $ripple.style.width = Math.max(rect.width, rect.height) + 'px';
            //     $cell.appendChild($ripple);
            // }

            // $ripple.classList.remove('show');
            // var top = mouseEvent.pageY - rect.top - $ripple.offsetHeight / 2 - document.body.scrollTop;
            // var left = mouseEvent.pageX - rect.left - $ripple.offsetWidth / 2 - document.body.scrollLeft;
            // $ripple.style.top = top + 'px';
            // $ripple.style.left = left + 'px';

            // $ripple.classList.add('show');
            // setTimeout(function () {
            //     $ripple.remove();
            // }, 500);
            // return false;
        }

        protected bool isThereValueDefined()
        {
            return cell.value != Sudoku.Store.Game.Const.NoValueInTheCell ? true : false;
        }

        protected bool isThereDraftValues()
        {
            // console.log(`drafted values?:${this.cell.drafted.find(val => val === true) || false}`,this.cell.drafted,)
            var isThereDraftedValues = cell.drafted.Find(val => val == true);
            if (isThereDraftedValues) Logger.LogInformation($"there is drafted value");
            return isThereDraftedValues;
        }

        protected string possibleValue(int value)
        {
            var possibleValue = cell.calculatedPossibleValues.Find(possibleValue => possibleValue == value);
            // console.log(`POssibleValues:`,this.cell.possibleValues);
            return default == possibleValue ? possibleValue.ToString() : "";
        }

    }
}
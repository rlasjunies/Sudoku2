using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sudoku.Components
{
    public partial class AccKeyboardBase : AccComponentBase
    {

        [Parameter]
        public EventCallback<int> OnNumberClicked { get; set; }
        [Parameter]
        public EventCallback<int> OnDraftNumberClicked { get; set; }
        [Parameter]
        public EventCallback OnClearClicked { get; set; }
        [Parameter]
        public EventCallback OnUndoClicked { get; set; }

        [Parameter]
        public int[] remainingNumbers { get; set; } = new int[] {9,9,9,9,9,9,9,9,9};
        
        [Parameter]
        public bool hideClearKey { get; set; } = false;
        
        [Parameter]
        public bool hideUndoKey { get; set; } = false;

        protected async void numberClickedHandler(int numberClicked){
            await OnNumberClicked.InvokeAsync(numberClicked);
        }

        protected async void  clearClickedHandler(){
            await OnClearClicked.InvokeAsync();
        }

        protected async void undoClickedHandler(){
            await OnUndoClicked.InvokeAsync();
        }

    }
}
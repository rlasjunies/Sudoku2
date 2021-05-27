using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace Sudoku.Components
{
    public partial class AccHeaderBase : AccComponentBase
    {
        [Parameter]
        public bool backbutton {get;set;} = false; 
        
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        
        [Parameter]
        public EventCallback<MouseEventArgs> OnClickBackButton { get; set; }
        
        public async void onClickBackButtonHandler( MouseEventArgs e){
            // Logger?.LogDebug("!!!!!!!!Button clicked!!!!!!");
            await OnClickBackButton.InvokeAsync(e);
        }
    }
}
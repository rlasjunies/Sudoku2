using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sudoku.Components
{
    public partial class AccSwitchBase : AccComponentBase
    {

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnChangeCallback { get; set; }

        [Parameter]
        public bool valueChecked { get; set; } = false;

        protected async void OnChangeHandler(ChangeEventArgs e)
        {
            await OnChangeCallback.InvokeAsync(e);
        }

        protected void ToggleSwitch(){
            valueChecked = !valueChecked;
            OnChangeHandler(new ChangeEventArgs());
        }
    }
}
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sudoku.Components
{
    public partial class AccButtonBase : AccComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
        //private string TitleCssClass => ContainerTabSet.ActiveTab == this ? "active" : null;

        [Parameter]
        public string styling {get;set;} = "";

        [Parameter]
        public bool info {get;set;} = false;
        
        [Parameter]
        public bool success {get;set;} = false;

        protected string CalculateClass(){
            if (info) return "info";
            if (success) return "success";
            return "default";
        }
    }
}
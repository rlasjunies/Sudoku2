using Microsoft.AspNetCore.Components;

namespace Sudoku.Components
{
    public partial class AccPageBase : AccComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

    }
}
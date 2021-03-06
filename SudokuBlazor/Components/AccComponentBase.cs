using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Sudoku.Components
{
    public class AccComponentBase : ComponentBase
    {
        [Parameter]
        public string id {get;set;}

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; } = new Dictionary<string, object>(){};

        public string CssClass => InputAttributes.ContainsKey("class") ? InputAttributes["class"].ToString() : "" ;

    }
}
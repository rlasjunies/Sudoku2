using System;
using AngleSharp.Dom;
using Bunit;

namespace Sudoku.Pages.Tests
{
    public static class Helpers
    {
        public static bool IsCheckBoxChecked(IElement checkbox)
        {
            return (checkbox.Attributes["checked"] is null) ? false : true; ;
        }

        public static void ToggleTheCheckBox(IElement checkBoxShowIdentical)
        {
            checkBoxShowIdentical.Change("");
        }
    }
}

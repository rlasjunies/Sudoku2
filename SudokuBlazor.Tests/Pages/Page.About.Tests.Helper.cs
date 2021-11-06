using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sudoku.Pages.Tests
{
    public static class PageAboutHelper
    {
        public static string BackButtonId => "#back";

        public static AngleSharp.Dom.IElement BackButton( IRenderedComponent<Page_About> cut ) => cut.Find(BackButtonId);
    }
}

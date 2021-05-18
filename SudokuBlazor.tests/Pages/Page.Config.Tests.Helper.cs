using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Pages.Tests
{
    public static class PageConfigHelper
    {
        public static string BackButtonId => "#back";
        public static string ShowIdenticalNumberId => "#showidenticalnumber";
        public static string ShowErrornousCellId => "#showerrornouscells";
        public static string HighlightUniqueCandidateInRowColZoneId => "#highlightuniquecandidateinzone";
    }
}

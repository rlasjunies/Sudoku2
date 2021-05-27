﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Pages.Tests
{
    public static class PageConfigHelper
    {
        public static string BackButtonId => "#back";
        public static string ShowIdenticalNumberId => "#showidenticalnumber input";
        public static string ShowErrornousCellId => "#showerrornouscells input";
        public static string HighlightUniqueCandidateInRowColZoneId => "#highlightuniquecandidateinzone input";
    }
}

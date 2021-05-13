using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.About
{
    public record StateAbout
    {
        public string Version { get; init; }
        public bool Initialized { get; init; }

    }
}

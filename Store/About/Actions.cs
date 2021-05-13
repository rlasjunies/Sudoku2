using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.About
{

    namespace Actions
    {
        public record RetrieveAboutInformation { }

        public record AboutInformationRetrieved { 
            public string Version {get;init;}
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;

namespace Sudoku.Store.App
{
    public record StateApp
    {
        public string Version { get; init; }
        public bool Initialized { get; init; }

    }
    public class Feature : Feature<StateApp>
	{
        public override string GetName() => nameof(StateApp);
		protected override StateApp GetInitialState() =>
			new StateApp
			{
				Initialized = false,
				Version = string.Empty,
			};
	}
}

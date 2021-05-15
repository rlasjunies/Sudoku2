using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.About
{
	public class Feature : Feature<StateAbout>
	{
		public override string GetName() => nameof(StateAbout);
		protected override StateAbout GetInitialState() =>
			new StateAbout{
				Initialized = false,
				Version =  string.Empty,
			};
	}
}

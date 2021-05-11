using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.WeatherUseCase
{
	public class Feature : Feature<WeatherState>
	{
		public override string GetName() => nameof(WeatherState);
		protected override WeatherState GetInitialState() =>
			new WeatherState(
				isLoading: false,
				forecasts: null);
	}
}

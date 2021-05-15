using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.Weather
{
	public class Feature : Feature<StateWeather>
	{
		public override string GetName() => nameof(Weather.StateWeather);
		protected override StateWeather GetInitialState() =>
			new StateWeather{
				Initialized = false,
				Loading =  false,
				Forecasts =  Array.Empty<WeatherForecast>()
			};
	}
}

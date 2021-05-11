using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.WeatherUseCase
{
	public static class Reducers
	{
		[ReducerMethod]
		public static WeatherState ReduceFetchDataAction(WeatherState state, ActionFetchData action) =>
			new WeatherState(
				isLoading: true,
				forecasts: null);

		[ReducerMethod]
		public static WeatherState ReduceFetchDataResultAction(WeatherState state, ActionFetchDataEffectResult action) =>
	new WeatherState(
		isLoading: false,
		forecasts: action.Forecasts);
	}

}

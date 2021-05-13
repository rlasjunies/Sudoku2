using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.Weather
{
    public static class Reducers
    {

        [ReducerMethod]
        public static StateWeather OnSetForecasts(StateWeather state, Actions.SetForecasts action) =>
            state with { Forecasts = action.Forecasts };

        [ReducerMethod]
        public static StateWeather OnStartLoading(StateWeather state, Actions.StartLoading action) =>
            state with { Loading = true };

        [ReducerMethod]
        public static StateWeather OnStopLoading(StateWeather state, Actions.StopLoading action) =>
            state with { Loading = false };

        [ReducerMethod(typeof(Actions.SetInitialized))]
        public static StateWeather OnSetInitialized(StateWeather state) =>
            state with { Initialized = true };
    }
}
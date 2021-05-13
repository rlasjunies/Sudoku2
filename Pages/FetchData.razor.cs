using Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherStore = Sudoku.Store.Weather;
namespace Sudoku.Pages
{
    public partial class FetchData
    {
        [Inject]
        private IState<WeatherStore::StateWeather> WeatherState { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }

        private WeatherStore::WeatherForecast[] _forecasts => WeatherState.Value.Forecasts;
        private bool _loading => WeatherState.Value.Loading;

        protected override void OnInitialized()
        {
            if (WeatherState.Value.Initialized == false)
            {
                LoadForecasts();
                Dispatcher.Dispatch(new WeatherStore::Actions.SetInitialized());
            }
            base.OnInitialized();
        }

        private void LoadForecasts()
        {
            Dispatcher.Dispatch(new WeatherStore::Actions.LoadForecasts());
        }

    }
}

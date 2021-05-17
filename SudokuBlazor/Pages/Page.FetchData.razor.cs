using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store = Sudoku.Store.Weather;
namespace Sudoku.Pages
{
    public partial class FetchDataBase : PageBase<Store::StateWeather>, IDisposable
    {

        protected Store::WeatherForecast[] Forecasts => State.Value.Forecasts;
        protected bool Loading => State.Value.Loading;

        protected void LoadForecasts()
        {
            Dispatcher.Dispatch(new Store::Actions.LoadForecasts());
        }
        protected override void OnInitialized()
        {
            if (State.Value.Initialized == false)
            {
                LoadForecasts();
                Dispatcher.Dispatch(new Store::Actions.SetInitialized());
            }
            base.OnInitialized();
        }
    }
}

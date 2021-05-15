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
    public partial class FetchDataBase : ComponentBase, IDisposable
    {
        [Inject]
        protected IState<Store::StateWeather> _State { get; set; }

        [Inject]
        private IDispatcher Dispatcher { get; set; }


        protected Store::WeatherForecast[] _forecasts => _State.Value.Forecasts;
        private bool _loading => _State.Value.Loading;

        protected void LoadForecasts()
        {
            Dispatcher.Dispatch(new Store::Actions.LoadForecasts());
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            _State.StateChanged += StateChanged;
            if (_State.Value.Initialized == false)
            {
                LoadForecasts();
                Dispatcher.Dispatch(new Store::Actions.SetInitialized());
            }
        }
        
        private void StateChanged(Object sender, Store::StateWeather state)
        {
            InvokeAsync(StateHasChanged);
        }

        void IDisposable.Dispose()
        {
            _State.StateChanged -= StateChanged;
        }

    }
}

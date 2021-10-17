//using Fluxor;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Components;

//namespace Sudoku.Store.Weather.Effects
//{
//    // public class FetchForeCastsDataEffect : Effect<Actions.LoadForecastsAction>
//    public class LoadForeCastsData
//    {
//        private readonly HttpClient Http;

//        public LoadForeCastsData(HttpClient http)
//        {
//            Http = http;
//        }

//        // public override async Task HandleAsync(Actions.FetchDataAction action, IDispatcher dispatcher)
//        // {
//        // await Task.Delay(1000);
//        // var forecasts = await Http.GetJsonAsync<WeatherForecast[]>("sample-data/weather.json");
//        // dispatcher.Dispatch(new ActionFetchDataEffectResult(forecasts));

//        // }

//        [EffectMethod(typeof(Actions.LoadForecasts))]
//        public async Task LoadForecasts(IDispatcher dispatcher)
//        {
//            dispatcher.Dispatch(new Actions.StartLoading());
//            await Task.Delay(3000);
//            // var forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
//            var forecasts = await Http.GetJsonAsync<WeatherForecast[]>("sample-data/weather.json");
//            dispatcher.Dispatch(new Actions.SetForecasts(forecasts));
//            dispatcher.Dispatch(new Actions.StopLoading());
//        }
//    }

//    public record ActionFetchDataEffectResult
//    {
//        public IEnumerable<WeatherForecast> Forecasts { get; }

//        public ActionFetchDataEffectResult(IEnumerable<WeatherForecast> forecasts)
//        {
//            Forecasts = forecasts;
//        }
//    }
//}

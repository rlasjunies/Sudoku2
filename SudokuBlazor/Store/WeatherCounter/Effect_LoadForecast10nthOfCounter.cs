//using Fluxor;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Components;
//using CounterStore = Sudoku.Store.Counter;
//using WeatherStore = Sudoku.Store.Weather;


//namespace Sudoku.Store.Weather
//{
//    // public class FetchForeCastsDataEffect : Effect<Actions.LoadForecastsAction>
//    public class WeatherCounterEffect
//    {
//        private readonly HttpClient Http;
//        private readonly IState< CounterStore::StateCounter> CounterState;

//        public WeatherCounterEffect(HttpClient http, IState<CounterStore::StateCounter> counterState)
//        {
//            Http = http;
//            CounterState = counterState;
//        }

//        [EffectMethod(typeof(CounterStore::Actions.IncrementCounter))]
//        public async Task  ReloadForecastEvery10Increment(IDispatcher dispatcher)
//        {
//            // every tenth increment
//            if (CounterState.Value.ClickCount % 10 == 0)
//            {
//                dispatcher.Dispatch(new WeatherStore::Actions.LoadForecasts());
//            } 

//            // trick: without this line there is compiler error 
//            await Task.CompletedTask;
//        }
//    }
//}

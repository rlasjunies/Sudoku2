using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sudoku.Store.WeatherUseCase
{
	public class ActionFetchDataEffect : Effect<ActionFetchData>
	{
		private readonly HttpClient Http;

		public ActionFetchDataEffect(HttpClient http)
		{
			Http = http;
		}

		public override async Task HandleAsync(ActionFetchData action, IDispatcher dispatcher)
		{
			//var forecasts = await Http.GetJsonAsync<WeatherForecast[]>("WeatherForecast");

			await Task.Delay(1000);
			var forecasts = await Http.GetJsonAsync<WeatherForecast[]>("sample-data/weather.json");
			dispatcher.Dispatch(new ActionFetchDataEffectResult(forecasts));
		}
	}
}

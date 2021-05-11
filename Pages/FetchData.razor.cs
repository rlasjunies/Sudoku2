using Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.Store.WeatherUseCase;

namespace Sudoku.Pages
{
	public partial class FetchData
	{
		[Inject]
		private IState<WeatherState> WeatherState { get; set; }

		[Inject]
		private IDispatcher Dispatcher { get; set; }

	}
}

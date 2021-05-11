using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.WeatherUseCase
{
    public class ActionFetchDataEffectResult
    {
        public IEnumerable<WeatherForecast> Forecasts { get; }

        public ActionFetchDataEffectResult(IEnumerable<WeatherForecast> forecasts)
        {
            Forecasts = forecasts;
        }
    }
}

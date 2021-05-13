using Fluxor;
using Microsoft.AspNetCore.Components;
using Sudoku.Store.Weather;
using Fluxor.Blazor.Web.Components;

namespace Sudoku.Shared
{
    public partial class NavMenu
    {
        [Inject]
        private IState<StateWeather> WeatherState { get; set; }

        private bool collapseNavMenu = true;

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        private string WeatherItemClass => WeatherState.Value.Loading ? "font-weight-bold" : null;

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
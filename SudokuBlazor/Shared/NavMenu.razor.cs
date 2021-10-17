//using Fluxor;
//using Microsoft.AspNetCore.Components;
//using Sudoku.Store.Weather;
//using Fluxor.Blazor.Web.Components;

//namespace Sudoku.Shared
//{
//    public partial class NavMenu
//    {
//        [Inject]
//        protected IState<StateWeather> WeatherState { get; set; }

//        protected bool collapseNavMenu = true;

//        protected string NavMenuCssClass => collapseNavMenu ? "collapse" : null;
//        protected string WeatherItemClass => WeatherState.Value.Loading ? "font-weight-bold" : null;

//        protected void ToggleNavMenu()
//        {
//            collapseNavMenu = !collapseNavMenu;
//        }
//    }
//}
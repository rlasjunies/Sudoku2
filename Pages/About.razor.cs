using Fluxor;
using Microsoft.AspNetCore.Components;
using Fluxor.Blazor.Web.Components;
using Store = Sudoku.Store.About;
using Microsoft.Extensions.Logging;

namespace Sudoku.Pages
{
    public partial class About
    {
        [Inject]
        protected ILogger<Counter> Logger { get; set; }

        [Inject]
        private IState<Store::StateAbout> State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        protected override void OnInitialized()
        {
            if (!State.Value.Initialized)
            {
                Dispatcher.Dispatch(new Store::Actions.RetrieveAboutInformation());
            }
            base.OnInitialized();
        }
    }
}
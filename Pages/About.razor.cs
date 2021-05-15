using Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using Store = Sudoku.Store.About;

namespace Sudoku.Pages
{
    public partial class AboutBase : ComponentBase, IDisposable
    {
        [Inject]
        protected IState<Store::StateAbout> _State { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        protected override void OnInitialized()
        {
            _State.StateChanged += StateChanged;
            if (!_State.Value.Initialized)
            {
                Dispatcher.Dispatch(new Store::Actions.RetrieveAboutInformation());
            }
            base.OnInitialized();
        }

        private void StateChanged(Object sender, Store::StateAbout state)
        {
            InvokeAsync(StateHasChanged);
        }
        void IDisposable.Dispose()
        {
            _State.StateChanged -= StateChanged;
        }

    }
}
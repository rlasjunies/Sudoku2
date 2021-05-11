using Fluxor;


namespace Sudoku.Store.CounterState
{
    public class Feature : Feature<CounterState>
    {
        public override string GetName() => nameof(CounterState);
        protected override CounterState GetInitialState() =>
            new CounterState { ClickCount = 0 };
    }
}
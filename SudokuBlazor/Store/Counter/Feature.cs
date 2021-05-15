using Fluxor;


namespace Sudoku.Store.Counter
{
    public class Feature : Feature<StateCounter>
    {
        public override string GetName() => nameof(Counter.StateCounter);
        protected override StateCounter GetInitialState() =>
            new StateCounter { ClickCount = 0 };
    }
}
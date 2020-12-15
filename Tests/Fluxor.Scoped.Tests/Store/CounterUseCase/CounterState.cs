namespace Fluxor.Scoped.Tests.Store.CounterUseCase
{
    public sealed class CounterState
    {
        public CounterState(int clickCount) => ClickCount = clickCount;

        public int ClickCount { get; }
    }
}

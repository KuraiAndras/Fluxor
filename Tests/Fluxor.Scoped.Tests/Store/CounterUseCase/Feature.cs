namespace Fluxor.Scoped.Tests.Store.CounterUseCase
{
    public sealed class Feature : Feature<CounterState>
    {
        public override string GetName() => "Counter";

        protected override CounterState GetInitialState() => new(0);
    }
}
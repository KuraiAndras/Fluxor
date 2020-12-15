using FluentAssertions;
using Fluxor.Scoped.Tests.Store.CounterUseCase;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Fluxor.Scoped.Tests
{
    public class ScopedTests
    {
        [Fact]
        public async Task DifferentlyScopedStateShouldHaveSameValue()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddFluxor(o => o.ScanAssemblies(typeof(ScopedTests).Assembly))
                .BuildServiceProvider();
            await serviceProvider.GetRequiredService<IStore>().InitializeAsync();

            var counter = serviceProvider.GetRequiredService<IState<CounterState>>();
            var dispatcher = serviceProvider.GetRequiredService<IDispatcher>();

            dispatcher.Dispatch(new IncrementCounterAction());
            dispatcher.Dispatch(new IncrementCounterAction());

            // Act
            var scope = serviceProvider.CreateScope();
            var scopedCounter = scope.ServiceProvider.GetRequiredService<IState<CounterState>>();
            var scopedDispatcher = scope.ServiceProvider.GetRequiredService<IDispatcher>();

            scopedDispatcher.Dispatch(new IncrementCounterAction());

            // Assert
            scopedCounter.Value.ClickCount.Should().NotBe(0);
            counter.Value.ClickCount.Should().NotBe(0);

            // TODO: this is debatable
            scopedCounter.Value.ClickCount.Should().Be(counter.Value.ClickCount);
        }
    }
}

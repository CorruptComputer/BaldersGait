using BaldersGait.Models;
using Xunit.Abstractions;

namespace BaldersGait.UnitTests.Models;

public class CalculatedGameStateTests(ITestOutputHelper log)
{
    [Fact]
    public void CalculatedGameState_ShouldNotThrow()
    {
        Action act = () =>
        {
            CalculatedGameState _ = new();
        };

        act.Should().NotThrow();
    }

    [Theory] // TODO: There has to be a better way to do this for a range of values
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    public void SetBaseHairPerTick_ShouldNotThrow(int hairGrowthUpgrades)
    {
        CalculatedGameState calculatedGameState = new();

        Action act = () =>
        {
            calculatedGameState.SetBaseHairPerTick(hairGrowthUpgrades);
            log.WriteLine($"BaseHairPerTick: {calculatedGameState.BaseHairPerTick}");
        };

        act.Should().NotThrow();
    }
}
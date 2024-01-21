using BaldersGait.Models.State;

namespace BaldersGait.UnitTests.Models.State;

public class BarberShopStateTests
{
    [Fact]
    public void HairGrowth_ShouldCalculateCorrectly()
    {
        BarberShopState state = new();
        state.HairGrowth.Should().Be(0.25);
    }

    [Fact]
    public void TickMe_ShouldNotThrow()
    {
        BarberShopState state = new();
        Action act = () => state.TickMe();

        act.Should().NotThrow();
    }
}
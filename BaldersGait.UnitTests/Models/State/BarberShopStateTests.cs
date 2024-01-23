using BaldersGait.Models.State;

namespace BaldersGait.UnitTests.Models.State;

public class BarberShopStateTests
{
    [Fact]
    public void TickMe_ShouldNotThrow()
    {
        BarberShopState state = new();
        Action act = () => state.TickMe();

        act.Should().NotThrow();
    }
}
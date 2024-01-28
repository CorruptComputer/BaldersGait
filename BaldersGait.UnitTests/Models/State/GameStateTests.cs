using BaldersGait.Models;

namespace BaldersGait.UnitTests.Models.State;

public class GameStateTests
{
    [Fact]
    public void TickMe_ShouldNotThrow()
    {
        GameState state = new();
        Action act = () => state.TickMe();

        act.Should().NotThrow();
    }
}
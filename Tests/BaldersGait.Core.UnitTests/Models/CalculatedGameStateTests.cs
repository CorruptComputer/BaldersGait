using BaldersGait.Core.Models.CalculatedState;

namespace BaldersGait.Core.UnitTests.Models;

public class CalculatedGameStateTests
{
    [Fact]
    public void CalculatedGameState_ShouldNotThrow()
    {
        Action act = () =>
        {
            CalculatedGameState _ = new(new());
        };

        act.ShouldNotThrow();
    }
}
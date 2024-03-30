using BaldersGait.Models.Stylists;

namespace BaldersGait.UnitTests.Models.Stylists;

public class StylistTests
{
    [Fact]
    public void RollRandomStylist_ShouldNotThrow()
    {
        Action act = () =>
        {
            Stylist.RollRandomStylist(new(), 0);
        };

        act.Should().NotThrow();
    }
}
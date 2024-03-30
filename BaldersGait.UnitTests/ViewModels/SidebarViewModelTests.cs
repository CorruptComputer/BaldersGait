using BaldersGait.Services;
using BaldersGait.Services.Interface;
using BaldersGait.ViewModels.Sidebar;

namespace BaldersGait.UnitTests.ViewModels;

public class SidebarViewModelTests
{
    private readonly IStateService _stateService = new StateService(new EnvironmentService(), autoTickState: false);

    private SidebarViewModel? _sidebarViewModelBackingField = null;
    private SidebarViewModel SidebarViewModel => _sidebarViewModelBackingField ??= new(
        new(_stateService, autoRefreshUi: false), new(_stateService, autoRefreshUi: false),
        new(_stateService, autoRefreshUi: false), new(_stateService, autoRefreshUi: false),
        _stateService, autoRefreshUi: false);

    [Theory]
    [InlineData(1000.123, "1,000.12")]
    [InlineData(1000000.123, "1,000,000.12")]
    public void HairCollectedLabel_ShouldFormatAsExpected(double input, string expectedFormat)
    {
        _stateService.SavedState.HairCollected = input;
        SidebarViewModel.HairCollectedLabel.Should().Contain(expectedFormat);
    }
}
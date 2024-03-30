using BaldersGait.Models;

namespace BaldersGait.Services.Interface;

public interface IStateService
{
    public CalculatedGameState CalculatedState { get; }

    public SavedGameState SavedState { get; }

    public bool LoadState(bool resetState = false);

    public bool SaveState();
}
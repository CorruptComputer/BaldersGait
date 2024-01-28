using BaldersGait.Models;

namespace BaldersGait.Services.Interface;

public interface IStateService
{
    public GameState GetGameState();

    public bool LoadState(bool resetState = false);

    public bool SaveState();

    public void TickState();
}
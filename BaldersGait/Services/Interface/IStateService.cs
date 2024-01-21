using BaldersGait.Models.State;

namespace BaldersGait.Services.Interface;

public interface IStateService
{
    public BarberShopState GetBarberShopState();

    public bool LoadState(bool resetState = false);
    
    public bool SaveState();
    
    public void TickState();
}
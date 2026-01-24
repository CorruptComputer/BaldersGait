using BaldersGait.Core.Models.CalculatedState;
using BaldersGait.Core.Models.SavedState;

namespace BaldersGait.Core.Services.Interface;

/// <summary>
///   The state service should handle all state-related operations, including both calculated and saved state.
/// </summary>
public interface IStateService
{
    /// <summary>
    ///   The Calculated game state, nothing from this will be saved.
    /// </summary>
    public CalculatedGameState CalculatedState { get; }

    /// <summary>
    ///   The parts of the game state that will be saved.
    /// </summary>
    public SavedGameState SavedState { get; }

    /// <summary>
    ///   Loads the state from the latest save.
    /// </summary>
    /// <param name="resetState"></param>
    /// <returns></returns>
    public bool LoadState(bool resetState = false);

    /// <summary>
    ///   Saves the state to a file.
    /// </summary>
    /// <returns></returns>
    public bool SaveState();
}
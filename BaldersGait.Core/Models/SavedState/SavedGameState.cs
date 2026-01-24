using System.Text.Json.Serialization;
using BaldersGait.Core.Models.BarberShop;
using BaldersGait.Core.Models.Enums;

namespace BaldersGait.Core.Models.SavedState;

/// <summary>
///   Represents the saved state of the game.
/// </summary>
[JsonSerializable(typeof(SavedGameState))]
public sealed record SavedGameState
{
    /// <summary>
    ///   The current amount of hair collected.
    /// </summary>
    public double HairCollected { get; set; } = 0;

    /// <summary>
    ///   The current amount of money collected.
    /// </summary>
    public double MoneyCollected { get; set; } = 0;

    #region Barber Shop
    /// <summary>
    ///   The list of barber shop chairs.
    /// </summary>
    public List<BarberShopChair> Chairs { get; set; } =
    [
        new()
        {
            ChairNumber = ChairNumbers.One,
            Unlocked = true
        },
        new()
        {
            ChairNumber = ChairNumbers.Two,
        },
        new()
        {
            ChairNumber = ChairNumbers.Three,
        },
        new()
        {
            ChairNumber = ChairNumbers.Four,
        },
        new()
        {
            ChairNumber = ChairNumbers.Five,
        },
        new()
        {
            ChairNumber = ChairNumbers.Six,
        },
        new()
        {
            ChairNumber = ChairNumbers.Seven,
        },
        new()
        {
            ChairNumber = ChairNumbers.Eight,
        }

    ];
    #endregion

    #region Upgrades Shop
    /// <summary>
    ///   Have the clippers been purchased?
    /// </summary>
    public bool ClippersPurchased { get; set; } = false;

    /// <summary>
    ///   Has the company been purchased?
    /// </summary>
    public bool CompanyPurchased { get; set; } = false;

    /// <summary>
    ///   The number of hair growth V1 upgrades purchased.
    /// </summary>
    public int HairGrowthV1Upgrades { get; set; } = 0;

    /// <summary>
    ///   The number of scaling factor V1 upgrades purchased.
    /// </summary>
    public int ScalingFactorV1Upgrades { get; set; } = 0;

    /// <summary>
    ///   The number of max hair V1 upgrades purchased.
    /// </summary>
    public int MaxHairV1Upgrades { get; set; } = 0;
    #endregion
}
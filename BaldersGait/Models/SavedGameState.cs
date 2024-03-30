using System.Text.Json.Serialization;
using BaldersGait.Models.BarberShop;
using BaldersGait.Models.Enums;
using BaldersGait.Models.Stylists;

namespace BaldersGait.Models;

[Serializable]
public class SavedGameState
{
    [JsonIgnore]
    private static readonly Random Random = new();

    public double HairCollected { get; set; } = 0;

    public double MoneyCollected { get; set; } = 0;

    #region Barber Shop
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
    public bool ClippersPurchased { get; set; } = false;
    public bool CompanyPurchased { get; set; } = false;
    public bool StylistsPurchased { get; set; } = false;

    public int HairGrowthV1Upgrades { get; set; } = 0;
    public int ScalingFactorV1Upgrades { get; set; } = 0;
    public int MaxHairV1Upgrades { get; set; } = 0;
    #endregion

    #region Stylists
    public List<Stylist> StylistsForHire { get; set; } = Stylist.RollNRandomStylists(Random, 0, 4);

    public int StylistsForHireRerollCount { get; set; } = 0;
    #endregion
}
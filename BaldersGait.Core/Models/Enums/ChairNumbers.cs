using System.Text.Json.Serialization;

namespace BaldersGait.Core.Models.Enums;

/// <summary>
///   There are 8 chairs in the game, each will have its own number, this just makes it easier to refer to them.
/// </summary>
public enum ChairNumbers
{
    /// <summary>
    ///   Some kind of error happened, this is just here for the enum default.
    /// </summary>
    Unknown = 0,

    /// <summary>
    ///   Chair one
    /// </summary>
    One = 1,

    /// <summary>
    ///   Chair two
    /// </summary>
    Two = 2,

    /// <summary>
    ///   Chair three
    /// </summary>
    Three = 3,

    /// <summary>
    ///   Chair four
    /// </summary>
    Four = 4,

    /// <summary>
    ///   Chair five
    /// </summary>
    Five = 5,

    /// <summary>
    ///   Chair six
    /// </summary>
    Six = 6,

    /// <summary>
    ///   Chair seven
    /// </summary>
    Seven = 7,

    /// <summary>
    ///   Chair eight
    /// </summary>
    Eight = 8
}
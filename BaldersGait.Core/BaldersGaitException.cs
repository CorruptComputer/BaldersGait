using System.Runtime.CompilerServices;
using Serilog;

namespace BaldersGait.Core;

/// <summary>
///   Oh no, an error happened!
/// </summary>
public class BaldersGaitException : Exception
{
    /// <summary>
    ///   Flag for if the error that happened is recoverable from.
    /// </summary>
    public bool SaveRecoverable { get; init; }

    /// <summary>
    ///   Initializes a new instance of the <see cref="BaldersGaitException"/> class.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="saveRecoverable"></param>
    /// <param name="thrownBy"></param>
    /// <param name="thrownAt"></param>
    public BaldersGaitException(string message, bool saveRecoverable, [CallerMemberName] string thrownBy = "", [CallerLineNumber] int thrownAt = 0)
        : base(message)
    {
        SaveRecoverable = saveRecoverable;

        Log.Error($"{thrownBy}:{thrownAt}: {message}");
    }
}
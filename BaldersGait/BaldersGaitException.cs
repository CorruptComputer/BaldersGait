using System.Runtime.CompilerServices;
using Serilog;

namespace BaldersGait;

/// <summary>
///   Oh no, an error happened!
/// </summary>
public class BaldersGaitException : ApplicationException
{
    /// <summary>
    ///   Flag for if the error that happened is recoverable from.
    /// </summary>
    public bool Recoverable { get; init; }
    
    public BaldersGaitException(string message, bool recoverable, [CallerMemberName] string thrownBy = "", [CallerLineNumber] int thrownAt = 0) : base(message)
    {
        Recoverable = recoverable;
        
        Log.Error($"{thrownBy}:{thrownAt}: {message}");
    }
}
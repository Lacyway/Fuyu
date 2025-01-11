using System;

namespace Fuyu.Backend.BSG.Services;

public class TimeService
{
    public static TimeService Instance => instance.Value;
    private static readonly Lazy<TimeService> instance = new(() => new TimeService());

    private TimeService()
    {

    }

    /// <summary>
    /// Returns a timestamp in seconds
    /// </summary>
    public int Timestamp
    {
        get
        {
            return (int)(TimestampMs / 1000);
        }
    }

    /// <summary>
    /// Returns a timestamp in milliseconds
    /// </summary>
    public long TimestampMs
    {
        get
        {
            return DateTimeOffset.Now.ToUniversalTime().ToUnixTimeMilliseconds();
        }
    }
}

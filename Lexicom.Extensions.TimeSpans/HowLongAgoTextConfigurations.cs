using Lexicom.Extensions.TimeSpans.Exceptions;
using System.Collections;

namespace Lexicom.Extensions.TimeSpans;

public class HowLongAgoTextConfigurations : IEnumerable<HowLongAgoTextConfiguration>
{
    public static HowLongAgoTextConfigurations Standard { get; } =
    [
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Days),
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Hours),
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Minutes),
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Seconds),
    ];
    public static HowLongAgoTextConfigurations All { get; } = new HowLongAgoTextConfigurations(Standard)
    {
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Nanoseconds),
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Microseconds),
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Milliseconds),
    };
    public static HowLongAgoTextConfigurations DaysHoursMinutes { get; } =
    [
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Days),
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Hours),
        new HowLongAgoTextConfiguration(TimeSpanDelineation.Minutes),
    ];

    public HowLongAgoTextConfigurations()
    {
        DelineationToConfiguration = [];
        OrderedConfigurations = [];
    }
    public HowLongAgoTextConfigurations(IEnumerable<HowLongAgoTextConfiguration?>? constraints)
    {
        DelineationToConfiguration = [];
        OrderedConfigurations = [];

        if (constraints is not null)
        {
            foreach (HowLongAgoTextConfiguration? constraint in constraints)
            {
                if (constraint is not null)
                {
                    Add(constraint);
                }
            }
        }
    }

    private Dictionary<TimeSpanDelineation, HowLongAgoTextConfiguration> DelineationToConfiguration { get; }
    private List<HowLongAgoTextConfiguration> OrderedConfigurations { get; set; }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HowLongAgoTextDelineationAlreadyAddedException"/>
    public void Add(HowLongAgoTextConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        if (DelineationToConfiguration.ContainsKey(configuration.Delineation))
        {
            throw new HowLongAgoTextDelineationAlreadyAddedException(configuration.Delineation);
        }

        DelineationToConfiguration.Add(configuration.Delineation, configuration);

        OrderedConfigurations = DelineationToConfiguration.Values
            .OrderByDescending(d => d.Delineation)
            .ToList();
    }

    public IEnumerator<HowLongAgoTextConfiguration> GetEnumerator() => OrderedConfigurations.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

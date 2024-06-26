namespace BenchPressPlus20Kg.Core;

public abstract record Set
{
    public int? Ordinal { get; init; }
    public int? Reps { get; init; }
    public bool IsNegative { get; init; }
    public bool IsFailureTest { get; init; }
}

public record SetAbsolute(Weight Weight) : Set
{
    public override string ToString()
    {
        var repsStr = (Reps, IsNegative, IsFailureTest) switch
        {
            (_, _, true) => "?",
            (_, true, _) => "1/2",
            var (reps, _, _) => reps!.ToString(),
        };

        return $"{Weight.Round()} x {repsStr}";
    }
}

public record SetRelative(decimal Percent) : Set
{
    public SetAbsolute ToAbsolute(Weight weight) => new(weight * Percent)
    {
        Ordinal = Ordinal,
        Reps = Reps,
        IsNegative = IsNegative,
        IsFailureTest = IsFailureTest,
    };
}

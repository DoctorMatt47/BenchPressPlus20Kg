namespace BenchPressPlus20Kg.Domain;

public record Set
{
    public required Weight Weight { get; init; }
    public required int Ordinal { get; init; }
    public int? Reps { get; init; }
    public bool IsNegative { get; init; }
    public bool IsFailureTest { get; init; }

    public string ToString(Weight.Unit unit)
    {
        var repsStr = (Reps, IsNegative, IsFailureTest) switch
        {
            (null, _, true) => "?",
            (var reps, _, true) => $"({reps})",
            (_, true, _) => "1/2",
            var (reps, _, _) => reps!.ToString(),
        };

        return $"{Weight.ToString(unit)} x {repsStr}";
    }
}

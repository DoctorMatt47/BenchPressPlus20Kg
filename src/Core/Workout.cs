using System.Collections;

namespace BenchPressPlus20Kg.Core;

public record Workout
{
    public required int Ordinal { get; init; }
    public required List<Set> Sets { get; init; }
    public required Weight Orm { get; init; }

    public bool HasFailureTest => Sets.Any(set => set.IsFailureTest);
}

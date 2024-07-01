using System.Collections;

namespace BenchPressPlus20Kg.Core;

public record Workout : IEnumerable<Set>
{
    public required int Ordinal { get; init; }
    public required List<Set> Sets { get; init; }
    public required Weight Orm { get; init; }
    public bool IsDone { get; init; }

    public bool HasFailureTest => Sets.Any(set => set.IsFailureTest);

    // ReSharper disable once NotDisposedResourceIsReturned
    public IEnumerator<Set> GetEnumerator() => Sets.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

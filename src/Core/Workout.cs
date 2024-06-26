using System.Collections;

namespace BenchPressPlus20Kg.Core;

public record Workout : IEnumerable<SetAbsolute>
{
    public required List<SetAbsolute> Sets { get; set; }
    public required Weight Orm { get; set; }
    public bool IsDone { get; private set; }
    public int? LastSetReps { get; private set; }
    public required int Ordinal { get; init; }

    public bool HasFailureTest => Sets.Any(set => set.IsFailureTest);

    public void Done(int? lastSetReps = null)
    {
        if (HasFailureTest && lastSetReps is null)
        {
            throw new InvalidOperationException("Last set reps must be provided for workout with failure test.");
        }
        
        IsDone = true;
        LastSetReps = lastSetReps;
    }

    // ReSharper disable once NotDisposedResourceIsReturned
    public IEnumerator<SetAbsolute> GetEnumerator() => Sets.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

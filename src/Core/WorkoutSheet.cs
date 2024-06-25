using System.Collections.Frozen;

namespace BenchPressPlus20Kg.Core;

public record WorkoutSetWithoutWeight
{
    public bool IsNegative { get; }
    public bool IsFailureTest { get; }
    public int? Reps { get; }

    private WorkoutSetWithoutWeight(int? reps, bool isNegative = false, bool isFailureTest = false)
    {
        IsNegative = isNegative;
        IsFailureTest = isFailureTest;
        Reps = reps;
    }

    public static WorkoutSetWithoutWeight FromReps(int reps) => new(reps);

    public static WorkoutSetWithoutWeight Negative() => new(reps: 1, isNegative: true);

    public static WorkoutSetWithoutWeight FailureTest() => new(reps: null, isFailureTest: true);
}

public record WorkoutSet
{
    public Weight Weight { get; }
    public bool IsNegative { get; }
    public bool IsFailureTest { get; }
    public int? Reps { get; }
    
    public WorkoutSet(WorkoutSetWithoutWeight setWithoutWeight, Weight weight)
    {
        IsNegative = setWithoutWeight.IsNegative;
        IsFailureTest = setWithoutWeight.IsFailureTest;
        Reps = setWithoutWeight.Reps;
        Weight = weight;
    }
}

public class WorkoutSheet
{
    public FrozenDictionary<int, List<WorkoutSetWithoutWeight>> Workouts { get; } =
        new List<List<IEnumerable<WorkoutSetWithoutWeight>>>
            {
                new () {Sets(count: 1, reps: 6), Sets(count: 2, reps: 5), Sets(count: 2, reps: 4)},
                new () {Sets(count: 2, reps: 3), Sets(count: 2, reps: 2), Sets(count: 1, isNegative: true)},
                new () {Sets(count: 1, reps: 6), Sets(count: 2, reps: 5), Sets(count: 2, reps: 4)},
                new () {Sets(count: 2, reps: 3), Sets(count: 2, reps: 2), Sets(count: 1, isNegative: true)},
                new () {Sets(count: 1, reps: 6), Sets(count: 2, reps: 5), Sets(count: 1, isFailureTest: true)},
                new () {Sets(count: 2, reps: 3), Sets(count: 2, reps: 2), Sets(count: 1, isNegative: true)},
                new () {Sets(count: 2, reps: 5), Sets(count: 2, reps: 3), Sets(count: 1, isFailureTest: true)},
                new () {Sets(count: 2, reps: 3), Sets(count: 2, reps: 1), Sets(count: 1, isNegative: true)},
                new () {Sets(count: 2, reps: 5), Sets(count: 2, reps: 3), Sets(count: 1, isFailureTest: true)},
                new () {Sets(count: 2, reps: 3), Sets(count: 2, reps: 2), Sets(count: 1, reps: 1)},
                new () {Sets(count: 2, reps: 5), Sets(count: 2, reps: 3), Sets(count: 1, isFailureTest: true)},
                new () {Sets(count: 2, reps: 3), Sets(count: 2, reps: 2), Sets(count: 1, reps: 1)},
                new () {Sets(count: 1, reps: 5), Sets(count: 2, reps: 3), Sets(count: 2, reps: 2)},
                new () {Sets(count: 1, reps: 3), Sets(count: 1, reps: 2), Sets(count: 1, reps: 1)},
            }
            .SelectMany(x => x)
            .Select((x, i) => (x, i))
            .ToFrozenDictionary(t => t.i, t => t.x.ToList());

    private static IEnumerable<WorkoutSetWithoutWeight> Sets(
        int count,
        int? reps = null,
        bool isNegative = false,
        bool isFailureTest = false)
    {
        for (var i = 0; i < count; i++)
        {
            yield return (isNegative, isUntilFailure: isFailureTest) switch
            {
                (false, false) => WorkoutSetWithoutWeight.FromReps(reps!.Value),
                (true, false) => WorkoutSetWithoutWeight.Negative(),
                (false, true) => WorkoutSetWithoutWeight.FailureTest(),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}

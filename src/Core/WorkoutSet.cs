namespace BenchPressPlus20Kg.Core;

public record WorkoutSet
{
    private WorkoutSet(decimal percent, int? reps, bool isNegative = false, bool isFailureTest = false)
    {
        Percent = percent;
        IsNegative = isNegative;
        IsFailureTest = isFailureTest;
        Reps = reps;
    }

    public decimal Percent { get; }
    public bool IsNegative { get; }
    public bool IsFailureTest { get; }
    public int? Reps { get; }

    public static WorkoutSet FromReps(decimal percent, int reps) => new(percent, reps);

    public static WorkoutSet Negative(decimal percent) => new(percent, reps: 1, isNegative: true);

    public static WorkoutSet FailureTest(decimal percent) => new(percent, reps: null, isFailureTest: true);

    public string ToString(Weight orm)
    {
        var weight = (orm * Percent).Round();
        var repsStr = (Reps, IsNegative, IsFailureTest) switch
        {
            (_, _, true) => "?",
            (_, true, _) => "1/2",
            var (reps, _, _) => reps!.ToString(),
        };

        return $"{weight} x {repsStr}";
    }
}

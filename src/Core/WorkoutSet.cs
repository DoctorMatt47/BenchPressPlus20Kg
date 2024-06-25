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

    public string ToString(Weight orm, Weight.Unit unit = Weight.Unit.Kg)
    {
        var weightStr = (orm * Percent).ToString(unit);
        var repsStr = Reps.HasValue ? $"{Reps}" : "?";
        var isNegativeStr = IsNegative ? " negative" : "";
        var isFailureTestStr = IsFailureTest ? " failure test" : "";

        return
            $"{weightStr} x {repsStr}{isNegativeStr}{isFailureTestStr}";
    }
}

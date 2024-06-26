namespace BenchPressPlus20Kg.Core;

public abstract record WorkoutSetBase
{
    public int? Reps { get; protected init; }
    public bool IsNegative { get; protected init; }
    public bool IsFailureTest { get; protected init; }
}

public record WorkoutSet : WorkoutSetBase
{
    public decimal Percent { get; }

    private WorkoutSet(decimal percent) => Percent = percent;

    public static WorkoutSet FromReps(decimal percent, int reps) => new(percent) { Reps = reps };

    public static WorkoutSet Negative(decimal percent) => new(percent) { IsNegative = true };

    public static WorkoutSet FailureTest(decimal percent) => new(percent) { IsFailureTest = true };
}

public record WorkoutSetAbsolute : WorkoutSetBase
{
    public Weight Weight { get; }

    private WorkoutSetAbsolute(Weight weight) => Weight = weight;
    
    public static WorkoutSetAbsolute FromRelative(Weight orm, WorkoutSet relative)
    {
        return new WorkoutSetAbsolute(orm * relative.Percent)
        {
            Reps = relative.Reps,
            IsNegative = relative.IsNegative,
            IsFailureTest = relative.IsFailureTest,
        };
    }
    
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

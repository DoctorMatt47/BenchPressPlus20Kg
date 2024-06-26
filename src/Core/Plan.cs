namespace BenchPressPlus20Kg.Core;

public class Plan<TWorkoutSet> where TWorkoutSet : Set
{
    public required IReadOnlyList<IReadOnlyList<TWorkoutSet>> Workouts { get; init; }

    public void Test(int workout, int reps)
    {
        var sets = Workouts[workout];
        var testSet = sets.Select((set, i) => (set, i)).First(t => t.set.IsFailureTest);
        
    }
}

public static class PlanExtensions
{
    public static Plan<SetAbsolute> ToAbsolute(this Plan<SetRelative> relative, Weight orm)
    {
        var workouts = relative.Workouts
            .Select(workout => workout
                .Select(set => set.ToAbsolute(orm))
                .ToList())
            .ToList();
        
        return new Plan<SetAbsolute> {Workouts = workouts};
    }
}

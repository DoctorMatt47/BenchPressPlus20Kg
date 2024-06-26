namespace BenchPressPlus20Kg.Core;

public class Plan
{
    public Plan(List<List<SetRelative>> relativePlan, Weight orm)
    {
        RelativePlan = relativePlan;
        CurrentOrm = orm;

        AbsoluteWorkouts = relativePlan
            .Select(
                (workout, i) => new Workout
                {
                    Sets = workout
                        .Select(set => set.ToAbsolute(orm))
                        .ToList(),
                    Orm = orm,
                    Ordinal = i + 1,
                }
            )
            .ToList();
    }

    public List<List<SetRelative>> RelativePlan { get; }
    public List<Workout> AbsoluteWorkouts { get; }
    public Weight CurrentOrm { get; private set; }

    public void UpdateOrm(int startFromOrdinal, Weight orm)
    {
        for (var i = startFromOrdinal - 1; i < AbsoluteWorkouts.Count; i++)
        {
            var workout = AbsoluteWorkouts[i];
            workout.Orm = orm;

            workout.Sets = RelativePlan[i]
                .Select(set => set.ToAbsolute(workout.Orm))
                .ToList();
        }
        
        CurrentOrm = orm;
    }

    public void DoneWorkout(int ordinal, int? lastSetReps = null)
    {
        var workout = AbsoluteWorkouts[ordinal - 1];
        workout.Done(lastSetReps);

        if (!workout.HasFailureTest)
        {
            return;
        }

        switch (lastSetReps!.Value)
        {
            case <= 1:
                UpdateOrm(ordinal + 1, CurrentOrm.DecrementStep());
                break;
            case >= 5:
                UpdateOrm(ordinal + 1, CurrentOrm.IncrementStep());
                break;
        }
    }
}

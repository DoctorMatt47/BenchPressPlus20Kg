namespace BenchPressPlus20Kg.Core;

public class Plan(
    Weight orm,
    Func<Weight, IEnumerable<Workout>> getWorkouts,
    Func<int> getFailureTestReps)
{
    public List<Workout> Workouts { get; } = getWorkouts(orm).ToList();
    
    public Weight CurrentOrm { get; private set; } = orm;
    public int CurrentIndex { get; private set; }

    public void NextWorkout()
    {
        var workout = Workouts[CurrentIndex];

        if (!workout.HasFailureTest)
        {
            return;
        }

        switch (getFailureTestReps())
        {
            case <= 1:
                UpdateOrm(CurrentOrm.DecrementStep());
                break;
            case >= 5:
                UpdateOrm(CurrentOrm.IncrementStep());
                break;
        }
        
        CurrentIndex++;
        
    }

    private void UpdateOrm(Weight orm)
    {
        var workouts = getWorkouts(orm).ToList();
                
        for (var i = CurrentIndex; i < Workouts.Count; i++)
        {
            Workouts[i] = Workouts[i] with
            {
                Orm = orm,
                Sets = workouts[i].Sets,
            };
        }
        
        CurrentOrm = orm;
    }

}

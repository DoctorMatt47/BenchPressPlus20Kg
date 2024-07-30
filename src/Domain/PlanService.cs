namespace BenchPressPlus20Kg.Domain;

public interface IWorkoutRepository
{
    Task<IEnumerable<Workout>> GetWorkouts(Weight weight);
}

public interface IFailureTestService
{
    Task<int> GetFailureTestReps();
}

public interface IPlanRepository
{
    Task<Plan?> GetPlan();
    Task SavePlan(Plan plan);
}

public interface IPlanService
{
    Task<Plan> CreatePlan(Weight orm);
    Task<Plan> NextWorkout();
}

public class PlanService(
    IPlanRepository repository,
    IWorkoutRepository workoutRepository,
    IFailureTestService failureTestService) : IPlanService
{
    public async Task<Plan> CreatePlan(Weight orm)
    {
        var plan = new Plan
        {
            CurrentOrm = orm,
            Workouts = (await workoutRepository.GetWorkouts(orm)).ToList(),
        };

        await repository.SavePlan(plan);
        
        return plan;
    }

    public async Task<Plan> NextWorkout()
    {
        var plan = await repository.GetPlan();

        if (plan is null)
        {
            throw new InvalidOperationException("Plan not found");
        }

        var workout = plan.Workouts[plan.CurrentIndex++];

        if (workout.HasFailureTest)
        {
                
            var reps = await failureTestService.GetFailureTestReps();

            workout.Sets[^1] = workout.Sets[^1] with {Reps = reps};

            var orm = reps switch
            {
                <= 1 => plan.CurrentOrm.DecrementStep(),
                >= 5 => plan.CurrentOrm.IncrementStep(),
                _ => plan.CurrentOrm,
            };

            if (plan.CurrentOrm != orm)
            {
                await UpdateOrm(plan, orm);
            }
        }

        await repository.SavePlan(plan);
        return plan;
    }

    private async Task UpdateOrm(Plan plan, Weight orm)
    {
        var workouts = (await workoutRepository.GetWorkouts(orm)).ToList();

        for (var i = plan.CurrentIndex; i < workouts.Count; i++)
        {
            plan.Workouts[i] = workouts[i] with
            {
                Orm = orm,
                Sets = workouts[i].Sets,
            };
        }

        plan.CurrentOrm = orm;
    }
}

using BenchPressPlus20Kg.ConsoleUi;
using BenchPressPlus20Kg.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(
    (_, services) =>
    {
        using var reader = new StreamReader("WorkoutSheet.csv");
        services.AddSingleton<IWorkoutRepository>(new CsvWorkoutRepository(reader));
        services.AddSingleton<IPlanRepository, InMemoryPlanRepository>();
        services.AddSingleton<IFailureTestService, HardcodeFailureTestService>();
        services.AddSingleton<IPlanService, PlanService>();
        services.AddSingleton<WorkoutSheetUi>();
    });

var host = builder.Build();

const Weight.Unit unit = Weight.Unit.Kg;
var ui = host.Services.GetRequiredService<WorkoutSheetUi>();
var planService = host.Services.GetRequiredService<IPlanService>();

await ui.Print(unit);

foreach (var _ in Enumerable.Range(start: 0, count: 14))
{
    await planService.NextWorkout();
}

await ui.Print(unit);

internal class InMemoryPlanRepository(IWorkoutRepository workoutRepository) : IPlanRepository
{
    private static readonly Weight Orm = Weight.FromKg(105);

    private Plan? _plan = new()
    {
        Workouts = workoutRepository.GetWorkouts(Orm).Result.ToList(),
        CurrentOrm = Orm,
        CurrentIndex = 0,
    };

    public Task<Plan?> GetPlan() => Task.FromResult(_plan);

    public Task SavePlan(Plan? plan)
    {
        _plan = plan;
        return Task.CompletedTask;
    }

    public Task DeletePlan()
    {
        _plan = null;
        return Task.CompletedTask;
    }
}

internal class HardcodeFailureTestService : IFailureTestService
{
    public Task<int> GetFailureTestReps()
    {
        return Task.FromResult(4);
    }
}

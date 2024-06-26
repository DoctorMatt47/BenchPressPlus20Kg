using BenchPressPlus20Kg.Core;
using Spectre.Console;

namespace BenchPressPlus20Kg.ConsoleUi;

public class WorkoutSheetUi(Weight orm)
{
    public void Print()
    {
        var table = new Table();

        table.Title($"Workout Sheet orm {orm}");
        table.AddColumn("Day");
        table.AddColumn("Orm");
        table.AddColumn("Is done");

        table.AddColumns(
            Enumerable.Range(start: 1, count: 5)
                .Select(i => new TableColumn($"Set {i}"))
                .ToArray()
        );

        var plan = new Plan(WorkoutSheet.GetRelativePlan(), orm);

        // Enumerable.Range(start: 1, count: 10).ToList().ForEach(
        //     i =>
        //     {
        //         if (plan.AbsoluteWorkouts[i - 1].HasFailureTest)
        //         {
        //             plan.DoneWorkout(i, lastSetReps: 5);
        //             return;
        //         }
        //         
        //         plan.DoneWorkout(i);
        //     });
        
        foreach (var workout in plan.AbsoluteWorkouts)
        {
            var sets = workout.Select(set => set.ToString()).ToList();
            
            if (workout.LastSetReps is not null)
            {
                sets[^1] += $" ({workout.LastSetReps})";
            }
            
            table.AddRow(
                    sets
                    .Prepend(workout.IsDone.ToString())
                    .Prepend(workout.Orm.ToString())
                    .Prepend(workout.Ordinal.ToString())
                    .ToArray()
            );
        }

        AnsiConsole.Write(table);
    }
}

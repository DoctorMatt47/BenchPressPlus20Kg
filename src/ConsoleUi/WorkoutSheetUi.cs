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
        
        table.AddColumns(
            Enumerable.Range(start: 1, count: 5)
                .Select(i => new TableColumn($"Set {i}"))
                .ToArray()
        );

        var count = 1;

        foreach (var workout in WorkoutSheet.PlanRelative.ToAbsolute(orm).Workouts)
        {
            table.AddRow(
                workout
                    .Select(set => set.ToString())
                    .Prepend((count++).ToString())
                    .ToArray()
            );
        }

        AnsiConsole.Write(table);
    }
}

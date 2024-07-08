using BenchPressPlus20Kg.ConsoleUi;
using BenchPressPlus20Kg.Domain;
using BenchPressPlus20Kg.Infrastructure;

var plan = new Plan(
    Weight.FromKg(107.5m),
    new CsvWorkoutSheetParser(new StreamReader("WorkoutSheet.csv")).GetWorkouts,
    () => 1
);

var ui = new WorkoutSheetUi(plan, Weight.Unit.Kg);

ui.Print();

foreach (var _ in Enumerable.Range(0, 14))
{
    plan.NextWorkout();
    ui.Print();
}

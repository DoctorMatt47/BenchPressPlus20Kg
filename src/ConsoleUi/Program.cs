// See https://aka.ms/new-console-template for more information

using BenchPressPlus20Kg.ConsoleUi;
using BenchPressPlus20Kg.Core;
using BenchPressPlus20Kg.SheetParser;

var sheet = new Sheet();

var plan = new Plan(Weight.FromKg(107.5m), sheet.GetWorkouts, () => 1);
    
var ui = new WorkoutSheetUi(plan, Weight.Unit.Kg);

ui.Print();

foreach (var _ in Enumerable.Range(0, 14))
{
    plan.NextWorkout();
    ui.Print();
}

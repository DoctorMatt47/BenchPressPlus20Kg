// See https://aka.ms/new-console-template for more information

using BenchPressPlus20Kg.ConsoleUi;
using BenchPressPlus20Kg.Core;

new WorkoutSheetUi(new Plan(Weight.FromKg(107.5m), (w) => null!, () => 4)).Print();

using BenchPressPlus20Kg.Core;
using Bogus;
using FluentAssertions;

namespace BenchPressPlus20Kg.UnitTests.Core;

public class SetTests
{
   private readonly Faker _faker = new(); 
   
   [Fact]
   public void FromReps_SetRelative_ReturnsPercentAndReps()
   {
       // Arrange
       var percent = _faker.Random.Decimal();
       var reps = _faker.Random.Int();
       
       // Act
       var result = new SetRelative(percent) {Reps = reps};
       
       // Assert
       result.Percent.Should().Be(percent);
       result.Reps.Should().Be(reps);
   }
   
   [Fact]
   public void Negative_SetRelative_ReturnsPercentAndIsNegative()
   {
       // Arrange
       var percent = _faker.Random.Decimal();
       
       // Act
       var result = new SetRelative(percent) {IsNegative = true};
       
       // Assert
       result.Percent.Should().Be(percent);
       result.IsNegative.Should().BeTrue();
   }
   
   [Fact]
   public void FailureTest_SetRelative_ReturnsPercentAndIsFailureTest()
   {
       // Arrange
       var percent = _faker.Random.Decimal();
       
       // Act
       var result = new SetRelative(percent) {IsFailureTest = true};
       
       // Assert
       result.Percent.Should().Be(percent);
       result.IsFailureTest.Should().BeTrue();
   }
   
   [Fact]
   public void ToString_SetAbsolute_ReturnsWeightAndRepsString()
   {
       // Arrange
       var weight = Weight.FromUnit(_faker.Random.Decimal(), Weight.Unit.Kg);
       var reps = _faker.Random.Int();
       var set = new SetAbsolute(weight) { Reps = reps };
       
       // Act
       var result = set.ToString();
       
       // Assert
       result.Should().Be($"{weight.Round()} x {reps}");
   }
}

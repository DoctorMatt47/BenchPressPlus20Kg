using BenchPressPlus20Kg.Core;
using Bogus;
using FluentAssertions;

namespace BenchPressPlus20Kg.UnitTests.Core;

public class WeightTests
{
    private readonly Faker _faker = new();
    
    [Fact]
    public void FromKg_ReturnsWeightInKg()
    {
        // Arrange
        var weight = _faker.Random.Decimal();
        
        // Act
        var result = Weight.FromUnit(weight, Weight.Unit.Kg);
        
        // Assert
        result.InKg.Should().Be(weight);
    }
    
    [Fact]
    public void FromLb_ReturnsWeightInLb()
    {
        // Arrange
        var weight = _faker.Random.Decimal();
        
        // Act
        var result = Weight.FromUnit(weight, Weight.Unit.Lb);
        
        // Assert
        result.InLb.Should().Be(weight);
    }
    
    [Fact]
    public void FromUnit_ReturnsWeightInUnit()
    {
        // Arrange
        var weight = _faker.Random.Decimal();
        var unit = _faker.PickRandom<Weight.Unit>();
        
        // Act
        var result = Weight.FromUnit(weight, unit);
        
        // Assert
        result.InUnit.Should().Be(unit);
    }
}

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
        var kg = _faker.Random.Decimal();
        var weight = Weight.FromKg(kg);

        weight.InKg.Should().BeApproximately(kg, precision: 2.5m);
    }
    
    [Fact]
    public void FromLb_ReturnsWeightInLb()
    {
        var lb = _faker.Random.Decimal();
        var weight = Weight.FromLb(lb);
        
        weight.InLb.Should().BeApproximately(lb, precision: 5m);
    }
    
    [Fact]
    public void IncrementStep_IncrementsWeightBy5()
    {
        var weight = Weight.FromLb(0);
        
        weight.IncrementStep().InLb.Should().Be(5);
    }
    
    [Fact]
    public void DecrementStep_DecrementsWeightBy5()
    {
        var weight = Weight.FromLb(10);
        
        weight.DecrementStep().InLb.Should().Be(5);
    }
    
    [Fact]
    public void Equals_ReturnsTrue_WhenWeightsAreEqual()
    {
        var weight1 = Weight.FromKg(0);
        var weight2 = Weight.FromKg(0);
        
        weight1.Equals(weight2).Should().BeTrue();
    }
    
    [Fact]
    public void Equals_ReturnsFalse_WhenWeightsAreNotEqual()
    {
        var weight1 = Weight.FromKg(0);
        var weight2 = Weight.FromKg(5);
        
        weight1.Equals(weight2).Should().BeFalse();
    }
    
    [Fact]
    public void GetHashCode_ReturnsHashCode()
    {
        var weight = Weight.FromKg(0);
        
        weight.GetHashCode().Should().Be(weight.InLb.GetHashCode());
    }
}

using App.UpdatePolicies;
using FluentAssertions;

namespace Tests.UpdatePolicies;

public class NormalPolicyTest
{
    [Theory]
    [InlineData(2, 80, 1, 79)]
    [InlineData(2, 2, 1, 1)]
    [InlineData(1, 1, 0, 0)]
    [InlineData(0, 0, -1, 0)]
    [InlineData(-1, 0, -2, 0)]
    [InlineData(0, 4, -1, 2)]
    [InlineData(0, 1, -1, 0)]
    // Once the sell by date has passed, Quality degrades twice as fast
    // The Quality of an item is never negative
    public void UpdatedSellInAndQuality(int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        new NormalPolicy().UpdatedSellInAndQuality(sellIn, quality).Should().Be((expectedSellIn, expectedQuality));
    }
}
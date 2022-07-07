using App.UpdatePolicies;
using FluentAssertions;

namespace Tests.UpdatePolicies;

public class LegendaryPolicyTest
{
    [Theory]
    [InlineData(2, 80, 2, 80)]
    [InlineData(0, 80, 0, 80)]
    [InlineData(-1, 80, -1, 80)]
    [InlineData(-2, 80, -2, 80)]
    [InlineData(-2, 50, -2, 50)]
    // never has to be sold or decreases in Quality
    // Quality is 80 and it never alters.
    public void UpdatedSellInAndQuality(int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        new LegendaryPolicy().UpdatedSellInAndQuality(sellIn, quality).Should().Be((expectedSellIn, expectedQuality));
    }
}
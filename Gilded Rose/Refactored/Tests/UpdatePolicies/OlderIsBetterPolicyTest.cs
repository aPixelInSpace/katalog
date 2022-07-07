using App.UpdatePolicies;
using FluentAssertions;

namespace Tests.UpdatePolicies;

public class OlderIsBetterPolicyTest
{
    [Theory]
    [InlineData(2, 0, 1, 1)]
    [InlineData(0, 49, -1, 50)]
    [InlineData(0, 50, -1, 50)]
    [InlineData(-1, 50, -2, 50)]
    // increases in Quality the older it gets
    // The Quality of an item is never more than 50
    public void UpdatedSellInAndQuality(int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        new OlderIsBetterPolicy().UpdatedSellInAndQuality(sellIn, quality).Should().Be((expectedSellIn, expectedQuality));
    }
}
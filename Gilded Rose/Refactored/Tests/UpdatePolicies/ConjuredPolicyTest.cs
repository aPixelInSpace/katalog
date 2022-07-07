using App.UpdatePolicies;
using FluentAssertions;

namespace Tests.UpdatePolicies;

public class ConjuredPolicyTest
{
    [Theory]
    [InlineData(4, 4, 3, 2)]
    [InlineData(0, 0, -1, 0)]
    // "Conjured" items degrade in Quality twice as fast as normal items
    public void UpdatedSellInAndQuality(int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        new ConjuredPolicy().UpdatedSellInAndQuality(sellIn, quality).Should().Be((expectedSellIn, expectedQuality));
    }
}
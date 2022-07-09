using App.UpdatePolicies;
using FluentAssertions;

namespace Tests.UpdatePolicies;

public class HotItemPolicyTest
{
    [Theory]
    [InlineData(11, 1, 10, 2)]
    [InlineData(10, 1, 9, 3)]
    [InlineData(9, 3, 8, 5)]
    [InlineData(6, 3, 5, 5)]
    [InlineData(5, 3, 4, 6)]
    [InlineData(2, 8, 1, 11)]
    [InlineData(1, 8, 0, 11)]
    [InlineData(0, 8, -1, 0)]
    [InlineData(50, 50, 49, 50)]
    [InlineData(2, 49, 1, 50)]
    // increases in Quality as its SellIn value approaches; Quality increases by 2
    // when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
    public void UpdatedSellInAndQuality(int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        new HotItemPolicy().UpdatedSellInAndQuality(sellIn, quality).Should().Be((expectedSellIn, expectedQuality));
    }
}
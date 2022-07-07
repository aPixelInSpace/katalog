using App;
using App.UpdatePolicies;
using FluentAssertions;

namespace Tests;

public class PolicySelectorTest
{
    [Theory]
    [InlineData("foo", typeof(NormalPolicy))]
    [InlineData("Sulfuras, Hand of Ragnaros", typeof(LegendaryPolicy))]
    [InlineData("Aged Brie", typeof(OlderIsBetterPolicy))]
    [InlineData("Conjured Sword of the Leviathan", typeof(ConjuredPolicy))]
    [InlineData("Backstage passes : to a THC concert", typeof(HotItemPolicy))]
    public void ChooseAppropriate(string itemName, Type expectedType)
    {
        PolicySelector.ChooseAppropriate(itemName).GetType().Should().Be(expectedType);
    }
}
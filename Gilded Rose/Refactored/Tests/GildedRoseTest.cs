using App;
using FluentAssertions;

namespace Tests;

public class GildedRoseTest
{
    [Theory]
    [InlineData("foo", 2, 80, 1, 79)]
    [InlineData("foo", 2, 2, 1, 1)]
    [InlineData("foo", 1, 1, 0, 0)]
    public void NormalItem_Degrades(string name, int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        var items = new List<Item> { new() { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        
        items[0].Name.Should().Be(name);
        items[0].SellIn.Should().Be(expectedSellIn);
        items[0].Quality.Should().Be(expectedQuality);
    }
    
    [Theory]
    [InlineData("foo", 0, 0, -1, 0)]
    [InlineData("foo", -1, 0, -2, 0)]
    // The Quality of an item is never negative
    public void NormalItem_StopsDegrading(string name, int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        var items = new List<Item> { new() { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        
        items[0].Name.Should().Be(name);
        items[0].SellIn.Should().Be(expectedSellIn);
        items[0].Quality.Should().Be(expectedQuality);
    }
    
    [Theory]
    [InlineData("foo", 0, 4, -1, 2)]
    [InlineData("foo", 0, 1, -1, 0)]
    // Once the sell by date has passed, Quality degrades twice as fast
    // The Quality of an item is never negative
    public void NormalItem_DegradesTwiceAsFast(string name, int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        var items = new List<Item> { new() { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();

        items[0].Name.Should().Be(name);
        items[0].SellIn.Should().Be(expectedSellIn);
        items[0].Quality.Should().Be(expectedQuality);
    }
    
    [Theory]
    [InlineData("Aged Brie", 2, 0, 1, 1)]
    [InlineData("Aged Brie", 0, 49, -1, 50)]
    [InlineData("Aged Brie", 0, 50, -1, 50)]
    [InlineData("Aged Brie", -1, 50, -2, 50)]
    // "Aged Brie" actually increases in Quality the older it gets
    // The Quality of an item is never more than 50
    public void AgedBrie_IncreasesInQuality(string name, int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        var items = new List<Item> { new() { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();

        items[0].Name.Should().Be(name);
        items[0].SellIn.Should().Be(expectedSellIn);
        items[0].Quality.Should().Be(expectedQuality);
    }
    
    [Theory]
    [InlineData("Sulfuras, Hand of Ragnaros", 2, 80, 2, 80)]
    [InlineData("Sulfuras, Hand of Ragnaros", 0, 80, 0, 80)]
    [InlineData("Sulfuras, Hand of Ragnaros", -1, 80, -1, 80)]
    [InlineData("Sulfuras, Hand of Ragnaros", -2, 80, -2, 80)]
    [InlineData("Sulfuras, Hand of Ragnaros", -2, 50, -2, 50)]
    // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
    // "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.
    public void Sulfuras_NeverAlters(string name, int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        var items = new List<Item> { new() { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();

        items[0].Name.Should().Be(name);
        items[0].SellIn.Should().Be(expectedSellIn);
        items[0].Quality.Should().Be(expectedQuality);
    }
    
    [Theory]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 11, 1, 10, 2)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 1, 9, 3)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 9, 3, 8, 5)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 6, 3, 5, 5)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 3, 4, 6)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 2, 8, 1, 11)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 8, 0, 11)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", 0, 8, -1, 0)]
    // "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches; Quality increases by 2
    // when there are 10 days or less and by 3 when there are 5 days or less but Quality drops to 0 after the concert
    public void BackstagePasses_IncreasesInQualityBeforeSellInThen0After(string name, int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        var items = new List<Item> { new() { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();

        items[0].Name.Should().Be(name);
        items[0].SellIn.Should().Be(expectedSellIn);
        items[0].Quality.Should().Be(expectedQuality);
    }
    
    // [Theory]
    // [InlineData("Conjured Mana Cake", 4, 4, 3, 2)]
    // // "Conjured" items degrade in Quality twice as fast as normal items
    // public void Conjured_DecreasesInQualityTwiceAsFastAsNormalItem(string name, int sellIn, int quality, int expectedSellIn, int expectedQuality)
    // {
    //     var items = new List<Item> { new() { Name = name, SellIn = sellIn, Quality = quality } };
    //     var app = new GildedRose(items);
    //     app.UpdateQuality();
    //
    //     items[0].Name.Should().Be(name);
    //     items[0].SellIn.Should().Be(expectedSellIn);
    //     items[0].Quality.Should().Be(expectedQuality);
    // }
    
    [Theory]
    [InlineData("Sulfuras, Hand of Ragnaros", GildedRose.UpdatePolicy.Legendary)]
    [InlineData("Aged Brie", GildedRose.UpdatePolicy.OlderIsBetter)]
    [InlineData("Elixir of the Mongoose", GildedRose.UpdatePolicy.Normal)]
    [InlineData("Conjured Sword of the Leviathan", GildedRose.UpdatePolicy.Conjured)]
    [InlineData("Backstage passes to a TAFKAL80ETC concert", GildedRose.UpdatePolicy.HotItem)]
    public void GetUpdatePolicyBasedOnName(string name, GildedRose.UpdatePolicy expectedUpdatePolicy)
    {
        GildedRose.GetUpdatePolicy(name).Should().Be(expectedUpdatePolicy);
    }
    
    [Theory]
    [InlineData(GildedRose.UpdatePolicy.Legendary, 10, 80, 10, 80)]
    [InlineData(GildedRose.UpdatePolicy.Normal, 2, 2, 1, 1)]
    [InlineData(GildedRose.UpdatePolicy.Normal, 0, 0, -1, 0)]
    [InlineData(GildedRose.UpdatePolicy.Normal, -1, 0, -2, 0)]
    [InlineData(GildedRose.UpdatePolicy.Normal, 0, 4, -1, 2)]
    [InlineData(GildedRose.UpdatePolicy.Normal, 0, 1, -1, 0)]
    [InlineData(GildedRose.UpdatePolicy.OlderIsBetter, 2, 0, 1, 1)]
    [InlineData(GildedRose.UpdatePolicy.OlderIsBetter, 0, 49, -1, 50)]
    [InlineData(GildedRose.UpdatePolicy.OlderIsBetter, 0, 50, -1, 50)]
    [InlineData(GildedRose.UpdatePolicy.OlderIsBetter, -1, 50, -2, 50)]
    [InlineData(GildedRose.UpdatePolicy.HotItem, 11, 1, 10, 2)]
    [InlineData(GildedRose.UpdatePolicy.HotItem, 10, 1, 9, 3)]
    [InlineData(GildedRose.UpdatePolicy.HotItem, 9, 3, 8, 5)]
    [InlineData(GildedRose.UpdatePolicy.HotItem, 6, 3, 5, 5)]
    [InlineData(GildedRose.UpdatePolicy.HotItem, 5, 3, 4, 6)]
    [InlineData(GildedRose.UpdatePolicy.HotItem, 2, 8, 1, 11)]
    [InlineData(GildedRose.UpdatePolicy.HotItem, 1, 8, 0, 11)]
    [InlineData(GildedRose.UpdatePolicy.HotItem, 0, 8, -1, 0)]
    [InlineData(GildedRose.UpdatePolicy.Conjured, 4, 4, 3, 2)]
    [InlineData(GildedRose.UpdatePolicy.Conjured, 0, 3, -1, 0)]
    [InlineData(GildedRose.UpdatePolicy.Conjured, 0, 5, -1, 1)]
    public void GetUpdatedSellInAndQuality(GildedRose.UpdatePolicy updatePolicy, int sellIn, int quality, int expectedSellIn, int expectedQuality)
    {
        GildedRose
            .GetUpdatedSellInAndQuality(updatePolicy, sellIn, quality)
            .Should().Be((updatePolicy, expectedSellIn, expectedQuality));
    }
}
using System.Linq;

namespace App;

public class GildedRose
{
    public enum UpdatePolicy
    {
        Normal,
        Legendary,
        OlderIsBetter,
        HotItem,
        Conjured,
    }

    //
    // we could get these from a config
    //
    private static readonly string[] LegendaryItems = { "Sulfuras, Hand of Ragnaros" };
    private static readonly string[] OlderAreBetterItems = { "Aged Brie" };
    private static readonly string[] HotItemsPrefix = { "Backstage passes" };
    private static readonly string[] ConjuredPrefix = { "Conjured" };

    private static readonly int NormalSellInDegradationRate = 1;
    private static readonly int NormalQualityDegradationRate = 1;
    
    private static readonly int OtherQualityIncreaseRate = 1;
    
    private static readonly int HotItemQualityIncreaseRateFirstDay = 10;
    private static readonly int HotItemQualityIncreaseRateFirst = 2;
    private static readonly int HotItemQualityIncreaseRateSecondDay = 5;
    private static readonly int HotItemQualityIncreaseRateSecond = 3;
    
    private static readonly int MinQuality = 0;
    private static readonly int MaxQuality = 50;
    private static readonly int SellInThreshold = 0;
    //
    //
    //
    
    
    IList<Item> Items;
    
    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            // determine the update policy for the item
            var updatePolicy = GetUpdatePolicy(item.Name);

            // update according to the previous type
            var (_, newSellIn, newQuality) = GetUpdatedSellInAndQuality(updatePolicy, item.SellIn, item.Quality);
            item.SellIn = newSellIn;
            item.Quality = newQuality;
        }
    }

    public static UpdatePolicy GetUpdatePolicy(string itemName)
    {
        if (LegendaryItems.Contains(itemName)) return UpdatePolicy.Legendary;
        if (OlderAreBetterItems.Contains(itemName)) return UpdatePolicy.OlderIsBetter;
        if (HotItemsPrefix.Any(itemName.StartsWith)) return UpdatePolicy.HotItem;
        if (ConjuredPrefix.Any(itemName.StartsWith)) return UpdatePolicy.Conjured;

        return UpdatePolicy.Normal;
    }

    public static (UpdatePolicy updatePolicy, int newSellIn, int newQuality) GetUpdatedSellInAndQuality(UpdatePolicy updatePolicy, int sellIn, int quality)
    {
        // still work to do...
        switch (updatePolicy)
        {
            case UpdatePolicy.Normal:
            {
                var decreasedQuality = sellIn > SellInThreshold ? quality - NormalQualityDegradationRate : quality - NormalQualityDegradationRate * 2;
            
                // The Quality of an item is never negative
                var newQuality = decreasedQuality >= MinQuality ? decreasedQuality : MinQuality;
            
                return (
                    updatePolicy,
                    sellIn - NormalSellInDegradationRate,
                    newQuality);
            }
            case UpdatePolicy.Legendary:
                // legendary item never alters
                return (updatePolicy, sellIn, quality);
            case UpdatePolicy.OlderIsBetter:
                // Quality increases the older it gets
                // The Quality of an item is never more than 50
                return (
                    updatePolicy,
                    sellIn - NormalSellInDegradationRate,
                    quality < MaxQuality ? quality + OtherQualityIncreaseRate : MaxQuality);
            case UpdatePolicy.HotItem:
            {
                // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
                var increaseValue = sellIn <= HotItemQualityIncreaseRateSecondDay ? HotItemQualityIncreaseRateSecond : sellIn <= HotItemQualityIncreaseRateFirstDay ? HotItemQualityIncreaseRateFirst : OtherQualityIncreaseRate;
                var increasedQuality = quality < MaxQuality ? quality + increaseValue : MaxQuality;
            
                // but Quality drops to 0 after the concert
                var newQuality = sellIn <= SellInThreshold ? MinQuality : increasedQuality;
            
                return (
                    updatePolicy,
                    sellIn - NormalSellInDegradationRate,
                    newQuality);
            }
            case UpdatePolicy.Conjured:
            {
                // "Conjured" items degrade in Quality twice as fast as normal items
                var decreasedQuality = sellIn > SellInThreshold ? quality - NormalQualityDegradationRate * 2 : quality - NormalQualityDegradationRate * 4;
            
                // The Quality of an item is never negative
                var newQuality = decreasedQuality >= MinQuality ? decreasedQuality : MinQuality;
            
                return (
                    updatePolicy,
                    sellIn - NormalSellInDegradationRate,
                    newQuality);
            }
            default: throw new Exception($"No rules found for the update policy {updatePolicy.ToString()}");
        }
    }
}
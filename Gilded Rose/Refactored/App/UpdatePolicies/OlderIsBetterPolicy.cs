namespace App.UpdatePolicies;

public class OlderIsBetterPolicy : IUpdatePolicy
{
    private const int QualityIncreaseRate = 1;
    private const int SellInDegradationRate = 1;
    
    public (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality)
    {
        var increaseQuality = sellIn > Config.SellInThreshold ? quality + QualityIncreaseRate : quality + QualityIncreaseRate * 2;
        
        // Quality increases the older it gets
        // The Quality of an item is never more than 50
        return (sellIn - SellInDegradationRate,
            increaseQuality < Config.MaxQuality ? increaseQuality : Config.MaxQuality);
    }
}
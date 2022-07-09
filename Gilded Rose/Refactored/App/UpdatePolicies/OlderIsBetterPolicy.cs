namespace App.UpdatePolicies;

public class OlderIsBetterPolicy : IUpdatePolicy
{
    private const int QualityIncreaseRate = 1;
    private const int SellInDegradationRate = 1;
    
    public (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality)
    {
        // Quality increases the older it gets
        var increaseQuality = sellIn > Config.SellInThreshold ? quality + QualityIncreaseRate : quality + QualityIncreaseRate * 2;
        
        // The Quality of an item is never more than 50
        return (sellIn - SellInDegradationRate, IUpdatePolicy.MaxQuality(increaseQuality));
    }
}
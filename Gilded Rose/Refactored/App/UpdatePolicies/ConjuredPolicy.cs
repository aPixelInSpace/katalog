namespace App.UpdatePolicies;

public class ConjuredPolicy : IUpdatePolicy
{
    private const int QualityDegradationRate = 1;
    private const int SellInDegradationRate = 1;
    
    public (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality)
    {
        // "Conjured" items degrade in Quality twice as fast as normal items
        var decreasedQuality = sellIn > Config.SellInThreshold ? quality - QualityDegradationRate * 2 : quality - QualityDegradationRate * 4;
            
        // The Quality of an item is never negative
        var newQuality = IUpdatePolicy.NonNegativeQuality(decreasedQuality);
            
        return (
            sellIn - SellInDegradationRate,
            newQuality);
    }
}
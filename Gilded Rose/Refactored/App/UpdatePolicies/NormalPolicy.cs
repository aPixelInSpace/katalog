namespace App.UpdatePolicies;

public class NormalPolicy : IUpdatePolicy
{
    private const int QualityDegradationRate = 1;
    private const int SellInDegradationRate = 1;
    
    public (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality)
    {
        var decreasedQuality = sellIn > Config.SellInThreshold ? quality - QualityDegradationRate : quality - QualityDegradationRate * 2;
            
        // The Quality of an item is never negative
        var newQuality = IUpdatePolicy.NonNegativeQuality(decreasedQuality);
            
        return (
            sellIn - SellInDegradationRate,
            newQuality);
    }
}
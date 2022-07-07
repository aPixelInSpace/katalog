namespace App.UpdatePolicies;

public class HotItemPolicy : IUpdatePolicy
{
    private const int QualityIncreaseRateDefault = 1;
    private const int QualityIncreaseRateFirstDay = 10;
    private const int QualityIncreaseRateFirstValue = 2;
    private const int QualityIncreaseRateSecondDay = 5;
    private const int QualityIncreaseRateSecondValue = 3;
    
    private static readonly int SellInDegradationRate = 1;
    
    public (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality)
    {
        // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
        var increaseValue = sellIn <= QualityIncreaseRateSecondDay ? QualityIncreaseRateSecondValue : sellIn <= QualityIncreaseRateFirstDay ? QualityIncreaseRateFirstValue : QualityIncreaseRateDefault;
        var increasedQuality = quality < Config.MaxQuality ? quality + increaseValue : Config.MaxQuality;
            
        // but Quality drops to 0 after the concert
        var newQuality = sellIn <= Config.SellInThreshold ? Config.MinQuality : increasedQuality;
            
        return (
            sellIn - SellInDegradationRate,
            newQuality);
    }
}
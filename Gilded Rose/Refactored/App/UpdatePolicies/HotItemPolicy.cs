namespace App.UpdatePolicies;

public class HotItemPolicy : IUpdatePolicy
{   
    private const int SellInDegradationRate = 1;
    
    public (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality)
    {
        // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
        var increaseValue = 
            sellIn <= 5 ? 3 : 
                sellIn <= 10 ? 2 : 1;
        var increasedQuality = IUpdatePolicy.MaxQuality(quality < Config.MaxQuality ? quality + increaseValue : Config.MaxQuality);
            
        // but Quality drops to 0 after the concert
        var newQuality = sellIn <= Config.SellInThreshold ? Config.MinQuality : increasedQuality;
            
        return (
            sellIn - SellInDegradationRate,
            newQuality);
    }
}
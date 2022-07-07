namespace App.UpdatePolicies;

public class LegendaryPolicy : IUpdatePolicy
{
    public (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality)
        // legendary item never alters
        => (sellIn, quality);
}
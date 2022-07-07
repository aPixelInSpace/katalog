namespace App.UpdatePolicies;

public interface IUpdatePolicy
{
    (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality);
}
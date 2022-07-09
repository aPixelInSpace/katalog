namespace App.UpdatePolicies;

public interface IUpdatePolicy
{
    (int newSellIn, int newQuality) UpdatedSellInAndQuality(int sellIn, int quality);

    static int NonNegativeQuality(int quality)
        => quality >= Config.MinQuality ? quality : Config.MinQuality;

    static int MaxQuality(int quality)
        => quality < Config.MaxQuality ? quality : Config.MaxQuality;
}
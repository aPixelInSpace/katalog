using App.UpdatePolicies;

namespace App;

public static class PolicySelector
{
    public static IUpdatePolicy ChooseAppropriate(string itemName)
    {
        // new values according to the policy
        if (Config.LegendaryItems.Contains(itemName)) return new LegendaryPolicy();
        if (Config.OlderAreBetterItems.Contains(itemName)) return new OlderIsBetterPolicy();
        if (Config.HotItemsPrefix.Any(itemName.StartsWith)) return new HotItemPolicy();
        if (Config.ConjuredPrefix.Any(itemName.StartsWith)) return new ConjuredPolicy();

        return new NormalPolicy();
    }
}
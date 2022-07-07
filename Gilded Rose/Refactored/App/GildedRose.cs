using System.Linq;
using App.UpdatePolicies;

namespace App;

public class GildedRose
{
    IList<Item> Items;
    
    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            // determine the update policy for the item
            var policy = PolicySelector.ChooseAppropriate(item.Name);
            
            // update according to the policy
            var (newSellIn, newQuality) = policy.UpdatedSellInAndQuality(item.SellIn, item.Quality);
            
            item.SellIn = newSellIn;
            item.Quality = newQuality;
        }
    }
}
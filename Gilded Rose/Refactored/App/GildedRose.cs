namespace App;

public class GildedRose
{
    public enum UpdatePolicy
    {
        Normal,
        Legendary,
        OlderIsBetter,
        HotItem,
        Conjured,
    }
    
    IList<Item> Items;
    
    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            // determine the type of item
            
            // update according to the previous type
            
            
            if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (Items[i].Quality > 0)
                {
                    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                    {
                        Items[i].Quality = Items[i].Quality - 1;
                    }
                }
            }
            else
            {
                if (Items[i].Quality < 50)
                {
                    Items[i].Quality = Items[i].Quality + 1;

                    if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].SellIn < 11)
                        {
                            if (Items[i].Quality < 50)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }

                        if (Items[i].SellIn < 6)
                        {
                            if (Items[i].Quality < 50)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }
                    }
                }
            }

            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
                Items[i].SellIn = Items[i].SellIn - 1;
            }

            if (Items[i].SellIn < 0)
            {
                if (Items[i].Name != "Aged Brie")
                {
                    if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].Quality > 0)
                        {
                            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                            {
                                Items[i].Quality = Items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        Items[i].Quality = Items[i].Quality - Items[i].Quality;
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;
                    }
                }
            }
        }
    }

    public static UpdatePolicy GetUpdatePolicy(string itemName)
    {
        var legendaryItems = new[] { "Sulfuras, Hand of Ragnaros" };
        var olderAreBetterItems = new[] { "Aged Brie" };
        var hotItemsPrefix = new[] { "Backstage passes" };
        var conjuredPrefix = new[] { "Conjured" };
        
        if (legendaryItems.Contains(itemName)) return UpdatePolicy.Legendary;
        if (olderAreBetterItems.Contains(itemName)) return UpdatePolicy.OlderIsBetter;
        if (hotItemsPrefix.Any(itemName.StartsWith)) return UpdatePolicy.HotItem;
        if (conjuredPrefix.Any(itemName.StartsWith)) return UpdatePolicy.Conjured;

        return UpdatePolicy.Normal;
    }
}
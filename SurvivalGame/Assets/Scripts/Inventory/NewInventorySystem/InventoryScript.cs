using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript
{
    public List<ItemScript> itemList;

    public InventoryScript()
    {
        itemList = new List<ItemScript>();
        AddItem(new ItemScript { itemType = ItemScript.ItemType.Gladius, amount= 1});
        AddItem(new ItemScript { itemType = ItemScript.ItemType.HealthPotion, amount= 1});
        AddItem(new ItemScript { itemType = ItemScript.ItemType.Gladius, amount= 1});
        
    }

    public void AddItem(ItemScript item)
    {
        itemList.Add(item);
    }

    public List<ItemScript> GetItemList()
    {
        return itemList;
    }
}

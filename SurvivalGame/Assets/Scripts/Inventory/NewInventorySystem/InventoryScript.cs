using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript
{
    public List<ItemScript> itemList;

    public event EventHandler OnItemListchanged;

    public InventoryScript()
    {
        itemList = new List<ItemScript>();
    }

    public void AddItem(ItemScript item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(ItemScript inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount+= item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if(itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }

        OnItemListchanged?.Invoke(this, EventArgs.Empty);
    }

    public List<ItemScript> GetItemList()
    {
        return itemList;
    }
}

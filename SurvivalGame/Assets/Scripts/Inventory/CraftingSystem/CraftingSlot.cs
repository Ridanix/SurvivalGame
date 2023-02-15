using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public Image icon;
    public ScriptableItem item;
    public Button removeButton;
    public Button toInventoryButton;
    public bool isOutputslot = false;
    public CraftingSlot[] allOtehrInputCraftingSlots;


    public virtual void AddItem(ScriptableItem newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        toInventoryButton.interactable = true;
    }
    
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        toInventoryButton.interactable = false;
    }

    public void UseItem(bool fromCraftingTable)
    {
        if (item != null)
        {
            
            if(fromCraftingTable)
            {
                if (isOutputslot)
                {
                    Crafting.instance.onCraftingTable.Clear();
                }
                else if (Crafting.instance.onCraftingTable.Count>3)
                {
                    Crafting.instance.onCraftingTable.RemoveAt(3);
                }
            }
            else
            {
                if (isOutputslot)
                {
                    Crafting.instance.onUpgradeTable.Clear();
                }
                else if (Crafting.instance.onUpgradeTable.Count>2)
                {
                    Crafting.instance.onUpgradeTable.RemoveAt(2);
                }
            }


            Debug.LogWarning($"click: {fromCraftingTable}");
            if (Crafting.instance.TableToInventory(item, fromCraftingTable) == false)
            {
                return;
            }
        }        
    }
}

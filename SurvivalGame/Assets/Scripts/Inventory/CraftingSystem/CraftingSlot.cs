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
    public bool isOutputslot= false;
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

    public void UseItem()
    {
        if (item != null)
        {
            if(Crafting.instance.TableToInventory(item) == false)
            {
                return;
            }
            
            if (isOutputslot)
            {
                foreach (ScriptableItem iteminCrafttable in Crafting.instance.onCraftingTable)
                {
                    Crafting.instance.RemoveItemFromTable(iteminCrafttable);
                }
                Debug.LogWarning("Clearing Input Slots");
            }
        }        
    }
}

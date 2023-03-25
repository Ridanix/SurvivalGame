using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotScript : MonoBehaviour
{
    public Image icon;
    public ScriptableItem item;
    public Button removeButton;
    public Button toHotbarButton;
    public bool isInNormalInventory = true;

    public virtual void AddItem(ScriptableItem newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        toHotbarButton.interactable = true;
    }
    
    public virtual void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        toHotbarButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.RemoveItem(item);
    }

    public virtual void UseItem(bool conectedToCrafting)
    {
        if (isInNormalInventory)
        {
           if (item != null) item.UseItem("Inventory");
        }
        else
        {
            if (item != null)
                Crafting.instance.InventoryToTable(item, conectedToCrafting);
        }
    }
    //NEW
    public virtual void OnSwitchButton()
    {
        Inventory.instance.InventoryToHotbar(item);
    }
}

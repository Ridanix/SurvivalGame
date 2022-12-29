using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlotScript : MonoBehaviour
{
    public Image icon;
    public ScriptableItem item;
    public Button toInventoryButton;

    public void AddItem(ScriptableItem newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        toInventoryButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        toInventoryButton.interactable = false;
    }

    public void OnSwitchButton()
    {
        Inventory.instance.HotbarToInventory(item);
    }

    public void UseItem()
    {
        if (item != null) item.UseItem("Hotbar");
    }
}

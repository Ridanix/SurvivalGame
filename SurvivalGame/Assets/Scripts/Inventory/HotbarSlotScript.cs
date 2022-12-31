using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlotScript : InventorySlotScript
{
    public Button toInventoryButton;
    public Sprite placeholder;

    public void Awake()
    {
        icon.enabled = true;
    }

    public override void AddItem(ScriptableItem newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        toInventoryButton.interactable = true;
    }

    public override void ClearSlot()
    {
        item = null;
        icon.sprite = placeholder;
        toInventoryButton.interactable = false;
    }

    public override void OnSwitchButton()
    {
        Inventory.instance.HotbarToInventory(item);
    }

    public override void UseItem()
    {
        if (item != null) item.UseItem("Hotbar");
    }
}

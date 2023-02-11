using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public Transform itemsParentCraftingSlots;
    public Transform itemsParentCraftingInventory;

    public CraftingSlot[] craftingSlots;
    public InventorySlotScript[] inventoryScripts;


    private void Awake()
    {
        craftingSlots = itemsParentCraftingInventory.GetComponentsInChildren<CraftingSlot>();
        // Inventory.instance.onItemChangedCallback += ItemPlacedInCrafting();

    }

    public void ItemPlacedInCrafting()
    {
        
    }


}

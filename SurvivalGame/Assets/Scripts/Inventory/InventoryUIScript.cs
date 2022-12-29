using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIScript : MonoBehaviour
{
    public Transform itemsParentInventory;
    public Transform itemsParentHotbar;
    public GameObject inventoryUIGameObject;
    Inventory inventory;
    InventorySlotScript[] slots;
    //NEW
    HotbarSlotScript[] hotbarSlots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParentInventory.GetComponentsInChildren<InventorySlotScript>();
        //NEW
        hotbarSlots = itemsParentHotbar.GetComponentsInChildren<HotbarSlotScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUIGameObject.SetActive(!inventoryUIGameObject.activeSelf);
        }
    }
    
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.itemList.Count)
            {
                slots[i].AddItem(inventory.itemList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        //NEW
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i < inventory.hotbarList.Count)
            {
                hotbarSlots[i].AddItem(inventory.hotbarList[i]);
            }
            else
            {
                hotbarSlots[i].ClearSlot();
            }
        }
        Debug.Log("Updating");
    }
}

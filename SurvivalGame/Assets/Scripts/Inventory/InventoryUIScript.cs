using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    public static InventoryUIScript instance;
    
    //NEW
    private void Awake()
    {
        instance = this;
    }

    public Transform itemsParentInventory;
    public Transform itemsParentHotbar;
    public GameObject inventoryUIGameObject;
    Inventory inventory;
    InventorySlotScript[] slots;
    //NEW
    public HotbarSlotScript[] hotbarSlots;

    public Sprite hotbarPlaceholder;
    public Image[] hotbarInGameSlots;

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
    

    public void UpdateUI()
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
                hotbarInGameSlots[i].sprite = inventory.hotbarList[i].icon;
            }
            else
            {
                hotbarSlots[i].ClearSlot();
                hotbarInGameSlots[i].sprite = hotbarPlaceholder;
            }
        }
        Debug.Log("Updating");
    }
}

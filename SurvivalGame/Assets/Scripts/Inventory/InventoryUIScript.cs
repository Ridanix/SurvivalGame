using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIScript : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUIGameObject;
    Inventory inventory;
    InventorySlotScript[] slots;
    
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlotScript>();
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
        Debug.Log("Updating");
    }
}

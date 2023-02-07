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
        bookMarksSpriteRenders = new SpriteRenderer[bookMarks.Length];
        for (int i = 0; i < bookMarks.Length; i++)
        {
            bookMarksSpriteRenders[i] = bookMarks[i].GetComponent<SpriteRenderer>();
        }
    }

    public Transform itemsParentInventory;
    public Transform itemsParentHotbar;
    public GameObject inventoryUIMainPanel;
    Inventory inventory;
    InventorySlotScript[] slots;
    //NEW
    public HotbarSlotScript[] hotbarSlots;

    public Sprite hotbarPlaceholder;
    public Image[] hotbarInGameSlots;

    public GameObject[] bookMarks;
    public GameObject rootOfInventoryUI;
    public SpriteRenderer[] bookMarksSpriteRenders;
    public SpriteRenderer activeBookMark;

    public void OnBookMarkClick(SpriteRenderer clickedBookMark)
    {
        activeBookMark = clickedBookMark;

        for (int i = 0; i < bookMarksSpriteRenders.Length; i++)
        {
            bookMarksSpriteRenders[i].sortingOrder = 0;
            if (bookMarksSpriteRenders[i] == activeBookMark)
            {
                bookMarksSpriteRenders[i].sortingOrder = 2;
            }
        }
    }
     
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
            rootOfInventoryUI.SetActive(!rootOfInventoryUI.activeSelf);
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

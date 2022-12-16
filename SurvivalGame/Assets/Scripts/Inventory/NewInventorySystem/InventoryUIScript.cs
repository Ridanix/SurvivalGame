using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIScript : MonoBehaviour
{
    private InventoryScript inventoryScript;

    [SerializeField] Transform itemSlotInventory;
    [SerializeField] Transform itemSlotTemplate;

    public void SetInventory(InventoryScript inventory)
    {
        this.inventoryScript = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach(ItemScript item in inventoryScript.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotInventory).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x*itemSlotCellSize, y*itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Item").GetComponent<Image>();
            image.sprite = item.GetSprite();
            
            x++;
            if(x>4)
            {
                x=0;
                y++;
            }
        }
    }
}

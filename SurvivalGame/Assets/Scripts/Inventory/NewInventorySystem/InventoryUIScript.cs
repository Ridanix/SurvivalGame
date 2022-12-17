using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIScript : MonoBehaviour
{
    private InventoryScript inventoryScript;

    [SerializeField] Transform itemSlotInventory;
    [SerializeField] Transform itemSlotTemplate;

    public void SetInventory(InventoryScript inventory)
    {
        this.inventoryScript = inventory;
        inventoryScript.OnItemListchanged+=InventoryScript_OnItemListchanged;
        RefreshInventoryItems();
    }

    private void InventoryScript_OnItemListchanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotInventory)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach(ItemScript item in inventoryScript.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotInventory).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            
            itemSlotRectTransform.anchoredPosition = new Vector2(x*itemSlotCellSize, y*itemSlotCellSize);
            
            Image image = itemSlotRectTransform.Find("ImageTemplate").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("TextTemplate").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            x++;
            if(x>4)
            {
                x=0;
                y++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventorySlotScript inventorySlotScript;
    public Image pictogram;
    new public TMP_Text name;
    public TMP_Text stats;
    public TMP_Text description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.LogWarning("Hovered");
        SetDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void SetDescription()
    {
        if(inventorySlotScript.item != null)
        {
            pictogram.sprite = inventorySlotScript.item.icon;
            name.text = inventorySlotScript.item.name;
            description.text = inventorySlotScript.item.description;
            stats.text = inventorySlotScript.item.stats;
        }
    }
}

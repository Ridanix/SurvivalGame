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
        SetDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pictogram.gameObject.SetActive(false);
        name.text = "Item name";
        description.text = default;
        stats.text = default;
    }

    public void SetDescription()
    {
        //int numberOfSlot;
        //if (int.TryParse(this.gameObject.name, out numberOfSlot)&&EquipmentManager.instance.currentEquipment[numberOfSlot] != null)
        //{
        //    pictogram.gameObject.SetActive(true);
        //    pictogram.sprite = EquipmentManager.instance.currentEquipment[numberOfSlot].icon;
        //    name.text = EquipmentManager.instance.currentEquipment[numberOfSlot].name;
        //    description.text = EquipmentManager.instance.currentEquipment[numberOfSlot].description;
        //    stats.text = EquipmentManager.instance.currentEquipment[numberOfSlot].stats;
        //}
        //else
        if (this.gameObject.GetComponent<InventorySlotScript>()!=null &&inventorySlotScript.item != null)
        {
            pictogram.gameObject.SetActive(true);
            pictogram.sprite = inventorySlotScript.item.icon;
            name.text = inventorySlotScript.item.name;
            description.text = inventorySlotScript.item.description;
            stats.text = inventorySlotScript.item.stats;
        }
        
    }
}

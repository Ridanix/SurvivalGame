using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventorySlotScript inventorySlotScript;

    public void Start()
    {
        Inventory.instance.onItemChangedCallback += ToltipManager.instance.HideTolTip;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Invoke("SetDescription", 2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("SetDescription");
        ToltipManager.instance.HideTolTip();
    }

    public void SetDescription()
    {
        int numberOfSlot;
        string stats = "";
        
        if (int.TryParse(this.gameObject.name, out numberOfSlot) && EquipmentManager.instance.currentEquipment[numberOfSlot] != null)
        {
            for (int i = 0; i <EquipmentManager.instance.currentEquipment[numberOfSlot].stats.Count; i++)
            {
                stats += $"{EquipmentManager.instance.currentEquipment[numberOfSlot].stats[i]}: {EquipmentManager.instance.currentEquipment[numberOfSlot].statsValues[i]}\n";
            }
            ToltipManager.instance.SetAndShowTolTip(EquipmentManager.instance.currentEquipment[numberOfSlot].name, EquipmentManager.instance.currentEquipment[numberOfSlot].description, EquipmentManager.instance.currentEquipment[numberOfSlot].icon, stats);
        }
        else if (inventorySlotScript !=null && inventorySlotScript.item != null)
        {
            Debug.LogWarning(inventorySlotScript.item.name);
            for (int i = 0; i <inventorySlotScript.item.stats.Count; i++)
            {
                stats += $"{inventorySlotScript.item.stats[i]}: {inventorySlotScript.item.statsValues[i]}\n";
            }
            ToltipManager.instance.SetAndShowTolTip(inventorySlotScript.item.name, inventorySlotScript.item.description, inventorySlotScript.item.icon, stats);
        }
    }
}

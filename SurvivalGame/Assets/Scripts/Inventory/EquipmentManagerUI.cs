using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManagerUI : MonoBehaviour
{
    public static EquipmentManagerUI instance;
    
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < System.Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            equipmentSlots[i].sprite = placeholders[i];
        }
    }

    public List<Image> equipmentSlots = new List<Image>();
    public List<Sprite> placeholders = new List<Sprite>();

    void Update()
    {
        UpdateSprites();
    }

    public void UpdateSprites()
    {
        //Equipment Slots Update
        for (int i = 0; i < System.Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            if (EquipmentManager.instance.currentEquipment[i] != null)
            {
                equipmentSlots[i].sprite = EquipmentManager.instance.currentEquipment[i].icon;
            }
            else if (EquipmentManager.instance.currentEquipment[i] == null)
            {
                equipmentSlots[i].sprite = placeholders[i];
            }
        }
    }
}

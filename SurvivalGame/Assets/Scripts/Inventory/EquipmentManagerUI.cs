using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManagerUI : MonoBehaviour
{
    public List<Image> imagesToUpdate = new List<Image>();
    
    public List<Sprite> placeholders = new List<Sprite>();


    void Update()
    {
        UpdateSprites();
    }

    public void Awake()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            imagesToUpdate[i].sprite = placeholders[i];
        }
    }

    public void UpdateSprites()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            if (EquipmentManager.instance.currentEquipment[i] != null)
            {
                imagesToUpdate[i].sprite = EquipmentManager.instance.currentEquipment[i].icon;
            }
            else if (EquipmentManager.instance.currentEquipment[i] == null)
            {
                imagesToUpdate[i].sprite = placeholders[i];
            }
        }
    }
}

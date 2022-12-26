using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManagerUI : MonoBehaviour
{
    public List<Image> imagesToUpdate = new List<Image>();
    
    public List<Image> placeholders = new List<Image>();


    void Update()
    {
        UpdateSprites();
    }

    public void UpdateSprites()
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            if (EquipmentManager.instance.currentEquipment[i] == null)
            {
                imagesToUpdate[i].sprite = null;
                imagesToUpdate[i].gameObject.SetActive(false);
                continue;
            }
            else
            {
                imagesToUpdate[i].gameObject.SetActive(true);
            }
            imagesToUpdate[i].sprite = EquipmentManager.instance.currentEquipment[i].icon;

        }
    }
}

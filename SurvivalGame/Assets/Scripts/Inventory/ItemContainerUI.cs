using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerUI : MonoBehaviour
{
    [Header("Data Reference")]
    public ItemContainer container;
    [Space]
    [Header("UI")]
    public List<ItemSlot> itemsSlots = new List<ItemSlot>();

    public void Start()
    {
        UpdateContainerUI();
    }

    public void UpdateContainerUI()
    {
        if (container != null)
        {
            if (itemsSlots.Count == container.containerSize)
            {
                for (int i = 0; i < container.containerSize; i++)
                {
                    itemsSlots[i].SetSlot(i, container);
                }
            }
            else
            {
                Debug.Log("UI is setuped wrong - Add more or less slots");
            }
        }
    }
}

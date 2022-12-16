//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class ItemContainer : MonoBehaviour
//{
//    [Header("Settings")]
//    public int containerSize = 10;
//    [HideInInspector]
//    public List<ItemStack> items = new List<ItemStack>();
//    [Space]
//    [Header("UI")]
//    public List<ItemContainerUI> containerUI = new List<ItemContainerUI>();
//    [Space]
//    [Header("Events")]
//    public UnityEvent OnIventoryChange;

//    public void Start()
//    {
//        ResetInventory();
//    }

//    public void ResetInventory()
//    {
//        items.Clear();
//        for (int i = 0; i < containerSize; i++)
//        {
//            items.Add(new ItemStack());
//        }
//        UpdateUI();
//    }

//    public void UpdateUI()
//    {
//        for (int i = 0; i < containerUI.Count; i++)
//        {
//            containerUI[i].UpdateContainerUI();
//        }
//    }

//}

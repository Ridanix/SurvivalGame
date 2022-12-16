//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UniversalHoldingItemContainer : MonoBehaviour
//{
//    public static UniversalHoldingItemContainer instance;

//    private void Awake()
//    {
//        instance = this;
//    }

//    [Header("References")]
//    public ItemSlot itemSlot;

//    public void Start()
//    {
//        itemSlot.OnSlotUpdate.AddListener(StateChange);
//    }

//    public void StateChange()
//    {
//        if (itemSlot.itemContainer.items.Count > itemSlot.index && itemSlot.index >= 0)
//        {
//            if (itemSlot.itemContainer.items[itemSlot.index].item == null || itemSlot.itemContainer.items[itemSlot.index].itemCount <= 0)
//            {
//                itemSlot.gameObject.SetActive(false);
//            }
//            else
//            {
//                itemSlot.gameObject.SetActive(true);
//            }
//        }
//    }

//    public void Update()
//    {
//        if(itemSlot.gameObject.activeSelf)
//        {
//            itemSlot.transform.position = Input.mousePosition;
//        }
//    }
//}

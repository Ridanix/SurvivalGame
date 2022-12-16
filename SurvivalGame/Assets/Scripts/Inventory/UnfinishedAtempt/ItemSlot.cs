//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using UnityEngine.EventSystems;
//using UnityEngine.Events;

//public class ItemSlot : MonoBehaviour, IPointerClickHandler
//{
//    [HideInInspector]
//    public int index = 0;
//    [Header("Setup")]
//    public ItemContainer itemContainer;
//    [Header("Settings")]
//    public bool blockItemPickup = false;
//    public bool blockItemDrop = false;
//    [Header("Filters")]
//    public bool useItemCategorieFilter = false;
//    public List<ItemCategory> itemCategoriesFilter = new List<ItemCategory>();
//    public bool useItemFilter = false;
//    public List<ItemBase> itemFilter = new List<ItemBase>();
//    [Header("UI")]
//    public Image itemIcon;
//    public TextMeshProUGUI itemStackCount;
//    [Space]
//    public Image itemSlotImage;
//    [Header("Placeholders")]
//    public Sprite placeholderIcon;
//    [Space]
//    [Header("Events")]
//    public UnityEvent OnSlotUpdate;

//    public void OnPointerClick(PointerEventData eventData)
//    {

//    }

//    public void SetSlot(int index, ItemContainer container)
//    {
//        itemContainer = container;
//        this.index = index;
//        UpdateSlot();
//    }

//    public void UpdateSlot()
//    {
//        if (itemContainer != null)
//        {
//            if (itemContainer.items.Count > index && index>=0)
//            {
//                if (itemContainer.items[index] != null)
//                {
//                    if (itemContainer.items[index].item != null)
//                    {
//                        itemIcon.color = new Color32(255, 255, 255, 255);
//                        itemIcon.gameObject.SetActive(true);
//                        itemIcon.sprite = itemContainer.items[index].item.itemIcon;

//                        if (itemContainer.items[index].item.isStackable)
//                        {
//                            itemStackCount.gameObject.SetActive(false);
//                            itemStackCount.text = itemContainer.items[index].itemCount.ToString();
//                        }
//                        else
//                        {
//                            itemStackCount.gameObject.SetActive(false);
//                        }

//                    }
//                    else
//                    {
//                        itemStackCount.gameObject.SetActive(false);
//                        if (placeholderIcon == null)
//                        {
//                            itemIcon.gameObject.SetActive(false);
//                        }
//                        else
//                        {
//                            itemIcon.color = new Color32(255, 255, 255, 100);
//                            itemIcon.sprite = placeholderIcon;
//                            itemIcon.gameObject.SetActive(true);
//                        }
//                    }
//                }
//                else
//                {
//                    itemStackCount.gameObject.SetActive(false);
//                    if(placeholderIcon == null)
//                    {
//                        itemIcon.gameObject.SetActive(false);
//                    }
//                    else
//                    {
//                        itemIcon.color = new Color32(255,255,255,100);
//                        itemIcon.sprite = placeholderIcon;
//                        itemIcon.gameObject.SetActive(true);
//                    }
//                }
//            }

//            if(OnSlotUpdate !=null)
//            {
//                OnSlotUpdate.Invoke();
//            }
//        }
//        else
//        {
//            Debug.Log("Slot Container == null");
//        }
//    }

//    public bool ItemCanUseSlot(ItemBase item)
//    {
//        if (item != null)
//        {
//            if(!useItemCategorieFilter && !useItemFilter)
//            {
//                return true;
//            }
//            else
//            {
//                if(useItemCategorieFilter && !useItemFilter)
//                {
//                    if (itemCategoriesFilter.Contains(item.itemCategory))
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }
//                else if(!useItemCategorieFilter && useItemFilter)
//                {
//                    if(itemFilter.Contains(item))
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }
//                else if (useItemCategorieFilter && useItemFilter)
//                {
//                    if (itemCategoriesFilter.Contains(item.itemCategory) && itemFilter.Contains(item))
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                }
//            }
//        }

//        return true;
//    }
//}

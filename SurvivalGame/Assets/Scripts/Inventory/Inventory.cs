using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("You Did Done Fucked Up");
            return;
        }
        instance = this;
    }

    //Inventory
    public List<ScriptableItem> itemList = new List<ScriptableItem>();
    public int inventorySpace = 30;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public bool AddItem(ScriptableItem item)
    {
        if (itemList.Count >= inventorySpace)
        {
            return false;
        }
        itemList.Add(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke(); 
        return true;
    }

    public void RemoveItem(ScriptableItem item)
    {
        itemList.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    public List<ScriptableItem> GetItemList()
    {
        return itemList;
    }



    //Hotbar
    public List<ScriptableItem> hotbarList = new List<ScriptableItem>();
    public int hotbarSpace = 8;
    
    public void RemoveItemFromHotbar(ScriptableItem item)
    {
        hotbarList.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }
    
    public bool AddItemToHotbar(ScriptableItem item)
    {
        if (hotbarList.Count >= hotbarSpace)
        {
            return false;
        }
        hotbarList.Add(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        return true;
    }

    public bool InventoryToHotbar(ScriptableItem item)
    {
        bool goOn = AddItemToHotbar(item);
        if (goOn == false)
            return false;
        itemList.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        return true;
    }

    public bool HotbarToInventory(ScriptableItem item)
    {
        bool goOn = AddItem(item);
        if (goOn == false)
            return false;
        hotbarList.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        return true;
    }
}


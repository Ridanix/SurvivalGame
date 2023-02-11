using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{

    void Awake()
    {
        instance = this;
    }

    public static Crafting instance;

    public List<ScriptableItem> onCraftingTable = new List<ScriptableItem>();
    public int CrafttableSpace = 3;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public void RemoveItemFromTable(ScriptableItem item)
    {
        instance.onCraftingTable.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    public bool AddItemToTable(ScriptableItem item)
    {
        if (instance.onCraftingTable.Count >= CrafttableSpace)
        {
            return false;
        }
        instance.onCraftingTable.Add(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        return true;
    }

    public bool InventoryToTable(ScriptableItem item)
    {
        bool goOn = AddItemToTable(item);
        if (goOn == false)
            return false;
        Inventory.instance.RemoveItem(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        return true;
    }

    public bool TableToInventory(ScriptableItem item)
    {
        bool goOn = Inventory.instance.AddItem(item);
        if (goOn == false)
            return false;
        RemoveItemFromTable(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        return true;
    }
}

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
}


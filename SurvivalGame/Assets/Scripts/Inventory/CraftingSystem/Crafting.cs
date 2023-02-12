using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Crafting : MonoBehaviour
{

    void Awake()
    {
        instance = this;
        string[] recepiesInString = File.ReadAllLines("Recepies.txt");
        for (int i = 0; i < recepiesInString.Length; i++)
        {
            string[] line = recepiesInString[i].Split('|');
            Recepie recepie = new Recepie(line[0], line[1], line[2], line[3]);
            recepies.Add(recepie);
        }
    }

    public static Crafting instance;

    public List<ScriptableItem> onCraftingTable = new List<ScriptableItem>();
    public int crafttableSpace = 3;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //CraftingData
    public List<Recepie> recepies = new List<Recepie>();
    public ScriptableItem[] allItemsWeCanCraft;

    public class Recepie
    {
        public Recepie(string ingredient1, string ingredient2, string ingredient3, string output)
        {
            this.ingredient1 = ingredient1;
            this.ingredient2 = ingredient2;
            this.ingredient3 = ingredient3;
            this.output = output;
            Debug.Log($"Recepie: {this.ingredient1} + {this.ingredient2} + {this.ingredient3} = {this.output}");
        }
        public Recepie(string ingredient1, string ingredient2, string ingredient3)
        {
            this.ingredient1 = ingredient1;
            this.ingredient2 = ingredient2;
            this.ingredient3 = ingredient3;
        }

        public string ingredient1, ingredient2, ingredient3, output;
    }
    
    public void RemoveItemFromTable(ScriptableItem item)
    {
        instance.onCraftingTable.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    public bool AddItemToTable(ScriptableItem item, bool fromRecepieFinder = false)
    {
        if (instance.onCraftingTable.Count >= crafttableSpace && fromRecepieFinder == false)
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

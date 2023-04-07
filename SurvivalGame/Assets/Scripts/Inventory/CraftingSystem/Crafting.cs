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
            Recepie recepie = new Recepie(line, line[0] + "|" +line[1] + "|" + line[2]);
            recepies.Add(recepie);
            Debug.LogWarning($"{recepie.ingridientsAndOutput[0]}+{ recepie.ingridientsAndOutput[1]} + {recepie.ingridientsAndOutput[2]} = { recepie.ingridientsAndOutput[3]}");
        }
        string[] upgradeRecepiesInString = File.ReadAllLines("UpgradeRecepies.txt");
        for (int i = 0; i < upgradeRecepiesInString.Length; i++)
        {
            string[] line = upgradeRecepiesInString[i].Split('|');
            UpgradeRecepie recepie = new UpgradeRecepie(line[0], line[1]);
            upgradeRecepies.Add(recepie);
        }

    }

    public static Crafting instance;

    public List<ScriptableItem> onCraftingTable = new List<ScriptableItem>();
    public List<ScriptableItem> onUpgradeTable = new List<ScriptableItem>();
    public int crafttableSpace = 3;
    public int upgradeableSpace = 2;

    public delegate void OnItemChanged(bool fromCraftingTable);
    public OnItemChanged onItemChangedCallback;

    //CraftingData
    public List<Recepie> recepies = new List<Recepie>();
    public List<UpgradeRecepie> upgradeRecepies = new List<UpgradeRecepie>();
    public ScriptableItem[] allItemsWeCanCraft;

    public class Recepie
    {
        public Recepie(string[] line, string noLine)
        {
            ingridientsAndOutput = line;
            noSplit = noLine;
        }
        public Recepie(string ingredient1, string ingredient2, string ingredient3)
        {
            ingridientsAndOutput = new string[3];
            ingridientsAndOutput[0] = ingredient1;
            ingridientsAndOutput[1] = ingredient2;
            ingridientsAndOutput[2] = ingredient3;
        }

        public string noSplit;
        public string[] ingridientsAndOutput;
    }

    public class UpgradeRecepie
    {
        public string equipment, material;
       
        public UpgradeRecepie(string equipment, string material)
        {
            this.equipment = equipment;
            this.material = material;
        }
    }


    public void RemoveItemFromTable(ScriptableItem item, bool fromCraftingTable)
    {
        if(fromCraftingTable) instance.onCraftingTable.Remove(item);
        else instance.onUpgradeTable.Remove(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke(fromCraftingTable);
    }

    public bool AddItemToTable(ScriptableItem item, bool fromCraftingTable, bool fromRecepieFinder = false)
    {
        if(fromCraftingTable)
        {
            if (instance.onCraftingTable.Count >= crafttableSpace && fromRecepieFinder == false)
            {
                return false;
            }
            instance.onCraftingTable.Add(item);
        }
        else
        {
            if (instance.onUpgradeTable.Count >= upgradeableSpace && fromRecepieFinder == false)
            {
                return false;
            }
            instance.onUpgradeTable.Add(item);
        }
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke(fromCraftingTable);
        return true;
    }

    public bool InventoryToTable(ScriptableItem item, bool fromCraftingTable)
    {
        bool goOn = AddItemToTable(item,fromCraftingTable);
        if (goOn == false)
            return false;
        Inventory.instance.RemoveItem(item);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke(fromCraftingTable);
        return true;
    }

    public bool TableToInventory(ScriptableItem item, bool fromCraftingTable)
    {
        bool goOn = Inventory.instance.AddItem(item);
        if (goOn == false)
            return false;
        RemoveItemFromTable(item,fromCraftingTable);
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke(fromCraftingTable);
        return true;
    }
}

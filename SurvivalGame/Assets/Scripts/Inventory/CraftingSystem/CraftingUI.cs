using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{

    public Transform itemsParentCraftingSlots;
    public Transform itemsParentUpgradeSlots;
    public CraftingSlot[] craftingSlots;
    public CraftingSlot[] upgradeSlots;
    
    public Crafting crafting;
    
    private void Start()
    {
        crafting = Crafting.instance;
        crafting.onItemChangedCallback += ItemPlacedInCrafting;
    }

    private void Awake()
    {
        craftingSlots = itemsParentCraftingSlots.GetComponentsInChildren<CraftingSlot>();
        upgradeSlots = itemsParentUpgradeSlots.GetComponentsInChildren<CraftingSlot>();
    }

    public void ItemPlacedInCrafting(bool fromCraftingTable)
    {
        if (fromCraftingTable)
        {
            for (int i = 0; i < craftingSlots.Length; i++)
            {
                if (i < Crafting.instance.onCraftingTable.Count)
                {
                    craftingSlots[i].AddItem(Crafting.instance.onCraftingTable[i]);
                }
                else
                {
                    craftingSlots[i].ClearSlot();
                }
            }
            if (craftingSlots[0].item != null && craftingSlots[1].item != null && craftingSlots[2].item != null)
            {
                Crafting.Recepie recepie = new Crafting.Recepie(Crafting.instance.onCraftingTable[0].name, Crafting.instance.onCraftingTable[1].name, Crafting.instance.onCraftingTable[2].name);

                for (int i = 0; i < Crafting.instance.recepies.Count; i++)
                {
                    if (craftingSlots[3].item == null)
                    {
                        string weaAreCheking = Crafting.instance.recepies[i].noSplit;
                        if (weaAreCheking.Contains(recepie.ingridientsAndOutput[0]))
                        {
                            weaAreCheking = ReplaceFirst(weaAreCheking, recepie.ingridientsAndOutput[0], "x");
                            if (weaAreCheking.Contains(recepie.ingridientsAndOutput[1]))
                            {
                                weaAreCheking = ReplaceFirst(weaAreCheking, recepie.ingridientsAndOutput[1], "x");
                                if (weaAreCheking.Contains(recepie.ingridientsAndOutput[2]))
                                {
                                    for (int j = 0; j < Crafting.instance.allItemsWeCanCraft.Length; j++)
                                    {
                                        if (Crafting.instance.recepies[i].ingridientsAndOutput[3] == Crafting.instance.allItemsWeCanCraft[j].name)
                                        {
                                            crafting.AddItemToTable(crafting.allItemsWeCanCraft[j], true, true);
                                            craftingSlots[3].AddItem(crafting.onCraftingTable[3]);
                                            crafting.onCraftingTable[3].playerWhoUse = crafting.gameObject.GetComponent<Player_Data>();
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

            }
        }
        else
        {
            for (int i = 0; i < upgradeSlots.Length; i++)
            {
                if (i < Crafting.instance.onUpgradeTable.Count)
                {
                    upgradeSlots[i].AddItem(Crafting.instance.onUpgradeTable[i]);
                }
                else
                {
                    upgradeSlots[i].ClearSlot();
                }
            }
            
            if (upgradeSlots[0].item != null && upgradeSlots[1].item != null)
            {
                Crafting.UpgradeRecepie recepie = new Crafting.UpgradeRecepie(Crafting.instance.onUpgradeTable[0].name, Crafting.instance.onUpgradeTable[1].name);

                for (int i = 0; i < Crafting.instance.upgradeRecepies.Count; i++)
                {
                    if (Crafting.instance.upgradeRecepies[i].equipment == recepie.equipment && Crafting.instance.upgradeRecepies[i].material == recepie.material && upgradeSlots[2].item == null)
                    {
                        Debug.LogWarning(Crafting.instance.onUpgradeTable[0].name);
                        ScriptableItem itemNew = Instantiate(Crafting.instance.onUpgradeTable[0]);
                        itemNew.upgradeMade+=1;
                        if(itemNew.upgradeMade < 5)
                        {
                            if (itemNew.upgradeMade == 1)
                                itemNew.name += "(1)";
                            else
                            {
                                string[] pseudoName = itemNew.name.Split(' ');
                                pseudoName[pseudoName.Length-1] = $"({itemNew.upgradeMade})";
                                itemNew.name = "";
                                for (int k = 0; k < pseudoName.Length; k++)
                                {
                                    itemNew.name+= pseudoName[k];
                                }
                                itemNew.name.Trim(' ');
                            }
                            for (int j = 0; j < itemNew.statsValues.Count; j++)
                            {
                                itemNew.statsValues[j] =Crafting.instance.onUpgradeTable[0].statsValues[j]* 1.2f;
                            }
                            crafting.AddItemToTable(itemNew, false, true);
                            break;
                        }
                        else
                        {
                            Debug.LogWarning("To much upgrades");
                        }
                    }
                }
            }
        }
    }

    public static string ReplaceFirst(string originalString, string substringToReplace, string replacementSubstring)
    {
        int index = originalString.IndexOf(substringToReplace);
        if (index != -1)
        {
            return originalString.Substring(0, index) + replacementSubstring + originalString.Substring(index + substringToReplace.Length);
        }
        else
        {
            return originalString;
        }
    }
}

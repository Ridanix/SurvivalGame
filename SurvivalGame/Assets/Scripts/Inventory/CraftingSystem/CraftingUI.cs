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
                    if (Crafting.instance.recepies[i].ingredient1 == recepie.ingredient1 && Crafting.instance.recepies[i].ingredient2 == recepie.ingredient2 && Crafting.instance.recepies[i].ingredient3 == recepie.ingredient3 && craftingSlots[3].item == null)
                    {
                        for (int j = 0; j < Crafting.instance.allItemsWeCanCraft.Length; j++)
                        {
                            if (Crafting.instance.recepies[i].output == Crafting.instance.allItemsWeCanCraft[j].name)
                            {
                                crafting.AddItemToTable(crafting.allItemsWeCanCraft[j], true, true);
                                craftingSlots[3].AddItem(crafting.onCraftingTable[3]);
                                break;
                            }
                        }
                        break;
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
                Debug.LogWarning("Atempt to Upgrade");
                Crafting.UpgradeRecepie recepie = new Crafting.UpgradeRecepie(Crafting.instance.onUpgradeTable[0].name, Crafting.instance.onUpgradeTable[1].name);

                for (int i = 0; i < Crafting.instance.upgradeRecepies.Count; i++)
                {
                    if (Crafting.instance.upgradeRecepies[i].equipment == recepie.equipment && Crafting.instance.upgradeRecepies[i].material == recepie.material && upgradeSlots[2].item == null)
                    {
                        Debug.LogWarning(Crafting.instance.onUpgradeTable[0].name);
                        ScriptableItem itemNew = Crafting.instance.onUpgradeTable[0];
                        Debug.LogWarning(itemNew.name);
                        
                        for (int j = 0; j < itemNew.statsValues.Count; j++)
                        {
                            itemNew.statsValues[j] =Crafting.instance.onUpgradeTable[0].statsValues[j]* 2;
                            //TADY KONCIL SPUTNIK---------------------------------------------------------------------------------------------------------------
                        }
                         Debug.LogWarning("Found");
                         crafting.AddItemToTable(itemNew, false , true);
                         break;
                    }
                }
            }
        }
    }


}

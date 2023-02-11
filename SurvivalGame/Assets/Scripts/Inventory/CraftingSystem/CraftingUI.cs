using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{
    public Transform itemsParentCraftingSlots;
    public Transform itemsParentCraftingInventory;

    public CraftingSlot[] craftingSlots;
    public InventorySlotScript[] inventoryScripts;


    private void Awake()
    {
        craftingSlots = itemsParentCraftingInventory.GetComponentsInChildren<CraftingSlot>();
        //Inventory.instance.onItemChangedCallback += ItemPlacedInCrafting();

    }

    public void ItemPlacedInCrafting()
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

        Crafting.Recepie recepie = new Crafting.Recepie(Crafting.instance.onCraftingTable[0].name, Crafting.instance.onCraftingTable[1].name, Crafting.instance.onCraftingTable[2].name);
        
        for (int i = 0; i < Crafting.instance.recepies.Count; i++)
        {
            if(Crafting.instance.recepies[i].ingredient1 == recepie.ingredient1 && Crafting.instance.recepies[i].ingredient2 == recepie.ingredient2 && Crafting.instance.recepies[i].ingredient3 == recepie.ingredient3)
            {
                for (int j = 0; j < Crafting.instance.allItemsWeCanCraft.Length; j++)
                {
                    if(Crafting.instance.recepies[i].output == Crafting.instance.allItemsWeCanCraft[j].name)
                    {
                        Crafting.instance.onCraftingTable[0] = Crafting.instance.allItemsWeCanCraft[j];
                        break;
                    }
                }
                break;
            }
        }
    }


}

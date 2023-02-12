using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour
{

    public Transform itemsParentCraftingSlots;
    public CraftingSlot[] craftingSlots;
    
    public Crafting crafting;
    
    private void Start()
    {
        crafting = Crafting.instance;
        crafting.onItemChangedCallback += ItemPlacedInCrafting;
    }

    private void Awake()
    {
        craftingSlots = itemsParentCraftingSlots.GetComponentsInChildren<CraftingSlot>();
    }

    public void ItemPlacedInCrafting()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            if (i < Crafting.instance.onCraftingTable.Count)
            {
                craftingSlots[i].AddItem(Crafting.instance.onCraftingTable[i]);
                Debug.LogWarning($"item {i}: {crafting.onCraftingTable[i].name}");
            }
            else
            {
                craftingSlots[i].ClearSlot();
            }
        }

        if (craftingSlots[0].item != null && craftingSlots[1].item != null && craftingSlots[2].item != null)
        {
            Debug.LogWarning("Atempt to craft");
            Crafting.Recepie recepie = new Crafting.Recepie(Crafting.instance.onCraftingTable[0].name, Crafting.instance.onCraftingTable[1].name, Crafting.instance.onCraftingTable[2].name);

            for (int i = 0; i < Crafting.instance.recepies.Count; i++)
            {
                if (Crafting.instance.recepies[i].ingredient1 == recepie.ingredient1 && Crafting.instance.recepies[i].ingredient2 == recepie.ingredient2 && Crafting.instance.recepies[i].ingredient3 == recepie.ingredient3 && craftingSlots[3].item == null)
                {
                    for (int j = 0; j < Crafting.instance.allItemsWeCanCraft.Length; j++)
                    {
                        if (Crafting.instance.recepies[i].output == Crafting.instance.allItemsWeCanCraft[j].name)
                        {
                            Debug.LogWarning("Found");
                            crafting.AddItemToTable(crafting.allItemsWeCanCraft[j], true );
                            craftingSlots[3].AddItem(crafting.onCraftingTable[3]);
                            break;
                        }
                    }
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Not enough items");
        }



    }


}

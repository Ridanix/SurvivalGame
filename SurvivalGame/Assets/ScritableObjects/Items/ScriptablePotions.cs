using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class ScriptablePotions : ScriptableItem
{

    public int healthModifier;
    public int manaModifier;

    public override void UseItem()
    {
        base.UseItem();
        DrinkPotion();
    }

    public void DrinkPotion()
    {
        playerWhoUse.ManaManipulaton(manaModifier);
        playerWhoUse.HealOrDamage(healthModifier);
        RemoveItemFromInventory();
    }
}

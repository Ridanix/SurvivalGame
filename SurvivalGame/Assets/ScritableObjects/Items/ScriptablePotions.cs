using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class ScriptablePotions : ScriptableItem
{

    public override void UseItem(string whereFromDoYouUseIt)
    {
        base.UseItem(whereFromDoYouUseIt);
        DrinkPotion(whereFromDoYouUseIt);
    }

    public void DrinkPotion(string whereFromDoYouUseIt)
    {
        if (stats.Contains("Hunger"))
        {
            playerWhoUse.HungerManipulation(statsValues[stats.IndexOf("Hunger")]);
            playerWhoUse.HealOrDamage(statsValues[stats.IndexOf("Health")]);
        }
        else
        {
            playerWhoUse.ManaManipulaton(statsValues[stats.IndexOf("Mana")]);
            playerWhoUse.HealOrDamage(statsValues[stats.IndexOf("Health")]);
        }
        RemoveItemFromInventory(whereFromDoYouUseIt);
    }
}

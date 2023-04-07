using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class ScriptableEquipment : ScriptableItem
{
    
    public override void UseItem(string whereFromDoYouUseIt)
    {
        base.UseItem(whereFromDoYouUseIt);
        EquipmentManager.instance.EquipItem(this, whereFromDoYouUseIt);
        RemoveItemFromInventory(whereFromDoYouUseIt);
    }
}

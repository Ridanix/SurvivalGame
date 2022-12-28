using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class ScriptableEquipment : ScriptableItem
{
    public int damageModifier;

    public override void UseItem()
    {
        base.UseItem();
        EquipmentManager.instance.EquipItem(this);
        RemoveItemFromInventory();
    }
}

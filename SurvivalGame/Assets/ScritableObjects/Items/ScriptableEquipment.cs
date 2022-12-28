using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class ScriptableEquipment : ScriptableItem
{
    public int damageModifier;
    public EquipmentSlot equip;

    public override void UseItem()
    {
        base.UseItem();
        EquipmentManager.instance.EquipItem(this);
        RemoveItemFromInventory();
    }
}
public enum EquipmentSlot { Head, Chest, Legs, Feet, Weapon}

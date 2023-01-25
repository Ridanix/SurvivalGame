using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class ScriptableEquipment : ScriptableItem
{
    public int damageModifier;
    public EquipmentSlot equip;
    public WeaponType damageType;
    
    public override void UseItem(string whereFromDoYouUseIt)
    {
        base.UseItem(whereFromDoYouUseIt);
        EquipmentManager.instance.EquipItem(this, whereFromDoYouUseIt);
        RemoveItemFromInventory(whereFromDoYouUseIt);
    }
}
public enum EquipmentSlot { Head, Chest, Legs, Feet, Weapon }
public enum WeaponType { axe, pickaxe, slashing }

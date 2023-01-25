using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    private void Awake()
    {
        instance = this;
    }

    public ScriptableEquipment[] currentEquipment;

    Inventory inventory;

    public delegate void OnEquipmentChanged(ScriptableEquipment newItem, ScriptableEquipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new ScriptableEquipment[numSlots];
    }

    public void EquipItem(ScriptableEquipment newItem,/*NEW*/ string whereUsed = "Hotbar")
    {
        int slotIndex = (int)newItem.equip;
        ScriptableEquipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            /*NEW*/
            if (whereUsed == "Inventory")
            {
                inventory.AddItem(oldItem);
            }
            else if (whereUsed == "Hotbar")
            {
                inventory.AddItemToHotbar(oldItem);
            }
        }

        currentEquipment[slotIndex] = newItem;

        Player_Controller.vulterablity = newItem.damageType;
        Player_Controller.attackDmg = newItem.damageModifier;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
    }

    public void UnequipItem(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            ScriptableEquipment oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAllItems()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            UnequipItem(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) UnequipAllItems();
    }
}

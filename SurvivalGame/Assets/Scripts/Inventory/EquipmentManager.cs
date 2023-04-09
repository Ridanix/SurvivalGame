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
    public List<GameObject> currentMeshes;
    public GameObject parentOfArmor;

    Inventory inventory;

    public delegate void OnEquipmentChanged(ScriptableEquipment newItem, ScriptableEquipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        currentMeshes = GetChildObjects(parentOfArmor);
        
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        currentEquipment = new ScriptableEquipment[numSlots];
    }

    public void EquipItem(ScriptableEquipment newItem, string whereUsed = "Hotbar")
    {
        int slotIndex = (int)newItem.equip;
        ScriptableEquipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
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

        if (slotIndex == 4)
        {
            Player_Controller.attackDmg = newItem.statsValues[newItem.stats.IndexOf("Damage")];
        }
        else
        {
            Player_Controller.armorPoints += newItem.statsValues[newItem.stats.IndexOf("Defense")];
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        if (newItem.mesh.Length != 0)
        {
            for (int i = 0; i < newItem.mesh.Length; i++)
            {
                for (int j = 0; j < currentMeshes.Count; j++)
                {
                    if(currentMeshes[j].name == newItem.mesh[i].name)
                    {
                        Debug.Log("found");
                        currentMeshes[j].SetActive(true);
                    }
                }
            }
        }
    }

    public void UnequipItem(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            ScriptableEquipment oldItem = currentEquipment[slotIndex];

            if(slotIndex != 4)
            {
                Player_Controller.armorPoints -= oldItem.statsValues[oldItem.stats.IndexOf("Defense")];
            }
            inventory.AddItem(oldItem);
            currentEquipment[slotIndex] = null;
            if(oldItem.mesh != null)
            {
                foreach (GameObject g in oldItem.mesh)
                {
                    for(int i = 0; i < currentMeshes.Count; i++)
                    {
                        if (g.name == currentMeshes[i].name)
                        {
                            currentMeshes[i].SetActive(false);
                        }
                    }
                }
            }


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

    public List<GameObject> GetChildObjects(GameObject parent)
    {
        int childCount = parent.transform.childCount;
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            children.Add(parent.transform.GetChild(i).gameObject);
            Debug.Log(children[i]);
        }
        return children;
    }
}

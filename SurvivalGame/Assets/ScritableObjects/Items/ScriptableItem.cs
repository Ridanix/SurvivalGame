using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ScriptableItem : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public string description;

    public List<string> stats = new List<string>();
    public List<int> statsValues = new List<int>();


    public Player_Data playerWhoUse;
    
    public int amount = 1;

    public virtual void UseItem(string whereFromDoYouUseIt)
    {
        Debug.Log("You used " + name + "from: " + whereFromDoYouUseIt);
    }

    public void RemoveItemFromInventory(string whereFromDoYouUseIt)
    {
        if(whereFromDoYouUseIt == "Inventory")
            Inventory.instance.RemoveItem(this);
        if (whereFromDoYouUseIt == "Hotbar")
            Inventory.instance.RemoveItemFromHotbar(this);
    }
}


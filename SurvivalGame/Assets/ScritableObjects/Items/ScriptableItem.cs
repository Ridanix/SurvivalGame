using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ScriptableItem : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    
    public virtual void UseItem()
    {
        Debug.Log("You used " + name);
    }

    public void RemoveItemFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }
}

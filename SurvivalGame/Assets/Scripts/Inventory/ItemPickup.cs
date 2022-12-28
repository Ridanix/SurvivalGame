using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ScriptableItem item;
    
    private void OnCollisionEnter(Collision pickUpCollision)
    {
        if (pickUpCollision.gameObject.tag == "Player")
        {
            PickUp();
            item.playerWhoUse = pickUpCollision.gameObject.GetComponent<Player_Data>();
        }
    }
    void PickUp()
    {
        Debug.Log("Picking Up " + item.name); ;
        bool wasPickedUp = Inventory.instance.AddItem(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        
    }
}

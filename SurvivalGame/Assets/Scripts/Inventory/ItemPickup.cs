using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public ScriptableItem item;
    PickUpTextParent pickUpTextParent;

    private void OnCollisionEnter(Collision pickUpCollision)
    {
        if (pickUpCollision.gameObject.tag == "Player")
        {
            PickUp();
        }

        item.playerWhoUse = pickUpCollision.gameObject.GetComponent<Player_Data>();
    }

    private void OnCollisionExit(Collision pickUpCollision)
    {
        if (pickUpCollision.gameObject.tag == "Player")
        { 
        
        }
    }

    void PickUp()
    {
        //Debug.Log("Picking Up " + item.name);
        
        bool wasPickedUp = true;
        if (item.name == "Coins")
        {
            System.Random rnd = new System.Random();
            item.amount = rnd.Next(0, 10);
            GameObject.Find("PlayerPrefab").GetComponent<Player_Data>().WealthManipulation(item.amount);
        }
        else
           wasPickedUp = Inventory.instance.AddItem(item);

        pickUpTextParent = GameObject.Find("PickUpTextParent").GetComponent<PickUpTextParent>();
        pickUpTextParent.itemstoDisplay.Add(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        
    }
}

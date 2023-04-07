using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public ScriptableItem item;
    PickUpTextParent pickUpTextParent;
    public float pickUpTime = 0f;
    public string pickUpText = "";
    public bool isNearPlayer = false;

    public void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.E) && PickUpHintScript.instance.main.activeInHierarchy)
        {
            if(isNearPlayer)
            {
                PickUpHintScript.instance.fillAmount += 1f/ (pickUpTime*10);
                if (PickUpHintScript.instance.fillAmount > 1f)
                {
                    PickUp();
                    PickUpHintScript.instance.fillAmount = 0f;
                }
            }
        }
        else
        {
            PickUpHintScript.instance.fillAmount = 0f;
        }
    }

    private void OnTriggerEnter(Collider pickUpCollision)
    {
        if (pickUpCollision.gameObject.tag == "Player")
        {
            PickUpHintScript.instance.Show(pickUpText);
            item.playerWhoUse = GameObject.Find("PlayerPrefab").gameObject.GetComponent<Player_Data>();
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider pickUpCollision)
    {
        if (pickUpCollision.gameObject.tag == "Player")
        {
            PickUpHintScript.instance.Hide();
            isNearPlayer = false;
        }
    }

    void PickUp()
    {
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

            PickUpHintScript.instance.Hide();
            isNearPlayer = false;
            Destroy(gameObject);
        }
    }
}

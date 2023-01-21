using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public ScriptableItem item;
    PickUpTextParent pickUpTextParent;
    [SerializeField] GameObject pickUpLoader;
    PickUpHintScript pickUpHintScript;

    private void Awake()
    {
        pickUpLoader.gameObject.SetActive(false);
        pickUpHintScript = pickUpLoader.GetComponent<PickUpHintScript>();
    }

    public void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.E) && pickUpLoader.gameObject.activeInHierarchy)
        {
            pickUpHintScript.fillAmount += 0.05f;
            if(pickUpHintScript.fillAmount > 1)
            {
                PickUp();
            }
        }
        else
        {
            pickUpHintScript.fillAmount = 0f;
        }
    }

    private void OnTriggerEnter(Collider pickUpCollision)
    {
        if (pickUpCollision.gameObject.tag == "Player")
        {
            pickUpLoader.SetActive(true);
            item.playerWhoUse = GameObject.Find("PlayerPrefab").gameObject.GetComponent<Player_Data>();
        }
    }

    private void OnTriggerExit(Collider pickUpCollision)
    {
        if (pickUpCollision.gameObject.tag == "Player")
        {
            pickUpLoader.SetActive(false);

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

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
    public static GameObject pickUpLoader;
    PickUpHintScript pickUpHintScript;

    private void Awake()
    {
        /*pickUpLoader = GameObject.Find("ActionProgressLoader");
        pickUpLoader.gameObject.SetActive(false);
        pickUpHintScript = pickUpLoader.GetComponent<PickUpHintScript>();*/
    }

    public void FixedUpdate()
    {
        //if(Input.GetKey(KeyCode.E) && pickUpLoader.gameObject)
        //if(Input.GetKey(KeyCode.E) && pickUpLoader.gameObject.activeInHierarchy)
        if(Input.GetKey(KeyCode.E))
        {
            //pickUpHintScript.fillAmount += 0.05f;
            /*if(pickUpHintScript.fillAmount > pickUpTime)
            {
            }*/
            PickUp();
        }
        /*else
        {
            pickUpHintScript.fillAmount = 0f;
        }*/
    }

    private void OnTriggerEnter(Collider pickUpCollision)
    {
        if (pickUpCollision.gameObject.tag == "Player")
        {
            //pickUpLoader.SetActive(true);
            //TMP_Text text = pickUpLoader.GetComponentInChildren<TMP_Text>();
            //text.text = pickUpText;
            item.playerWhoUse = GameObject.Find("PlayerPrefab").gameObject.GetComponent<Player_Data>();
        }
    }

    private void OnTriggerExit(Collider pickUpCollision)
    {
        /*if (pickUpCollision.gameObject.tag == "Player")
        {
            pickUpLoader.SetActive(false);
        }*/
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
            Destroy(gameObject);
        }
        
    }
}

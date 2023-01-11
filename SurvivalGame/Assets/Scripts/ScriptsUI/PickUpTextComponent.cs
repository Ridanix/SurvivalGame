using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpTextComponent : MonoBehaviour
{
    [SerializeField] TMP_Text pickUpText;
    public ScriptableItem item;
    public static PickUpTextParent parent;

    public void Awake()
    {
        Invoke("DisplayText", 0.1f);
        Invoke("Die", 5f);
    }

    public void DisplayText()
    {
        pickUpText.text = $"You've picked up {item.name} ({item.amount})";
    }

    public void Die()
    {
        parent.itemstoDisplay.Remove(item);
        PickUpTextParent.generatedInstances--;
        Destroy(this.gameObject);
    }
}

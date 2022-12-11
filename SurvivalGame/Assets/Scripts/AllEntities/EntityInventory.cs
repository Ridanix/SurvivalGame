using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EntityInventory : MonoBehaviour
{
    //Player Stats
    public float health = 100f;
    public float maxHealth = 100f;
    public float stamina = 100f;
    public float maxStamina = 100f;

    //Chosen Item
    public Transform currentObject;
    public GameObject[] slots;
    public GameObject chosenItemFrame;

    public void CheckStats()
    {
        if (stamina > maxStamina)
            stamina = maxStamina;
        else if (stamina < 0)
            stamina = 0;
        if (health > maxHealth)
            health = maxHealth;
        //else if (health < 0)
        //Die From cringe
    }

    //Možný pozdìjší health
    /*public void TakeDamage(float value)
    {
        health -= value;
    }*/
}

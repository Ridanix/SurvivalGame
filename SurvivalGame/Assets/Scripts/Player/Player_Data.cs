using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class Player_Data : NetworkBehaviour
{
    public Slider healthBar;
    [SerializeField]
    public NetworkVariable<float> networkPlayerHealth = new NetworkVariable<float>(100);
    public float health;
    public float maxHealth = 100f;
    public Slider staminaBar;
    public float stamina;
    public float maxStamina = 100f;
    public float healthSendTime = 0f;

    //Chosen Item
    public Transform currentObject;
    public GameObject[] slots;
    public GameObject chosenItemFrame;

    private void Awake()
    {
        currentObject = slots[0].transform;
        health = maxHealth;
        stamina = maxStamina;
        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;
    }

    private void FixedUpdate()
    {
        healthSendTime--;
        healthBar.value = health;
        staminaBar.value = stamina;  
        Round();
        chosenItemFrame.transform.position = currentObject.transform.position;

        if (healthSendTime < 1f)
        {
            healthSendTime = 50;
            SendClientHealthServerRpc();
        }

    }

    private void Round()
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

    [ServerRpc]
    private void SendClientHealthServerRpc()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class Player_Data : NetworkBehaviour
{
    public Slider health_bar;
    [SerializeField]
    public NetworkVariable<float> networkPlayerHealth = new NetworkVariable<float>(100);
    public float health;
    public float maxHealth = 100f;
    public Slider stamina_bar;
    public float stamina;
    public float maxStamina = 100f;
    public float healthSendTime = 0f;

    //Chosen Item
    public Transform current_object;
    public GameObject[] slots;
    public GameObject chosen_item_frame;

    private void Awake()
    {
        current_object = slots[0].transform;
        health = maxHealth;
        stamina = maxStamina;
        health_bar.maxValue = maxHealth;
        stamina_bar.maxValue = maxStamina;
    }

    private void FixedUpdate()
    {
        healthSendTime--;
        health_bar.value = health;
        stamina_bar.value = stamina;  
        Round();
        chosen_item_frame.transform.position = current_object.transform.position;

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
    public void TakeDamage(float value)
    {
        health -= value;
    }

    [ServerRpc]
    private void SendClientHealthServerRpc()
    {

    }

}
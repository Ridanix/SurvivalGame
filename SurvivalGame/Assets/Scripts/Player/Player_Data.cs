using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class Player_Data : EntityInventory
{
    public Slider healthBar;
    public Slider staminaBar;
    [SerializeField]
    public NetworkVariable<float> networkPlayerHealth = new NetworkVariable<float>(100);
    public float healthSendTime = 0f;

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
        CheckStats();
        chosenItemFrame.transform.position = currentObject.transform.position;

        if (healthSendTime < 1f)
        {
            healthSendTime = 50;
            SendClientHealthServerRpc();
        }

    }

    [ServerRpc]
    private void SendClientHealthServerRpc()
    {

    }

}

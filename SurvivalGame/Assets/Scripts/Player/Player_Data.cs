using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Data : EntityInventory
{
    public PlayerHealthBar playerHealthBar;
    public Slider healthBar;
    public Slider staminaBar;
    
    private void Awake()
    {
        health = 100;
        stamina = maxStamina;
        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;
    }

    private void Update()
    {
        healthBar.value = health;
        staminaBar.value = stamina;
    }


}

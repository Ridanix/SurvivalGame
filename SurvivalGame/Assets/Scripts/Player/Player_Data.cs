using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Data : EntityInventory
{
    public Slider healthBar;
    public Slider staminaBar;
    [SerializeField]
    
    

    private void Awake()
    {
        currentObject = slots[0].transform;
        health = maxHealth;
        stamina = maxStamina;
        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;
    }

    private void Update()
    {
       
        healthBar.value = health;
        staminaBar.value = stamina;
        CheckStats();
        chosenItemFrame.transform.position = currentObject.transform.position;

       

    }


}

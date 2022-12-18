using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Data : EntityInventory
{
    public PlayerHealthBar playerHealthBar;
    public Slider healthBar;
    public Slider staminaBar;
    public GameObject spathaGameObject;
    public GameObject lumberAxeGameObject;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    
    private void Awake()
    {
        health = 100;
        stamina = maxStamina;
        healthBar.maxValue = maxHealth;
        staminaBar.maxValue = maxStamina;
    }
    void OnEquipmentChanged(ScriptableEquipment newItem, ScriptableEquipment oldItem)
    {
        if (newItem != null)
        {
            if (newItem.name == "Spatha")
            {
                
                spathaGameObject.SetActive(true);
                lumberAxeGameObject.SetActive(false);
               
            }
        }
        if (newItem != null)
        {
            if (newItem.name == "Lumber Axe")
            {

                spathaGameObject.SetActive(false);
                lumberAxeGameObject.SetActive(true);

            }
        }
        if (newItem == null)
        {
            spathaGameObject.SetActive(false);
            lumberAxeGameObject.SetActive(false);
        }
    }
    private void Update()
    {
        healthBar.value = health;
        staminaBar.value = stamina;
    }


}

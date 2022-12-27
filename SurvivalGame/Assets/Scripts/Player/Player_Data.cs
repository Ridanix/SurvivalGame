using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Data : EntityInventory
{
    //public PlayerHealthBar playerHealthBar;
    public Slider[] healthBars;
    public Slider[] staminaBars;
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
        SetBars();
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

    public void UpateBars()
    {
        foreach(Slider s in healthBars)
        {
            if (s != null)
                s.value = health;
        }
        foreach (Slider s in staminaBars)
        {
            if (s != null)
                s.value = stamina;
        }
    }

    public void SetBars()
    {
        foreach (Slider s in healthBars)
        {
            if(s != null)
                s.maxValue = maxHealth;
        }
        foreach (Slider s in staminaBars)
        {
            if (s != null)
                s.maxValue = maxStamina;
        }
    }

    private void Update()
    {
        CheckStats();
        UpateBars();
    }


}

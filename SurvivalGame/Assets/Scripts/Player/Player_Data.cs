using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Data : EntityInventory
{
    //public PlayerHealthBar playerHealthBar;
    
    //Stats References
    public Slider[] healthBars;
    public TMP_Text[] healthTexts;

    public Slider[] staminaBars;
    public TMP_Text[] staminaTexts;

    public Slider[] manaBars;
    public TMP_Text[] manaTexts;

    //Items
    public GameObject spathaGameObject;
    public GameObject lumberAxeGameObject;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void Awake()
    {
        health = maxHealth;
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
        for (int s = 0; s < healthBars.Length; s++)
        {
            if (healthBars[s] != null)
            {
                healthBars[s].value = health;
                if (healthTexts[s] != null)
                    healthTexts[s].text = $"{health}";
            }
        }
        for (int s = 0; s < staminaBars.Length; s++)
        {
            if (staminaBars[s] != null)
            {
                staminaBars[s].value = stamina;
                if (staminaTexts[s] != null)
                    staminaTexts[s].text = $"{stamina}";
            }
        }
        for (int s = 0; s < manaBars.Length; s++)
        {
            if (manaBars[s] != null)
            {
                manaBars[s].value = mana;
                if (manaTexts[s] != null)
                    manaTexts[s].text = $"{mana}";
            }
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
        foreach (Slider s in manaBars)
        {
            if (s != null)
                s.maxValue = maxMana;
        }
    }

    private void Update()
    {
        CheckStats();
        UpateBars();
    }


}

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

    //Other Stats References
    public TMP_Text wealthText;

    //Items
    public GameObject spathaGameObject;
    public GameObject lumberAxeGameObject;
    public GameObject pickAxeGameObject;
    public GameObject[] armor;
    
    void Start()
    {
       
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void Awake()
    {
        int playerClassInt = PlayerPrefs.GetInt("selectedClassInt");
        switch (playerClassInt)
        {
            default:
            case 0:
                maxHealth = 200;
                break;
            case 1:
                maxStamina = 400;
                break;
            case 2:
                maxMana = 500;
                break;

        }
        health = maxHealth;
        stamina = maxStamina;
        mana = maxMana;
        SetBars();
    }

    void OnEquipmentChanged(ScriptableEquipment newItem, ScriptableEquipment oldItem)
    {
        if (newItem != null)
        {

            lumberAxeGameObject.SetActive(false);
            spathaGameObject.SetActive(false);
            spathaGameObject.SetActive(false);

            switch (newItem.name)
            {
                default:
                case "Spatha":
                    spathaGameObject.SetActive(true);
                    break;
                case "Lumber Axe":
                    lumberAxeGameObject.SetActive(true);
                    break;
                case "Pickaxe":
                    pickAxeGameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            spathaGameObject.SetActive(false);
            lumberAxeGameObject.SetActive(false);
            pickAxeGameObject.SetActive(false);
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

    public void UpdateStats()
    {
        wealthText.text = wealth.ToString();
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
        UpdateStats();
        if (gameObject.transform.position.y > 11.3)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 11, gameObject.transform.position.z);
        }
        //Teleport Back to ground
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector3 vec = transform.position;
            vec.y = 11f;
            transform.position = vec;
        }
    }


}

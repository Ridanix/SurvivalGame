using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;


public class EntityInventory : MonoBehaviour
{
    //Player Stats
    public float health = 100f;
    public float maxHealth = 100f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float hunger = 0f;
    public float maxHunger = 100f;
    public float mana = 100f;
    public float maxMana = 100f;
    public PlayerAbilityShield playerAbilityShield;

    //Other Player Stats
    public float wealth = 0;

    //RestartGame
    bool restartGame = false;

    public void CheckStats()
    {
        if (stamina > maxStamina)
            stamina = maxStamina;
        else if (stamina < 0) stamina = 0;

        if (mana > maxMana)
        {
            //Debug.Log($"Mana burn damage: {mana - maxMana}");
            HealOrDamage(maxMana - mana);
            mana = maxMana;
        }
        else if (mana < 0)
        {
            //Debug.Log($"Mana fatique damage: {-1*mana}");
            HealOrDamage(mana);
            mana = 0;
        }

        if (health > maxHealth && playerAbilityShield.playerAbilityShieldIsActive == false) health = maxHealth;
        else if (health <= 0)
        {
            health = 0;
            if(!restartGame) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            restartGame = true;
        }

        
    }

    public void WealthManipulation(float value)
    {
        wealth += value;
    }

    public void HealOrDamage(float value)
    {
        health += value;
    }

    public void ManaManipulaton(float value)
    {
        mana += value;
    }

    public void HungerManipulation(float value)
    {
        hunger += value;
    }
}

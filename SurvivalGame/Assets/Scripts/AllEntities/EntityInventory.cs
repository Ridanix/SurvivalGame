using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EntityInventory : MonoBehaviour
{
    //Player Stats
    public float health = 50f;
    public float maxHealth = 100f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float mana = 100f;
    public float maxMana = 100f;

    //Other Player Stats
    public float wealth = 0;

    public void CheckStats()
    {
        if (stamina > maxStamina)
            stamina = maxStamina;
        else if (stamina < 0)
            stamina = 0;
        if (mana > maxMana)
        {
            //Debug.Log($"Mana burn damage: {mana - maxMana}");
            HealOrDamage(maxMana-mana);
            mana=maxMana;
        }
        else if (mana < 0)
        {
            //Debug.Log($"Mana fatique damage: {-1*mana}");
            HealOrDamage(mana);
            mana=0;
        }
        if (health > maxHealth)
            health = maxHealth;
        else if (health < 0)
            health = 0;
        //Die From cringe
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
        mana+=value;
    }
}

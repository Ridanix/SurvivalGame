using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityShield : PlayerAbility
{
    public Player_Controller playerController;
    public Player_Data playerData;
    bool isActive = false;
    public bool playerAbilityShieldIsActive = false;
    public Transform player;
    public GameObject shieldParticle;
    
    
    public override void Activate()
    {

        if (playerAbilityShieldIsActive == false && isActive == false)
        {
            StartCoroutine("PlayerShielded");
            Debug.Log("Shielded");
        }
       
    }
    IEnumerator PlayerShielded()
    {
        playerAbilityShieldIsActive = true;
        isActive = true;
        playerData.health = playerData.health + 50f;
        playerData.mana = playerData.mana - 20;
        Instantiate(shieldParticle, player.position, Quaternion.Euler(-90,0,0));
        
        yield return new WaitForSeconds(3f);
        playerAbilityShieldIsActive = false;
        playerData.health = playerData.health - 50f;
        yield return new WaitForSeconds(5f);
        isActive = false;
    }

}

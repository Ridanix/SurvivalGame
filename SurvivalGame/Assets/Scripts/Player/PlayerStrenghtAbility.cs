using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrenghtAbility : PlayerAbility
{
    public Player_Controller playerController;
    public Player_Data playerData;
    bool BuffActive = false;
    public override void Activate()
    {
        if (BuffActive == false)
        {
            StartCoroutine("PlayerBuffed");
            Debug.Log("Harder Better Faster Stronger");
        }


    }
    IEnumerator PlayerBuffed()
    {
        BuffActive = true;
        if(playerData.stamina>= 20f)
        {
            playerController.strenghtModifier = 2;
            playerData.stamina -= 20;
            yield return new WaitForSeconds(3f);

            playerController.strenghtModifier = 1;
            yield return new WaitForSeconds(5f);
        }
        BuffActive = false;
        
        
    }
   
}

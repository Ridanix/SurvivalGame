using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerDashAbility : PlayerAbility
{
    bool isActive = false;
    public Player_Controller playerController;
    public Player_Data playerData;
    public float dashVelocity;
    public override void Activate()
    {
        if (isActive == false )
        {
            StartCoroutine("PlayerDashed");
        }
        
    }
   IEnumerator PlayerDashed()
    {
        isActive = true;
        if(playerData.stamina>= 20f)
        {
            playerController.speed = 40;
            playerData.stamina -= 20f;
            yield return new WaitForSeconds(0.15f);
            playerController.speed = 5;
            yield return new WaitForSeconds(5.85f);
        }
        isActive = false;
    }
}

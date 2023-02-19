using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballAbility : PlayerAbility
{
    public Player_Controller playerController;
    public Player_Data playerData;
    bool isActive = false;
    [SerializeField]private GameObject fireballPrefab;
    [SerializeField]private Transform attackPoint;
    [SerializeField] private Transform playerPoint;
    public override void Activate()
    {
        if (isActive == false)
        {
            StartCoroutine("PlayerFireballed");
            isActive = true;
        }
        


    }
    IEnumerator PlayerFireballed()
    {
        Rigidbody rb = Instantiate(fireballPrefab, attackPoint.position, playerPoint.rotation).GetComponent<Rigidbody>();
        rb.AddForce(attackPoint.transform.forward * 15f, ForceMode.VelocityChange);
        playerData.mana = playerData.mana - 20;
        yield return new WaitForSeconds(8f);
        isActive = false;
    }
}

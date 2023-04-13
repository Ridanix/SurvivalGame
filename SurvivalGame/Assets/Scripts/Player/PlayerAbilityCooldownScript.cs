using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAbilityCooldownScript : MonoBehaviour
{
    [SerializeField] TMP_Text cooldownTextDash;
    [SerializeField] TMP_Text cooldownTextBuff;
    [SerializeField] TMP_Text cooldownTextShield;
    [SerializeField] TMP_Text cooldownTextFireball;
    public float cooldownIntDash;
    public float cooldownIntBuff;
    public float cooldownIntShield;
    public float cooldownIntFireball;
    float decreasedPerSec = 1;

    void Start()
    {
        cooldownIntDash = 0;
        cooldownIntBuff = 0;
        cooldownIntShield = 0;
        cooldownIntFireball = 0; 

    }

    // Update is called once per frame
    void Update()
    {
        
        
        cooldownIntDash -= decreasedPerSec * Time.deltaTime;
        cooldownIntBuff -= decreasedPerSec * Time.deltaTime;
        cooldownIntShield -= decreasedPerSec * Time.deltaTime;
        cooldownIntFireball -= decreasedPerSec * Time.deltaTime;
        if (cooldownIntDash > 0) 
        {
            cooldownTextDash.color = new Color(255, 255, 255, 255);
            cooldownTextDash.text = ((int)cooldownIntDash).ToString();

        }
        if (cooldownIntDash < 0)
        {
            cooldownTextDash.color = new Color(255, 255, 255, 0);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                cooldownIntDash = 6;
            }
        }

        if (cooldownIntBuff > 0) 
        {
            cooldownTextBuff.color = new Color(255, 255, 255, 255);
            cooldownTextBuff.text = ((int)cooldownIntBuff).ToString();
        }
        if (cooldownIntBuff < 0)
        {
            cooldownTextBuff.color = new Color(255, 255, 255, 0);
            if (Input.GetKeyDown(KeyCode.R))
            {
                cooldownIntBuff = 8;
            }
        }
       

        if (cooldownIntShield > 0)
        {
            cooldownTextShield.color = new Color(255, 255, 255, 255);
            cooldownTextShield.text = ((int)cooldownIntShield).ToString();
        }
        if (cooldownIntShield < 0)
        {
            cooldownTextShield.color = new Color(255, 255, 255, 0);
            if (Input.GetKeyDown(KeyCode.X))
            {
                cooldownIntShield = 8;
            }
        }
       
        if (cooldownIntFireball > 0)
        {
           cooldownTextFireball.color = new Color(255, 255, 255, 255);
           cooldownTextFireball.text = ((int)cooldownIntFireball).ToString();
        }
        if (cooldownIntFireball < 0)
        {
            cooldownTextFireball.color = new Color(255, 255, 255, 0);
            if (Input.GetKeyDown(KeyCode.C))
            {
                cooldownIntFireball = 8;
            }
        }
        



    }
}

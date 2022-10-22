using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Data : MonoBehaviour
{
    public Slider health_bar;
    public float health = 100f;
    public float maxhealth = 100f;
    public Slider stamina_bar;
    public float stamina = 100f;
    public float maxstamina = 100f;

    //Chosen Item
    public Transform current_object;
    public GameObject[] slots;
    public GameObject chosen_item_frame;

    private void Awake()
    {
        current_object = slots[0].transform;
    }

    private void FixedUpdate()
    {
        health_bar.value = health;
        health_bar.maxValue = maxhealth;
        stamina_bar.value = stamina;
        stamina_bar.maxValue = maxstamina;
        Round();

        chosen_item_frame.transform.position = current_object.transform.position;
    }

    private void Round()
    {
        if (stamina > maxstamina)
            stamina = maxstamina;
        else if (stamina < 0)
            stamina = 0;
        if (health > maxhealth)
            health = maxhealth;
        //else if (health < 0)
            //Die From cringe
    }




}

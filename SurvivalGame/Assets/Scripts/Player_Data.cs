using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Data : MonoBehaviour
{
    public static Slider health_fill;
    public static GameObject health_bar;
    public static float healh = 100f;

    private void Awake()
    {
        health_bar = GameObject.Find("Health_bar");
        health_fill = health_bar.GetComponent<Slider>();
        health_fill.maxValue = healh;
        health_fill.value = health_fill.maxValue;
    }

    public static void Change_health_value(float points)
    {
        health_fill.value += points;
        healh = health_fill.value;
    }

    public static void Change_max_health(float points)
    {
        health_fill.maxValue += points;
        health_fill.value = health_fill.maxValue;
        healh = health_fill.value;
    }

}

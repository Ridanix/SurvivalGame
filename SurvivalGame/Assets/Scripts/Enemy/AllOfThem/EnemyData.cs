using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    bool dealDmg = false;

    //Health
    [SerializeField] public float maxHealth;
    public float health;
}

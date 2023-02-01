using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //HealthBarScript
    [SerializeField] EnemyHealthBar enemyHealthBar;

    //Health
    [SerializeField] public float maxHealth;
    public float health;

    //Animator
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //možný pozdìjší health
    public void TakeDamage(float amount, string name = "")
    {
        health -= amount;
        //if (name == "Golem" || (name == "Troll" && health > )) animator.SetTrigger("hit");
        if (name == "Golem" || (name == "Troll")) animator.SetTrigger("hit");
        Debug.Log(name);
        enemyHealthBar.SetHealth(health);
        if (health <= 0)
        {
            //animator.SetTrigger("die");
            Destroy(gameObject);
        }
    }
}

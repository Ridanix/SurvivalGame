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

    //Die
    bool die = false;
    bool dying = false;
    [SerializeField] AnimationClip deathAnim;
    float deathLenght;
    float deathTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealthBar.SetMaxHealth(maxHealth);

        health = maxHealth;
        deathLenght = deathAnim.length * 0.8f;
    }   

    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            animator.SetTrigger("die");
            deathTime = Time.time;
            dying = true;
            die = false;
            return;
        }
        else if (dying && Time.time - deathTime < deathLenght) return;
        else if (dying && Time.time - deathTime >= deathLenght) Destroy(gameObject);
    }

    //možný pozdìjší health
    public void TakeDamage(float amount, string name = "")
    {
        health -= amount;
        //Debug.Log(name);
        enemyHealthBar.SetHealth(health);
        if (health <= 0)
        {
            die = true;
            return;
            //animator.SetTrigger("die");
            //Destroy(gameObject);
        }
        //else if (name == "Golem" || (name == "Troll" && health > )) animator.SetTrigger("hit");
        else if (!die && !dying && name != "Golem" || (name == "Troll")) animator.SetTrigger("hit");
    }
}

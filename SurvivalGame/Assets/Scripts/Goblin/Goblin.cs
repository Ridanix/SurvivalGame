using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : MonoBehaviour
{
    [SerializeField] Transform player; //Hlavní hráè
    [SerializeField] int range; //Range na hledání nepøátel

    //Health
    [SerializeField] float maxHealth;
    float health;

    //Attack
    float cooldown = 1.117f; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    [SerializeField] LayerMask enemyLayers; 

    //Components
    Animator animator;
    NavMeshAgent nav;

    public Player_Data player_Data;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttack < cooldown) return; //èekání než dodìlá attack

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < range && distance > nav.stoppingDistance)
        {
            nav.SetDestination(player.position);
            transform.LookAt(player);
            animator.SetBool("following", true);
        }
        else if (distance >= range)
        {
            animator.SetBool("following", false);
        }
        else if (distance <= nav.stoppingDistance)
        {
            animator.SetBool("following", false);
            Attack();          
        }

        //Debug.Log(distance);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("miss");
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("hit");
            player_Data.TakeDamage(10f);
            //Destroy(gameObject);
        }
    }
    /*void OnTriggerEnter(Collider other)
    {
        Debug.Log("nehit2");
    }*/

    void Attack()
    {
        transform.LookAt(player);
        animator.SetTrigger("attack");
        lastAttack = Time.time;

        Collider[] hitEmemies = Physics.OverlapSphere(attackPoint.position,attackRange,enemyLayers);

        foreach(Collider enemy in hitEmemies)
        {
            //Debug.Log("trefa");
        }
    }

    void TakeDamage(float amout)
    {
        health -= amout;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] float agroRange; //Range na hledání nepøátel

    //Health
    [SerializeField] float maxHealth;
    float health;

    //Attack
    [SerializeField] AnimationClip attackAnimation;
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    float attackDamage = 20f;
    [SerializeField] LayerMask enemyLayers;

    //Components
    Animator animator;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = maxHealth;
        attackCooldown = attackAnimation.length;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("PlayerPrefab").transform;
        if (Time.time - lastAttack < attackCooldown) return; //èekání než dodìlá attack

        float distance = Vector3.Distance(transform.position, player.position);


        if (distance < agroRange && distance > nav.stoppingDistance)
        {

            nav.SetDestination(player.position);
            transform.LookAt(player);
            animator.SetBool("following", true);
        }
        else if (distance >= agroRange)
        {

            animator.SetBool("following", false);
        }
        else if (distance <= nav.stoppingDistance)
        {
            animator.SetBool("following", false);
            Attack();
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("hit"); //Test jestli to funguje
        }
    }

    void Attack()
    {
        transform.LookAt(player);
        animator.SetTrigger("attack");
        lastAttack = Time.time;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider player in hitEnemies)
        {


            if (hitEnemies.Length > 0)
            {
                player.GetComponent<Player_Data>().HealOrDamage(attackDamage*(-1));
                Debug.Log("Trefa");

            }
        }
    }

    //možný pozdìjší health
    /*void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }*/
}

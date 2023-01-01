using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] float agroRange; //Range na hledání nepøátel

    //Attack
    [SerializeField] float attackDmg;
    [SerializeField] AnimationClip attackAnimation;
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    float attackDamage = 20f;

    public LayerMask playerLayer;
    public bool dealDmg = false;

    //Components
    Animator animator;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        attackCooldown = attackAnimation.length;
    }

    // Update is called once per frame
    void Update()
    {
        //èekání do pùlky animace útoku, ubrání životù
        if (Time.time - lastAttack > attackCooldown / 1.5f && Time.time - lastAttack < attackCooldown && dealDmg == false)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
            foreach (Collider player in hitEnemies) player.GetComponent<Player_Data>().HealOrDamage(-attackDmg);

            dealDmg = true;
            return;
        }
        else if (Time.time - lastAttack < attackCooldown) return; //èekání než dodìlá attack

        dealDmg = false;

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

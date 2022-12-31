using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troll : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] float agroRange; //Range na hledání nepøátel

    //Attack
    [SerializeField] float attackDmg;
    [SerializeField] AnimationClip attackAnimation;
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 2f;
    [SerializeField] LayerMask playerLayer;
    bool dealDmg = false;

    //Health
    [SerializeField] EnemyHealth enemyHealth;

    //Components
    Animator animator;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCooldown = attackAnimation.length;
        player = GameObject.Find("PlayerPrefab").transform;
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

        float distance = Vector3.Distance(transform.position, player.position);
        //Debug.Log(distance);
        //Debug.Log(nav.stoppingDistance * 1.1f);
        if (distance < agroRange && distance > nav.stoppingDistance * 1.05f)
        {
            nav.SetDestination(player.position);
            transform.LookAt(player);
            animator.SetBool("following", true);
        }
        else if (distance >= agroRange)
        {
            animator.SetBool("following", false);
        }
        else if (distance <= nav.stoppingDistance*1.05f)
        {
            animator.SetBool("following", false);
            Attack();
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        //RageMode
        if (enemyHealth.health <= enemyHealth.maxHealth / 2)
        {
            attackDmg = attackDmg * 2;
            nav.speed = 4;
        }
    }

    void Attack()
    {
        transform.LookAt(player);
        animator.SetTrigger("attack");
        lastAttack = Time.time;
    }

}

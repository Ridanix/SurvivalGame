using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GoblinArcher : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] EnemyHealth enemyHealth; //Health Script
    [SerializeField] float agroRange; //Range na hledání nepøátel
    public GameObject projectile;
    float distance;

    //Attack
    //[SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation; //Aniamce útoku
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    //float attackRange = 0.6f;
    //public LayerMask playerLayer;
    //bool dealDmg = false;
    bool arrowShot = false;

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
        lastAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.dead) return;

        if (Time.time - lastAttack > attackCooldown / 1.5f && Time.time - lastAttack < attackCooldown && arrowShot == false)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            Rigidbody rb = Instantiate(projectile, attackPoint.position, transform.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
            //rb.AddForce(transform.right * 2f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 2f, ForceMode.Impulse);
            arrowShot = true;
            return;
        }
        else if (Time.time - lastAttack < attackCooldown)
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            return; //èekání než hodí vystøelí
        }

        arrowShot = false;

        distance = Vector3.Distance(transform.position, player.position);

        if (distance < agroRange && distance > nav.stoppingDistance)
        {
            nav.SetDestination(player.position);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
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
        //transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    void Attack()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        animator.SetTrigger("attack");
        lastAttack = Time.time;
    }
}

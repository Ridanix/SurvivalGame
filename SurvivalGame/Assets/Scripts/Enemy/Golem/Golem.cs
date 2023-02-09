using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Golem : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] EnemyHealth enemyHealth; //Health Script
    [SerializeField] float agroRange; //Range na hledání nepøátel
    public GameObject projectile;
    float distance;

    //Attack
    [SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation; //Aniamce útoku
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 2.5f;
    public LayerMask playerLayer;
    bool dealDmg = false;

    //ThrowRock
    [SerializeField] float throwDmg; //Dmg Moba
    float throwCooldown = 20f; //délka mezi útoky
    [SerializeField] AnimationClip throwAnimation; //Aniamce útoku
    float throwCooldownAnim; //délka animace attacku
    float lastThrow; //kdy attack zaèal
    bool throwRock = false;
    //[SerializeField] Transform attackPoint;
    //float attackRange = 2.5f;
    //public LayerMask playerLayer;

    //Components
    Animator animator;
    NavMeshAgent nav;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCooldown = attackAnimation.length;
        throwCooldownAnim = throwAnimation.length;
        player = GameObject.Find("PlayerPrefab").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.dead) return;

        //èekání do pùlky animace útoku, ubrání životù
        if (Time.time - lastAttack > attackCooldown / 1.5f && Time.time - lastAttack < attackCooldown && dealDmg == false)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
            foreach (Collider player in hitEnemies) player.GetComponent<Player_Data>().HealOrDamage(-attackDmg);

            dealDmg = true;
            return;
        }
        else if (Time.time - lastAttack < attackCooldown) return; //èekání než dodìlá attack

        if (Time.time - lastThrow > throwCooldownAnim / 1.5f && Time.time - lastThrow < throwCooldownAnim && throwRock == false)
        {
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 13f, ForceMode.Impulse);
            rb.AddForce(transform.up * 2f, ForceMode.Impulse);

            throwRock = true;
            return;
        }
        else if (Time.time - lastThrow < throwCooldownAnim) return; //èekání než hodí kámen

        dealDmg = false;
        throwRock = false;

        distance = Vector3.Distance(transform.position, player.position);

        if (distance < agroRange && distance > nav.stoppingDistance)
        {
            if (Time.time - lastThrow > throwCooldown)
            {
                Throw();
                return;
            }
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
    void Throw()
    {
        nav.SetDestination(transform.position);
        //Debug.Log("throw");
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        animator.SetTrigger("throw");
        lastThrow = Time.time;
    }
    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, sightRange);
    }*/
}

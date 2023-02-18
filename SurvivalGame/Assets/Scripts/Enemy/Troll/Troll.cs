using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troll : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] EnemyHealth enemyHealth;
    [SerializeField] GameObject shake;
    [SerializeField] float agroRange; //Range na hledání nepøátel
    float distance;

    //Attack
    [SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation; //Aniamce útoku
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 2f;
    public LayerMask playerLayer;
    bool dealDmg = false;

    //RageMode
    [SerializeField] AnimationClip roarAnimation; //Aniamce útoku
    float roadLenght; //délka animace attacku
    bool rageMode = false;


    //Components
    Animator animator;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCooldown = attackAnimation.length;
        roadLenght = roarAnimation.length;
        player = GameObject.Find("PlayerPrefab").transform;
        shake = GameObject.Find("Camera_GO");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.dead) return;

        if (Time.time - lastAttack < roadLenght) return;
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

        distance = Vector3.Distance(transform.position, player.position);

        if (distance < agroRange && distance > nav.stoppingDistance * 1.11)
        {
            nav.SetDestination(player.position);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            animator.SetBool("following", true);
        }
        else if (distance >= agroRange)
        {
            nav.SetDestination(transform.position);
            animator.SetBool("following", false);
        }
        else if (distance <= nav.stoppingDistance * 1.11)
        {
            nav.SetDestination(transform.position);
            animator.SetBool("following", false);
            Attack();
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        //RageMode
        if (!rageMode && enemyHealth.health  <= enemyHealth.maxHealth / 2)
        {
            animator.SetTrigger("rageMode");
            shake.GetComponent<Shake>().duration = roadLenght * 0.8f;
            shake.GetComponent<Shake>().start = true;
            Shake.isActive = false;
            lastAttack = Time.time;
            attackDmg = attackDmg * 2;
            nav.speed = 4;
            rageMode = true;
        }
    }

    void Attack()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        animator.SetTrigger("attack");
        lastAttack = Time.time;
    }
}

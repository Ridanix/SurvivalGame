using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troll : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth;  
    Transform player; //Hlavn� hr��sk� postava
    [SerializeField] float agroRange; //Range na hled�n� nep��tel

    //Attack
    [SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation; //Aniamce �toku
    float attackCooldown; //d�lka animace attacku
    float lastAttack; //kdy attack za�al
    [SerializeField] Transform attackPoint;
    float attackRange = 2f;
    public LayerMask playerLayer;
    bool dealDmg = false;

    //RageMode
    [SerializeField] AnimationClip roarAnimation; //Aniamce �toku
    float roarCooldown; //d�lka animace attacku
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
        roarCooldown = roarAnimation.length;
        player = GameObject.Find("PlayerPrefab").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttack < roarCooldown) return;
        //�ek�n� do p�lky animace �toku, ubr�n� �ivot�
        if (Time.time - lastAttack > attackCooldown / 1.5f && Time.time - lastAttack < attackCooldown && dealDmg == false)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
            foreach (Collider player in hitEnemies) player.GetComponent<Player_Data>().HealOrDamage(-attackDmg);

            dealDmg = true;
            return;
        }
        else if (Time.time - lastAttack < attackCooldown) return; //�ek�n� ne� dod�l� attack

        dealDmg = false;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < agroRange && distance > nav.stoppingDistance * 1.11)
        {
            nav.SetDestination(player.position);
            //transform.LookAt(player);
            Vector3 lookDiecrtion = player.position - transform.position;
            Quaternion lookRatation = Quaternion.LookRotation(lookDiecrtion, Vector3.up);
            transform.rotation = lookRatation;
            animator.SetBool("following", true);
        }
        else if (distance >= agroRange)
        {
            animator.SetBool("following", false);
        }
        else if (distance <= nav.stoppingDistance * 1.11)
        {
            animator.SetBool("following", false);
            Attack();
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        //RageMode

        if (!rageMode && enemyHealth.health  <= enemyHealth.maxHealth / 2)
        {
            animator.SetTrigger("rageMode");
            lastAttack = Time.time;
            attackDmg = attackDmg * 2;
            nav.speed = 4;
            rageMode = true;
        }
    }

    void Attack()
    {
        transform.LookAt(player);
        animator.SetTrigger("attack");
        lastAttack = Time.time;
    }
}

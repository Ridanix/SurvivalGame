using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Houbak : MonoBehaviour
{
    Transform player; //Hlavn� hr��sk� postava
    [SerializeField] EnemyHealth enemyHealth; //Health Script
    [SerializeField] float agroRange; //Range na hled�n� nep��tel
    float distance;

    //Attack
    [SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation; //Aniamce �toku
    float attackCooldown; //d�lka animace attacku
    float lastAttack; //kdy attack za�al
    [SerializeField] Transform attackPoint;
    float attackRange = 0.8f;
    public LayerMask playerLayer;
    bool dealDmg = false;

    //Spawn (mob vyleze ze zem�, za�ne �to�it)
    bool spawnTrigger = false;
    bool spawn = false;
    [SerializeField] float spawnRange;
    [SerializeField] AnimationClip spawnAnimation;
    float spawningTime;
    float spawnTime;

    //Health
    [SerializeField] GameObject healthCanvas;


    //Components
    Animator animator;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCooldown = attackAnimation.length;
        spawningTime = spawnAnimation.length;
        player = GameObject.Find("PlayerPrefab").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.dead) return;

        distance = Vector3.Distance(transform.position, player.position);

        if (!spawn)
        {
            if (!spawnTrigger && distance > spawnRange) return;
            else if (!spawnTrigger && distance <= spawnRange)
            {
                animator.SetTrigger("spawnTrigger");
                spawnTime = Time.time;
                spawnTrigger = true;
                return;
            }
            else if (Time.time - spawnTime > spawningTime * 0.6f)
            {
                healthCanvas.SetActive(true);
            }
            else if (Time.time - spawnTime < spawningTime)
            {
                return;
            }
            else if (Time.time - spawnTime >= spawningTime)
            {
                spawn = true;
                return;
            }
        }

        if (Time.time - lastAttack > attackCooldown / 1.5f && Time.time - lastAttack < attackCooldown && dealDmg == false)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
            foreach (Collider player in hitEnemies) player.GetComponent<Player_Data>().HealOrDamage(-attackDmg);

            dealDmg = true;
            return;
        }
        else if (Time.time - lastAttack < attackCooldown) return; //�ek�n� ne� dod�l� attack

        dealDmg = false;

        //AgroRange
        if (distance < agroRange && distance > nav.stoppingDistance)
        {
            nav.SetDestination(player.position);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            animator.SetBool("following", true);
        }
        //Range
        else if (distance >= agroRange)
        {
            nav.SetDestination(transform.position);
            animator.SetBool("following", false);
        }
        //AttackRange
        else if (distance <= nav.stoppingDistance)
        {
            nav.SetDestination(transform.position);
            animator.SetBool("following", false);
            Attack();
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    void Attack()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        animator.SetTrigger("attack");
        lastAttack = Time.time;
    }
}

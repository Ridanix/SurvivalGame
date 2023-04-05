using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Goblin : MonoBehaviour
{
    Transform player; //Hlavn� hr��sk� postava
    [SerializeField] EnemyHealth enemyHealth; //Health Script
    [SerializeField] float agroRange; //Range na hled�n� nep��tel
    float distance;
    public float followSpeed = 5f; 

    //Attack
    [SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation; //Aniamce �toku
    float attackCooldown; //d�lka animace attacku
    float lastAttack; //kdy attack za�al
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    public LayerMask playerLayer;
    bool dealDmg = false;

    //Patrol
    Vector3 patrolCenter;
    Vector3 currentDestination;
    float patrolSpeed = 2f; 
    float patrolWaitTime = 2.5f;
    float patrolRange = 15f; 
    float patrolTimer;
    float followingRange = 50f;
    bool returnToCenter = false;


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

        //Patrol
        patrolCenter = transform.position; // set the patrol center to the enemy's initial position
        currentDestination = GetRandomPointInRadius(patrolCenter, patrolRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.dead) return;

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


        distance = Vector3.Distance(transform.position, player.position); //Distance mezi AI, hr��em

        //AgroRange
        if (distance < agroRange && distance > nav.stoppingDistance) 
        {
            if (returnToCenter) return; //skip, kdy� se vrac� na st�ed
            
            nav.SetDestination(player.position);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            nav.speed = followSpeed;
            animator.SetBool("following", true);
            animator.SetBool("walking", false);
            //Vracen� se na st�ed, kdy� je moc daleko
            if (Vector3.Distance(patrolCenter, transform.position) > followingRange)
            {
                nav.SetDestination(patrolCenter);
                transform.LookAt(new Vector3(patrolCenter.x, transform.position.y, patrolCenter.z));
                returnToCenter = true;
            }
        }
        //PatrolRange
        else if (distance >= agroRange) 
        {
            returnToCenter = false; //zauto��, kdy� jsme moc bl�zko

            nav.speed = patrolSpeed;
            animator.SetBool("following", false);
            //Patrol
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                animator.SetBool("walking", false);
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= patrolWaitTime)
                {
                    currentDestination = GetRandomPointInRadius(patrolCenter, patrolRange);
                    nav.SetDestination(currentDestination);
                    patrolTimer = 0f;
                }
            }
            else
            {
                animator.SetBool("walking", true);
                patrolTimer = 0f;
            }
        }
        //AttackRange
        else if (distance <= nav.stoppingDistance) 
        {
            nav.SetDestination(transform.position);
            animator.SetBool("following", false);
            animator.SetBool("walking", false);
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

    private Vector3 GetRandomPointInRadius(Vector3 center, float radius)
    {
        Vector3 randomPoint = Random.insideUnitSphere * radius;
        randomPoint += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
}

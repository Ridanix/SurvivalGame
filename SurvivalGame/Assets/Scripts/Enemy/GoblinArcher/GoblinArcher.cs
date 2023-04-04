using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GoblinArcher : MonoBehaviour
{
    Transform player; //Hlavn� hr��sk� postava
    [SerializeField] EnemyHealth enemyHealth; //Health Script
    [SerializeField] float agroRange; //Range na hled�n� nep��tel
    public GameObject projectile;
    float distance;
    public float followSpeed = 5f;

    //Attack
    //[SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation; //Aniamce �toku
    float attackCooldown; //d�lka animace attacku
    float lastAttack; //kdy attack za�al
    [SerializeField] Transform attackPoint;
    //float attackRange = 0.6f;
    //public LayerMask playerLayer;
    //bool dealDmg = false;
    bool arrowShot = false;

    //Patrol
    Vector3 patrolCenter;
    Vector3 currentDestination;
    public float patrolSpeed = 2f;
    public float patrolWaitTime = 2.5f;
    public float patrolRange = 15f;
    private float patrolTimer;

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

        //Patrol
        patrolCenter = transform.position; // set the patrol center to the enemy's initial position
        currentDestination = GetRandomPointInRadius(patrolCenter, patrolRange);
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
            return; //�ek�n� ne� hod� vyst�el�
        }

        arrowShot = false;

        distance = Vector3.Distance(transform.position, player.position);

        if (distance < agroRange && distance > nav.stoppingDistance) //AgroRange
        {
            nav.SetDestination(player.position);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            nav.speed = followSpeed;
            animator.SetBool("following", true);
        }
        else if (distance >= agroRange) //PatrolRange
        {
            nav.speed = patrolSpeed;
            //Patrol
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                animator.SetBool("following", false);
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
                animator.SetBool("following", true);
                patrolTimer = 0f;
            }
        }
        else if (distance <= nav.stoppingDistance) //AttackRange
        {
            nav.SetDestination(transform.position);
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
    private Vector3 GetRandomPointInRadius(Vector3 center, float radius)
    {
        Vector3 randomPoint = Random.insideUnitSphere * radius;
        randomPoint += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
}

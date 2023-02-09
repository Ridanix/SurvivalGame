using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossGoblin : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] EnemyHealth enemyHealth; //Health Script
    [SerializeField] Shake shake;
    [SerializeField] float agroRange; //Range na hledání nepøátel
    float distance;

    //Attack
    [SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation1; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation2; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation3; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation4; //Aniamce útoku
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    public LayerMask playerLayer;
    bool dealDmg = false;
    [SerializeField] GameObject projectileAxe1;
    [SerializeField] GameObject projectileAxe2;
    int random;

    //Spawn (Boss vstane z trùnu)
    bool spawnTrigger = false;
    bool spawn = false;
    [SerializeField] float spawnRange;
    [SerializeField] AnimationClip spawnAnimation;
    float spawningTime;
    float spawnTime;
    /*[SerializeField] Transform Axe1;
    [SerializeField] Transform Axe2;
    [SerializeField] Transform axePlace1;
    [SerializeField] Transform axePlace2;
    public float lerp;*/

    //Phase
    bool phase1 = true;
    bool enteringPhase2 = false;
    bool roar = false;
    float phase2EnterTime;
    float throwEnterTime;
    bool throwingAxe = false;
    bool throwAxe = false;
    [SerializeField] AnimationClip RoarAnimation;
    [SerializeField] AnimationClip ThrowAnimation;
    float roarLenght;
    float throwLenght;
    [SerializeField] GameObject axeProjectile;

    //Components
    Animator animator;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCooldown = attackAnimation1.length;
        spawningTime = spawnAnimation.length;
        roarLenght = RoarAnimation.length;
        throwLenght = ThrowAnimation.length;
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
                animator.SetTrigger("standUp");
                spawnTime = Time.time;
                //healthCanvas.SetActive(true);
                spawnTrigger = true;
                return;
            }
            if (Time.time - spawnTime < spawningTime)
            {
                /*Vector3 a = Axe1.position;
                Vector3 b = Axe2.position;
                Vector3 c = axePlace1.position;
                Vector3 d = axePlace2.position;
                Axe1.position = Vector3.MoveTowards(a, c, 3);
                Axe1.position = Vector3.MoveTowards(b, d, 3);*/
                return;
            }
            else if (Time.time - spawnTime >= spawningTime)
            {
                spawn = true;
                projectileAxe1.SetActive(true);
                projectileAxe2.SetActive(true);
                return;
            }
            else return;
        }

        if (enteringPhase2)
        {
            if (!throwingAxe && Time.time - phase2EnterTime < roarLenght)
            {
                //shake.duration = roarLenght * 0.8f;
                shake.start = true;
                return;
            }
            /*else if (!throwingAxe && Time.time - phase2EnterTime > (roarLenght * 0.1f))
            {
                shake.start = true;
                return;
            }*/
            /*else if (!throwingAxe && Time.time - phase2EnterTime < (roarLenght * 0.5f))
            {
                shake.start = false;
                return;
            }*/
            else if (!throwingAxe && Time.time - phase2EnterTime >= roarLenght)
            {
                throwEnterTime = Time.time;
                throwingAxe = true;
                return;
            }
            if (!throwAxe && throwingAxe && Time.time - throwEnterTime > throwLenght * 0.3f)
            {
                projectileAxe2.SetActive(false);
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                Rigidbody rb = Instantiate(axeProjectile, attackPoint.position, transform.rotation).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 12f, ForceMode.Impulse);
                rb.AddForce(transform.up * 3f, ForceMode.Impulse);
                throwAxe = true;
            }
            else if (throwingAxe && Time.time - throwEnterTime < throwLenght)
            {
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                return;
            }
            else
            {
                nav.speed = nav.speed * 1.5f;
                enteringPhase2 = false;
                return;
            }

        }

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

        if (phase1 && enemyHealth.health < enemyHealth.maxHealth * 0.5f)
        {
            animator.SetTrigger("phase2");
            phase2EnterTime = Time.time;
            phase1 = false;
            enteringPhase2 = true;
            roar = true;
            return;
        }
    }

    void Attack()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        random = Random.Range(1, 3);
        //Debug.Log(random);
        if (random == 1)
        {
            animator.SetTrigger("attack");
            if (phase1) attackCooldown = attackAnimation1.length;
            else if (!phase1)attackCooldown = attackAnimation3.length;
        }
        else if (random == 2)
        {
            animator.SetTrigger("attack2");
            if (phase1) attackCooldown = attackAnimation2.length;
            else if (!phase1) attackCooldown = attackAnimation4.length;
        }

        lastAttack = Time.time;
    }
}

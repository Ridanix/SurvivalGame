using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossBear : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] EnemyHealth enemyHealth; //Health Script
    //[SerializeField] GameObject shake; //CameraShake Script
    GameObject shake; //CameraShake Script
    [SerializeField] float agroRange; //Range na hledání nepøátel
    float distance;

    //Attack
    float attackDmg = 30; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation1; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation2; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation3; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation4; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation5; //Aniamce útoku
    [SerializeField] float Attack1Dmg;
    [SerializeField] float Attack2Dmg;
    [SerializeField] float Attack3Dmg;
    [SerializeField] float Attack4Dmg;
    [SerializeField] float Attack5Dmg;
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    public LayerMask playerLayer;
    bool dealDmg = false;
    [SerializeField] GameObject spawnDrop;
    bool itemSpawned = false;
    int pattern;
    int patternMax = 5;
    float attackAnimPlayedTime = 0.8f;

    //Spawn (Boss spinká)
    bool sleep = true;
    bool spawnTrigger = false;
    bool spawn = false;
    [SerializeField] float wakeUpTriggerRange;
    [SerializeField] AnimationClip spawnAnimation;
    float spawningLenght;
    float spawnStartTime;


    //Phase2
    bool phase1 = true;
    bool enteringPhase2 = false;
    float speed = 0f;
    float phase2EnterTime;
    [SerializeField] AnimationClip RoarAnimation;
    float roarLenght;
    bool shakeCam = false;


    //Components
    Animator animator;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerPrefab").transform;
        shake = GameObject.Find("Camera_GO");

        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCooldown = attackAnimation1.length;
        spawningLenght = spawnAnimation.length;
        roarLenght = RoarAnimation.length;

    }

    // Update is called once per frame
    void Update()
    {

        if (enemyHealth.dead)
        {
            nav.enabled = false;
            if (itemSpawned == false)
            {
                Instantiate(spawnDrop, gameObject.transform.position, Quaternion.identity);
                itemSpawned = true;
            }
            return;
        }

        distance = Vector3.Distance(transform.position, player.position);
       
        if (sleep)
        {
            Sleep();
            return;
        }


        if (enteringPhase2)
        {
            if (Time.time - phase2EnterTime < roarLenght)
            {
                if (!shakeCam)
                {
                    shake.GetComponent<Shake>().duration = roarLenght * 0.8f;
                    shake.GetComponent<Shake>().start = true;
                    shakeCam = true;
                }
                return;
            }
            //else if (Time.time - phase2EnterTime >= roarLenght * 0.8f)
            else
            {
                patternMax = 6;
                Shake.isActive = false;
                enteringPhase2 = false;
                return;
            }

        }

        //if (phase1 == false) Debug.Log(nav.speed); //Test

        //èekání do pùlky animace útoku, ubrání životù
        if (Time.time - lastAttack > attackCooldown * attackAnimPlayedTime && Time.time - lastAttack < attackCooldown && dealDmg == false)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
            foreach (Collider player in hitEnemies) player.GetComponent<Player_Data>().HealOrDamage(-attackDmg);

            dealDmg = true;
            return;
        }
        else if (Time.time - lastAttack < attackCooldown) return; //èekání než dodìlá attack

        dealDmg = false;

        //AgroRange
        if (distance < agroRange && distance > nav.stoppingDistance)
        {
            nav.SetDestination(player.position);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            animator.SetBool("following", true);
        }
        //PatrolRange
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

        if (phase1 && enemyHealth.health < enemyHealth.maxHealth * 0.5f)
        {
            nav.speed = nav.speed * 0.8f;
            animator.SetTrigger("phase2");
            phase2EnterTime = Time.time;
            phase1 = false;
            enteringPhase2 = true;
            return;
        }
    }

    void Attack()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        //pattern = Random.Range(1, 3);
        if (pattern > patternMax) pattern = 1;

        //Debug.Log(random);
        if (pattern == 1 || pattern == 2 || pattern == 3 || pattern == 4)
        {
            animator.SetTrigger("attack");
            if (phase1)
            {
                attackCooldown = attackAnimation1.length;
                attackDmg = Attack1Dmg;
                attackAnimPlayedTime = 0.6f;
            }
            else if (!phase1)
            {
                attackCooldown = attackAnimation3.length;
                attackDmg = Attack3Dmg;
                attackAnimPlayedTime = 0.6f;
            }
        }
        else if (pattern == 5)
        {
            animator.SetTrigger("attack2");
            if (phase1)
            {
                attackCooldown = attackAnimation2.length;
                attackDmg = Attack2Dmg;
                attackAnimPlayedTime = 0.5f;
            }
            else if (!phase1)
            {
                attackCooldown = attackAnimation4.length;
                attackDmg = Attack4Dmg;
                attackAnimPlayedTime = 0.5f;
            }
        }
        else if (pattern == 6)
        {
            animator.SetTrigger("attack3");
            if (!phase1)
            {
                attackCooldown = attackAnimation5.length;
                attackDmg = Attack5Dmg;
                attackAnimPlayedTime = 0.5f;
            }
        }
        pattern++;
        lastAttack = Time.time;
    }

    void Sleep()
    {
        if (!spawnTrigger && distance > wakeUpTriggerRange) return;
        else if (!spawnTrigger && distance <= wakeUpTriggerRange)
        {
            animator.SetTrigger("wakeUp");
            spawnStartTime = Time.time;
            //healthCanvas.SetActive(true);
            spawnTrigger = true;
            return;
        }
        if (Time.time - spawnStartTime >= spawningLenght)
        {
            sleep = false;
            return;
        }
        else return;
    }
}

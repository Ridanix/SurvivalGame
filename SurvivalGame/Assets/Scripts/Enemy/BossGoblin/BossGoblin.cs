using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BossGoblin : MonoBehaviour
{
    Transform player; //Hlavní hráèská postava
    [SerializeField] EnemyHealth enemyHealth; //Health Script
    //[SerializeField] GameObject shake; //CameraShake Script
    GameObject shake; //CameraShake Script
    [SerializeField] float agroRange; //Range na hledání nepøátel
    float distance;
    //float posX;
    //float posZ;

    //Attack
    float attackDmg = 30; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation1; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation2; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation3; //Aniamce útoku
    [SerializeField] AnimationClip attackAnimation4; //Aniamce útoku
    [SerializeField] float Attack1Dmg;
    [SerializeField] float Attack2Dmg;
    [SerializeField] float Attack3Dmg;
    [SerializeField] float Attack4Dmg;
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    public LayerMask playerLayer;
    bool dealDmg = false;
    [SerializeField] GameObject projectileAxe1;
    [SerializeField] GameObject projectileAxe2;
    [SerializeField] GameObject spawnAxe;
    bool itemSpawned = false;
    int random;
    float attackAnimPlayedTime = 0.8f;

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
    float speed = 0f;
    float phase2EnterTime;
    float throwEnterTime;
    bool throwingAxe = false;
    bool throwAxe = false;
    [SerializeField] Transform throwPoint;
    [SerializeField] AnimationClip RoarAnimation;
    [SerializeField] AnimationClip ThrowAnimation;
    float roarLenght;
    float throwLenght;
    [SerializeField] GameObject axeProjectile;
    bool shakeCam = false;



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
        shake = GameObject.Find("Camera_GO");

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.dead)
        {
            nav.enabled = false;
            if (itemSpawned == false)
            {
                Instantiate(spawnAxe, gameObject.transform.position, Quaternion.identity);
                itemSpawned = true;
            }
            return;
        }

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
                if (!shakeCam)
                {
                    shake.GetComponent<Shake>().duration = roarLenght * 0.8f;
                    shake.GetComponent<Shake>().start = true;
                    shakeCam = true;
                }
                return;
            }
            else if (!throwingAxe && Time.time - phase2EnterTime >= roarLenght)
            {
                throwEnterTime = Time.time;
                throwingAxe = true;
                Shake.isActive = false;
                return;
            }
            if (!throwAxe && throwingAxe && Time.time - throwEnterTime > throwLenght * 0.3f)
            {
                projectileAxe2.SetActive(false);
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                Rigidbody rb = Instantiate(axeProjectile, throwPoint.position, transform.rotation).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
                rb.AddForce(transform.up * 4f, ForceMode.Impulse);
                throwAxe = true;
            }
            else if (throwingAxe && Time.time - throwEnterTime < throwLenght)
            {
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                return;
            }
            else
            {
                nav.speed = speed * 1.25f;
                enteringPhase2 = false;
                return;
            }

        }

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

        if (distance < agroRange && distance > nav.stoppingDistance)
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
        else if (distance <= nav.stoppingDistance)
        {
            nav.SetDestination(transform.position);
            animator.SetBool("following", false);
            Attack();
        }

        if (phase1 && enemyHealth.health < enemyHealth.maxHealth * 0.5f)
        {
            speed = nav.speed;
            nav.speed = 0f;
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
        //random = Random.Range(1, 3);
        if (random > 5) random = 1;

        //Debug.Log(random);
        if (random == 1 || random == 2 || random == 3 || random == 4)
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
        else if (random == 5)
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
        random++;
        lastAttack = Time.time;
    }
}

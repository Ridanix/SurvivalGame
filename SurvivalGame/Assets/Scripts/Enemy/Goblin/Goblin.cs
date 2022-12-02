using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;

public class Goblin : NetworkBehaviour
{    
    Transform player; //Hlavní hráèská postava
    [SerializeField] int agroRange; //Range na hledání nepøátel
    //public GameObject serverPrefab;
    //public GameObject playerPf;

    //Health
    [SerializeField] float maxHealth;
    float health;

    //Attack
    [SerializeField] AnimationClip attackAnimation;
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    [SerializeField] Transform attackPoint;
    float attackRange = 0.6f;
    [SerializeField] LayerMask enemyLayers;

    //Components
    Animator animator;
    NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = maxHealth;
        attackCooldown = attackAnimation.length;       
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("PlayerStartingPrefab(Clone)").transform;
        if (Time.time - lastAttack < attackCooldown) return; //èekání než dodìlá attack

        float distance = Vector3.Distance(transform.position, player.position); 

        //Hledání hráèské postavy
        //Transform GetClosestPlayer(Transform[] player)
        //{
        //    Transform tMin = null;
        //    float minDist = Mathf.Infinity;
        //    Vector3 currentPos = transform.position;
        //    foreach (Transform t in player)
        //    {
        //        float dist = Vector3.Distance(t.position, currentPos);
        //        if (dist < minDist)
        //        {
        //            tMin = t;
        //            minDist = dist;
        //        }
        //    }
        //    return tMin;
        //}

        if (distance < agroRange && distance > nav.stoppingDistance)
        {

            nav.SetDestination(player.position);
            transform.LookAt(player);
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
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("hit"); //Test jestli to funguje
        }
    }

    void Attack()
    {
        //transform.LookAt(player);
        animator.SetTrigger("attack");
        lastAttack = Time.time;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {

            var playerHit = enemy.GetComponent<NetworkObject>();
            if (playerHit != null)
            {
                //Debug.Log("trefa"); //Test jestli to funguje
                GoblinGivesDamageServerRpc();
            }
        }
    }

    //možný pozdìjší health
    /*void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }*/

    [ServerRpc]
    private void GoblinGivesDamageServerRpc()
    {
        PlayerReceivesDamageClientRpc();
    }
    [ClientRpc]
    private void PlayerReceivesDamageClientRpc()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.Netcode;

public class Goblin : NetworkBehaviour
{
    [SerializeField] Player_Data player_Data; //Player_Data script
    [SerializeField] Transform player; //Hlavn� hr��
    [SerializeField] int range; //Range na hled�n� nep��tel
    public GameObject serverPrefab;
    public GameObject playerPf;

    //Health
    [SerializeField] float maxHealth;
    float health;

    //Attack
    float cooldown = 1.117f; //d�lka animace attacku
    float lastAttack; //kdy attack za�al
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
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("PlayerStartingPrefab(Clone)").transform;
        if (Time.time - lastAttack < cooldown) return; //�ek�n� ne� dod�l� attack

        float distance = Vector3.Distance(transform.position, player.position);

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

        if (distance < range && distance > nav.stoppingDistance)
        {

            nav.SetDestination(player.position);
            transform.LookAt(player);
            animator.SetBool("following", true);
        }
        else if (distance >= range)
        {

            animator.SetBool("following", false);
        }
        else if (distance <= nav.stoppingDistance)
        {
            animator.SetBool("following", false);
            Attack();
        }

        //Debug.Log(distance);
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("miss");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            //player_Data.TakeDamage(10f);
            //Destroy(gameObject);
        }
    }
    /*void OnTriggerEnter(Collider other)
    {
        Debug.Log("nehit2");
    }*/

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
                Debug.Log("trefa");
                GoblinGivesDamageServerRpc();
            }
        }
    }

    void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

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

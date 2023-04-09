using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


public class Chicken : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyHealth; //Health Script

    //Drop po zabití
    [SerializeField] GameObject spawnDrop;
    bool itemSpawned = false;

    //Patrol
    Vector3 patrolCenter;
    Vector3 currentDestination;
    float patrolSpeed = 2f;
    float patrolWaitTime = 2.5f;
    float patrolRange = 15f;
    float patrolTimer;
    //float followingRange = 50f;
    //bool returnToCenter = false;

    //Components
    Animator animator;
    NavMeshAgent nav;


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


        //Patrol
        patrolCenter = transform.position; // set the patrol center to the enemy's initial position
        currentDestination = GetRandomPointInRadius(patrolCenter, patrolRange);
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

        if (enemyHealth.hit)
        {
            currentDestination = GetRandomPointInRadius(patrolCenter, patrolRange);
            nav.SetDestination(currentDestination);
            patrolTimer = 0f;
        }

        nav.speed = patrolSpeed;
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
    private Vector3 GetRandomPointInRadius(Vector3 center, float radius)
    {
        Vector3 randomPoint = Random.insideUnitSphere * radius;
        randomPoint += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GoblinArcher : MonoBehaviour
{
    Transform player; //Hlavn� hr��sk� postava
    [SerializeField] float agroRange; //Range na hled�n� nep��tel
    public GameObject projectile;

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

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttack > attackCooldown / 1.5f && Time.time - lastAttack < attackCooldown && arrowShot == false)
        {
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
            //rb.AddForce(transform.right * 2f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 2f, ForceMode.Impulse);
            arrowShot = true;
            return;
        }
        else if (Time.time - lastAttack < attackCooldown) return; //�ek�n� ne� hod� vyst�el�
        arrowShot = false;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < agroRange && distance > nav.stoppingDistance)
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
        else if (distance <= nav.stoppingDistance)
        {
            animator.SetBool("following", false);
            Attack();
        }
        //transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    void Attack()
    {
        transform.LookAt(player);
        animator.SetTrigger("attack");
        lastAttack = Time.time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Golem : MonoBehaviour
{
    Transform player; //Hlavn� hr��sk� postava
    [SerializeField] float agroRange; //Range na hled�n� nep��tel

    //Attack
    [SerializeField] float attackDmg; //Dmg Moba
    [SerializeField] AnimationClip attackAnimation; //Aniamce �toku
    float attackCooldown; //d�lka animace attacku
    float lastAttack; //kdy attack za�al
    [SerializeField] Transform attackPoint;
    float attackRange = 2.5f;
    public LayerMask playerLayer;
    bool dealDmg = false;

    //ThrowRock
    [SerializeField] float throwDmg; //Dmg Moba
    float throwCooldown = 20f; //d�lka mezi �toky
    [SerializeField] AnimationClip throwAnimation; //Aniamce �toku
    float throwCooldownAnim; //d�lka animace attacku
    float lastThrow; //kdy attack za�al
    bool throwRock = false;
    //[SerializeField] Transform attackPoint;
    //float attackRange = 2.5f;
    //public LayerMask playerLayer;

    //Components
    Animator animator;
    NavMeshAgent nav;

    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackCooldown = attackAnimation.length;
        throwCooldownAnim = throwAnimation.length;
        player = GameObject.Find("PlayerPrefab").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //�ek�n� do p�lky animace �toku, ubr�n� �ivot�
        if (Time.time - lastAttack > attackCooldown / 1.5f && Time.time - lastAttack < attackCooldown && dealDmg == false)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
            foreach (Collider player in hitEnemies) player.GetComponent<Player_Data>().HealOrDamage(-attackDmg);

            dealDmg = true;
            return;
        }
        else if (Time.time - lastAttack < attackCooldown) return; //�ek�n� ne� dod�l� attack

        if (Time.time - lastThrow > throwCooldownAnim / 1.2f && Time.time - lastThrow < throwCooldownAnim && throwRock == false)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            throwRock = true;
            return;
        }
        else if (Time.time - lastThrow < throwCooldownAnim) return; //�ek�n� ne� hod� k�men

        dealDmg = false;
        throwRock = false;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < agroRange && distance > nav.stoppingDistance)
        {
            if (Time.time - lastThrow > throwCooldown)
            {
                Throw();
                return;
            }
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

    void Attack()
    {
        transform.LookAt(player);
        animator.SetTrigger("attack");
        lastAttack = Time.time;
    }
    void Throw()
    {
        nav.SetDestination(transform.position);
        //Debug.Log("throw");
        transform.LookAt(player);
        animator.SetTrigger("throw");
        lastThrow = Time.time;
    }
    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, sightRange);
    }*/
}

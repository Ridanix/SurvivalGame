using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    string currentState;

    [SerializeField] AnimationClip attackAnimation;
    float attackCooldown; //délka animace attacku
    float lastAttack; //kdy attack zaèal
    public bool attackReady;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attackCooldown = attackAnimation.length;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.time - lastAttack < attackCooldown) return; //èekání než dodìlá attack
        attackReady = true;
        float MoveY = Input.GetAxisRaw("Horizontal");
        float MoveX = Input.GetAxisRaw("Vertical");
        if (Input.GetMouseButtonDown(0))
        {
            ChangeAnimationState("LightAttack1");
            lastAttack = Time.time;
            attackReady = false;
            return;
        }
        else if (MoveY != 0 || MoveX != 0)
        {
            ChangeAnimationState("Walking");
            return;
        }
        else
        {
            ChangeAnimationState("Idle");
            return;
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}

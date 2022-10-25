using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    string currentState;

    float cooldown = 0.567f; //d�lka animace attacku
    float lastAttack; //kdy attack za�al

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttack < cooldown) return; //�ek�n� ne� dod�l� attack
        float MoveY = Input.GetAxisRaw("Horizontal");
        float MoveX = Input.GetAxisRaw("Vertical");
        if (Input.GetMouseButtonDown(0))
        {
            ChangeAnimationState("LightAttack1");
            lastAttack = Time.time;
        }
        else if (MoveY != 0 || MoveX != 0)
        {
            ChangeAnimationState("Walking");
        }
        else
        {
            ChangeAnimationState("Idle");
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}

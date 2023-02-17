using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    //This Skill is used for Inheritance only, simple to ScriptableItem
    public new string name;
    public float activeTime;
    public float coolDownTime;
    public Sprite skillImage;
    public bool isActiveInSkillManager = true;

    public virtual void Activate()
    {
        Debug.Log($"{name} ability is Used");
    }
}

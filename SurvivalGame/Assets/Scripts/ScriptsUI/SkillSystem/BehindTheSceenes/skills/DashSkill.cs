using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashSkill : Skill
{
    public override void Activate()
    {
        Debug.LogWarning("***Dela, ze dashuje***");
    }
}

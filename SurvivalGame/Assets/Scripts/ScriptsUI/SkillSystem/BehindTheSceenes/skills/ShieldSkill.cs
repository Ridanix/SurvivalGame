using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldSkill : Skill
{
    public override void Activate()
    {
        Debug.LogWarning("***Dela, ze se brani***");
    }
}

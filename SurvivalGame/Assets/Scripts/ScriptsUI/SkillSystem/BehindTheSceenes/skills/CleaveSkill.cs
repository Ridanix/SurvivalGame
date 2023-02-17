using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CleaveSkill : Skill
{
    public override void Activate()
    {
        Debug.LogWarning("***Dela, ze je Darius***");
    }
}

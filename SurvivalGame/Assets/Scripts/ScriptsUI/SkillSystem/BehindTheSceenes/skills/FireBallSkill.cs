using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireBallSkill : Skill
{
    public override void Activate()
    {
        Debug.LogWarning("***Dela, ze Casti Fireball***");
    }
}

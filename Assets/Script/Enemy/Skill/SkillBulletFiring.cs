using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletFiring: EnemySkill
{
    public GameObject buletPrefeb;//발사할 총알 오브젝트
    public float fireForce = 30f;//발사 파워

    //스킬 구현 부분
    public override void Skill(Transform creationLocation, Vector2 attackVec)
    {
        
    }
}

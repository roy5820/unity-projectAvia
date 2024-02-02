using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThrowingAttack : EnemySkill
{
    public float fireForce = 30f;//발사 파워

    //스킬 구현 부분
    public override IEnumerator Skill()
    {
        GameObject thisPre = Instantiate(attackPrefeb, creationLocation.position, Quaternion.identity);//총알 프리펩 생성
        targetP = SetTargetToPlayer();//타겟 제설정
        Vector2 direction = targetP - (Vector2)transform.position;//타겟 위치 가져오기

        yield return base.Skill();
    }
}

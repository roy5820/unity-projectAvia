using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMeleeAttack : EnemySkill
{
    private GameObject thisPre;//생성한 공격 프리펩이 저장되는 곳
    public float attackTime = 0;//공격 시간
    //스킬 구현 부분
    public override IEnumerator Skill()
    {
        thisPre = Instantiate(attackPrefeb, creationLocation.position, Quaternion.identity);//공격 프리펩 생성

        yield return new WaitForSeconds(attackTime);

        yield return base.Skill();
    }

    //후딜레이 부분
    public override IEnumerator AfterCast()
    {
        Destroy(thisPre.gameObject);//공격 프리펩 제거

        yield return base.AfterCast();
    }
}

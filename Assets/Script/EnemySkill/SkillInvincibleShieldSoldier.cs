using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvincibleShieldSoldier : SkillRushAttack
{


    //후딜레이 부분
    public override IEnumerator AfterCast()
    {
        gameObject.GetComponent<EnemyStatusInterface>().EnemyStatus = 0;//무적 상태 해제

        yield return base.AfterCast();
    }

    public override IEnumerator CoolTime()
    {
        gameObject.GetComponent<EnemyStatusInterface>().EnemyStatus = 1;//무적 상태로 설정
        return base.CoolTime();
    }
}

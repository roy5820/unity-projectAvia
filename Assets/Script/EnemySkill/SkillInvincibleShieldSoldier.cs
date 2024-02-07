using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvincibleShieldSoldier : SkillRushAttack
{


    //�ĵ����� �κ�
    public override IEnumerator AfterCast()
    {
        gameObject.GetComponent<EnemyStatusInterface>().EnemyStatus = 0;//���� ���� ����

        yield return base.AfterCast();
    }

    public override IEnumerator CoolTime()
    {
        gameObject.GetComponent<EnemyStatusInterface>().EnemyStatus = 1;//���� ���·� ����
        return base.CoolTime();
    }
}

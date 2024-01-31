using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMeleeAttack : EnemySkill
{
    public GameObject damegeAreaPrefeb;//공격 범위 오브젝트

    //스킬 구현 부분
    public override IEnumerator Skill()
    {
        damegeAreaPrefeb.SetActive(true);//공격 활성화

        yield return base.Skill();
    }
}

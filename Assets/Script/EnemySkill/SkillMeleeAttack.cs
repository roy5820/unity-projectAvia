using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMeleeAttack : EnemySkill
{
    public GameObject damegeAreaPrefeb;//���� ���� ������Ʈ

    //��ų ���� �κ�
    public override IEnumerator Skill()
    {
        damegeAreaPrefeb.SetActive(true);//���� Ȱ��ȭ

        yield return base.Skill();
    }
}

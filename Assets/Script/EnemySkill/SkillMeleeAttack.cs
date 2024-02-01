using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMeleeAttack : EnemySkill
{
    private GameObject thisPre;//������ ���� �������� ����Ǵ� ��
    public float attackTime = 0;//���� �ð�
    //��ų ���� �κ�
    public override IEnumerator Skill()
    {
        thisPre = Instantiate(attackPrefeb, creationLocation.position, Quaternion.identity);//���� ������ ����

        yield return new WaitForSeconds(attackTime);

        yield return base.Skill();
    }

    //�ĵ����� �κ�
    public override IEnumerator AfterCast()
    {
        Destroy(thisPre.gameObject);//���� ������ ����

        yield return base.AfterCast();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThrowingAttack : EnemySkill
{
    public float fireForce = 30f;//�߻� �Ŀ�

    //��ų ���� �κ�
    public override IEnumerator Skill()
    {
        GameObject thisPre = Instantiate(attackPrefeb, creationLocation.position, Quaternion.identity);//�Ѿ� ������ ����
        targetP = SetTargetToPlayer();//Ÿ�� ������
        Vector2 direction = targetP - (Vector2)transform.position;//Ÿ�� ��ġ ��������

        yield return base.Skill();
    }
}

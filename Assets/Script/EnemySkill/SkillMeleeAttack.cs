using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMeleeAttack : EnemySkill
{
    public GameObject buletPrefeb;//�߻��� �Ѿ� ������Ʈ

    //��ų ���� �κ�
    public override void Skill(Transform creationLocation)
    {
        GameObject bulletPre = Instantiate(buletPrefeb);
        bulletPre.transform.position = creationLocation.transform.position;//�Ѿ˻��� ��ġ ����
        Vector2 direction = targetP - (Vector2)transform.position;//�÷��̾� ��ġ ��������
    }
}

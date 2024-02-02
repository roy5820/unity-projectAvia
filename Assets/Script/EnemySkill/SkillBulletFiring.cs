using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletFiring: EnemySkill
{
    public float fireForce = 30f;//�߻� �Ŀ�

    //��ų ���� �κ�
    public override IEnumerator Skill()
    {
        GameObject thisPre = Instantiate(attackPrefeb, creationLocation.position, Quaternion.identity);//�Ѿ� ������ ����
        Vector2 direction = targetP - (Vector2)transform.position;//Ÿ�� ��ġ ��������
        thisPre.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);//����ü �߻��ϱ�

        yield return base.Skill();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletFiring: EnemySkill
{
    public GameObject buletPrefeb;//�߻��� �Ѿ� ������Ʈ
    public float fireForce = 30f;//�߻� �Ŀ�

    //��ų ��� �κ�
    public override void Use(Transform creationLocation, EnemySkillController callbackComponent)
    {
        base.Use(creationLocation, callbackComponent);
    }

    //��ų ���� �κ�
    public override void Skill(Transform creationLocation)
    {
        GameObject bulletPre = Instantiate(buletPrefeb);
        bulletPre.transform.position = creationLocation.transform.position;//�Ѿ˻��� ��ġ ����
        Vector2 direction = targetP - (Vector2)transform.position;//�÷��̾� ��ġ ��������
        bulletPre.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);//����ü �߻��ϱ�
    }
}

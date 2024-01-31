using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletFiring: EnemySkill
{
    public GameObject buletPrefeb;//�߻��� �Ѿ� ������Ʈ
    public float fireForce = 30f;//�߻� �Ŀ�


    //��ų ���� �κ�
    public override IEnumerator Skill()
    {
        targetP = SetTargetToPlayer(0);//Ÿ�� ������
        buletPrefeb.transform.position = creationLocation;//�Ѿ� Ȱ��ȭ ��ġ ����
        buletPrefeb.SetActive(true);//�Ѿ� Ȱ��ȭ
        Vector2 direction = targetP - (Vector2)transform.position;//�÷��̾� ��ġ ��������
        buletPrefeb.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);//����ü �߻��ϱ�

        yield return base.Skill();
    }
}

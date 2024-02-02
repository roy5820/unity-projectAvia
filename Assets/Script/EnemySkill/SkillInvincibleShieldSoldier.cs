using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvincibleShieldSoldier : SkillRushAttack
{
    public float strunTime = 1.5f;//���� �ð�(���� ����)

    //�ĵ����� �κ�
    public override IEnumerator AfterCast()
    {
        gameObject.GetComponent<EnemyStatusInterface>().EnemyStatus = 0;//���� ���� ����

        //���� ���� �� ó��
        bodyCol.enabled = true;//�� �ݶ��̴� Ȱ��ȭ
        rb.velocity = Vector2.zero;//�̵� ����
        Destroy(thisPre);//���� ������ ����

        yield return new WaitForSeconds(strunTime);//���� �ð� ��� �� ���� ������

        if (GetBehavioralStatus() == 2)
        {
            gameObject.GetComponent<EnemyStatusInterface>().EnemyStatus = 1;//���� ���� On
            gameObject.GetComponent<EnemyStatusInterface>().BehavioralStatus = 0;//�ൿ �� �Ϲ����� ����
        }
        StartCoroutine(CoolTime());
    }
}

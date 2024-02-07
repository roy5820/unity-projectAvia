using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMainController : MonoBehaviour, CharacterHit, EnemyStatusInterface
{
    public int enemyStatus = 0;// �� ������Ʈ ���°� 0: �Ϲ� ����, 1: �Ϲ� ����, 2: ���� ����
    private int behavioralStatus = 0;//�ൿ ���� �� 0: ����, 1: �̵�, 2:����, 3:����

    public float deathTime = 2.0f;//�״� �ð�

    //�� ���°� ������Ƽ
    public int EnemyStatus
    {
        get
        {
            return enemyStatus;
        }
        set
        {
            enemyStatus = value;
        }
    }

    //�� �ൿ�� ������Ƽ
    public int BehavioralStatus
    {
        get
        {
            return behavioralStatus;
        }
        set
        {
            behavioralStatus = value;
        }
    }


    //���� �ǰ� ó�� �Լ�
    public void HitAction(int attackType)
    {
        switch (enemyStatus)
        {
            case 0://�Ϲ�
                Death(attackType);
                break;
            case 1://��ų ���� ����
                if(attackType == 2)
                    Death(attackType);
                break;
            case 2://���� ����
                break;
        }
    }

    //���� ���� ó��
    public void Death(int attackType)
    {

        //�ִϸ��̼� �޴��� ��������
        if(TryGetComponent<CharacterAnimationManager>(out CharacterAnimationManager aniManager))
        {
            aniManager.SetAniParameter(2);//���� �ִϸ��̼� ó��
        }

        //�Ϲ� �������� ���� óġ �� �÷��̾� ��ų ������ ȹ��
        if (attackType == 0)
        {
            PlayerMainController instanc = PlayerMainController.getInstanc;
            if (instanc != null)
            {
                instanc.SkillSatausNowGauge++;
            }
        }

        StartCoroutine(DeathImplementation());//���� ���� �ڷ�ƾ ȣ��
    }

    //���� �ִϸ��̼� ��� �� deathTime ���� �ش� ��ü �����ϴ� �ڷ�ƾ
    IEnumerator DeathImplementation()
    {
        behavioralStatus = 3;//���� ���·� ����
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;//����

        // ��� �ݶ��̴��� ã�Ƽ� ��Ȱ��ȭ
        Collider2D[] colliders = GetComponents<Collider2D>();

        if (colliders.Length > 0)
        {
            foreach (var collider2D in colliders)
            {
                if (collider2D != null)
                {
                    collider2D.enabled = false;
                }
            }
        }

        yield return new WaitForSeconds(deathTime);

        Destroy(gameObject);
    }
}

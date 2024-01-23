using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMainController : MonoBehaviour//�� ������Ʈ ��Ʈ�ѷ�
{
    private int enemyStatus = 0;// �� ������Ʈ ���°� 0: �Ϲ� ����, 2: �Ϲ� ����, 3: ���� ����
    private int behavioralStatus = 0;//�ൿ ���� �� 0: ����, 1: �̵�, 2:����, 3:����

    private GameObject targetObj;

    private void Update()
    {
        targetObj = GameObject.FindWithTag("Player");//Ÿ�� ������Ʈ ����
    }

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

    //Ÿ�� ������Ʈ�� �������� get ������Ƽ
    public GameObject TargetObj
    {
        get
        {
            return targetObj;
        }
    }

    public int BehavioralStatus
    {
        get
        {
            return behavioralStatus;
        }
        set
        {
            Debug.Log("Set"+value);
            behavioralStatus = value;
        }
    }
}

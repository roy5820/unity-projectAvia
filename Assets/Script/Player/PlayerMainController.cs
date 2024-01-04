using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    private static PlayerMainController instance = null;//���� ������Ʈ�ѷ� �ν��Ͻ�ȭ�� ���� ���� ����
    public GameObject weapon;//�÷��̾� ����
    private int playerStatus = 0; //�÷��̾� ���� 0: �Ϲ�, 1: ����, 2: ��Ȱ ��, 3: ���

    //�� �׼Ǻ� ���� ���� ����Ǵ� ���� 0: �׼� ����, 1: �׼� ��, 2: �׼� ����
    private int isMoveStatus = 0; //�̵� ���°�
    private int isDashStatus = 0; //�뽬 ���°�
    private int isFireStatus = 0; //�Ϲݰ��� ���°�
    private int isReloadStatus = 0; //���ε� ���°�
    private int isSkillStatus = 0; //��ų���� ���°�

    private int playerLife = 50; //�÷��̾� ��� ����

    private void Awake()
    {
        //�ν��Ͻ� �� �ʱ�ȭ
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    //�÷��̾� ������Ʈ�ѷ��� �ν��Ͻ����� �����ϴ� ������Ƽ
    public static PlayerMainController getInstanc
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    //�÷��̾� ���°� get set ������Ƽ
    public int getSetPlayerStatus
    {
        get
        {
            return playerStatus;
        }
        set
        {
            playerStatus = value;
        }
    }

    //�̵� ���°� get set ������Ƽ
    public int getSetMoveStatus
    {
        get
        {
            return isMoveStatus;
        }
        set
        {
            isMoveStatus = value;
        }
    }

    //�뽬 ���°� get set ������Ƽ
    public int getSetDashStatus
    {
        get
        {
            return isDashStatus;
        }
        set
        {
            isDashStatus = value;
        }
    }


    //�Ϲݰ��� ���°� get set ������Ƽ
    public int getSetFireStatus
    {
        get
        {
            return isFireStatus;
        }
        set
        {
            isFireStatus = value;
        }
    }

    //���ε� ���°� get set ������Ƽ
    public int getSetReloadStatus
    {
        get
        {
            return isReloadStatus;
        }
        set
        {
            isReloadStatus = value;
        }
    }

    //��ų ���°� get set ������Ƽ
    public int getSetSkillStatus
    {
        get
        {
            return isSkillStatus;
        }
        set
        {
            isSkillStatus = value;
        }
    }

    //�ǰ� ó�� �Լ�
    public void HitFuntion()
    {
        playerLife--;//��� ����

        if (playerLife > 0)//��Ȱ ó��
        {

        }
        else //���� ó��
        {
            
        }
    }
}

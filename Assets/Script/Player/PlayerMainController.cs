using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    private static PlayerMainController instance = null;//���� ������Ʈ�ѷ� �ν��Ͻ�ȭ�� ���� ���� ����
    public GameObject weapon;//�÷��̾� ����
    private int playerStatus = 0; //�÷��̾� ���� 0: �Ϲ�, 1: �뽬, 2: �Ϲݰ���, 3: ������, 4: ��ų, 5: ��Ȱ ��, 6: ���

    //�� �׼Ǻ� ��� ���� ���� ���� ����Ǵ� ���� 0: �׼� ����, 1: �׼� ����
    private int isMoveStatus = 0; //�̵� ���°�
    private int isDashStatus = 0; //�뽬 ���°�
    private int isFireStatus = 0; //�Ϲݰ��� ���°�
    private int isReloadStatus = 0; //���ε� ���°�
    private int isSkillStatus = 0; //��ų���� ���°�

    private int playerLife = 50; //�÷��̾� ��� ����

    private float deathTime = 2f; //�÷��̾� ���� �ð�
    private float resurrectionTime = 4f; //�÷��̾� ��Ȱ �ð�


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

            //�÷��̾� ���º� ���� ����
            switch (playerStatus)
            {
                case 0://�÷��̾� �Ϲ� ����
                    this.gameObject.layer = LayerMask.NameToLayer("Player");//�ش� ���¿� �´� ���̾ ����
                    //���º� �׼� ���� �� ����
                    isMoveStatus = 0;
                    isDashStatus = 0;
                    isFireStatus = 0;
                    isReloadStatus = 0;
                    isSkillStatus = 0;
                    break;
                case 1://�÷��̾� �뽬 ����
                    this.gameObject.layer = LayerMask.NameToLayer("PlayerInv");//�ش� ���¿� �´� ���̾ ����
                    //���º� �׼� ���� �� ����
                    isMoveStatus = 1;
                    isDashStatus = 1;
                    isFireStatus = 1;
                    isReloadStatus = 1;
                    isSkillStatus = 1;
                    break;
                case 2://�÷��̾� �Ϲݰ��� ����
                    this.gameObject.layer = LayerMask.NameToLayer("Player");//�ش� ���¿� �´� ���̾ ����
                    //���º� �׼� ���� �� ����
                    isMoveStatus = 0;
                    isDashStatus = 0;
                    isFireStatus = 1;
                    isReloadStatus = 0;
                    isSkillStatus = 0;
                    break;
                case 3://�÷��̾� ������ ����
                    this.gameObject.layer = LayerMask.NameToLayer("Player");//�ش� ���¿� �´� ���̾ ����
                    //���º� �׼� ���� �� ����
                    isMoveStatus = 0;
                    isDashStatus = 0;
                    isFireStatus = 0;
                    isReloadStatus = 1;
                    isSkillStatus = 0;
                    break;
                case 4://�÷��̾� ��ų ����
                    this.gameObject.layer = LayerMask.NameToLayer("Player");//�ش� ���¿� �´� ���̾ ����
                    //���º� �׼� ���� �� ����
                    isMoveStatus = 0;
                    isDashStatus = 0;
                    isFireStatus = 1;
                    isReloadStatus = 1;
                    isSkillStatus = 1;
                    break;
                case 5://�÷��̾� ��Ȱ �� ����
                    this.gameObject.layer = LayerMask.NameToLayer("PlayerInv");//�ش� ���¿� �´� ���̾ ����
                    //���º� �׼� ���� �� ����
                    isMoveStatus = 0;
                    isDashStatus = 1;
                    isFireStatus = 1;
                    isReloadStatus = 1;
                    isSkillStatus = 1;
                    break;
                case 6://�÷��̾� ���� ����
                    this.gameObject.layer = LayerMask.NameToLayer("PlayerInv");//�ش� ���¿� �´� ���̾ ����
                    //���º� �׼� ���� �� ����
                    isMoveStatus = 1;
                    isDashStatus = 1;
                    isFireStatus = 1;
                    isReloadStatus = 1;
                    isSkillStatus = 1;
                    break;
            }
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

    //�������ͽ� ���� �������� �Լ�
    public void OnLoadStatus(ref int playerStatus, ref int moveStatus, ref int dashStatus, ref int fireStatus, ref int reloadStatus, ref int skillStatus)
    {
        playerStatus = this.playerStatus;
        moveStatus = isMoveStatus;//�̵� ���°�
        dashStatus = isDashStatus;//�뽬 ���°� 
        fireStatus = isFireStatus;//�Ϲ� ���� ���°�
        reloadStatus = isReloadStatus;//������ ���°�
        skillStatus = isSkillStatus;//��ų ���°�
    }

    //�ǰ� ó�� �Լ�
    public void HitFuntion()
    {
        playerLife--;//��� ����

        if (playerLife > 0)//���� �� ��Ȱ ó��
        {
            StartCoroutine(PlayerDathEvent(1));
        }
        else //���� ó��
        {
            
        }
    }

    //���� ó�� �ڷ�ƾ revenge 0: gameOveró�� 1:���� �ִϸ��̼� ��� �� ��Ȱ ó��
    IEnumerator PlayerDathEvent(int revenge)
    {
        getSetPlayerStatus = 6;//���� ���·� ��ȯ

        yield return new WaitForSeconds(deathTime);

        if(revenge == 1)//��Ȱ ���·� ��ȯ
        {
            StartCoroutine(OnResurrection());
        }
        else//���� ���� â ����
        {
            
        }
    }

    //��Ȱ ó�� �ڷ�ƾ
    IEnumerator OnResurrection()
    {
        getSetPlayerStatus = 5;//��Ȱ ���� ó��
        yield return new WaitForSeconds(resurrectionTime);
        getSetPlayerStatus = 0;//��Ȱ ���� ���� ó��
    }
}

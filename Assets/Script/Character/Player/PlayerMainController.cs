using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour, CharacterHit
{
    private static PlayerMainController instance = null;//���� ������Ʈ�ѷ� �ν��Ͻ�ȭ�� ���� ���� ����
    public GameObject weapon;//�÷��̾� ����
    private int playerStatus = 0; //�÷��̾� ���°� 0: �Ϲ�, 1: ����, 2: ���

    //�� �׼Ǻ� ��� ���� ���� ���� ����Ǵ� ���� 0: �׼� ����, 1: �׼� ����, 2: �׼���
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
        {
            instance = this;
        }
            
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

            //�÷��̾� ���º� ���̾� �� ����
            switch (playerStatus)
            {
                case 0://�÷��̾� �Ϲ� ����
                case 1://�÷��̾� ���� ����
                    this.gameObject.layer = LayerMask.NameToLayer("Player");
                    break;
                case 2:
                    this.gameObject.layer = LayerMask.NameToLayer("PlayerInv");
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

    //�������ͽ� ���� �����ϴ��Լ� 0���� ���� ���� ���� �������� ����
    public void OnSetStatus(int playerStatus, int moveStatus, int dashStatus, int fireStatus, int reloadStatus, int skillStatus)
    {
        if(playerStatus > -1)
            this.getSetPlayerStatus = playerStatus;
        if (moveStatus > -1)
            this.isMoveStatus = moveStatus;
        if (dashStatus > -1)
            this.isDashStatus = dashStatus;
        if (fireStatus > -1)
            this.isFireStatus = fireStatus;
        if (reloadStatus > -1)
            this.isReloadStatus = reloadStatus;
        if (skillStatus > -1)
            this.isSkillStatus = skillStatus;
    }

    //�ǰ� ó�� �Լ�
    public void HitAction(int attackType)
    {
        if (playerStatus != 0) return;//�ǰ� ���� ���°� �ƴϸ� �׳� ����
        
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
        OnSetStatus(2, 1, 1, 1, 1, 1);//���� ���·� ��ȯ

        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = color;

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
        OnSetStatus(2, 0, 1, 1, 1, 1);//��Ȱ ���·� ��ȯ
        yield return new WaitForSeconds(resurrectionTime);
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        OnSetStatus(0, 0, 0, 0, 0, 0);//��Ȱ ���� ���� ó��
    }

    //��ų���� �� �ִ� ������ �� ������Ƽ
    public int SkillStatusMaxGauge
    {
        get
        {
            return weapon.GetComponent<SkillStatus>().maxGauge;
        }
        set
        {
            weapon.GetComponent<SkillStatus>().maxGauge = value;
        }
    }

    //��ų���� �� ���� ������ �� ������Ƽ
    public int SkillSatausNowGauge
    {
        get
        {
            return weapon.GetComponent<SkillStatus>().nowGauge;
        }
        set
        {
            int maxGauge = SkillStatusMaxGauge;
            int nowGauge = weapon.GetComponent<SkillStatus>().nowGauge = value;

            //���� ���������� �ִ밪 ���� ũ�� �ִ밪���� ����
            if (maxGauge < nowGauge)
                weapon.GetComponent<SkillStatus>().nowGauge = maxGauge;
        }
    }

    //������� �� �ִ� �Ѿ� �� ������Ƽ
    public int WeaponStatusMaxBullet
    {
        get
        {
            return weapon.GetComponent<WeaponStatus>().maxBullet;
        }
        set
        {
            weapon.GetComponent<WeaponStatus>().maxBullet = value;
        }
    }

    //������� �� ���� �Ѿ� �� ������Ƽ
    public int WeaponStatusNowBullet
    {
        get
        {
            return weapon.GetComponent<WeaponStatus>().nowBullet;
        }
        set
        {
            int maxBullet = WeaponStatusMaxBullet;
            int nowBullet = weapon.GetComponent<WeaponStatus>().nowBullet = value;

            //�������� �ִ밪���� ũ�� �ִ밪���� ����
            if (maxBullet < nowBullet)
                weapon.GetComponent<WeaponStatus>().nowBullet = WeaponStatusMaxBullet;
        }
    }
}

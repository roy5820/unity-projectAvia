using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunSkill : MonoBehaviour, SkillStatus
{

    //�� �׼Ǻ� ���°��� ������ ����
    PlayerMainController mainController = null;
    int getPlayerStatus;//�÷��̾� ���°�
    int getDashStatus;//�뽬 ���°�
    int getMoveStatus;//�̵� ���°�
    int getFireStatus;//�Ϲݰ��� ���°�
    int getReloadStatus;//�Ϲݰ��� ������ ���°�
    int getSkillStatus;//��ų ���°�

    public int maxSkillGauge = 14;//�ִ� ��ų ������
    public int nowSkillGauge = 0;//���� ��ų ������
    public int skillCoast = 7;//��ų ��� �� �ڽ�Ʈ


    public bool isCharge = false;//��¡ �� ����
    public float minSkillRange = 5f;//�ּ� ��ų �Ÿ�
    public float maxSkillRange = 15f;//�ִ� ��ų �Ÿ�
    public float nowSkillRange = 5f;//���� ��ų �Ÿ�
    public float increaseCycleTime = 0.25f;//���� �ֱ�
    public float increaseRange = 3f;//���� �Ÿ�
    public GameObject SkillPre = null;//����� ��ų �ǰ� ������

    public LayerMask wallLayer; // �����̾�

    // Start is called before the first frame update
    void Start()
    {
        mainController = PlayerMainController.getInstanc;//�÷��̾� ��Ʈ�ѷ��� �ʱ�ȭ
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);



    }

    //��ų ��� �Է� ó�� �Լ�
    public void OnSkill(InputValue value)
    {
        if(getSkillStatus == 0 && nowSkillGauge >= skillCoast)
        {
            //Ŭ�� �� �̺�Ʈ ó��
            if (value.isPressed)
            {
                
                StartCoroutine(Charging());
            }
        }

        //��ų ����� ��Ŭ���� ���� �� �̺�Ʈ ó��
        if(getSkillStatus == 2)
        {
            if(!value.isPressed)
                isCharge = false;// ��ų ��¡ ���� true�� ����
        }
    }

    //��ų ��¡ ���� �ڷ�ƾ
    IEnumerator Charging()
    {
        // ��ų ��¡ ���� ó��
        mainController.OnSetStatus(0, -1, -1, 1, 1, 2); // �÷��̾� ���°� ����
        isCharge = true; // ��ų ��¡ ���� false�� ����
        float startTime = Time.realtimeSinceStartup; // ���� �ð� ���
        nowSkillRange = minSkillRange; // ��ų ��� �Ÿ��� �ּ� �Ÿ��� �ʱ�ȭ

        float chkincreaseTime = 0;//üũŸ��

        // ��¡�ð� ��� ��ų ��� �Ÿ� �þ�� �� ����
        while (isCharge) {
            chkincreaseTime += Time.deltaTime;
            // ��ų ���Ÿ� ���� �ֱ� üũ
            if (chkincreaseTime > increaseCycleTime)
            {
                chkincreaseTime = 0;//üũŸ�� �ʱ�ȭ

                if ((nowSkillRange + increaseRange) <= maxSkillRange)
                    nowSkillRange += increaseRange;
                else
                    nowSkillRange = maxSkillRange;
            }

            yield return null;
        }
        isCharge = false;
        Debug.Log(nowSkillRange);
        // ��¡ ���� ó��
        StartCoroutine(Rampage(nowSkillRange)); // ���� ��ų �ߵ�
    }

    //��ų ���� �ڷ�ƾ
    IEnumerator Rampage(float skillRange)
    {

        mainController.OnSetStatus(0, -1, -1, 0, 0, 0);//�÷��̾� ���°� ����
        yield return null;
    }

    //�ִ� ��ų ������ ������Ƽ
    public int maxGauge
    {
        get
        {
            return maxSkillGauge;
        }
        set
        {
            maxSkillGauge = value;
        }
    }

    //���� ��ų ������ ������Ƽ
    public int nowGauge
    {
        get
        {
            return nowSkillGauge;
        }
        set
        {
            nowSkillGauge = value;
        }
    }
}

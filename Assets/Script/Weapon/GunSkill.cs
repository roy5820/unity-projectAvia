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
    public float skillDashPower = 50f;//��ų �뽬 �Ŀ�
    public float wallCastRange = 1f;//��üũ �Ÿ�
    public GameObject skillPre = null;//����� ��ų �ǰ� ������
    public Transform attackPoint = null;//��ų ���� ��ġ

    public LayerMask wallLayer; // �����̾�

    public GameObject SkillRangeIndicatorObj;//��ų ��Ÿ� ǥ�� ������Ʈ
    public float maxRangeIndicatorRange = 3f;//�ִ�� �þ�� ��Ÿ�ǥ��
    public float RangeIncreaseSpeed = 0.04f;//��ų �þ�� �ӵ�
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
            nowSkillGauge -= skillCoast;//��ų �ڽ�Ʈ �Ҹ�
            GetComponentInParent<WeaponStatus>().nowBullet = GetComponentInParent<WeaponStatus>().maxBullet;
            //Ŭ�� �� �̺�Ʈ ó��
            if (value.isPressed)
            {
                
                StartCoroutine(Charging());
            }
        }

        //��ų ����� ��Ŭ���� ���� �� �̺�Ʈ ó��
        if(isCharge)
        {
            if(!value.isPressed)
                isCharge = false;// ��ų ��¡ ���� true�� ����
        }
    }

    //��ų ��¡ ���� �ڷ�ƾ
    IEnumerator Charging()
    {
        // ��ų ��¡ ���� ó��
        mainController.OnSetStatus(0, -1, -1, 1, 1, -1); // �÷��̾� ���°� ����
        isCharge = true; // ��ų ��¡ ���� false�� ����
        float startTime = Time.realtimeSinceStartup; // ���� �ð� ���
        nowSkillRange = minSkillRange; // ��ų ��� �Ÿ��� �ּ� �Ÿ��� �ʱ�ȭ

        float chkincreaseTime = 0;//üũŸ��

        //��ų ��Ÿ� ǥ�� ����
        GameObject thisPre = Instantiate(SkillRangeIndicatorObj, transform.parent.position, Quaternion.identity, transform.parent);
        
        // ��¡�ð� ��� ��ų ��� �Ÿ� �þ�� �� ����
        while (isCharge) {
            //��ų ��¡�� ��҉����� ó��
            if (getSkillStatus == 1)
            {
                Destroy(thisPre);
                yield break;//�ڷ�ƾ ����
            }

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

            //��ų ��Ÿ� �þ�� �� ����
            if(thisPre.transform.localScale.x < maxRangeIndicatorRange)
                thisPre.transform.localScale += new Vector3(RangeIncreaseSpeed, 0, 0);
            yield return null;
        }
        isCharge = false;

        // ��¡ ���� ó��
        Destroy(thisPre);//��ų ��Ÿ� ǥ�� ����
        StartCoroutine(Rampage(nowSkillRange)); // ���� ��ų �ߵ�
    }

    //��ų ���� �ڷ�ƾ
    IEnumerator Rampage(float skillRange)
    {
        //��ų ��� ���� ����
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (new Vector2(transform.position.x, transform.position.y));
        Vector2 skillVec = direction.normalized;

        mainController.OnSetStatus(1, 1, 1, 1, 1, 2);//�÷��̾� ���°� ����

        //���� ���� �κ�
        Vector2 startP = this.transform.position;//���� ��ġ
        RaycastHit2D wallCast;//�� üũ�� ����

        GameObject thisPre = Instantiate(skillPre, attackPoint.parent.position, Quaternion.identity, gameObject.transform);//���� ������ ����
        Rigidbody2D rb = transform.GetComponentInParent<Rigidbody2D>();//������ �ٵ� ����

        float skillDistace = 0;//�뽬 �Ÿ�
        //�ִ� �̵� �Ÿ����� �̵� ����
        while (skillDistace <= nowSkillRange)
        {
            rb.velocity = skillVec * skillDashPower;//�̵� ����
            skillDistace = Vector2.Distance(transform.position, startP);//�뽬 �Ÿ� ����
            wallCast = Physics2D.Raycast(transform.position, skillVec, wallCastRange, wallLayer);//�� üũ
            //�̵� ��ο� ���� ���� �� �̵� ���� ����
            if (wallCast.collider != null)
                break;

            yield return null;
        }

        Destroy(thisPre);
        mainController.OnSetStatus(0, 0, 0, 0, 0, 0); // �÷��̾� ���°� ����
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

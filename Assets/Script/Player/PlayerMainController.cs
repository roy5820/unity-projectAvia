using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    Rigidbody2D playerRbody;//�÷��̾� ������ �ٵ�
    public GameObject weapon;//�÷��̾� ����
    int playerStatus = 0; //�÷��̾� ���� 0: �Ϲ� 1: ���� 2: ����

    //�׼����� ���� ����
    public bool isMoveAvailability = true; //�̵� ���� ����
    public bool isDashAvailability = true; //�뽬 ���� ����
    public bool isFireAvailability = true; //�Ϲݰ��� ���� ����
    public bool isSkillAvailability = true; //��ų���� ���� ����

    //�̵����� ����
    bool isMove = false;//�̵� ���� ����
    public float moveSpeed = 8.0f;//�̵� ���ǵ�
    Vector2 inputMoveVec;//�Էµ� �̵����� ���Ͱ�
    

    //�뽬���� ����
    bool isDash = false; //�뽬 ���� ����
    public float DashPower = 30.0f;//�뽬 �Ŀ�
    public float DashTime = 0.5f;//�뽬 �ð�
    public float dashCoolTime = 3.0f;//�뽬 ��Ÿ��
    Vector2 inputDashVec;//�뽬 �̵����� ���Ͱ�

    //�߻���� ����
    public float fireCoolTiem = 0.3f; //�߻� ��Ÿ��
    bool isFire = false;//�߻� ���� ����

    private void Start()
    {
        playerRbody = this.GetComponent<Rigidbody2D>();//�÷��̾� ������ٵ� �ʱ�ȭ
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //�÷��̾� �̵� ���� �κ�
        Vector2 MoveVec = inputMoveVec.normalized * (isFire ? moveSpeed/2 : moveSpeed);
        playerRbody.velocity = MoveVec;

        //�÷��̾� �뽬 ���� �κ�
        if (isDash)
        {
            //���� �̵� �Է°��� ���� �뽬 ���� ����
            Vector2 dashVec = inputDashVec * DashPower;
            playerRbody.velocity = dashVec;
        }
    }

    //�̵� �Է� �� ó���ϴ� �Լ�
    void OnMove(InputValue value)
    {
        //�̵����ɿ��� üũ
        if (isDashAvailability)
            inputMoveVec = value.Get<Vector2>();//�̵����� ����
        else
            inputMoveVec = new Vector2(0,0);//�̵����� 0���� ����
    }
    
    //�뽬 �Է� �� ó���ϴ� �Լ�
    void OnDash()
    {
        if (isDashAvailability)
        {
            inputDashVec = (inputMoveVec.x == 0 && inputMoveVec.y == 0 ? new Vector2(1, 0) : inputMoveVec);//�뽬 �̵����� ����
            StartCoroutine(DashImplementation());//�뽬 �� ���°����� �����ϴ� �ڷ�ƾ
        }
            
    }
    //�뽬 ���� �ڷ�ƾ
    IEnumerator DashImplementation()
    {
        isDash = true; //������ ���� Ȱ��ȭ
        isDashAvailability = false;//�뽬 ���� ���� ��Ȱ��ȭ
        isFireAvailability = false;//�Ϲ� ���� ���ɿ��� ��Ȱ��ȭ
        isMoveAvailability = false;//�̵� ���ɿ��� ��Ȱ��ȭ
        isSkillAvailability = false;//��ų ���ɿ��� ��Ȱ��ȭ

        yield return new WaitForSeconds(DashTime);

        isDash = false; //������ ���� ��Ȱ��ȭ
        isDashAvailability = true;//�뽬 ���� ���� Ȱ��ȭ
        isFireAvailability = true;//�Ϲ� ���� ���ɿ��� Ȱ��ȭ
        isMoveAvailability = true;//�̵� ���ɿ��� Ȱ��ȭ
        isSkillAvailability = true;//��ų ���ɿ��� Ȱ��ȭ
        
    }

    //�Ϲݰ��� �Է� �� ó���ϴ� �Լ�
    void OnFire()
    {
        if (isFireAvailability && !isFire)
        {
            //��ź�� üũ
            int nowBulletCnt = weapon.GetComponent<WeaponBase>().nowBulletCnt;
            if (nowBulletCnt > 0)
            {
                weapon.SendMessage("Fire");//������ �߻� �Լ� ȣ��
                isFire = true;//�߻� ���� Ȱ��ȭ
                StartCoroutine(FireDelayIementation(fireCoolTiem));//�������� ��Ȱ��ȭ�ϴ� �ڷ�ƾ ȣ��
            }
            else OnReloard();//�Ѿ��� ���� �� ����
        }
    }
    //�÷��̾� �Ϲݰ��� ������ ����
    IEnumerator FireDelayIementation(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        isFire = false;
    }

    //�Ѿ� ���� �Լ�
    void OnReloard()
    {
        
    }
}

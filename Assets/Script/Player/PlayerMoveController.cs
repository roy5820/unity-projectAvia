using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{
    Rigidbody2D playerRbody;//�÷��̾� ������ �ٵ�

    //�� �׼Ǻ� ���°��� ������ ����
    PlayerMainController mainController = null;
    int getPlayerStatus;//�÷��̾� ���°�
    int getDashStatus;//�뽬 ���°�
    int getMoveStatus;//�̵� ���°�
    int getFireStatus;//�Ϲݰ��� ���°�
    int getReloadStatus;//�Ϲݰ��� ������ ���°�
    int getSkillStatus;//��ų ���°�


    //�̵����� ����
    public float moveSpeed = 30.0f;//�̵� ���ǵ�
    Vector2 inputMoveVec;//�Էµ� �̵����� ���Ͱ�

    //�뽬���� ����
    public float DashPower = 30.0f;//�뽬 �Ŀ�
    public float DashTime = 0.5f;//�뽬 �ð�
    public float dashCoolTime = 3.0f;//�뽬 ��Ÿ��
    Vector2 inputDashVec;//�뽬 �̵����� ���Ͱ�

    private void Awake()
    {
        playerRbody = this.GetComponent<Rigidbody2D>();//�÷��̾� ������ٵ� �ʱ�ȭ

        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        LoadStatus();
        
    }

    // Update is called once per frame
    void Update()
    {
        //�� ���°����� ���� ��Ʈ�ѷ��ȿ� ������ �ʱ�ȭ �ϴ� �Լ�
        LoadStatus();
    }

    private void FixedUpdate()
    {
        //�÷��̾� �̵� ���� �κ�
        if (getMoveStatus != 2)
        {
            //�̵� ����
            Vector2 MoveVec = inputMoveVec.normalized * (getFireStatus == 1 || getReloadStatus == 1 ? moveSpeed / 2 : moveSpeed);
            playerRbody.velocity = MoveVec;
        }

        //�÷��̾� �뽬 ���� �κ�
        if(getDashStatus == 1)
        {
            Vector2 dashVec = inputDashVec * DashPower;// ���� �̵� �Է°��� ���� �뽬 ���� ����
            playerRbody.velocity = dashVec;//�뽬 ����
        }
    }

    //�̵� �Է� �� ó���ϴ� �Լ�
    void OnMove(InputValue value)
    {
        inputMoveVec = value.Get<Vector2>();//�̵����� ����

        if (getMoveStatus == 1 && inputMoveVec == (new Vector2(0, 0)))
            mainController.getSetMoveStatus = 1;
        else if (getMoveStatus == 0 && inputMoveVec != (new Vector2(0, 0)))
            mainController.getSetMoveStatus = 0;
    }

    //�뽬 �Է� �� ó���ϴ� �Լ�
    void OnDash()
    {
        if (getDashStatus == 0)
        {
            inputDashVec = (inputMoveVec.x == 0 && inputMoveVec.y == 0 ? new Vector2(1, 0) : inputMoveVec);//�뽬 �̵����� ����
            StartCoroutine(DashTimer());//�뽬 �� ���°����� �����ϴ� �뽬 Ÿ�̸� ����ƾ ȣ��
        }
    }
    //�뽬 Ÿ�̸� ���� �ڷ�ƾ
    IEnumerator DashTimer()
    {
        //�÷��̾ ���� ���·� �ٲٰ� �뽬�� ������ ��� �׼��� ���Ұ��� ����
        mainController.getSetPlayerStatus = 1;
        mainController.getSetMoveStatus = 2;
        mainController.getSetDashStatus = 1;
        mainController.getSetFireStatus = 2;
        mainController.getSetReloadStatus = 2;
        mainController.getSetSkillStatus = 2;

        yield return new WaitForSeconds(DashTime);

        //�÷���� �Ϲ� ���·� �ٲٰ� ��� �׼��� ��� �������� ����
        mainController.getSetPlayerStatus = 1;
        mainController.getSetMoveStatus = 0;
        mainController.getSetDashStatus = 2;
        mainController.getSetFireStatus = 0;
        mainController.getSetReloadStatus = 0;
        mainController.getSetSkillStatus = 0;

        yield return new WaitForSeconds(dashCoolTime-DashTime);

        mainController.getSetDashStatus = 0;
    }

    //���� ��Ʈ�ѷ����� ���°��� ������ �ʱ�ȭ�ϴ� �Լ�
    public void LoadStatus()
    {
        mainController = PlayerMainController.getInstanc;//���� ��Ʈ�ѷ� ��������
        if (mainController != null)
        {
            getPlayerStatus = mainController.getSetPlayerStatus;//�÷��̾� ���°��� ����
            getMoveStatus = mainController.getSetMoveStatus;//�̵� ���°� �ʱ�ȭ
            getDashStatus = mainController.getSetDashStatus;//�뽬 ���°� �ʱ�ȭ
            getFireStatus = mainController.getSetFireStatus;//�Ϲݰ��� ���°� �ʱ�ȭ
            getReloadStatus = mainController.getSetReloadStatus;//�Ϲݰ��� ������ ���°� �ʱ�ȭ
            getSkillStatus = mainController.getSetSkillStatus;//��ų ���°� �ʱ�ȭ
        }
    }
}

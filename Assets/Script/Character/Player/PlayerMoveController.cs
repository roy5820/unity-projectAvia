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
    private int thisDashAction = 0;//�뽬 ���� ����
    private bool isDashCool = false;//�뽬 �� ����

    private void Awake()
    {
        playerRbody = this.GetComponent<Rigidbody2D>();//�÷��̾� ������ٵ� �ʱ�ȭ

        if(PlayerMainController.getInstanc)
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

    private void FixedUpdate()
    {
        //�÷��̾� �̵� ���� �κ�
        if (getMoveStatus == 0)
        {
            //�̵� ����(�Ϲ� ����, ������, ��ų ��� �� �̵��ӵ� �������� ����)
            Vector2 MoveVec = inputMoveVec.normalized * (getFireStatus == 2 || getReloadStatus == 2 || getSkillStatus == 2 ? moveSpeed / 2 : moveSpeed);
            playerRbody.velocity = MoveVec;//���� �� ����

            if (playerRbody.velocity.x > 0)
                this.transform.localScale = new Vector3(1, 1, 1);
            else if(playerRbody.velocity.x < 0)
                this.transform.localScale = new Vector3(-1, 1, 1);
        }

        //�÷��̾� �뽬 ���� �κ�
        if(getDashStatus == 2)
        {
            Vector2 dashVec = inputDashVec * DashPower;// ���� �̵� �Է°��� ���� �뽬 ���� ����
            playerRbody.velocity = dashVec;//�뽬 ����
        }
    }

    //�̵� �Է� �� ó���ϴ� �Լ�
    void OnMove(InputValue value)
    {
        inputMoveVec = value.Get<Vector2>();//�̵����� ����
    }

    //�뽬 �Է� �� ó���ϴ� �Լ�
    void OnDash()
    {
        if (getDashStatus == 0 && !isDashCool)
        {
            inputDashVec = (inputMoveVec.x == 0 && inputMoveVec.y == 0 ? new Vector2(1, 0) : inputMoveVec);//�뽬 �̵����� ����
            StartCoroutine(DashTimer());//�뽬 �� ���°����� �����ϴ� �뽬 Ÿ�̸� ����ƾ ȣ��
        }
    }
    //�뽬 Ÿ�̸� ���� �ڷ�ƾ
    IEnumerator DashTimer()
    {
        isDashCool = true;//�뽬 �� ���� ����
        mainController.OnSetStatus(1, 1, 2, 1, 1, -1); //�뽬 �� ���°� ����

        yield return new WaitForSeconds(DashTime);

        mainController.OnSetStatus(0, 0, 0, 0, 0, -1); //�뽬 �� ���°� ����

        yield return new WaitForSeconds(dashCoolTime - DashTime);//�뽬 ��Ÿ�� ����
        isDashCool = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    Rigidbody2D PlayerRbody;//�÷��̾� ������ �ٵ�
    public GameObject Weapon;//�÷��̾� ����
    int PlayerStatus = 0; //�÷��̾� ���� 0: �Ϲ� 1: ���� 2: ����

    //�׼����� ���� ����
    public bool isMoveAvailability = true; //�̵� ���� ����
    public bool isDashAvailability = true; //�뽬 ���� ����
    public bool isFireAvailability = true; //�Ϲݰ��� ���� ����
    public bool isSkillAvailability = true; //��ų���� ���� ����

    //�̵����� ����
    float MoveSpeed = 4.0f;//�̵� ���ǵ�
    Vector2 InputMoveVec;//�Էµ� �̵����� ���Ͱ�

    //�뽬���� ����
    float DashPower = 15.0f;//�뽬 �Ŀ�

    private void Start()
    {
        PlayerRbody = this.GetComponent<Rigidbody2D>();//�÷��̾� ������ٵ� �ʱ�ȭ
        Weapon = GameObject.Find("PlayerWeapon");
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //�÷��̾� �̵� ���� �κ�
        Vector2 MoveVec = InputMoveVec.normalized * MoveSpeed * Time.fixedDeltaTime;
        PlayerRbody.MovePosition(PlayerRbody.position + MoveVec);
        PlayerRbody.velocity = MoveVec;
    }

    //�̵� ���� �Լ�
    void OnMove(InputValue value)
    {
        //�̵����ɿ��� üũ
        if (isDashAvailability)
            InputMoveVec = value.Get<Vector2>();//�̵����� ����
        else
            InputMoveVec = new Vector2(0,0);//�̵����� 0���� ����
    }
    
    //�뽬 ���� �Լ�
    void OnDash()
    {
        Debug.Log(3);
    }

    //�Ϲݰ��� �߻� ���� �Լ�
    void OnFire()
    {
        Weapon.SendMessage("Fire");
    }
}

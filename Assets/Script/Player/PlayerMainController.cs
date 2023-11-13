using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    Rigidbody2D PlayerRbody;//플레이어 리지드 바디
    public GameObject Weapon;//플레이어 무기
    int PlayerStatus = 0; //플레이어 상태 0: 일반 1: 무적 2: 죽음

    //액션제한 관련 변수
    public bool isMoveAvailability = true; //이동 가능 여부
    public bool isDashAvailability = true; //대쉬 가능 여부
    public bool isFireAvailability = true; //일반공격 가능 여부
    public bool isSkillAvailability = true; //스킬공격 가능 여부

    //이동관련 변수
    float MoveSpeed = 4.0f;//이동 스피드
    Vector2 InputMoveVec;//입력된 이동방향 벡터값

    //대쉬관련 변수
    float DashPower = 15.0f;//대쉬 파워

    private void Start()
    {
        PlayerRbody = this.GetComponent<Rigidbody2D>();//플레이어 리지드바디값 초기화
        Weapon = GameObject.Find("PlayerWeapon");
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //플레이어 이동 구현 부분
        Vector2 MoveVec = InputMoveVec.normalized * MoveSpeed * Time.fixedDeltaTime;
        PlayerRbody.MovePosition(PlayerRbody.position + MoveVec);
        PlayerRbody.velocity = MoveVec;
    }

    //이동 구현 함수
    void OnMove(InputValue value)
    {
        //이동가능여부 체크
        if (isDashAvailability)
            InputMoveVec = value.Get<Vector2>();//이동방향 설정
        else
            InputMoveVec = new Vector2(0,0);//이동방향 0으로 설정
    }
    
    //대쉬 구현 함수
    void OnDash()
    {
        Debug.Log(3);
    }

    //일반공격 발사 구현 함수
    void OnFire()
    {
        Weapon.SendMessage("Fire");
    }
}

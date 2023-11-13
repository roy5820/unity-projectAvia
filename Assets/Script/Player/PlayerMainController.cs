using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    Rigidbody2D playerRbody;//플레이어 리지드 바디
    public GameObject weapon;//플레이어 무기
    int playerStatus = 0; //플레이어 상태 0: 일반 1: 무적 2: 죽음

    //액션제한 관련 변수
    public bool isMoveAvailability = true; //이동 가능 여부
    public bool isDashAvailability = true; //대쉬 가능 여부
    public bool isFireAvailability = true; //일반공격 가능 여부
    public bool isSkillAvailability = true; //스킬공격 가능 여부

    //이동관련 변수
    bool isMove = false;//이동 상태 여부
    public float moveSpeed = 8.0f;//이동 스피드
    Vector2 inputMoveVec;//입력된 이동방향 벡터값
    

    //대쉬관련 변수
    bool isDash = false; //대쉬 상태 여부
    public float DashPower = 30.0f;//대쉬 파워
    public float DashTime = 0.5f;//대쉬 시간
    public float dashCoolTime = 3.0f;//대쉬 쿨타임
    Vector2 inputDashVec;//대쉬 이동방향 벡터값

    //발사관련 변수
    public float fireCoolTiem = 0.3f; //발사 쿨타임
    bool isFire = false;//발사 상태 여부

    private void Start()
    {
        playerRbody = this.GetComponent<Rigidbody2D>();//플레이어 리지드바디값 초기화
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //플레이어 이동 구현 부분
        Vector2 MoveVec = inputMoveVec.normalized * (isFire ? moveSpeed/2 : moveSpeed);
        playerRbody.velocity = MoveVec;

        //플레이어 대쉬 구현 부분
        if (isDash)
        {
            //현제 이동 입력값에 따른 대쉬 방향 설정
            Vector2 dashVec = inputDashVec * DashPower;
            playerRbody.velocity = dashVec;
        }
    }

    //이동 입력 시 처리하는 함수
    void OnMove(InputValue value)
    {
        //이동가능여부 체크
        if (isDashAvailability)
            inputMoveVec = value.Get<Vector2>();//이동방향 설정
        else
            inputMoveVec = new Vector2(0,0);//이동방향 0으로 설정
    }
    
    //대쉬 입력 시 처리하는 함수
    void OnDash()
    {
        if (isDashAvailability)
        {
            inputDashVec = (inputMoveVec.x == 0 && inputMoveVec.y == 0 ? new Vector2(1, 0) : inputMoveVec);//대쉬 이동방향 설정
            StartCoroutine(DashImplementation());//대쉬 시 상태값들을 별경하는 코루틴
        }
            
    }
    //대쉬 구현 코루틴
    IEnumerator DashImplementation()
    {
        isDash = true; //구르기 상태 활성화
        isDashAvailability = false;//대쉬 가능 여부 비활성화
        isFireAvailability = false;//일반 공격 가능여부 비활성화
        isMoveAvailability = false;//이동 가능여부 비활성화
        isSkillAvailability = false;//스킬 가능여부 비활성화

        yield return new WaitForSeconds(DashTime);

        isDash = false; //구르기 상태 비활성화
        isDashAvailability = true;//대쉬 가능 여부 활성화
        isFireAvailability = true;//일반 공격 가능여부 활성화
        isMoveAvailability = true;//이동 가능여부 활성화
        isSkillAvailability = true;//스킬 가능여부 활성화
        
    }

    //일반공격 입력 시 처리하는 함수
    void OnFire()
    {
        if (isFireAvailability && !isFire)
        {
            //장탄수 체크
            int nowBulletCnt = weapon.GetComponent<WeaponBase>().nowBulletCnt;
            if (nowBulletCnt > 0)
            {
                weapon.SendMessage("Fire");//무기의 발사 함수 호출
                isFire = true;//발사 상태 활성화
                StartCoroutine(FireDelayIementation(fireCoolTiem));//딜레이후 비활성화하는 코루틴 호출
            }
            else OnReloard();//총알이 없을 시 장전
        }
    }
    //플레이어 일반공격 딜레이 구현
    IEnumerator FireDelayIementation(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        isFire = false;
    }

    //총알 장전 함수
    void OnReloard()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{
    Rigidbody2D playerRbody;//플레이어 리지드 바디

    //각 액션별 상태값을 저장할 변수
    PlayerMainController mainController = null;
    int getPlayerStatus;//플레이어 상태값
    int getDashStatus;//대쉬 상태값
    int getMoveStatus;//이동 상태값
    int getFireStatus;//일반공격 상태값
    int getReloadStatus;//일반공격 재장전 상태값
    int getSkillStatus;//스킬 상태값


    //이동관련 변수
    public float moveSpeed = 30.0f;//이동 스피드
    Vector2 inputMoveVec;//입력된 이동방향 벡터값

    //대쉬관련 변수
    public float DashPower = 30.0f;//대쉬 파워
    public float DashTime = 0.5f;//대쉬 시간
    public float dashCoolTime = 3.0f;//대쉬 쿨타임
    Vector2 inputDashVec;//대쉬 이동방향 벡터값
    private int thisDashAction = 0;//대쉬 상태 여부
    private bool isDashCool = false;//대쉬 쿨 여부

    private void Awake()
    {
        playerRbody = this.GetComponent<Rigidbody2D>();//플레이어 리지드바디값 초기화

        if(PlayerMainController.getInstanc)
            mainController = PlayerMainController.getInstanc;//플레이어 컨트롤러값 초기화
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    // Update is called once per frame
    void Update()
    {
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        mainController.OnLoadStatus(ref getPlayerStatus, ref getMoveStatus, ref getDashStatus, ref getFireStatus, ref getReloadStatus, ref getSkillStatus);
    }

    private void FixedUpdate()
    {
        //플레이어 이동 구현 부분
        if (getMoveStatus == 0)
        {
            //이동 구현(일반 공격, 재장전, 스킬 사용 중 이동속도 절반으로 감속)
            Vector2 MoveVec = inputMoveVec.normalized * (getFireStatus == 2 || getReloadStatus == 2 || getSkillStatus == 2 ? moveSpeed / 2 : moveSpeed);
            playerRbody.velocity = MoveVec;//백터 값 적용

            if (playerRbody.velocity.x > 0)
                this.transform.localScale = new Vector3(1, 1, 1);
            else if(playerRbody.velocity.x < 0)
                this.transform.localScale = new Vector3(-1, 1, 1);
        }

        //플레이어 대쉬 구현 부분
        if(getDashStatus == 2)
        {
            Vector2 dashVec = inputDashVec * DashPower;// 현제 이동 입력값에 따른 대쉬 방향 설정
            playerRbody.velocity = dashVec;//대쉬 적용
        }
    }

    //이동 입력 시 처리하는 함수
    void OnMove(InputValue value)
    {
        inputMoveVec = value.Get<Vector2>();//이동방향 설정
    }

    //대쉬 입력 시 처리하는 함수
    void OnDash()
    {
        if (getDashStatus == 0 && !isDashCool)
        {
            inputDashVec = (inputMoveVec.x == 0 && inputMoveVec.y == 0 ? new Vector2(1, 0) : inputMoveVec);//대쉬 이동방향 설정
            StartCoroutine(DashTimer());//대쉬 시 상태값들을 변경하는 대쉬 타이머 구로틴 호출
        }
    }
    //대쉬 타이머 구현 코루틴
    IEnumerator DashTimer()
    {
        isDashCool = true;//대쉬 쿨 여부 설정
        mainController.OnSetStatus(1, 1, 2, 1, 1, -1); //대쉬 시 상태값 설정

        yield return new WaitForSeconds(DashTime);

        mainController.OnSetStatus(0, 0, 0, 0, 0, -1); //대쉬 후 상태값 설정

        yield return new WaitForSeconds(dashCoolTime - DashTime);//대쉬 쿨타임 적용
        isDashCool = false;
    }
}

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

    private void Awake()
    {
        playerRbody = this.GetComponent<Rigidbody2D>();//플레이어 리지드바디값 초기화

        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        LoadStatus();
        
    }

    // Update is called once per frame
    void Update()
    {
        //각 상태값들을 메인 컨트롤러안에 값으로 초기화 하는 함수
        LoadStatus();
    }

    private void FixedUpdate()
    {
        //플레이어 이동 구현 부분
        if (getMoveStatus != 2)
        {
            //이동 구현
            Vector2 MoveVec = inputMoveVec.normalized * (getFireStatus == 1 || getReloadStatus == 1 ? moveSpeed / 2 : moveSpeed);
            playerRbody.velocity = MoveVec;
        }

        //플레이어 대쉬 구현 부분
        if(getDashStatus == 1)
        {
            Vector2 dashVec = inputDashVec * DashPower;// 현제 이동 입력값에 따른 대쉬 방향 설정
            playerRbody.velocity = dashVec;//대쉬 적용
        }
    }

    //이동 입력 시 처리하는 함수
    void OnMove(InputValue value)
    {
        inputMoveVec = value.Get<Vector2>();//이동방향 설정

        if (getMoveStatus == 1 && inputMoveVec == (new Vector2(0, 0)))
            mainController.getSetMoveStatus = 1;
        else if (getMoveStatus == 0 && inputMoveVec != (new Vector2(0, 0)))
            mainController.getSetMoveStatus = 0;
    }

    //대쉬 입력 시 처리하는 함수
    void OnDash()
    {
        if (getDashStatus == 0)
        {
            inputDashVec = (inputMoveVec.x == 0 && inputMoveVec.y == 0 ? new Vector2(1, 0) : inputMoveVec);//대쉬 이동방향 설정
            StartCoroutine(DashTimer());//대쉬 시 상태값들을 변경하는 대쉬 타이머 구로틴 호출
        }
    }
    //대쉬 타이머 구현 코루틴
    IEnumerator DashTimer()
    {
        //플레이어를 무적 상태로 바꾸고 대쉬를 제외한 모든 액션을 사용불가로 설정
        mainController.getSetPlayerStatus = 1;
        mainController.getSetMoveStatus = 2;
        mainController.getSetDashStatus = 1;
        mainController.getSetFireStatus = 2;
        mainController.getSetReloadStatus = 2;
        mainController.getSetSkillStatus = 2;

        yield return new WaitForSeconds(DashTime);

        //플레리어를 일반 상태로 바꾸고 모든 액션을 사용 가능으로 설정
        mainController.getSetPlayerStatus = 1;
        mainController.getSetMoveStatus = 0;
        mainController.getSetDashStatus = 2;
        mainController.getSetFireStatus = 0;
        mainController.getSetReloadStatus = 0;
        mainController.getSetSkillStatus = 0;

        yield return new WaitForSeconds(dashCoolTime-DashTime);

        mainController.getSetDashStatus = 0;
    }

    //메인 컨트롤러에서 상태값을 가져와 초기화하는 함수
    public void LoadStatus()
    {
        mainController = PlayerMainController.getInstanc;//메인 컨트롤러 가져오기
        if (mainController != null)
        {
            getPlayerStatus = mainController.getSetPlayerStatus;//플레이어 상태값을 메인
            getMoveStatus = mainController.getSetMoveStatus;//이동 상태값 초기화
            getDashStatus = mainController.getSetDashStatus;//대쉬 상태값 초기화
            getFireStatus = mainController.getSetFireStatus;//일반공격 상태값 초기화
            getReloadStatus = mainController.getSetReloadStatus;//일반공격 재장전 상태값 초기화
            getSkillStatus = mainController.getSetSkillStatus;//스킬 상태값 초기화
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunSkill : MonoBehaviour, SkillStatus
{

    //각 액션별 상태값을 저장할 변수
    PlayerMainController mainController = null;
    int getPlayerStatus;//플레이어 상태값
    int getDashStatus;//대쉬 상태값
    int getMoveStatus;//이동 상태값
    int getFireStatus;//일반공격 상태값
    int getReloadStatus;//일반공격 재장전 상태값
    int getSkillStatus;//스킬 상태값

    public int maxSkillGauge = 14;//최대 스킬 게이지
    public int nowSkillGauge = 0;//현재 스킬 게이지
    public int skillCoast = 7;//스킬 상용 시 코스트


    public bool isCharge = false;//차징 중 여부
    public float minSkillRange = 5f;//최소 스킬 거리
    public float maxSkillRange = 15f;//최대 스킬 거리
    public float nowSkillRange = 5f;//현재 스킬 거리
    public float increaseCycleTime = 0.25f;//증가 주기
    public float increaseRange = 3f;//증가 거리
    public GameObject SkillPre = null;//사용할 스킬 피격 프리펩

    public LayerMask wallLayer; // 벽레이어

    // Start is called before the first frame update
    void Start()
    {
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

    //스킬 사용 입력 처리 함수
    public void OnSkill(InputValue value)
    {
        if(getSkillStatus == 0 && nowSkillGauge >= skillCoast)
        {
            //클릭 시 이벤트 처리
            if (value.isPressed)
            {
                
                StartCoroutine(Charging());
            }
        }

        //스킬 사용후 우클릭을 떘을 떄 이벤트 처리
        if(getSkillStatus == 2)
        {
            if(!value.isPressed)
                isCharge = false;// 스킬 차징 여부 true로 설정
        }
    }

    //스킬 차징 구현 코루틴
    IEnumerator Charging()
    {
        // 스킬 차징 상태 처리
        mainController.OnSetStatus(0, -1, -1, 1, 1, 2); // 플레이어 상태값 설정
        isCharge = true; // 스킬 차징 여부 false로 설정
        float startTime = Time.realtimeSinceStartup; // 시작 시간 기록
        nowSkillRange = minSkillRange; // 스킬 사용 거리를 최소 거리로 초기화

        float chkincreaseTime = 0;//체크타임

        // 차징시간 비례 스킬 사용 거리 늘어나는 거 구현
        while (isCharge) {
            chkincreaseTime += Time.deltaTime;
            // 스킬 사용거리 증가 주기 체크
            if (chkincreaseTime > increaseCycleTime)
            {
                chkincreaseTime = 0;//체크타임 초기화

                if ((nowSkillRange + increaseRange) <= maxSkillRange)
                    nowSkillRange += increaseRange;
                else
                    nowSkillRange = maxSkillRange;
            }

            yield return null;
        }
        isCharge = false;
        Debug.Log(nowSkillRange);
        // 차징 종료 처리
        StartCoroutine(Rampage(nowSkillRange)); // 난사 스킬 발동
    }

    //스킬 구현 코루틴
    IEnumerator Rampage(float skillRange)
    {

        mainController.OnSetStatus(0, -1, -1, 0, 0, 0);//플레이어 상태값 설정
        yield return null;
    }

    //최대 스킬 게이지 프로퍼티
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

    //현재 스킬 게이지 프로퍼티
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

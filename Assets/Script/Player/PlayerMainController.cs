using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    private static PlayerMainController instance = null;//게임 메인컨트롤러 인스턴스화를 위한 변수 선언
    public GameObject weapon;//플레이어 무기
    private int playerStatus = 0; //플레이어 상태값 0: 일반, 1: 무적, 2: 사망

    //각 액션별 사용 가능 여부 값이 저장되는 변수 0: 액션 가능, 1: 액션 제한, 2: 액션중
    private int isMoveStatus = 0; //이동 상태값
    private int isDashStatus = 0; //대쉬 상태값
    private int isFireStatus = 0; //일반공격 상태값
    private int isReloadStatus = 0; //리로드 상태값
    private int isSkillStatus = 0; //스킬공격 상태값

    private int playerLife = 50; //플레이어 목숨 개수

    private float deathTime = 2f; //플레이어 죽음 시간
    private float resurrectionTime = 4f; //플레이어 부활 시간


    private void Awake()
    {
        //인스턴스 값 초기화
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    //플레이어 메인컨트롤러에 인스턴스값에 접근하는 프로퍼티
    public static PlayerMainController getInstanc
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    //플레이어 상태값 get set 프로퍼티
    public int getSetPlayerStatus
    {
        get
        {
            return playerStatus;
        }
        set
        {
            playerStatus = value;

            //플레이어 상태별 레이어 값 적용
            switch (playerStatus)
            {
                case 0://플레이어 일반 상태
                    this.gameObject.layer = LayerMask.NameToLayer("Player");
                    break;
                case 1://플레이어 무적 상태
                    this.gameObject.layer = LayerMask.NameToLayer("PlayerInv");
                    break;
            }
        }
    }

    //이동 상태값 get set 프로퍼티
    public int getSetMoveStatus
    {
        get
        {
            return isMoveStatus;
        }
        set
        {
            isMoveStatus = value;
        }
    }

    //대쉬 상태값 get set 프로퍼티
    public int getSetDashStatus
    {
        get
        {
            return isDashStatus;
        }
        set
        {
            isDashStatus = value;
        }
    }


    //일반공격 상태값 get set 프로퍼티
    public int getSetFireStatus
    {
        get
        {
            return isFireStatus;
        }
        set
        {
            isFireStatus = value;
        }
    }

    //리로드 상태값 get set 프로퍼티
    public int getSetReloadStatus
    {
        get
        {
            return isReloadStatus;
        }
        set
        {
            isReloadStatus = value;
        }
    }

    //스킬 상태값 get set 프로퍼티
    public int getSetSkillStatus
    {
        get
        {
            return isSkillStatus;
        }
        set
        {
            isSkillStatus = value;
        }
    }

    //스테이터스 값을 가져오는 함수
    public void OnLoadStatus(ref int playerStatus, ref int moveStatus, ref int dashStatus, ref int fireStatus, ref int reloadStatus, ref int skillStatus)
    {
        playerStatus = this.playerStatus;
        moveStatus = isMoveStatus;//이동 상태값
        dashStatus = isDashStatus;//대쉬 상태값 
        fireStatus = isFireStatus;//일반 공격 상태값
        reloadStatus = isReloadStatus;//재장전 상태값
        skillStatus = isSkillStatus;//스킬 상태값
    }

    //스테이터스 값을 설정하는함수
    public void OnSetStatus(int playerStatus, int moveStatus, int dashStatus, int fireStatus, int reloadStatus, int skillStatus)
    {
        this.getSetPlayerStatus = playerStatus;
        this.isMoveStatus = moveStatus;
        this.isDashStatus = dashStatus;
        this.isFireStatus = fireStatus;
        this.isReloadStatus = reloadStatus;
        this.isSkillStatus = skillStatus;
    }

    //피격 처리 함수
    public void HitFuntion()
    {
        playerLife--;//목숨 감소

        if (playerLife > 0)//죽음 후 부활 처리
        {
            StartCoroutine(PlayerDathEvent(1));
        }
        else //죽음 처리
        {
            
        }
    }

    //죽음 처리 코루틴 revenge 0: gameOver처리 1:죽음 애니메이션 출력 후 부활 처리
    IEnumerator PlayerDathEvent(int revenge)
    {
        OnSetStatus(1, 1, 1, 1, 1, 1);//죽음 상태로 전환

        yield return new WaitForSeconds(deathTime);

        if(revenge == 1)//부활 상태로 전환
        {
            StartCoroutine(OnResurrection());
        }
        else//게임 오버 창 띄우기
        {
            
        }
    }

    //부활 처리 코루틴
    IEnumerator OnResurrection()
    {
        OnSetStatus(1, 0, 1, 1, 1, 1);//부활 상태로 전환
        yield return new WaitForSeconds(resurrectionTime);
        OnSetStatus(0, 0, 0, 0, 0, 0);//부활 상태 종료 처리
    }
}

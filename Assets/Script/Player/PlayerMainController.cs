using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    private static PlayerMainController instance = null;//게임 메인컨트롤러 인스턴스화를 위한 변수 선언
    public GameObject weapon;//플레이어 무기
    private int playerStatus = 0; //플레이어 상태 0: 일반, 1: 대쉬, 2: 일반공격, 3: 재장전, 4: 스킬, 5: 부활 중, 6: 사망

    //각 액션별 사용 가능 여부 값이 저장되는 변수 0: 액션 가능, 1: 액션 제한
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

            //플레이어 상태별 상태 적용
            switch (playerStatus)
            {
                case 0://플레이어 일반 상태
                    this.gameObject.layer = LayerMask.NameToLayer("Player");//해당 상태에 맞는 레이어값 설정
                    //상태별 액션 제한 값 설정
                    isMoveStatus = 0;
                    isDashStatus = 0;
                    isFireStatus = 0;
                    isReloadStatus = 0;
                    isSkillStatus = 0;
                    break;
                case 1://플레이어 대쉬 상태
                    this.gameObject.layer = LayerMask.NameToLayer("PlayerInv");//해당 상태에 맞는 레이어값 설정
                    //상태별 액션 제한 값 설정
                    isMoveStatus = 1;
                    isDashStatus = 1;
                    isFireStatus = 1;
                    isReloadStatus = 1;
                    isSkillStatus = 1;
                    break;
                case 2://플레이어 일반공격 상태
                    this.gameObject.layer = LayerMask.NameToLayer("Player");//해당 상태에 맞는 레이어값 설정
                    //상태별 액션 제한 값 설정
                    isMoveStatus = 0;
                    isDashStatus = 0;
                    isFireStatus = 1;
                    isReloadStatus = 0;
                    isSkillStatus = 0;
                    break;
                case 3://플레이어 재장전 상태
                    this.gameObject.layer = LayerMask.NameToLayer("Player");//해당 상태에 맞는 레이어값 설정
                    //상태별 액션 제한 값 설정
                    isMoveStatus = 0;
                    isDashStatus = 0;
                    isFireStatus = 0;
                    isReloadStatus = 1;
                    isSkillStatus = 0;
                    break;
                case 4://플레이어 스킬 상태
                    this.gameObject.layer = LayerMask.NameToLayer("Player");//해당 상태에 맞는 레이어값 설정
                    //상태별 액션 제한 값 설정
                    isMoveStatus = 0;
                    isDashStatus = 0;
                    isFireStatus = 1;
                    isReloadStatus = 1;
                    isSkillStatus = 1;
                    break;
                case 5://플레이어 부활 중 상태
                    this.gameObject.layer = LayerMask.NameToLayer("PlayerInv");//해당 상태에 맞는 레이어값 설정
                    //상태별 액션 제한 값 설정
                    isMoveStatus = 0;
                    isDashStatus = 1;
                    isFireStatus = 1;
                    isReloadStatus = 1;
                    isSkillStatus = 1;
                    break;
                case 6://플레이어 죽음 상태
                    this.gameObject.layer = LayerMask.NameToLayer("PlayerInv");//해당 상태에 맞는 레이어값 설정
                    //상태별 액션 제한 값 설정
                    isMoveStatus = 1;
                    isDashStatus = 1;
                    isFireStatus = 1;
                    isReloadStatus = 1;
                    isSkillStatus = 1;
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
        getSetPlayerStatus = 6;//죽음 상태로 전환

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
        getSetPlayerStatus = 5;//부활 상태 처리
        yield return new WaitForSeconds(resurrectionTime);
        getSetPlayerStatus = 0;//부활 상태 종료 처리
    }
}

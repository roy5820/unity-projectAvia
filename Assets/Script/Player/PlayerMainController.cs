using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    private static PlayerMainController instance = null;//게임 메인컨트롤러 인스턴스화를 위한 변수 선언
    public GameObject weapon;//플레이어 무기
    private int playerStatus = 0; //플레이어 상태 0: 일반, 1: 무적, 2: 부활 중, 3: 사망

    //각 액션별 상태 값이 저장되는 변수 0: 액션 가능, 1: 액션 중, 2: 액션 제한
    private int isMoveStatus = 0; //이동 상태값
    private int isDashStatus = 0; //대쉬 상태값
    private int isFireStatus = 0; //일반공격 상태값
    private int isReloadStatus = 0; //리로드 상태값
    private int isSkillStatus = 0; //스킬공격 상태값

    private int playerLife = 50; //플레이어 목숨 개수

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

    //피격 처리 함수
    public void HitFuntion()
    {
        playerLife--;//목숨 감소

        if (playerLife > 0)//부활 처리
        {

        }
        else //죽음 처리
        {
            
        }
    }
}

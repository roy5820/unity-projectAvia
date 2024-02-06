using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMainController : MonoBehaviour, CharacterHit, EnemyStatusInterface
{
    public int enemyStatus = 0;// 적 오브젝트 상태값 0: 일반 상태, 1: 일반 무적, 2: 절대 무적
    private int behavioralStatus = 0;//행동 상태 값 0: 정지, 1: 이동, 2:공격, 3:죽음

    public float deathTime = 2.0f;//죽는 시간

    //적 상태값 프로퍼티
    public int EnemyStatus
    {
        get
        {
            return enemyStatus;
        }
        set
        {
            enemyStatus = value;
        }
    }

    //적 행동값 프로퍼티
    public int BehavioralStatus
    {
        get
        {
            return behavioralStatus;
        }
        set
        {
            behavioralStatus = value;
        }
    }


    //몬스터 피격 처리 함수
    public void HitAction(int attackType)
    {
        switch (enemyStatus)
        {
            case 0://일반
                Death(attackType);
                break;
            case 1://스킬 제외 무적
                if(attackType == 2)
                    Death(attackType);
                break;
            case 2://절대 무적
                break;
        }
    }

    //몬스터 죽음 처리
    public void Death(int attackType)
    {

        //애니메이션 메니저 가져오기
        if(TryGetComponent<CharacterAnimationManager>(out CharacterAnimationManager aniManager))
        {
            aniManager.SetAniParameter(2);//죽음 애니메이션 처리
        }

        //일반 공격으로 유닛 처치 시 플레이어 스킬 게이지 획득
        if (attackType == 0)
        {
            PlayerMainController instanc = PlayerMainController.getInstanc;
            if (instanc != null)
            {
                instanc.SkillSatausNowGauge++;
            }
        }

        StartCoroutine(DeathImplementation());//죽음 구현 코루틴 호출
    }

    //죽음 애니메이션 출력 후 deathTime 이후 해당 객체 제거하는 코루틴
    IEnumerator DeathImplementation()
    {
        behavioralStatus = 3;//죽음 상태로 변경
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;//정지

        // 모든 콜라이더를 찾아서 비활성화
        Collider2D[] colliders = GetComponents<Collider2D>();

        if (colliders.Length > 0)
        {
            foreach (var collider2D in colliders)
            {
                if (collider2D != null)
                {
                    collider2D.enabled = false;
                }
            }
        }

        yield return new WaitForSeconds(deathTime);

        Destroy(gameObject);
    }
}

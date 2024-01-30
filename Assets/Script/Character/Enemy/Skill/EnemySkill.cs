using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill: MonoBehaviour
{
    public float cooldown;//쿨타임
    public float castTime; // 선딜레이
    public float afterCastTime; // 후딜레이
    public float lastUsedTime;//마지막 사용 시간
    public float skillRange;//스킬 사거리
    public Vector2 targetP;//타겟 오브젝트 위치값
    public LayerMask targetLayer;//타겟 레이어
    public LayerMask wallLayer;//벽 레이어

    private Coroutine skillCoroutine = null;//스킬 사용시 코루틴
    private EnemySkillController callbackComponent;//콜백할 컴포넌트

    //스킬 사용 여부체크
    public virtual bool CanUse()
    {
        float distance = 0;//타겟과의 사거리가 저장되는 변수
        bool withinAttackRange;//스킬사용 사거리 안인지 체크 값을 저장하는 변수

        if (skillRange > 0)//스킬 사거리가 0보다 크면 사거리 체크
        {
            targetP =  SetTargetToPlayer();// 타겟 설정하기
            
            //스킬 사거리 체크 밑 거리 내 장애물 체크
            if (targetP != Vector2.zero)
            {
                distance = Vector2.Distance(this.transform.position, targetP);//타겟과의 거리 가져오기

                if (skillRange >= distance) withinAttackRange = true;//스킬 사거리 안이면 true
                else withinAttackRange = false;
            }
            else withinAttackRange = false;//타겟이 없으면 false
        }
        else withinAttackRange = true;//스킬 사거리가 없으면 사거리 계산없이 true
        
        return skillCoroutine == null && withinAttackRange;//스킬 쿨타임과 스킬 사용중인지 여부 그리고 사거리 체크하여 스킬 사용 여부 반환
    }

    //스킬 사용 부분 creationLocation: 스킬 생성 방향, 
    public virtual void Use(Transform creationLocation, EnemySkillController callbackComponent)
    {
        this.callbackComponent = callbackComponent;//콜백할 컴포넌트 저장
        this.callbackComponent.SetStatus(2);//공격 상태로 스테이터스 전환
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//공격 시 정지 상태로 초기화

        skillCoroutine = StartCoroutine(PerformSkill(creationLocation));//스킬 선딜, 사용, 후딜, 쿨타임 구현 코루틴 호출
    }

    //선딜 후딜레이 구현 부분
    private IEnumerator PerformSkill(Transform creationLocation)
    {

        if (castTime > 0f && GetBehavioralStatus() == 2)
            yield return new WaitForSeconds(castTime); // 선딜레이 후에 스킬 수행

        if (GetBehavioralStatus() == 2)
            Skill(creationLocation);//스킬 사용

        if (afterCastTime > 0f && GetBehavioralStatus() == 2)
            yield return new WaitForSeconds(afterCastTime); // 후딜레이 후에 쿨타임 돌아감

        if(GetBehavioralStatus() == 2)
            this.callbackComponent.SetStatus(0);//정지 상태로 스테이터스 전환

        if (cooldown > 0f)
            yield return new WaitForSeconds(cooldown);//쿨다운 후 코루틴 초기화

        skillCoroutine = null; // 코루틴 종료 후 초기화
    }

    //타겟 설정하는 함수(공격 사거리 내 장애물 없이 타겟이 있으면 가져오고 아니면 Vector2.zero 값으로 출력
    public Vector2 SetTargetToPlayer()
    {
        Vector2 attackVec = (GameObject.FindWithTag("Player").transform.position - this.gameObject.transform.position).normalized ;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, attackVec, skillRange, (targetLayer | wallLayer));//경로 상에 플레이어 체크를 위한 raycast
        Vector2 targetPosition = Vector2.zero;//타겟 포지션

        if(hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & targetLayer) != 0)
            {
                targetPosition = hit.collider.gameObject.transform.position;
            }
        }

        return targetPosition;//타겟 위치 리턴
    }

    //Enemy객체의 상태값을 가져오는 함수
    public int GetBehavioralStatus()
    {
        int getBehavioralStatus;

        //오브젝트 행동 상태값을 가져와서 공격상태가 취소되면 강제 종료
        if (TryGetComponent<EnemyStatusInterface>(out EnemyStatusInterface enemyStatusInterface))
        {
            getBehavioralStatus = enemyStatusInterface.BehavioralStatus;
        }
        else
            return -1;

        return getBehavioralStatus;
    }

    //스킬 구현 부분
    public abstract void Skill(Transform creationLocation);
}

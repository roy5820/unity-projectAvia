
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill: MonoBehaviour
{
    public float cooldown;//쿨타임
    public float castTime; //캐스팅 타임
    public float afterCastTime; //후딜레이 타임
    public float skillRange;//스킬 사거리
    public Vector2 targetP;//타겟 오브젝트 위치값
    public LayerMask targetLayer;//타겟 레이어
    public LayerMask wallLayer;//벽 레이어
    public GameObject attackPrefeb;//발사할 총알 오브젝트
    public Transform creationLocation;//생성 위치
    public bool reTargeting = false;//공격 시 타겟 재설정 여부

    private Coroutine skillCoroutine = null;//스킬 사용시 코루틴

    private CharacterAnimationManager aniManager;//애니메이션 메니저
    public int castAniNum = 0;//스킬 캐스트 애니메이션 번호
    public int skillAniNum = 0;//스킬 사용 애니메이션 번호
    public int afterCast = 0;//스킬 사용 후 애니메이션 번호
    private void Start()
    {
        //시작시 스킬 쿨타임 돌리기
        skillCoroutine = StartCoroutine(CoolTime());

        aniManager = GetComponent<CharacterAnimationManager>();//애니메이션 매니저 값 초기화
    }

    //스킬 사용 여부체크
    public virtual bool CanUse()
    {
        float distance = 0;//타겟과의 사거리가 저장되는 변수
        bool withinAttackRange;//스킬사용 사거리 안인지 체크 값을 저장하는 변수

        if (skillRange > 0)//스킬 사거리가 0보다 크면 사거리 체크
        {
            targetP =  SetTargetToPlayer(1);// 경로상에 있는 타겟값 설정하기
            
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
    public virtual void Use()
    {
        gameObject.GetComponent<EnemyStatusInterface>().BehavioralStatus = 2;//공격 상태로 스테이터스 전환
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//공격 시 정지 상태로 초기화

        //플레이어 방향으로 로컬 스케일 바꿈
        if (targetP.x < gameObject.transform.position.x)
            gameObject.transform.localScale = new Vector2(-1, 1);
        else
            gameObject.transform.localScale = new Vector2(1, 1);

        skillCoroutine = StartCoroutine(Cast());//스킬 선딜, 사용, 후딜, 쿨타임 구현 코루틴 호출
    }

    //스킬 캐스팅 구현
    public virtual IEnumerator Cast()
    {
        if (castAniNum > 0)
            aniManager.SetAniParameter(castAniNum);//스킬 캐스트 애니메이션 출력

        if (castTime > 0f && GetBehavioralStatus() == 2)
            yield return new WaitForSeconds(castTime); // 선딜레이 후에 스킬 수행

        if (GetBehavioralStatus() == 2)
        {
            if (skillAniNum > 0)
                aniManager.SetAniParameter(skillAniNum);//스킬 사용 애니메이션 출력
            //타겟 재설정 여부에 따른 타겟 재설정
            if (reTargeting)
            {
                Vector2 getTargetP = SetTargetToPlayer();//타겟 정보 가져오기
                
                targetP = getTargetP == Vector2.zero ? targetP : getTargetP;//타겟 재설정
            }

            skillCoroutine = StartCoroutine(Skill());//스킬 사용
        }
            
    }

    //스킬 구현 부분
    public virtual IEnumerator Skill()
    {

        skillCoroutine =  StartCoroutine(AfterCast());//후딜레이 구현 코루틴

        yield return null;
    }

    //스킬 후딜레이 구현
    public virtual IEnumerator AfterCast()
    {
        if (afterCast > 0)
            aniManager.SetAniParameter(afterCast);//스킬 후딜레이 애니메이션 출력
        else
            aniManager.SetAniParameter(0);
        if (afterCastTime > 0f && GetBehavioralStatus() == 2)
            yield return new WaitForSeconds(afterCastTime); // 후딜레이 후에 쿨타임 돌아감
        if (GetBehavioralStatus() == 2)
        {
            aniManager.SetAniParameter(0);
            gameObject.GetComponent<EnemyStatusInterface>().BehavioralStatus = 0;//일반 상태로 스테이터스 전환
        }
            

        skillCoroutine = StartCoroutine(CoolTime());//쿨타임 구현 코루틴 호출
        yield return null;
    }

    //스킬 쿨타임 구현
    public virtual IEnumerator CoolTime()
    {
        if (cooldown > 0f)
            yield return new WaitForSeconds(cooldown);//쿨다운 후 코루틴 초기화
        skillCoroutine = null;//스킬 코루틴 null로 초기화
    }

    //타겟 설정하는 함수 mode 1: 장애물 체크하여 위치값 가져오기 그외: 그냥 타겟 위치값 가져오기
    public Vector2 SetTargetToPlayer(int mode = 0)
    {
        Vector2 targetPosition = Vector2.zero;//타겟 포지션
        
        //벽 체크
        if (mode == 1)
        {
            Vector2 attackVec = (GameObject.FindWithTag("Player").transform.position - this.gameObject.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, attackVec, skillRange, (targetLayer | wallLayer));//경로 상에 플레이어 체크를 위한 raycast


            if (hit.collider != null)
            {
                if (((1 << hit.collider.gameObject.layer) & targetLayer) != 0)
                {
                    targetPosition = hit.collider.gameObject.transform.position;
                }
            }
        }
        //그냥 위치값 가져오기
        else
            targetPosition = GameObject.FindWithTag("Player").transform.position;


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
}

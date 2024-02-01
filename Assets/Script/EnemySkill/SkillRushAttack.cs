using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRushAttack : EnemySkill
{
    public float dashRange = 10f;//돌진 거리
    public float dashPower = 40f;//대쉬 파워
    public float wallCastRange = 2f;//돌진 시 벽 체크 거리
    public Collider2D bodyCol;//몸 콜라이더

    private Rigidbody2D rb;//리지드 바디
    private GameObject thisPre;//생성한 공격 프리펩이 저장되는 곳
    public override IEnumerator Skill()
    {
        bodyCol.enabled = false;//몸 콜라이더 비활성화
        Vector2 startP = this.transform.position;//시작 위치
        rb = this.gameObject.GetComponent<Rigidbody2D>();//Enemy의 리지드 바디
        RaycastHit2D wallCast;//벽 체크할 센서
        float dashDistace = 0;//대쉬 거리

        thisPre = Instantiate(attackPrefeb, creationLocation.position, Quaternion.identity, gameObject.transform);//공격 프리펩 생성
        
        Vector2 getTargetP = SetTargetToPlayer();//타겟 정보 가져오기
        targetP = getTargetP == Vector2.zero ? targetP : getTargetP;//타겟 재설정

        Vector2 direction = (targetP - (Vector2)transform.position).normalized;//플레이어 방향

        //최대 이동 거리까지 이동 구현
        while (dashDistace <= dashRange)
        {
            rb.velocity = direction * dashPower;//이동 구현
            dashDistace = Vector2.Distance(transform.position, startP);//대쉬 거리 갱신
            wallCast = Physics2D.Raycast(transform.position, direction, wallCastRange, wallLayer);//벽 체크
            //이동 경로에 벽이 있을 시 이동 강제 종료
            if (wallCast.collider != null)
                break;

            yield return null;
        }

        yield return base.Skill();
    }

    //후딜레이 부분
    public override IEnumerator AfterCast()
    {
        //공격 종료 후 처리
        bodyCol.enabled = true;//몸 콜라이더 활성화
        rb.velocity = Vector2.zero;//이동 정지
        Destroy(thisPre);//공격 프리펩 제거

        yield return base.AfterCast();
    }
}

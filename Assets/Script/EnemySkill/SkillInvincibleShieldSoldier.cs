using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvincibleShieldSoldier : SkillRushAttack
{
    public float strunTime = 1.5f;//기절 시간(무적 해제)

    //후딜레이 부분
    public override IEnumerator AfterCast()
    {
        gameObject.GetComponent<EnemyStatusInterface>().EnemyStatus = 0;//무적 상태 해제

        //공격 종료 후 처리
        bodyCol.enabled = true;//몸 콜라이더 활성화
        rb.velocity = Vector2.zero;//이동 정지
        Destroy(thisPre);//공격 프리펩 제거

        yield return new WaitForSeconds(strunTime);//기절 시간 경과 후 상태 재적용

        if (GetBehavioralStatus() == 2)
        {
            gameObject.GetComponent<EnemyStatusInterface>().EnemyStatus = 1;//무적 상태 On
            gameObject.GetComponent<EnemyStatusInterface>().BehavioralStatus = 0;//행동 값 일반으로 변경
        }
        StartCoroutine(CoolTime());
    }
}

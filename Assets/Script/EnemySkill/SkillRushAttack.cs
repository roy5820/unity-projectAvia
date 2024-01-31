using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRushAttack : EnemySkill
{
    public float dashRange = 10f;//돌진 거리
    public float dashPower = 40f;//대쉬 파워
    public GameObject damegeAreaPrefeb;//활성화할 데미지 영역 오브젝트
    public float wallCastRange = 2f;//돌진 시 벽 체크 거리

    public override IEnumerator Skill()
    {

        yield return base.Skill();
    }

    IEnumerator Dash(Vector2 creationP)
    {
        Vector2 startP = this.transform.position;//시작 위치
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();//Enemy의 리지드 바디
        RaycastHit2D wallCast;//벽 체크할 센서
        float dashDistace = 0;//대쉬 거리

        damegeAreaPrefeb.SetActive(true);//공격 활성화
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

        //대쉬 종료 처리
        rb.velocity = Vector2.zero;//이동 정지
        damegeAreaPrefeb.SetActive(false);//공격 비활성화
    }
}

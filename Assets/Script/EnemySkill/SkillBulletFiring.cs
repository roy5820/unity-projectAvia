using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletFiring: EnemySkill
{
    public GameObject buletPrefeb;//발사할 총알 오브젝트
    public float fireForce = 30f;//발사 파워


    //스킬 구현 부분
    public override IEnumerator Skill()
    {
        targetP = SetTargetToPlayer(0);//타겟 제설정
        buletPrefeb.transform.position = creationLocation;//총알 활성화 위치 설정
        buletPrefeb.SetActive(true);//총알 활성화
        Vector2 direction = targetP - (Vector2)transform.position;//플레이어 위치 가져오기
        buletPrefeb.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);//투사체 발사하기

        yield return base.Skill();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletFiring: EnemySkill
{
    public float fireForce = 30f;//발사 파워

    //스킬 구현 부분
    public override IEnumerator Skill()
    {
        GameObject thisPre = Instantiate(attackPrefeb, creationLocation.position, Quaternion.identity);//총알 프리펩 생성
        Vector2 direction = targetP - (Vector2)transform.position;//타겟 위치 가져오기
        thisPre.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);//투사체 발사하기

        yield return base.Skill();
    }
}

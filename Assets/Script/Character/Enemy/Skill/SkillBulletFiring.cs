using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBulletFiring: EnemySkill
{
    public GameObject buletPrefeb;//발사할 총알 오브젝트
    public float fireForce = 30f;//발사 파워

    //스킬 사용 부분
    public override void Use(Transform creationLocation, EnemySkillController callbackComponent)
    {
        base.Use(creationLocation, callbackComponent);
    }

    //스킬 구현 부분
    public override void Skill(Transform creationLocation)
    {
        GameObject bulletPre = Instantiate(buletPrefeb);
        bulletPre.transform.position = creationLocation.transform.position;//총알생성 위치 설정
        Vector2 direction = targetP - (Vector2)transform.position;//플레이어 위치 가져오기
        bulletPre.GetComponent<Rigidbody2D>().AddForce(direction.normalized * fireForce, ForceMode2D.Impulse);//투사체 발사하기
    }
}

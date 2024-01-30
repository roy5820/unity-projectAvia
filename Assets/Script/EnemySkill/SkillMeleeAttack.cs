using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMeleeAttack : EnemySkill
{
    public GameObject buletPrefeb;//발사할 총알 오브젝트

    //스킬 구현 부분
    public override void Skill(Transform creationLocation)
    {
        GameObject bulletPre = Instantiate(buletPrefeb);
        bulletPre.transform.position = creationLocation.transform.position;//총알생성 위치 설정
        Vector2 direction = targetP - (Vector2)transform.position;//플레이어 위치 가져오기
    }
}

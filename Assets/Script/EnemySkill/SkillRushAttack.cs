using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRushAttack : SkillMeleeAttack
{
    public float dashRange = 10f;//돌진 거리
    public override void Skill(Transform creationLocation)
    {
        StartCoroutine(Dash());//대쉬 구현 코루틴 호출
        base.Skill(creationLocation);
    }

    IEnumerator Dash()
    {
        Vector2 startP = this.transform.position;//
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        

        yield return null;
    }
}

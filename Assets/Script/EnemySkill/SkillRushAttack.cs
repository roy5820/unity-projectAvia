using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRushAttack : SkillMeleeAttack
{
    public float dashRange = 10f;//���� �Ÿ�
    public override void Skill(Transform creationLocation)
    {
        StartCoroutine(Dash());//�뽬 ���� �ڷ�ƾ ȣ��
        base.Skill(creationLocation);
    }

    IEnumerator Dash()
    {
        Vector2 startP = this.transform.position;//
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();

        

        yield return null;
    }
}

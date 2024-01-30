using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObj : MonoBehaviour
{
    public int attackType = 0;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어 또는 적 오브젝트 충돌 시
        if (other.TryGetComponent<CharacterHit>(out CharacterHit characterHit))
        {
            characterHit.HitAction(attackType);//피격 함수 호출
        }

        Destroy(this.gameObject);//총알 오브젝트 제거
    }
}

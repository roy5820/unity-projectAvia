using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObj : MonoBehaviour
{
    public int attackType = 0;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //�÷��̾� �Ǵ� �� ������Ʈ �浹 ��
        if (other.TryGetComponent<CharacterHit>(out CharacterHit characterHit))
        {
            characterHit.HitAction(attackType);//�ǰ� �Լ� ȣ��
        }

        Destroy(this.gameObject);//�Ѿ� ������Ʈ ����
    }
}

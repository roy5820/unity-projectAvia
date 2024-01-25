using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float bulletDistance = 6f;//�Ѿ� �ִ� �߻� �Ÿ�
    Vector2 startPosition;//�Ѿ� �߻� ��ġ
    public int attackType = 0;

    private void Start()
    {
        startPosition = this.transform.position;
    }

    private void Update()
    {
        float shotDistance = Vector2.Distance(startPosition, this.transform.position);//�߻�Ÿ� ���ϱ�
        //�Ѿ��� ��Ÿ� ������ ������ ����
        if (shotDistance > bulletDistance)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�÷��̾� �Ǵ� �� ������Ʈ �浹 ��
        if (other.TryGetComponent<CharacterHit>(out CharacterHit characterHit))
        {
            characterHit.HitAction(attackType);//�ǰ� �Լ� ȣ��
        }


        Destroy(this.gameObject);//�Ѿ� ������Ʈ ����
    }
}

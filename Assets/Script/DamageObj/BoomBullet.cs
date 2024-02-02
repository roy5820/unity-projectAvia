using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : BulletBase
{
    public GameObject boomPre;//���� ������

    //�ı��� ����
    private void OnDestroy()
    {
        Instantiate(boomPre, this.gameObject.transform.position, Quaternion.identity);//���� ������ ����
    }

    //�浹 �� �Ѿ� �ı�
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Destroy(this.gameObject);
    }
}

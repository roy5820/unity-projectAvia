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
}

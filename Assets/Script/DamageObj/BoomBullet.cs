using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : BulletBase
{
    public GameObject boomPre;//���� ������
    
    //�Ѿ� �ı��� ȣ��Ǵ� �ڷ�ƾ
    public override IEnumerator BulletDestroy()
    {
        Instantiate(boomPre, this.gameObject.transform.position, Quaternion.identity);//���� ������ ����
        yield return base.BulletDestroy();
    }
}

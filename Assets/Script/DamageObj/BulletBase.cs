using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : DamageObj
{
    public float bulletDistance = 6f;//�Ѿ� �ִ� �߻� �Ÿ�
    Vector2 startPosition;//�Ѿ� �߻� ��ġ

    //Ȱ��ȭ�� ������ �ʱ�ȭ
    public virtual void OnEnable()
    {
        startPosition = this.transform.position;
    }

    public virtual void Update()
    {
        float shotDistance = Vector2.Distance(startPosition, this.transform.position);//�߻�Ÿ� ���ϱ�
        //�Ѿ��� ��Ÿ� ������ ������ ����
        if (shotDistance > bulletDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Time.fixedDeltaTime = 0.01f;
    }
}

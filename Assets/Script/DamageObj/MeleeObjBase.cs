using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeObjBase : DamageObj
{
    public float deletionTime = 0.1f;//���� �ð�
    float cntTime = 0;//�帥 �ð� ����

    public virtual void Update()
    {
        cntTime += Time.deltaTime;//��� �ð� ����

        //��� �ð��� ���� �ð� ���� ũ�� ����
        if (deletionTime <= cntTime)
            Destroy(this.gameObject);
    }
}

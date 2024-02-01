using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : DamageObj
{
    public float durationTime = 0.5f;//���ӽð�
    private float crtTime = 0;//������� �ð�

    private void Start()
    {
        
    }

    private void Update()
    {
        crtTime += Time.deltaTime;

        //���ӽð��� ������ ����
        if (crtTime > durationTime)
            Destroy(gameObject);
    }
}

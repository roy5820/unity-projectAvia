using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : DamageObj
{
    public float durationTime = 0.5f;//지속시간
    private float crtTime = 0;//생성경과 시간

    private void Start()
    {
        
    }

    private void Update()
    {
        crtTime += Time.deltaTime;

        //지속시간이 지나면 삭제
        if (crtTime > durationTime)
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : DamageObj
{
    public float bulletDistance = 6f;//총알 최대 발사 거리
    Vector2 startPosition;//총알 발사 위치

    //활성화시 설정값 초기화
    public virtual void OnEnable()
    {
        startPosition = this.transform.position;
    }

    public virtual void Update()
    {
        float shotDistance = Vector2.Distance(startPosition, this.transform.position);//발사거리 구하기
        //총알이 사거리 밖으로 나갈시 제거
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

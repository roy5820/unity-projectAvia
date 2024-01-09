using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float bulletDistance = 6f;//총알 최대 발사 거리
    Vector2 startPosition;//총알 발사 위치
    

    private void Start()
    {
        startPosition = this.transform.position;
    }

    private void Update()
    {
        float shotDistance = Vector2.Distance(startPosition, this.transform.position);//발사거리 구하기
        //총알이 사거리 밖으로 나갈시 제거
        if (shotDistance > bulletDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //플레이어 또는 적 오브젝트 충돌 시
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.SendMessage("HitFuntion");//피격 함수 호출
        }

        Destroy(this.gameObject);//총알 오브젝트 제거
    }
}

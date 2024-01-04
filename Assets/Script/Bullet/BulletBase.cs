using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Vector2 bulletVec;//총알 방향
    public bool isLaunch = false;//발사 여부
    public float fireForce = 20f;//발사 파워
    Rigidbody2D bulletRbody;
    public float bulletDistance = 6f;//총알 최대 발사 거리
    Vector2 startPosition;//총알 발사 위치
    

    private void Start()
    {
        bulletRbody = this.GetComponent<Rigidbody2D>();//총알 리지드 바디 초기화
        startPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        //총알 발사 구현
        if (isLaunch)
        {
            bulletRbody.velocity = bulletVec.normalized * fireForce;
        }

        float shotDistance = Vector2.Distance(startPosition, this.transform.position);//발사거리 구하기
        //총알이 사거리 밖으로 나갈시 제거
        if (shotDistance > bulletDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit");
        //플레이어 또는 적 오브젝트 충돌 시
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.SendMessage("HitFuntion");//피격 함수 호출
        }

        Destroy(this.gameObject);//총알 오브젝트 제거
    }
}

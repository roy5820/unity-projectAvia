using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float bulletDistance = 6f;//총알 최대 발사 거리
    Vector2 startPosition;//총알 발사 위치
    public int attackType = 0;

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
    private void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어 또는 적 오브젝트 충돌 시
        if (other.TryGetComponent<CharacterHit>(out CharacterHit characterHit))
        {
            characterHit.HitAction(attackType);//피격 함수 호출
        }


        Destroy(this.gameObject);//총알 오브젝트 제거
    }
}

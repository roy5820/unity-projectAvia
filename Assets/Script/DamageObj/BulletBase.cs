using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : DamageObj
{
    public float bulletDistance = 6f;//총알 최대 발사 거리
    Vector2 startPosition;//총알 발사 위치

    public LayerMask wallLayer;//벽레이어

    public float destroyTime = 0.5f;//총알 파괴 시간
    public float destroyAniNum = 1;//총알 파괴시 출력할 애니메이션 번호

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
            StartCoroutine(BulletDestroy());//파괴 코루틴 호출
        }
    }

    private void FixedUpdate()
    {
        Time.fixedDeltaTime = 0.01f;
    }

    //총알 충돌시 처리
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(((1 << other.gameObject.layer) & wallLayer) != 0)
        {
            StartCoroutine(BulletDestroy());//파괴 코루틴 호출
        }
    }

    //총알 파괴 구현 코루틴
    public virtual IEnumerator BulletDestroy()
    {
        GetComponent<CharacterAnimationManager>().SetAniParameter(1);//파괴 시 애니메이션 재생
        GetComponent<Collider2D>().enabled = false;//충돌 해제
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;//이동 값 0으로 초기화

        if(destroyTime > 0)
            yield return new WaitForSeconds(destroyTime);//파괴까지 걸리는 시간
        Destroy(this.gameObject);//오브젝트 제거
    }
}

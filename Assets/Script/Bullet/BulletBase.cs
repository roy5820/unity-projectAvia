using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Vector2 bulletVec;//총알 방향
    public bool isLaunch = false;//발사 여부
    public float fireForce = 20f;//발사 파워
    Rigidbody2D bulletRbody;
    public float bulletLifeTime = 1f;
    float isTime = 0;
    

    private void Start()
    {
        bulletRbody = this.GetComponent<Rigidbody2D>();//총알 리지드 바디 초기화
    }

    private void FixedUpdate()
    {
        //총알 발사 구현
        if (isLaunch)
        {
            bulletRbody.velocity = bulletVec.normalized * fireForce;
        }

        isTime += Time.fixedDeltaTime;
        if (isTime > bulletLifeTime)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}

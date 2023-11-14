using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Vector2 bulletVec;//�Ѿ� ����
    public bool isLaunch = false;//�߻� ����
    public float fireForce = 20f;//�߻� �Ŀ�
    Rigidbody2D bulletRbody;
    public float bulletLifeTime = 1f;
    float isTime = 0;
    

    private void Start()
    {
        bulletRbody = this.GetComponent<Rigidbody2D>();//�Ѿ� ������ �ٵ� �ʱ�ȭ
    }

    private void FixedUpdate()
    {
        //�Ѿ� �߻� ����
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

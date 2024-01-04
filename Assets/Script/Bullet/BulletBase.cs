using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public Vector2 bulletVec;//�Ѿ� ����
    public bool isLaunch = false;//�߻� ����
    public float fireForce = 20f;//�߻� �Ŀ�
    Rigidbody2D bulletRbody;
    public float bulletDistance = 6f;//�Ѿ� �ִ� �߻� �Ÿ�
    Vector2 startPosition;//�Ѿ� �߻� ��ġ
    

    private void Start()
    {
        bulletRbody = this.GetComponent<Rigidbody2D>();//�Ѿ� ������ �ٵ� �ʱ�ȭ
        startPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        //�Ѿ� �߻� ����
        if (isLaunch)
        {
            bulletRbody.velocity = bulletVec.normalized * fireForce;
        }

        float shotDistance = Vector2.Distance(startPosition, this.transform.position);//�߻�Ÿ� ���ϱ�
        //�Ѿ��� ��Ÿ� ������ ������ ����
        if (shotDistance > bulletDistance)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit");
        //�÷��̾� �Ǵ� �� ������Ʈ �浹 ��
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.SendMessage("HitFuntion");//�ǰ� �Լ� ȣ��
        }

        Destroy(this.gameObject);//�Ѿ� ������Ʈ ����
    }
}

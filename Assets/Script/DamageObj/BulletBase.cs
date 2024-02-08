using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : DamageObj
{
    public float bulletDistance = 6f;//�Ѿ� �ִ� �߻� �Ÿ�
    Vector2 startPosition;//�Ѿ� �߻� ��ġ

    public LayerMask wallLayer;//�����̾�

    public float destroyTime = 0.5f;//�Ѿ� �ı� �ð�
    public float destroyAniNum = 1;//�Ѿ� �ı��� ����� �ִϸ��̼� ��ȣ

    public Coroutine bulletDestroy = null;

    //Ȱ��ȭ�� ������ �ʱ�ȭ
    public virtual void OnEnable()
    {
        startPosition = this.transform.position;
    }

    public virtual void Update()
    {
        float shotDistance = Vector2.Distance(startPosition, this.transform.position);//�߻�Ÿ� ���ϱ�
        //�Ѿ��� ��Ÿ� ������ ������ ����
        if (shotDistance > bulletDistance && bulletDestroy == null)
        {
            bulletDestroy = StartCoroutine(BulletDestroy());//�ı� �ڷ�ƾ ȣ��
        }
    }

    private void FixedUpdate()
    {
        Time.fixedDeltaTime = 0.01f;
    }

    //�Ѿ� �浹�� ó��
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(((1 << other.gameObject.layer) & wallLayer) != 0)
        {
            if (bulletDestroy == null)
                bulletDestroy = StartCoroutine(BulletDestroy());//�ı� �ڷ�ƾ ȣ��
        }
    }

    //�Ѿ� �ı� ���� �ڷ�ƾ
    public virtual IEnumerator BulletDestroy()
    {
        if(TryGetComponent<CharacterAnimationManager>(out CharacterAnimationManager aniManager))
            aniManager.SetAniParameter(1);//�ı� �� �ִϸ��̼� ���
        if(TryGetComponent<Collider2D>(out Collider2D col))
            col.enabled = false;//�浹 ����
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rd))
            rd.velocity = Vector2.zero;//�̵� �� 0���� �ʱ�ȭ

        if(destroyTime > 0)
            yield return new WaitForSeconds(destroyTime);//�ı����� �ɸ��� �ð�
        Destroy(this.gameObject);//������Ʈ ����
    }
}

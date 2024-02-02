using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRushAttack : EnemySkill
{
    public float dashRange = 10f;//���� �Ÿ�
    public float dashPower = 40f;//�뽬 �Ŀ�
    public float wallCastRange = 2f;//���� �� �� üũ �Ÿ�
    public Collider2D bodyCol;//�� �ݶ��̴�

    private Rigidbody2D rb;//������ �ٵ�
    private GameObject thisPre;//������ ���� �������� ����Ǵ� ��
    public override IEnumerator Skill()
    {
        bodyCol.enabled = false;//�� �ݶ��̴� ��Ȱ��ȭ
        Vector2 startP = this.transform.position;//���� ��ġ
        rb = this.gameObject.GetComponent<Rigidbody2D>();//Enemy�� ������ �ٵ�
        RaycastHit2D wallCast;//�� üũ�� ����
        float dashDistace = 0;//�뽬 �Ÿ�

        thisPre = Instantiate(attackPrefeb, creationLocation.position, Quaternion.identity, gameObject.transform);//���� ������ ����
        
        Vector2 getTargetP = SetTargetToPlayer();//Ÿ�� ���� ��������
        targetP = getTargetP == Vector2.zero ? targetP : getTargetP;//Ÿ�� �缳��

        Vector2 direction = (targetP - (Vector2)transform.position).normalized;//�÷��̾� ����

        //�ִ� �̵� �Ÿ����� �̵� ����
        while (dashDistace <= dashRange)
        {
            rb.velocity = direction * dashPower;//�̵� ����
            dashDistace = Vector2.Distance(transform.position, startP);//�뽬 �Ÿ� ����
            wallCast = Physics2D.Raycast(transform.position, direction, wallCastRange, wallLayer);//�� üũ
            //�̵� ��ο� ���� ���� �� �̵� ���� ����
            if (wallCast.collider != null)
                break;

            yield return null;
        }

        yield return base.Skill();
    }

    //�ĵ����� �κ�
    public override IEnumerator AfterCast()
    {
        //���� ���� �� ó��
        bodyCol.enabled = true;//�� �ݶ��̴� Ȱ��ȭ
        rb.velocity = Vector2.zero;//�̵� ����
        Destroy(thisPre);//���� ������ ����

        yield return base.AfterCast();
    }
}

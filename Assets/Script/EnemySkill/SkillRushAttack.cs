using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRushAttack : EnemySkill
{
    public float dashRange = 10f;//���� �Ÿ�
    public float dashPower = 40f;//�뽬 �Ŀ�
    public GameObject damegeAreaPrefeb;//Ȱ��ȭ�� ������ ���� ������Ʈ
    public float wallCastRange = 2f;//���� �� �� üũ �Ÿ�

    public override IEnumerator Skill()
    {

        yield return base.Skill();
    }

    IEnumerator Dash(Vector2 creationP)
    {
        Vector2 startP = this.transform.position;//���� ��ġ
        Rigidbody2D rb = this.gameObject.GetComponent<Rigidbody2D>();//Enemy�� ������ �ٵ�
        RaycastHit2D wallCast;//�� üũ�� ����
        float dashDistace = 0;//�뽬 �Ÿ�

        damegeAreaPrefeb.SetActive(true);//���� Ȱ��ȭ
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

        //�뽬 ���� ó��
        rb.velocity = Vector2.zero;//�̵� ����
        damegeAreaPrefeb.SetActive(false);//���� ��Ȱ��ȭ
    }
}

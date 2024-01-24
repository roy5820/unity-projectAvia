using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill: MonoBehaviour
{
    public float cooldown;//��Ÿ��
    public float castTime; // ��������
    public float afterCastTime; // �ĵ�����
    public float lastUsedTime;//������ ��� �ð�
    public float skillRange;//��ų ��Ÿ�
    public Vector2 targetP;//Ÿ�� ������Ʈ ��ġ��
    public LayerMask targetLayer;//Ÿ�� ���̾�
    public LayerMask wallLayer;//�� ���̾�

    private Coroutine skillCoroutine = null;//��ų ���� �ڷ�ƾ
    private EnemySkillController callbackComponent;//�ݹ��� ������Ʈ

    //��ų ��� ����üũ
    public virtual bool CanUse()
    {
        float distance = 0;//Ÿ�ٰ��� ��Ÿ��� ����Ǵ� ����
        bool withinAttackRange;//��ų��� ��Ÿ� ������ üũ ���� �����ϴ� ����

        if (skillRange > 0)//��ų ��Ÿ��� 0���� ũ�� ��Ÿ� üũ
        {
            targetP =  SetTargetToPlayer();// Ÿ�� �����ϱ�
            
            //��ų ��Ÿ� üũ �� �Ÿ� �� ��ֹ� üũ
            if (targetP != Vector2.zero)
            {
                distance = Vector2.Distance(this.transform.position, targetP);//Ÿ�ٰ��� �Ÿ� ��������

                if (skillRange >= distance) withinAttackRange = true;//��ų ��Ÿ� ���̸� true
                else withinAttackRange = false;
            }
            else withinAttackRange = false;//Ÿ���� ������ false
        }
        else withinAttackRange = true;//��ų ��Ÿ��� ������ ��Ÿ� ������ true
        
        return skillCoroutine == null && withinAttackRange;//��ų ��Ÿ�Ӱ� ��ų ��������� ���� �׸��� ��Ÿ� üũ�Ͽ� ��ų ��� ���� ��ȯ
    }

    //��ų ��� �κ� creationLocation: ��ų ���� ����, 
    public virtual void Use(Transform creationLocation, EnemySkillController callbackComponent)
    {
        this.callbackComponent = callbackComponent;//�ݹ��� ������Ʈ ����
        this.callbackComponent.SetStatus(2);//���� ���·� �������ͽ� ��ȯ
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//���� �� ���� ���·� �ʱ�ȭ

        skillCoroutine = StartCoroutine(PerformSkill(creationLocation));//��ų ����, ���, �ĵ�, ��Ÿ�� ���� �ڷ�ƾ ȣ��
    }

    //���� �ĵ����� ���� �κ�
    private IEnumerator PerformSkill(Transform creationLocation)
    {
        if (castTime > 0f)
            yield return new WaitForSeconds(castTime); // �������� �Ŀ� ��ų ����

        Skill(creationLocation);//��ų ���

        if (afterCastTime > 0f)
            yield return new WaitForSeconds(afterCastTime); // �ĵ����� �Ŀ� ��Ÿ�� ���ư�

        this.callbackComponent.SetStatus(0);//���� ���·� �������ͽ� ��ȯ

        if (cooldown > 0f)
            yield return new WaitForSeconds(cooldown);//��ٿ� �� �ڷ�ƾ �ʱ�ȭ

        skillCoroutine = null; // �ڷ�ƾ ���� �� �ʱ�ȭ
    }

    //Ÿ�� �����ϴ� �Լ�(���� ��Ÿ� �� ��ֹ� ���� Ÿ���� ������ �������� �ƴϸ� Vector2.zero ������ ���
    public Vector2 SetTargetToPlayer()
    {
        Vector2 attackVec = (GameObject.FindWithTag("Player").transform.position - this.gameObject.transform.position).normalized ;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, attackVec, skillRange, (targetLayer | wallLayer));//��� �� �÷��̾� üũ�� ���� raycast
        Vector2 targetPosition = Vector2.zero;//Ÿ�� ������

        if(hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & targetLayer) != 0)
            {
                targetPosition = hit.collider.gameObject.transform.position;
            }
        }

        return targetPosition;//Ÿ�� ��ġ ����
    }

    //��ų ���� �κ�
    public abstract void Skill(Transform creationLocation);
}

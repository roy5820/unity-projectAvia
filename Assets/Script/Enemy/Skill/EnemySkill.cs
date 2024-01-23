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

    private Coroutine skillCoroutine = null;//��ų ���� �ڷ�ƾ
    private EnemySkillController callbackComponent;//�ݹ��� ������Ʈ

    //��ų ��� ����üũ
    public virtual bool CanUse()
    {
        float distance = 0;//Ÿ�ٰ��� ��Ÿ��� ����Ǵ� ����
        bool withinAttackRange;//��ų��� ��Ÿ� ������ üũ ���� �����ϴ� ����

        if (skillRange > 0)//��ų ��Ÿ��� 0���� ũ�� ��Ÿ� üũ
        {
            SetTargetToPlayer();// Ÿ�� �����ϱ�

            if (targetP != Vector2.zero)
            {
                distance = Vector2.Distance(this.transform.position, targetP);//Ÿ�ٰ��� �Ÿ� ��������
                if (skillRange < distance) withinAttackRange = true;//��ų ��Ÿ� ���̸� true
                else withinAttackRange = false;
            }
            else withinAttackRange = false;//Ÿ���� ������ false
        }
        else withinAttackRange = true;//��ų ��Ÿ��� ������ ��Ÿ� ������ true
        
        return (Time.time - lastUsedTime >= cooldown) && skillCoroutine == null && withinAttackRange;//��ų ��Ÿ�Ӱ� ��ų ��������� ���� �׸��� ��Ÿ� üũ�Ͽ� ��ų ��� ���� ��ȯ
    }

    //��ų ��� �κ� creationLocation: ��ų ���� ����, 
    public virtual void Use(Transform creationLocation, EnemySkillController callbackComponent)
    {
        this.callbackComponent = callbackComponent;//�ݹ��� ������Ʈ ����
        this.callbackComponent.SetStatus(2);//���� ���·� �������ͽ� ��ȯ

        skillCoroutine = StartCoroutine(PerformSkill(creationLocation));//��ų ����, ���, �ĵ� ���� �ڷ�ƾ ȣ��
    }

    //���� �ĵ����� ���� �κ�
    private IEnumerator PerformSkill(Transform creationLocation)
    {
        if (castTime > 0f)
        {
            yield return new WaitForSeconds(castTime); // �������� �Ŀ� ��ų ����
        }

        lastUsedTime = Time.time;//��ų ������ ��� �ð� ����

        Skill(creationLocation);//��ų ���

        if (afterCastTime > 0f)
        {
            yield return new WaitForSeconds(afterCastTime); // �ĵ����� �Ŀ� ��ų ����
        }

        this.callbackComponent.SetStatus(0);//���� ���·� �������ͽ� ��ȯ

        skillCoroutine = null; // �ڷ�ƾ ���� �� �ʱ�ȭ
    }

    //Ÿ�� �����ϴ� �Լ�
    public Vector2 SetTargetToPlayer()
    {
        GameObject targetObj = GameObject.FindWithTag("Player");//Ÿ�� ������Ʈ ��������
        int targetLayerNum = 1 << targetLayer;//Ÿ�� ���̾ ��ȣ�� ��ȯ
        Vector2 targetPosition = Vector2.zero;//Ÿ�� ������
        if (targetObj.layer == targetLayerNum)//Ÿ�� ������Ʈ�� Ÿ�� ���̾�� ��ġ ����
            targetPosition = targetObj.transform.position;

        return targetPosition;//Ÿ�� ��ġ ����
    }

    //��ų ���� �κ�
    public abstract void Skill(Transform creationLocation);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill: MonoBehaviour
{
    public float cooldown;//��Ÿ��
    public float castTime; // ��������
    public float afterCastTime; // �ĵ�����
    public float lastUsedTime;//������ ��� �ð�

    private Coroutine skillCoroutine = null;//��ų ���� �ڷ�ƾ

    //��ų ��� ����üũ
    public virtual bool CanUse()
    {
        return (Time.time - lastUsedTime >= cooldown) && skillCoroutine == null;//��ų ��Ÿ�Ӱ� ��ų ��������� ���ο� ���� 
    }

    //��ų ��� �κ� creationLocation: ��ų ���� ����, attackVec: ��ų ����
    public virtual void Use(Transform creationLocation, Vector2 attackVec = new Vector2())
    {
        skillCoroutine = StartCoroutine(PerformSkill(creationLocation, attackVec));
    }

    //���� �ĵ����� ���� �κ�
    private IEnumerator PerformSkill(Transform creationLocation, Vector2 attackVec)
    {
        if (castTime > 0f)
        {
            yield return new WaitForSeconds(castTime); // �������� �Ŀ� ��ų ����
        }

        lastUsedTime = Time.time;//��ų ������ ��� �ð� ����

        Skill(creationLocation, attackVec);//��ų ���

        if (afterCastTime > 0f)
        {
            yield return new WaitForSeconds(afterCastTime); // �ĵ����� �Ŀ� ��ų ����
        }

        skillCoroutine = null; // �ڷ�ƾ ���� �� �ʱ�ȭ
    }

    //��ų ���� �κ�
    public abstract void Skill(Transform creationLocation, Vector2 attackVec);
}

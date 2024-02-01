
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill: MonoBehaviour
{
    public float cooldown;//��Ÿ��
    public float castTime; //ĳ���� Ÿ��
    public float afterCastTime; //�ĵ����� Ÿ��
    public float skillRange;//��ų ��Ÿ�
    public Vector2 targetP;//Ÿ�� ������Ʈ ��ġ��
    public LayerMask targetLayer;//Ÿ�� ���̾�
    public LayerMask wallLayer;//�� ���̾�
    public GameObject attackPrefeb;//�߻��� �Ѿ� ������Ʈ
    public Transform creationLocation;//���� ��ġ

    private Coroutine skillCoroutine = null;//��ų ���� �ڷ�ƾ
    private EnemySkillController callbackComponent;//�ݹ��� ������Ʈ

    //��ų ��� ����üũ
    public virtual bool CanUse()
    {
        float distance = 0;//Ÿ�ٰ��� ��Ÿ��� ����Ǵ� ����
        bool withinAttackRange;//��ų��� ��Ÿ� ������ üũ ���� �����ϴ� ����

        if (skillRange > 0)//��ų ��Ÿ��� 0���� ũ�� ��Ÿ� üũ
        {
            targetP =  SetTargetToPlayer(1);// ��λ� �ִ� Ÿ�ٰ� �����ϱ�
            
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
    public virtual void Use(EnemySkillController callbackComponent)
    {
        this.callbackComponent = callbackComponent;//�ݹ��� ������Ʈ ����
        this.callbackComponent.SetStatus(2);//���� ���·� �������ͽ� ��ȯ
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//���� �� ���� ���·� �ʱ�ȭ

        //�÷��̾� �������� ���� ������ �ٲ�
        if (targetP.x < gameObject.transform.position.x)
            gameObject.transform.localScale = new Vector2(-1, 1);
        else
            gameObject.transform.localScale = new Vector2(1, 1);

        skillCoroutine = StartCoroutine(Cast());//��ų ����, ���, �ĵ�, ��Ÿ�� ���� �ڷ�ƾ ȣ��
    }

    //��ų ĳ���� ����
    public virtual IEnumerator Cast()
    {

        if (castTime > 0f && GetBehavioralStatus() == 2)
            yield return new WaitForSeconds(castTime); // �������� �Ŀ� ��ų ����

        if (GetBehavioralStatus() == 2)
            skillCoroutine = StartCoroutine(Skill());//��ų ���
    }

    //��ų ���� �κ�
    public virtual IEnumerator Skill()
    {
        skillCoroutine =  StartCoroutine(AfterCast());//�ĵ����� ���� �ڷ�ƾ

        yield return null;
    }

    //��ų �ĵ����� ����
    public virtual IEnumerator AfterCast()
    {
        if (afterCastTime > 0f && GetBehavioralStatus() == 2)
            yield return new WaitForSeconds(afterCastTime); // �ĵ����� �Ŀ� ��Ÿ�� ���ư�
        if (GetBehavioralStatus() == 2)
            this.callbackComponent.SetStatus(0);//�Ϲ� ���·� �������ͽ� ��ȯ

        skillCoroutine = StartCoroutine(CoolTime());//��Ÿ�� ���� �ڷ�ƾ ȣ��
        yield return null;
    }

    //��ų ��Ÿ�� ����
    public virtual IEnumerator CoolTime()
    {
        if (cooldown > 0f)
            yield return new WaitForSeconds(cooldown);//��ٿ� �� �ڷ�ƾ �ʱ�ȭ
        skillCoroutine = null;//��ų �ڷ�ƾ null�� �ʱ�ȭ
    }

    //Ÿ�� �����ϴ� �Լ� mode 1: ��ֹ� üũ�Ͽ� ��ġ�� �������� �׿�: �׳� Ÿ�� ��ġ�� ��������
    public Vector2 SetTargetToPlayer(int mode = 0)
    {
        Vector2 targetPosition = Vector2.zero;//Ÿ�� ������
        Vector2 attackVec = (GameObject.FindWithTag("Player").transform.position - this.gameObject.transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, attackVec, skillRange, mode == 1 ? (targetLayer | wallLayer) : targetLayer);//��� �� �÷��̾� üũ�� ���� raycast


        if (hit.collider != null)
        {
            if (((1 << hit.collider.gameObject.layer) & targetLayer) != 0)
            {
                targetPosition = hit.collider.gameObject.transform.position;
            }
        }

        return targetPosition;//Ÿ�� ��ġ ����
    }

    //Enemy��ü�� ���°��� �������� �Լ�
    public int GetBehavioralStatus()
    {
        int getBehavioralStatus;

        //������Ʈ �ൿ ���°��� �����ͼ� ���ݻ��°� ��ҵǸ� ���� ����
        if (TryGetComponent<EnemyStatusInterface>(out EnemyStatusInterface enemyStatusInterface))
        {
            getBehavioralStatus = enemyStatusInterface.BehavioralStatus;
        }
        else
            return -1;

        return getBehavioralStatus;
    }
}

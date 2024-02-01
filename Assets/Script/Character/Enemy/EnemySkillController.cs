using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillController : MonoBehaviour
{

    public List<EnemySkill> enemySkills;

    private int getBehavioralStatus = 0;//�� ������Ʈ �ൿ ���°�

    private void Update()
    {
        //������Ʈ �ൿ ���°� ��������
        if (TryGetComponent<EnemyStatusInterface>(out EnemyStatusInterface enemyStatusInterface))
            getBehavioralStatus = enemyStatusInterface.BehavioralStatus;
        else
            return;

        //����, �̵� ���¿����� ���� ����
        if (getBehavioralStatus == 0 || getBehavioralStatus == 1)
        {
            // ��ų ��� ����
            foreach (EnemySkill SkillData in enemySkills)
            {
                //��ų ��� ���� üũ
                if (SkillData.CanUse())
                {
                    SkillData.Use(this.GetComponent<EnemySkillController>());//��ų ���
                    break;
                }
            }
        }
    }

    //�������ͽ� �����ϴ� �Լ�
    public void SetStatus(int mode)
    {
        if (TryGetComponent<EnemyStatusInterface>(out EnemyStatusInterface enemyStatusInterface))
            getBehavioralStatus = enemyStatusInterface.BehavioralStatus = mode;
    }
}